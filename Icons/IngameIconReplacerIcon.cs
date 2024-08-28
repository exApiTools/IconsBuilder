using System;
using System.Linq;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Abstract;
using ExileCore.Shared.Helpers;

namespace IconsBuilder.Icons;

public class IngameIconReplacerIcon : BaseIcon
{
    public IngameIconReplacerIcon(Entity entity, IconsBuilderSettings settings)
        : base(entity, settings)
    {
        var isHidden = false;
        var transitionableFlag1 = 1;
        var shrineIsAvailable = true;
        var isOpened = false;

        T Update<T>(ref T store, Func<T> update)
        {
            return entity.IsValid ? store = update() : store;
        }

        Show = () => !Update(ref isHidden, () => entity.GetComponent<MinimapIcon>()?.IsHide ?? isHidden) &&
                     Update(ref transitionableFlag1, () => entity.GetComponent<Transitionable>()?.Flag1 ?? 1) == 1 &&
                     Update(ref shrineIsAvailable, () => entity.GetComponent<Shrine>()?.IsAvailable ?? shrineIsAvailable) &&
                     !Update(ref isOpened, () => entity.GetComponent<Chest>()?.IsOpened ?? isOpened) &&
                     !entity.IsValid;
        var name = entity.GetComponent<MinimapIcon>()?.Name ?? "";
        var iconIndexByName = Extensions.IconIndexByName(name);

        var iconSizeMultiplier = RemoteMemoryObject.pTheGame.Files.MinimapIcons.EntriesList.ElementAtOrDefault((int)iconIndexByName)?.LargeMinimapSize ?? 1;
        MainTexture = new HudTexture("Icons.png")
        {
            UV = SpriteHelper.GetUV(iconIndexByName),
            Size = RemoteMemoryObject.pTheGame.IngameState.IngameUi.Map.LargeMapZoom * RemoteMemoryObject.pTheGame.IngameState.Camera.Height * iconSizeMultiplier / 64,
        };
    }
}
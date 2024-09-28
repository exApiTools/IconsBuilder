using System.Collections.Generic;
using ExileCore;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Abstract;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using SharpDX;

namespace IconsBuilder
{
    public class SelfIcon : BaseIcon
    {
        public SelfIcon(Entity entity, GameController gameController, IconsBuilderSettings settings, Dictionary<string, Size2> modIcons) :
            base(entity, settings)
        {
            Show = () => entity.IsValid && !settings.HideSelf;
            MainTexture = new HudTexture("Icons.png") { UV = SpriteHelper.GetUV(MapIconsIndex.MyPlayer) };
            MainTexture.Size = settings.SizeSelf;
        }
    }
}

using ExileCore.Shared.Attributes;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;

namespace IconsBuilder
{
    public class IconsBuilderSettings : ISettings
    {
        public ToggleNode UseReplacementsForGameIconsWhenOutOfRange { get; set; } = new ToggleNode(true);

        [Menu("Default size")]
        public float SizeDefaultIcon { get; set; } = new RangeNode<int>(16, 1, 50);
        [Menu("Size NPC icon")]
        public RangeNode<int> SizeNpcIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size Character icon")]
        public RangeNode<int> SizeSelf { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size monster icon")]
        public RangeNode<int> SizeEntityWhiteIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size magic monster icon")]
        public RangeNode<int> SizeEntityMagicIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size rare monster icon")]
        public RangeNode<int> SizeEntityRareIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size unique monster icon")]
        public RangeNode<int> SizeEntityUniqueIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size Proximity monster icon")]
        public RangeNode<int> SizeEntityProximityMonsterIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size breach chest icon")]
        public RangeNode<int> SizeBreachChestIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        public RangeNode<int> SizeHeistChestIcon { get; set; } = new RangeNode<int>(30, 1, 50);
        public RangeNode<int> ExpeditionChestIconSize { get; set; } = new RangeNode<int>(30, 1, 50);
        public RangeNode<int> SanctumChestIconSize { get; set; } = new RangeNode<int>(30, 1, 50);
        public RangeNode<int> SanctumGoldIconSize { get; set; } = new RangeNode<int>(30, 1, 50);

        [Menu("Size chests icon")]
        public RangeNode<int> SizeChestIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Show small chests")]
        public ToggleNode ShowSmallChest { get; set; } = new ToggleNode(false);
        [Menu("Size small chests icon")]
        public RangeNode<int> SizeSmallChestIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size misc icon")]
        public RangeNode<int> SizeMiscIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size shrine icon")]
        public RangeNode<int> SizeShrineIcon { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Size secondory icon")]
        public RangeNode<int> SecondaryIconSize { get; set; } = new RangeNode<int>(10, 1, 50);
        [Menu("Hidden monster icon size")]
        public RangeNode<float> HideSize { get; set; } = new RangeNode<float>(1, 0, 1);
        [Menu("Debug information about entities")]
        public ToggleNode LogDebugInformation { get; set; } = new ToggleNode(true);
        [Menu("Reparse entities")]
        public ButtonNode Reparse { get; set; } = new ButtonNode();
        public ToggleNode MultiThreading { get; set; } = new ToggleNode(false);
        public RangeNode<int> MultiThreadingWhenEntityMoreThan { get; set; } = new RangeNode<int>(10, 1, 200);
        public ToggleNode HideSelf { get; set; } = new ToggleNode(false);
        public ToggleNode HideOtherPlayers { get; set; } = new ToggleNode(false);
        public ToggleNode HideMinions { get; set; } = new ToggleNode(false);
        public ToggleNode DeliriumText { get; set; } = new ToggleNode(false);
        public ToggleNode HeistText { get; set; } = new ToggleNode(true);
        public ToggleNode HideBurriedMonsters { get; set; } = new ToggleNode(false);
        public ToggleNode ShowWhiteMonsterName { get; set; } = new ToggleNode(false);
        public ToggleNode ShowMagicMonsterName { get; set; } = new ToggleNode(false);
        public ToggleNode ShowRareMonsterName { get; set; } = new ToggleNode(false);
        public ToggleNode ShowUniqueMonsterName { get; set; } = new ToggleNode(false);
        public ToggleNode ReplaceMonsterNameWithArchnemesis { get; set; } = new ToggleNode(false);
        public ToggleNode Enable { get; set; } = new ToggleNode(true);
    }
}

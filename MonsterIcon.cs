using System;
using System.Collections.Generic;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.Shared;
using ExileCore.Shared.Abstract;
using ExileCore.Shared.Enums;
using ExileCore.Shared.Helpers;
using JM.LinqFaster;
using SharpDX;

namespace IconsBuilder
{
    public class MonsterIcon : BaseIcon
    {
        public MonsterIcon(Entity entity, GameController gameController, IconsBuilderSettings settings, Dictionary<string, Size2> modIcons)
            : base(entity, settings)
        {
            Update(entity, settings, modIcons);
        }

        public long ID { get; set; }

        public void Update(Entity entity, IconsBuilderSettings settings, Dictionary<string, Size2> modIcons)
        {
            Show = () => entity.IsAlive;
            if (entity.IsHidden && settings.HideBurriedMonsters)
            {
                Show = () => !entity.IsHidden && entity.IsAlive;
            }
            ID = entity.Id;

            if (!_HasIngameIcon) MainTexture = new HudTexture("Icons.png");

            switch (Rarity)
            {
                case MonsterRarity.White:
                    MainTexture.Size = settings.SizeEntityWhiteIcon;
                    break;
                case MonsterRarity.Magic:
                    MainTexture.Size = settings.SizeEntityMagicIcon;
                    break;
                case MonsterRarity.Rare:
                    MainTexture.Size = settings.SizeEntityRareIcon;
                    break;
                case MonsterRarity.Unique:
                    MainTexture.Size = settings.SizeEntityUniqueIcon;
                    break;
                default:
                    throw new ArgumentException(
                        $"{nameof(MonsterIcon)} wrong rarity for {entity.Path}. Dump: {entity.GetComponent<ObjectMagicProperties>().DumpObject()}");

                    break;
            }

            //if (_HasIngameIcon && entity.HasComponent<MinimapIcon>() && !entity.GetComponent<MinimapIcon>().Name.Equals("NPC") && entity.League != LeagueType.Heist)
            // return;

            if (!entity.IsHostile)
            {
                if (!_HasIngameIcon)
                {
                    MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterSmallGreenCircle);
                    Priority = IconPriority.Low;
                    Show = () => !settings.HideMinions && entity.IsAlive;
                }

                //Spirits icon
            }
            else if (Rarity == MonsterRarity.Unique && entity.Path.Contains("Metadata/Monsters/Spirit/"))
                MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterLargeGreenHexagon);
            else
            {
                string modName = null;

                if (entity.HasComponent<ObjectMagicProperties>())
                {
                    var objectMagicProperties = entity?.GetComponent<ObjectMagicProperties>();
                    if (objectMagicProperties != null)
                    {

                        var mods = objectMagicProperties.Mods;

                        if (mods != null)
                        {
                            if (mods.Contains("MonsterConvertsOnDeath_")) Show = () => entity.IsAlive && entity.IsHostile;

                            modName = mods.FirstOrDefaultF(modIcons.ContainsKey);
                        }
                    }
                }

                if (modName != null)
                {
                    MainTexture = new HudTexture("sprites.png");
                    MainTexture.UV = SpriteHelper.GetUV(modIcons[modName], new Size2F(7, 8));
                    Priority = IconPriority.VeryHigh;
                }
                else
                {
                    switch (Rarity)
                    {
                        case MonsterRarity.White:
                            MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterLargeRedCircle);
                            if (settings.ShowWhiteMonsterName)
                            {
                                Text = RenderName.Split(',').FirstOrDefault();
                                if (settings.ReplaceMonsterNameWithArchnemesis)
                                    GenerateArchNemString(entity, settings);
                            }

                            break;
                        case MonsterRarity.Magic:
                            MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterLargeBlueCircle);
                            if (settings.ShowMagicMonsterName)
                            {
                                Text = RenderName.Split(',').FirstOrDefault();
                                if (settings.ReplaceMonsterNameWithArchnemesis)
                                    GenerateArchNemString(entity, settings);
                            }

                            break;
                        case MonsterRarity.Rare:
                            MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterLargeYellowCircle);
                            if (settings.ShowRareMonsterName)
                            {
                                Text = RenderName.Split(',').FirstOrDefault();
                                if (settings.ReplaceMonsterNameWithArchnemesis)
                                    GenerateArchNemString(entity, settings);
                            }

                            break;
                        case MonsterRarity.Unique:
                            MainTexture.UV = SpriteHelper.GetUV(MapIconsIndex.LootFilterLargeWhiteHexagon);
                            MainTexture.Color = Color.DarkOrange;
                            if (settings.ShowUniqueMonsterName)
                                Text = RenderName.Split(',').FirstOrDefault();

                            break;
                        default:
                            throw new ArgumentOutOfRangeException(
                                $"Rarity wrong was is {Rarity}. {entity.GetComponent<ObjectMagicProperties>().DumpObject()}");
                    }
                }
            }

            void GenerateArchNemString(Entity entity, IconsBuilderSettings settings)
            {
                #region archnemMods List<()>()
                var archnemMods = new List<(string, string)>()
                {
                    ("Abberath-touched", "MonsterArchnemesisAbberath"),
                    ("Arakaali-touched", "MonsterArchnemesisArakaali_"),
                    ("Arcane Buffer", "MonsterArchnemesisArcaneEnchantedMagic"),
                    ("Arcane Buffer", "MonsterArchnemesisArcaneEnchanted"),
                    ("Assassin", "MonsterArchnemesisAssassin"),
                    ("Benevolent Guardian", "MonsterArchnemesisDivineTouched__"),
                    ("Berserker", "MonsterArchnemesisBerserker__"),
                    ("Bloodletter", "MonsterArchnemesisBloodletterMagic"),
                    ("Bloodletter", "MonsterArchnemesisBloodletter_"),
                    ("Bombardier", "MonsterArchnemesisBombardier___"),
                    ("Bonebreaker", "MonsterArchnemesisBonebreakerMagic"),
                    ("Bonebreaker", "MonsterArchnemesisBonebreaker_"),
                    ("Brine King-touched", "MonsterArchnemesisBrineKing___"),
                    ("Consecrator", "MonsterArchnemesisConsecratorMagic"),
                    ("Consecrator", "MonsterArchnemesisConsecration_"),
                    ("Corpse Detonator", "MonsterArchnemesisCorpseExploder_"),
                    ("Corrupter", "MonsterArchnemesisCorrupterMagic"),
                    ("Corrupter", "MonsterArchnemesisCorrupter_"),
                    ("Crystal-skinned", "MonsterArchnemesisLivingCrystals__"),
                    ("Deadeye", "MonsterArchnemesisDeadeyeMagic"),
                    ("Deadeye", "MonsterArchnemesisDeadeye"),
                    ("Drought Bringer", "MonsterArchnemesisFlaskDrain__"),
                    ("Effigy", "MonsterArchnemesisVoodooDoll"),
                    ("Echoist", "MonsterArchnemesisEchoist___"),
                    ("Electrocuting", "MonsterArchnemesisShockerMagic"),
                    ("Electrocuting", "MonsterArchnemesisShocker_"),
                    ("Empowered Elements", "MonsterArchnemesisCycleOfElements"),
                    ("Empowering Minions", "MonsterArchnemesisUnionOfSouls"),
                    ("Entangler", "MonsterArchnemesisGraspingVines"),
                    ("Executioner", "MonsterArchnemesisExecutioner"),
                    ("Final Gasp", "MonsterArchnemesisFinalGasp"),
                    ("Flame Strider", "MonsterArchnemesisFlameWalkerMagic"),
                    ("Flame Strider", "MonsterArchnemesisFlameWalker_"),
                    ("Flameweaver", "MonsterArchnemesisFlameTouched"),
                    ("Frenzied", "MonsterArchnemesisRampage"),
                    ("Frost Strider", "MonsterArchnemesisFrostWalkerMagic"),
                    ("Frost Strider", "MonsterArchnemesisFrostWalker"),
                    ("Frostweaver", "MonsterArchnemesisFrostTouched_"),
                    ("Gargantuan", "MonsterArchnemesisGargantuan"),
                    ("Hasted", "MonsterArchnemesisRaiderMagic"),
                    ("Hasted", "MonsterArchnemesisRaider_"),
                    ("Heralding Minions", "MonsterArchnemesisHeraldOfTheObelisk_"),
                    ("Heralds of the Obelisk", "MonsterArchnemesisHeraldOfTheObeliskMagic"),
                    ("Hexer", "MonsterArchnemesisHexer"),
                    ("Chaosweaver", "MonsterArchnemesisVoidTouched"),
                    ("Ice Prison", "MonsterArchnemesisGlacialCage"),
                    ("Incendiary", "MonsterArchnemesisIgniterMagic"),
                    ("Incendiary", "MonsterArchnemesisIgniter"),
                    ("Innocence-touched", "MonsterArchnemesisInnocence_____"),
                    ("Juggernaut", "MonsterArchnemesisJuggernaut___"),
                    ("Kitava-touched", "MonsterArchnemesisKitava"),
                    ("Lunaris-touched", "MonsterArchnemesisLunaris"),
                    ("Magma Barrier", "MonsterArchnemesisVolatileFlameBlood"),
                    ("Malediction", "MonsterArchnemesisOppressor_"),
                    ("Mana Siphoner", "MonsterArchnemesisManaDonut"),
                    ("Mirror Image", "MonsterArchnemesisMirrorImage"),
                    ("Necromancer", "MonsterArchnemesisNecromancer_"),
                    ("Opulent", "MonsterArchnemesisWealthy"),
                    ("Overcharged", "MonsterArchnemesisChargeGenerator__"),
                    ("Permafrost", "MonsterArchnemesisFreezerMagic"),
                    ("Permafrost", "MonsterArchnemesisFreezer__"),
                    ("Prismatic", "MonsterArchnemesisPrismaticMagic"),
                    ("Prismatic", "MonsterArchnemesisPrismatic"),
                    ("Rejuvenating", "MonsterArchnemesisRejuvenating"),
                    ("Sentinel", "MonsterArchnemesisDefenderMagic"),
                    ("Sentinel", "MonsterArchnemesisDefender"),
                    ("Shakari-touched", "MonsterArchnemesisShakari_"),
                    ("Solaris-touched", "MonsterArchnemesisSolaris"),
                    ("Soul Conduit", "MonsterArchnemesisSoulConduit____"),
                    ("Soul Eater", "MonsterArchnemesisSoulEater_"),
                    ("Spirit Walkers", "MonsterArchnemesisSpiritWalkersMagic"),
                    ("Splinterer", "MonsterArchnemesisMultiProjectiles"),
                    ("Splitting", "MonsterArchnemesisSplitting_"),
                    ("Steel-infused", "MonsterArchnemesisSteelAttuned___"),
                    ("Storm Herald", "MonsterArchnemesisLightningStorm"),
                    ("Storm Strider", "MonsterArchnemesisLightningWalkerMagic"),
                    ("Storm Strider", "MonsterArchnemesisLightningWalker_"),
                    ("Stormweaver", "MonsterArchnemesisStormTouched_"),
                    ("Temporal Bubble", "MonsterArchnemesisTimeBubble"),
                    ("Toxic", "MonsterArchnemesisPoisonerMagic"),
                    ("Toxic", "MonsterArchnemesisPoisoner_"),
                    ("Treant Horde", "MonsterArchnemesisSaplings"),
                    ("Trickster", "MonsterArchnemesisTrickster_"),
                    ("Tukohama-touched", "MonsterArchnemesisTukohama"),
                    ("Union of Souls", "MonsterArchnemesisUnionOfSoulsMagic"),
                    ("Vampiric", "MonsterArchnemesisVampiric"),
                };
                #endregion

                if (settings.ReplaceMonsterNameWithArchnemesis)
                {
                    var archNemText = "";
                    if (entity.HasComponent<ObjectMagicProperties>())
                    {
                        var objectMagicProperties = entity?.GetComponent<ObjectMagicProperties>();
                        if (objectMagicProperties != null)
                        {
                            var mods = objectMagicProperties.Mods;

                            if (mods != null)
                            {
                                foreach (var modText in mods)
                                {
                                    foreach (var value in archnemMods)
                                    {
                                        if (value.Item2.Contains(modText))
                                        {
                                            archNemText += "[" + value.Item1 + "] ";
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(archNemText))
                        Text = archNemText;
                }
            }
        }
    }
}

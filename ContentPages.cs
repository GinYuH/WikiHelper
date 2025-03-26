using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;
using Terraria.ModLoader;
using System;
using Terraria.GameContent.ItemDropRules;
using System.Reflection;
using Terraria.Map;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;

namespace WikiHelper
{
    public class ContentPages
    {
        #region Items
        /// <summary>
        /// Generates a page for every single item i nthe mod
        /// </summary>
        /// <returns></returns>
        public static string GenerateAllItemPages()
        {
            string ret = "";
            foreach (var item in ContentSamples.ItemsByType)
            {
                if (item.Value.ModItem != null)
                {
                    if (item.Value.ModItem.Mod == CurMod)
                    {
                        ret += ExportItemInfo(item.Value) + "\n\n\n\n";
                    }
                }
            }
            return ret;
        }


        /// <summary>
        /// Creates an item page
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public static string ExportItemInfo(Item item)
        {
            Projectile shootSample = new Projectile();
            shootSample.type = -1;
            string projName = "";
            if (item.shoot > ProjectileID.None)
            {
                shootSample = ContentSamples.ProjectilesByType[item.shoot];
                projName = shootSample.Name;
            }

            string itemtype = "";
            string itemtype2 = "";
            string templateType = "";
            string extension = "png";
            string mountName = "";
            bool linkType = true;
            if (shootSample.type > 0 && item.damage <= 0)
            {
                if (Main.vanityPet[item.buffType])
                {
                    itemtype = "Pet Summon";
                    itemtype2 = "pet";
                    projName = shootSample.Name;
                    templateType = "Pets";
                    if (Main.projFrames[shootSample.type] > 2)
                    {
                        extension = "png";
                    }
                }
                if (Main.lightPet[item.buffType])
                {
                    itemtype = "Light Pet";
                    itemtype2 = "light pet";
                    projName = shootSample.Name;
                    templateType = "Pets";
                    if (Main.projFrames[shootSample.type] > 2)
                    {
                        extension = "png";
                    }
                }
            }
            else if (item.createTile > TileID.Dirt)
            {
                itemtype = "Block";
                itemtype2 = "block";
                templateType = "Tiles";
            }
            else if (item.createWall > 0)
            {
                itemtype = "Wall";
                itemtype2 = "wall";
                templateType = "Tiles";
            }
            else if (item.accessory)
            {
                itemtype = "Accessory";
                itemtype2 = "accessory";
                templateType = "Equipables";
            }
            else if (item.mountType > 0)
            {
                itemtype = "Mount summon";
                itemtype2 = "mount";
                templateType = "Mounts";
                mountName = MountLoader.GetMount(item.mountType).Name;
            }
            else if (item.damage > 0)
            {
                itemtype = "Weapon";
                itemtype2 = "weapon";
                templateType = "Weapons";
            }
            else if (item.pick > 0 || item.axe > 0 || item.fishingPole > 0 || item.hammer > 0 || (item.shoot > ProjectileID.None && ContentSamples.ProjectilesByType[item.shoot].aiStyle == ProjAIStyleID.Hook))
            {
                itemtype = "Tool";
                itemtype2 = "tool";
                templateType = "Tools";
            }
            else if (item.accessory)
            {
                itemtype = "Acessory";
                itemtype2 = "accessory";
                templateType = "Equipables";
            }
            else if (item.headSlot > 0 || item.bodySlot > 0 || item.legSlot > 0)
            {
                itemtype = "Armor";
                itemtype2 = "armor";
                templateType = "Equipables";
            }
            else
            {
                itemtype = item.material ? "Crafting Material" : "Consumable";
                itemtype2 = item.material ? "crafting material" : "consumable";
                templateType = "Consumables";
                if (!item.material)
                    linkType = false;
            }

            string weaponClass = item.damage > 0 ? (item.DamageType.Name.Replace("DamageClass", "") + " ") : "";
            weaponClass = weaponClass.ToLower();

            string top = standaloneMods.Contains(CurMod) ? "" : "{{mod sub-page}}<!--DO NOT REMOVE THIS LINE! It is required for Mod sub-pages to work properly.-->";
            string dropstuff = Drops(item);
            string craftingstuff = Crafting(item);
            string craftable = IsCraftable(item.type) ? " craftable" : "";
            string summoninfo = ((item.DamageType == DamageClass.Default && item.damage == 0) || item.DamageType == DamageClass.Summon) ? SummonInfo(item, projName, extension, itemtype2) : "";
            string mountInfo = MountInfo(item, mountName);
            string addWrapper = dropstuff != "" ? "\n{{infobox wrapper" + "\n|" : "";
            string n = dropstuff != "" ? "" : "\n";
            string consumable = item.consumable ? "\n| consumable = yes" : "";
            string placeable = item.createTile > TileID.Dirt || item.createWall > 0 ? "\n| placeable = yes" : "";
            string auto = item.autoReuse ? "\n| auto = yes" : "";
            string summontxt = templateType == "Pets" ? " summoning item. It summons a " + projName + ", to follow the player." : "";
            string finalType = linkType ? GetLinkAccountingForMod(weaponClass + itemtype2) : (weaponClass + itemtype2);
            string tier = item.rare <= ItemRarityID.LightRed ? " [[Pre-Hardmode]]" : " [[Hardmode]]";
            string masterTemp = standaloneMods.Contains(CurMod) ? "\n{{Master Template " + templateType : "\n{{" + CurMod.DisplayName + "/Master Template " + templateType;
            return
                top +
                addWrapper + n + "{{item infobox"
                + "\n| type = " + itemtype
                + "\n| stack = " + item.maxStack
                + "\n| research = " + item.ResearchUnlockCount
                + "\n| tooltip = " + GetTooltip(item)
                + "\n| rare = " + GetRarity(item)
                + (item.damage > 0 ? ("\n| damage = " + item.damage) : "")
                + (item.damage > 0 ? ("\n| knockback = " + item.knockBack) : "")
                + (item.damage > 0 ? ("\n| damagetype = " + item.DamageType.Name.Replace("DamageClass", "")) : "")
                + GetTileDimensions(item)
                + GetUse(item)
                + GetBuff(item)
                + GetBuyPrice(item.type)
                + consumable
                + placeable
                + auto
                + "\n| sell = " + GetValue(item.value)
                + "\n}}"
                + dropstuff
                + summoninfo
                + mountInfo
                + "\nThe '''" + item.Name + "''' is a" + craftable + tier + " " + finalType + summontxt +
                "\n" +
                craftingstuff +
                "\n{{-}}" +
                "\n{{vh}}" +
                masterTemp +
                "\n| show-main = yes" +
                ShowStrings(item) +
                "\n}}" +
                "\n";
        }
        #endregion

        #region NPCs
        /// <summary>
        /// Generates pages for every single NPC
        /// </summary>
        /// <returns></returns>
        public static string GenerateAllNPCPages()
        {
            string ret = "";
            foreach (var npc in ContentSamples.NpcsByNetId)
            {
                NPC ne = npc.Value;
                if (ne.ModNPC == null)
                    continue;
                if (ne.ModNPC.Mod != CurMod)
                    continue;
                ret += ExportNPCInfo(ne) + "\n\n\n\n\n";
            }
            return ret;
        }

        public static string ExportNPCInfo(NPC npc)
        {
            string itemtype = "";
            string itemtype2 = "";
            if (npc.boss || NPCID.Sets.ShouldBeCountedAsBoss[npc.type])
            {
                itemtype = "Boss";
                itemtype2 = "boss";
            }
            else if (npc.CountsAsACritter)
            {
                itemtype = "Critter";
                itemtype2 = "critter";
            }
            else if (npc.friendly || npc.townNPC)
            {
                itemtype = "Town NPC";
                itemtype2 = "npc";
            }
            else
            {
                itemtype = "Enemy";
                itemtype2 = "common";
            }

            string debugName = "-->" + npc.FullName + "<!--\n\n";
            string top = standaloneMods.Contains(CurMod) ? "" : "{{mod sub-page}}<!--DO NOT REMOVE THIS LINE! It is required for Mod sub-pages to work properly.-->";
            string masterTemp = standaloneMods.Contains(CurMod) ? "\n{{Master Template Characters\n" : "\n{{" + ModName + "/Master Template Characters";
            string banner = npc.BannerID() > 0 ? "yes" : "no";
            return
                debugName + top +
                "{{npc infobox"
                + "\n| type = " + itemtype
                + "\n| environment = "
                + "\n| damage = {{dv|" + npc.damage + "|" + 2 * npc.damage + "|" + 3 * npc.damage + "}}"
                + "\n| life = {{dv|" + npc.lifeMax + "|" + 2 * npc.lifeMax + "|" + 3 * npc.lifeMax + "}}"
                + "\n| defense = " + npc.defense
                + "\n| knockback = " + (1 - npc.knockBackResist) * 100 + "%"
                + "\n| banner = " + banner
                + "\n| money = " + GetValue((int)(npc.value * 5))
                + NPCDrops(npc)
                + "\n}}"
                + "\nThe '''" + npc.FullName + "''' is a " + itemtype.ToLower() + " that " +
                "\n{{-}}" +
                "\n{{vh}}" +
                masterTemp +
                "| show-main = yes" +
                "\n| show-" + itemtype2 + " = yes" +
                "\n}}" +
                "\n";
        }

        public static string NPCDrops(NPC npc)
        {
            List<int> bannedItems = new List<int>()
            {
                ItemID.BloodyMachete,
                ItemID.JungleKey,
                ItemID.DungeonDesertKey,
                ItemID.HallowedKey,
                ItemID.CrimsonKey,
                ItemID.CorruptionKey,
                ItemID.FrozenKey,
                ItemID.Yelets,
                ItemID.Present,
                ItemID.GoodieBag,
                ItemID.LivingFireBlock,
                ItemID.Amarok,
                ItemID.Cascade,
                ItemID.Kraken,
                ItemID.Amarok,
                ItemID.KOCannon,
                ItemID.SoulofLight,
                ItemID.SoulofNight,
                ItemID.HelFire,
                ItemID.PirateMap,
                //ModContent.ItemType<AstralPearl>()
            };
            string ret = "";
            List<IItemDropRule> rules = Main.ItemDropsDB.GetRulesForNPCID(npc.type);
            foreach (IItemDropRule rouxl in rules)
            {
                if (rouxl is CommonDrop cDrop)
                {
                    if (bannedItems.Contains(cDrop.itemId))
                        continue;

                    ret += "\n| " + ContentSamples.ItemsByType[cDrop.itemId].Name + "| " + cDrop.amountDroppedMinimum + "-" + cDrop.amountDroppedMaximum + "|" + Math.Round(((cDrop.chanceNumerator / (float)cDrop.chanceDenominator) * 100), 2) + "%";
                }
            }
            return ret;
        }
        #endregion

        /// <summary>
        /// Grabs NPC drop rates for said item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string Drops(Item item)
        {
            List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
            for (int i = 0; i < ContentSamples.NpcsByNetId.Count; i++)
            {
                if (!ContentSamples.NpcsByNetId.ContainsKey(i))
                    continue;
                List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(i);
                for (int l = 0; l < loot.Count; l++)
                {
                    if (loot[l] is CommonDrop fuck)
                    {
                        if (fuck.itemId == item.type)
                        {
                            float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                            int min = fuck.amountDroppedMinimum;
                            int max = fuck.amountDroppedMaximum;
                            npcDropIds.Add((ContentSamples.NpcsByNetId[i], min, max, percent));
                        }
                    }
                }
            }
            List<(Item, int, int, float)> itemDropIds = new List<(Item, int, int, float)>();

            string templateName = standaloneMods.Contains(CurMod) ? "item drops infobox" : "drop infobox";
            string dropstuff = npcDropIds.Count > 0 ? "\n|{{" + templateName : "";
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                if (!ContentSamples.ItemsByType.ContainsKey(i))
                    continue;
                List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForItemID(i);
                for (int l = 0; l < loot.Count; l++)
                {
                    if (loot[l] is CommonDrop fuck)
                    {
                        if (fuck.itemId == item.type)
                        {
                            float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                            int min = fuck.amountDroppedMinimum;
                            int max = fuck.amountDroppedMaximum;
                            itemDropIds.Add((ContentSamples.ItemsByType[i], min, max, percent));
                        }
                    }
                }
            }

            bool alone = standaloneMods.Contains(CurMod);
            string hash = alone ? "" : "#";
            string L1 = alone ? "[[" : "";
            string L2 = alone ? "]]" : "";

            if (dropstuff.Length > 2)
            {
                foreach ((NPC, int, int, float) drop in npcDropIds)
                {
                    string name = drop.Item1.ModNPC != null ? hash + drop.Item1.FullName : drop.Item1.FullName;
                    string dropAmt = drop.Item2 == drop.Item3 ? drop.Item2.ToString() : drop.Item2 + "-" + drop.Item3;
                    dropstuff += "\n| " + L1 + name + L2 + "|" + dropAmt + "|" + drop.Item4 + "%";
                }
                foreach ((Item, int, int, float) drop in itemDropIds)
                {
                    string name = GetTaggedItemName(drop.Item1);
                    string dropAmt = drop.Item2 == drop.Item3 ? drop.Item2.ToString() : drop.Item2 + "-" + drop.Item3;
                    dropstuff += "\n| " + L1 + name + L2 + "|" + dropAmt + "|" + drop.Item4 + "%";
                }
                dropstuff += "\n}}" + "\n}}";
            }
            return dropstuff;
        }

        /// <summary>
        /// Grabs if an item is craftable and/or is a material
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string Crafting(Item item)
        {
            bool craftable = IsCraftable(item.type);
            bool crafty = item.material || craftable;
            string recipestuff = crafty ? "\n== Crafting ==" : "";
            if (craftable)
            {
                recipestuff +=
                "\n=== Recipes ===" +
                "\n{{recipes|result=" + GetTaggedItemName(item) + "}}" +
                "\n";
            }
            if (item.material)
            {
                recipestuff +=
                "\n=== Used in ===" +
                "\n{{recipes|ingredient=" + GetTaggedItemName(item) + "}}" +
                "\n";
            }
            return recipestuff;
        }

        /// <summary>
        /// A wiki formatted function to write the type of minion/pet/light pet that is summoned
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <param name="extension"></param>
        /// <param name="summonType"></param>
        /// <returns></returns>
        public static string SummonInfo(Item item, string name, string extension, string summonType)
        {
            string summon = summonType == "weapon" ? "summon" : summonType;
            return item.shoot > ProjectileID.None ? "\n{{summoned|" + name + "|image=[[File:" + name + ImageName() + "." + extension + "]]|type=" + summon + "}}"
                + "\n" : "";
        }

        /// <summary>
        /// Writes information for a mount
        /// </summary>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string MountInfo(Item item, string name)
        {
            return item.mountType > 0 ? "\n{{summoned|" + name + "|image=[[File:" + name + ImageName() + ".png]]|type=Mount}}"
                + "\n" : "";
        }

        /// <summary>
        /// Gets use time for an item, accounting for true melee's funny - 1
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetUse(Item item)
        {
            if (item.useStyle < ItemUseStyleID.Swing)
                return "";

            int trueUse = item.useStyle == ItemUseStyleID.Swing ? item.useTime - 1 : item.useTime;
            return "\n| use = " + trueUse;
        }

        /// <summary>
        /// Grabs a buff's information, wiki formatted
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetBuff(Item item)
        {
            string hash = standaloneMods.Contains(CurMod) ? "" : "#";
            string ret = "";
            if (item.buffType > 0)
            {
                string buffName = Lang.GetBuffName(item.buffType);
                if (item.shoot > ProjectileID.None)
                {
                    //if (ContentSamples.ProjectilesByType[item.shoot].Name == buffName)
                    {
                        buffName += " (buff)";
                    }
                }
                ret = "\n| buff = " + hash + buffName + " | bufflink = no"
                + "\n| bufftip = " + Lang.GetBuffDescription(item.buffType);
            }
            if (item.mountType > 0)
            {
                Mount.MountData data = MountLoader.GetMount(item.mountType).MountData;
                string buffName = Lang.GetBuffName(data.buff);
                ret = "\n| buff = " + hash + buffName + " | bufflink = no"
                + "\n| bufftip = " + Lang.GetBuffDescription(data.buff);
            }
            return ret;
        }

        /// <summary>
        /// Gets dimensions for a tile and formats them for tile infoboxes
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetTileDimensions(Item item)
        {
            if (item.createTile <= TileID.Dirt)
            {
                return "";
            }
            string ret = "";
            FieldInfo colorArray = typeof(MapHelper).GetField("colorLookup", BindingFlags.Static | BindingFlags.NonPublic);

            Color[] cArray = (Color[])colorArray.GetValue(null);
            Color c = cArray[item.createTile];
            ret += "\n| color = " + ColorToHex(c);
            TileObjectData data = TileObjectData.GetTileData(item.createTile, item.placeStyle);
            if (data == null)
                return ret;
            if (data.Height > 0)
            {
                ret += "\n| height = " + data.Height;
            }
            if (data.Width > 0)
            {
                ret += "\n| width = " + data.Width;
            }
            return ret;
        }

        public static string ShowStrings(Item item)
        {
            string ret = "\n| show-";
            if (item.damage > 0 && item.ammo <= 0)
            {
                return GetClass(item);
            }
            else if (item.ammo > 0)
            {
                ret += "ammo";
            }
            else if (item.createTile > TileID.Dirt && Main.tileFrameImportant[item.createTile])
            {
                ret += "furniture";
            }
            else if (item.createWall > 0)
            {
                ret += "walls";
            }
            else if (item.createTile > TileID.Dirt)
            {
                ret += "blocks";
            }
            else if (item.buffType > 0)
            {
                ret += "potion";
            }
            else if (item.createTile > TileID.Dirt)
            {
                ret += "blocks";
            }
            else if (item.material)
            {
                ret += "material";
            }
            else if (item.accessory)
            {
                ret += "accessories";
            }
            else if (item.headSlot > 0 || item.bodySlot > 0 || item.legSlot > 0)
            {
                ret += "armor";
            }
            else
            {
                ret += "other";
            }
            return ret + " = yes";
        }

        public static string GetClass(Item item)
        {
            if (item.damage <= 0)
                return "";
            if (item.ammo <= 0)
            {
                if (item.DamageType.CountsAsClass(DamageClass.Melee))
                    return "\n| show-melee = yes";
                else if (item.DamageType.CountsAsClass(DamageClass.Ranged))
                    return "\n| show-ranged = yes";
                else if (item.DamageType.CountsAsClass(DamageClass.Magic))
                    return "\n| show-magic = yes";
                else if (item.DamageType.CountsAsClass(DamageClass.Summon))
                    return "\n| show-summon = yes";
                else if (item.DamageType.CountsAsClass(DamageClass.Throwing))
                    return "\n| show-rogue = yes";
                else
                    return "\n| show-typeless = yes";
            }
            return "";
        }
    }
}

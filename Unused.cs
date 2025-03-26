using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;
using Terraria.ModLoader;
using System;
using Terraria.GameContent.ItemDropRules;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;

namespace WikiHelper
{
    public class Unused
    {
        /// <summary>
        /// Generates various master templates for the Calamity's Vanities wiki
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>

        public static string GenerateMasterTemplate()
        {
            string ret = "";
            List<string> melee = new List<string> { };
            List<string> ranged = new List<string> { };
            List<string> magic = new List<string> { };
            List<string> summon = new List<string> { };
            List<string> rogue = new List<string> { };
            List<string> typeless = new List<string> { };

            Mod CalVal = CurMod;
            foreach (var item in ContentSamples.ItemsByType)
            {
                Item i = item.Value;
                if (item.Value.ModItem != null)
                {
                    if (item.Value.ModItem.Mod == CurMod)
                    {
                        if (i.damage > 0 && !i.accessory)
                        {
                            if (i.DamageType.CountsAsClass(DamageClass.Melee) && i.pick <= 0 && i.axe <= 0 && i.hammer <= 0)
                            {
                                melee.Add(i.Name);
                            }
                            else if (i.DamageType.CountsAsClass(DamageClass.Magic))
                            {
                                magic.Add(i.Name);
                            }
                            else if (i.DamageType.CountsAsClass(DamageClass.Ranged) && i.ammo <= 0)
                            {
                                ranged.Add(i.Name);
                            }
                            else if (i.DamageType.CountsAsClass(DamageClass.Summon))
                            {
                                summon.Add(i.Name);
                            }
                            else if (i.DamageType.CountsAsClass(DamageClass.Throwing))
                            {
                                rogue.Add(i.Name);
                            }
                            else
                            {
                                typeless.Add(i.Name);
                            }
                        }
                    }
                }
            }
            melee.Sort();
            ranged.Sort();
            magic.Sort();
            summon.Sort();
            rogue.Sort();
            typeless.Sort();

            ret += "<!--- MELEE --->\n\n";
            foreach (string m in melee)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            ret += "<!--- RANGED --->\n\n";
            foreach (string m in ranged)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            ret += "<!--- MAGIC --->\n\n";
            foreach (string m in magic)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            ret += "<!--- SUMMON --->\n\n";
            foreach (string m in summon)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            ret += "<!--- ROGUE --->\n\n";
            foreach (string m in rogue)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            ret += "<!--- TYPELESS --->\n\n";
            foreach (string m in typeless)
            {
                ret += "            -->|" + GetLinkAccountingForMod(m) + "<!--\n";
            }

            return ret;
        }

        /// <summary>
        /// Generates a list of equips for a master template
        /// </summary>
        /// <returns></returns>
        public static List<string> GenerateEquipsTemplate()
        {
            Mod CalVal = CurMod;
            List<string> itemsToAdd = new List<string>();
            List<string> boditemsToAdd = new List<string>();
            List<string> legitemsToAdd = new List<string>();
            List<string> balitemsToAdd = new List<string>();
            List<string> scarfitemsToAdd = new List<string>();
            List<string> capeitemsToAdd = new List<string>();
            List<string> transitemsToAdd = new List<string>();
            List<string> shielditemsToAdd = new List<string>();
            List<string> miscitemstoadd = new List<string>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.headSlot > 0)
                        itemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.bodySlot > 0)
                        boditemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.legSlot > 0)
                        legitemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.balloonSlot > 0)
                        balitemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.neckSlot > 0)
                        scarfitemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.shieldSlot > 0)
                        shielditemsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.accessory)
                    {
                        if (item.backSlot > 0)
                            capeitemsToAdd.Add(ContentSamples.ItemsByType[i].Name);
                    }
                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.accessory && item.headSlot + item.bodySlot + item.legSlot + item.backSlot + item.shieldSlot + item.neckSlot + item.wingSlot + item.balloonSlot + item.wingSlot <= 0)
                    {
                        miscitemstoadd.Add(ContentSamples.ItemsByType[i].Name);
                    }
                }
            }

            itemsToAdd.Sort();
            boditemsToAdd.Sort();
            legitemsToAdd.Sort();
            balitemsToAdd.Sort();
            scarfitemsToAdd.Sort();
            capeitemsToAdd.Sort();
            shielditemsToAdd.Sort();
            miscitemstoadd.Sort();
            itemsToAdd.AddRange(boditemsToAdd);
            itemsToAdd.AddRange(legitemsToAdd);
            itemsToAdd.AddRange(balitemsToAdd);
            itemsToAdd.AddRange(scarfitemsToAdd);
            itemsToAdd.AddRange(capeitemsToAdd);
            itemsToAdd.AddRange(shielditemsToAdd);
            itemsToAdd.AddRange(miscitemstoadd);
            return itemsToAdd;
        }

        /// <summary>
        /// Creates a list of every Calamity NPC, wiki formatted
        /// </summary>
        public static void MakeCalamityNPCPage()
        {

            string path;
            List<string> list = new List<string>();
            List<NPC> calProj = new List<NPC>();
            path = $@"{exportPath}\CalamityNPCDump.txt";
            list.Add(path);
            string ret = "{| class=\"terraria sortable lined\" style=\"text-align:center\"\r\n" +
                "! width=10% | Item\r\n" +
                "! width=20% | Internal Name\r\n" +
                "|-";
            for (int i = 0; i < ContentSamples.NpcsByNetId.Count; i++)
            {
                if (ContentSamples.NpcsByNetId.ContainsKey(i))
                {
                    NPC p = ContentSamples.NpcsByNetId[i];
                    if (p.ModNPC != null)
                    {
                        if (p.ModNPC.Mod == ModLoader.GetMod("CalamityMod"))
                        {
                            calProj.Add(p);
                        }
                    }
                }
            }
            calProj.Sort((x, y) => NPCLoader.GetNPC(x.type).Name.CompareTo(NPCLoader.GetNPC(y.type).Name));
            for (int i = 0; i < calProj.Count; i++)
            {
                NPC p = calProj[i];
                string lore = "";
                string internalName = NPCLoader.GetNPC(p.type).Name;
                switch (p.FullName)
                {
                    case "Hive":
                    case "Crown Jewel":
                        lore = " (enemy)";
                        break;
                    case "Demon":
                        lore = " (Indignant)";
                        break;
                    case "Tooth Ball":
                        lore = " (Old Duke)";
                        break;
                }
                if (internalName.Contains("Head"))
                {
                    lore = " Head";
                }
                if (internalName.Contains("Body"))
                {
                    if (internalName != "AresBody")
                        lore = " Body";
                }
                if (internalName.Contains("Tail"))
                {
                    lore = " Tail";
                }
                ret += "\r\n";
                ret += "| {{item|" + p.FullName + lore + "}} || <code>" + NPCLoader.GetNPC(p.type).Name + "</code>";
                ret += "\r\n|-";
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
        }

        /// <summary>
        /// Generates a page for every misc item
        /// </summary>
        /// <returns></returns>
        public static string GenerateConsumablesPage()
        {
            string ret = "";
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if ((item.material && item.maxStack > 1 && item.shoot <= ProjectileID.None && item.makeNPC <= 0 && item.createTile <= TileID.Dirt && item.createWall <= 0) /*|| item.type == ModContent.ItemType<ProfanedBattery>() || item.type == ModContent.ItemType<ProfanedWheels>() || item.type == ModContent.ItemType<ProfanedFrame>()*/)
                {
                    items.Add(item);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
                for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
                {
                    List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                    for (int l = 0; l < loot.Count; l++)
                    {
                        if (loot[l] is CommonDrop fuck)
                        {
                            if (fuck.itemId == items[i].type)
                            {
                                float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                                int min = fuck.amountDroppedMinimum;
                                int max = fuck.amountDroppedMaximum;
                                npcDropIds.Add((ContentSamples.NpcsByNetId[n], min, max, percent));
                                break;
                            }
                        }
                    }
                }

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generates a table for individual equip pieces
        /// </summary>
        /// <returns></returns>
        public static string GenerateVanityStandalone()
        {
            string ret = "";
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if (item.headSlot >= 0 || item.bodySlot >= 0 || item.legSlot >= 0)
                {
                    if (!item.Name.Contains("Profaned Cultist") && !item.Name.Contains("Cultist Assassin") && !item.Name.Contains("Odd Polterghast") && !item.Name.Contains("Brimstone") && !item.Name.Contains("Fallen Paladin's") && !item.Name.Contains("Draedon") && !item.Name.Contains("Arsenal Soldier") && !item.Name.Contains("Cryo") && !item.Name.Contains("Demonshade") && !item.Name.Contains("Belladonna") && !item.Name.Contains("Astrachnid") && !item.Name.Contains("Bloody Mary") && !item.Name.Contains("Earthen"))
                        items.Add(item);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
                for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
                {
                    List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                    for (int l = 0; l < loot.Count; l++)
                    {
                        if (loot[l] is CommonDrop fuck)
                        {
                            if (fuck.itemId == items[i].type)
                            {
                                float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                                int min = fuck.amountDroppedMinimum;
                                int max = fuck.amountDroppedMaximum;
                                npcDropIds.Add((ContentSamples.NpcsByNetId[n], min, max, percent));
                                break;
                            }
                        }
                    }
                }

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (equipped)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + GetArmorSlotType(items[i]) +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generate a table of vanity sets from the Calamity's Vanities mod
        /// </summary>
        /// <returns></returns>
        public static string GenerateVanitySets()
        {
            string ret = "";
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if (item.headSlot >= 0 || item.bodySlot >= 0 || item.legSlot >= 0)
                {
                    if (item.Name.Contains("Profaned Cultist") || item.Name.Contains("Cultist Assassin") || item.Name.Contains("Odd Polterghast") || item.Name.Contains("Brimstone") || item.Name.Contains("Fallen Paladin's") || item.Name.Contains("Draedon") || item.Name.Contains("Arsenal Soldier") || item.Name.Contains("Cryo") || item.Name.Contains("Demonshade") || item.Name.Contains("Belladonna") || item.Name.Contains("Astrachnid") || item.Name.Contains("Bloody Mary") || item.Name.Contains("Earthen") || item.Name.Contains("Perennial"))
                        items.Add(item);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
                for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
                {
                    List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                    for (int l = 0; l < loot.Count; l++)
                    {
                        if (loot[l] is CommonDrop fuck)
                        {
                            if (fuck.itemId == items[i].type)
                            {
                                float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                                int min = fuck.amountDroppedMinimum;
                                int max = fuck.amountDroppedMaximum;
                                npcDropIds.Add((ContentSamples.NpcsByNetId[n], min, max, percent));
                                break;
                            }
                        }
                    }
                }

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (equipped)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + GetArmorSlotType(items[i]) +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generates a table of vanity transformations from the Calamity's Vanities mod
        /// </summary>
        /// <returns></returns>
        public static string GenerateTransformations()
        {
            string ret = "";
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if (item.accessory && !item.vanity && item.wingSlot <= 0 && item.createTile <= TileID.Dirt)
                {
                    items.Add(item);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
                for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
                {
                    List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                    for (int l = 0; l < loot.Count; l++)
                    {
                        if (loot[l] is CommonDrop fuck)
                        {
                            if (fuck.itemId == items[i].type)
                            {
                                float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                                int min = fuck.amountDroppedMinimum;
                                int max = fuck.amountDroppedMaximum;
                                npcDropIds.Add((ContentSamples.NpcsByNetId[n], min, max, percent));
                                break;
                            }
                        }
                    }
                }

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (equipped)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Lists out every block, furniture, and wall in a mod
        /// </summary>
        /// <returns></returns>
        public static List<string> GenerateTilesTemplate()
        {
            Mod CalVal = CurMod;
            List<string> blocksToAdd = new List<string>();
            List<string> wallsToAdd = new List<string>();
            List<string> furnitureToAdd = new List<string>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.createTile > TileID.Dirt && !Main.tileFrameImportant[item.createTile])
                        blocksToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.createWall > 0)
                        wallsToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (ContentSamples.ItemsByType[i]?.ModItem?.Mod == CalVal)
                {
                    if (item.createTile > TileID.Dirt && Main.tileFrameImportant[item.createTile])
                        furnitureToAdd.Add(ContentSamples.ItemsByType[i].Name);

                }
            }

            blocksToAdd.Sort();
            wallsToAdd.Sort();
            //furnitureToAdd.Sort();
            blocksToAdd.AddRange(wallsToAdd);
            blocksToAdd.AddRange(furnitureToAdd);
            return blocksToAdd;
        }

        /// <summary>
        /// Generates an accessory page table
        /// </summary>
        /// <returns></returns>
        public static string GenerateVanityAccessories()
        {
            string ret = "";
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if (item.accessory && item.headSlot <= 0 && item.wingSlot <= 0 && item.bodySlot <= 0 && item.legSlot <= 0)
                {
                    items.Add(item);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
                for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
                {
                    List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                    for (int l = 0; l < loot.Count; l++)
                    {
                        if (loot[l] is CommonDrop fuck)
                        {
                            if (fuck.itemId == items[i].type)
                            {
                                float percent = (float)Math.Round(1 / (float)fuck.chanceDenominator * 100, 2);
                                int min = fuck.amountDroppedMinimum;
                                int max = fuck.amountDroppedMaximum;
                                npcDropIds.Add((ContentSamples.NpcsByNetId[n], min, max, percent));
                                break;
                            }
                        }
                    }
                }
                if (npcDropIds.Count <= 0)
                    return "";
                string itemName = GetLinkAccountingForMod(npcDropIds[0].Item1.FullName);
                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + itemName + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (equipped)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + GetAccessorySlotType(items[i]) +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generates a list of every NPC
        /// </summary>
        /// <returns></returns>
        public static string GenerateNPCMasterTemplate()
        {
            string ret = "";

            ret += "-->{{navbox/start<!--\n" +
                "-->|header = NPCs<!--\n" +
                "-->|class = $show-common$<!--\n" +
                "-->}}<!--\n" +
                "--><div class=\"table\"><!--\n";

            List<(string, string)> entriesByBiome = new List<(string, string)>();

            ret += "-->}}<!--\n--></div><!--\n--><div><!--\n-->{{navbox/h1|[[Common]]}}<!--\n-->{{dotlist<!--\n";
            foreach (var n in ContentSamples.NpcsByNetId)
            {
                if (n.Value.ModNPC == null)
                    continue;
                if (n.Value.ModNPC.Mod != CurMod)
                    continue;
                ret += "-->|{{item|#" + n.Value.FullName + "}}<!--\n";

            }
            ret += "\n-->{{navbox/end}}<!--";

            return ret;
        }

        /// <summary>
        /// Creates a list of every Calamity item, wiki formatted
        /// </summary>
        public static void MakeCalamityItemPage()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> calProj = new List<Item>();
            path = $@"{exportPath}\CalamityItemDump.txt";
            list.Add(path);
            string ret = "{| class=\"terraria sortable lined\" style=\"text-align:center\"\r\n" +
                "! width=10% | Item\r\n" +
                "! width=20% | Internal Name\r\n" +
                "|-";
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item p = ContentSamples.ItemsByType[i];
                if (p.ModItem != null)
                {
                    if (p.ModItem.Mod == ModLoader.GetMod("CalamityMod"))
                    {
                        calProj.Add(p);
                    }
                }
            }
            calProj.Sort((x, y) => ItemLoader.GetItem(x.type).Name.CompareTo(ItemLoader.GetItem(y.type).Name));
            for (int i = 0; i < calProj.Count; i++)
            {
                Item p = calProj[i];
                string lore = "";
                string internalName = ItemLoader.GetItem(p.type).Name;
                if (internalName[0] == 'L' && internalName[1] == 'o' && internalName[2] == 'r' && internalName[3] == 'e')
                {
                    lore = " (Lore)";
                }
                switch (p.Name)
                {
                    case "Butcher":
                    case "Thunderstorm":
                    case "Sandstorm":
                        lore = " (weapon)";
                        break;
                    case "Elderberry":
                    case "Blood Orange":
                    case "Pineapple":
                        lore = " (calamity)";
                        break;
                    case "Trash Can":
                        lore = " (pet)";
                        break;
                    case "Purity":
                        lore = " (accessory)";
                        break;
                }
                if (internalName == "SlimeGodMask")
                {
                    lore = " (Corruption)";
                }
                if (internalName == "SlimeGodMask2")
                {
                    lore = " (Crimson)";
                }
                ret += "\r\n";
                ret += "| {{item|" + p.Name + lore + "}} || <code>" + ItemLoader.GetItem(p.type).Name + "</code>";
                ret += "\r\n|-";
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
        }


        /// <summary>
        /// Dumps out a list of wings not in the Any Wings recipe group
        /// </summary>
        public static void WingItems()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> items = new List<Item>();
            List<Item> existingItems = new List<Item>();
            path = $@"{exportPath}\Wings.txt";
            list.Add(path);
            string ret = "Missing Wings:";
            int key = -1;
            foreach (var r in RecipeGroup.recipeGroups)
            {
                if (r.Value.ContainsItem(ItemID.AngelWings))
                {
                    key = r.Key;
                    break;
                }
            }
            foreach (int i in RecipeGroup.recipeGroups[key].ValidItems)
            {
                existingItems.Add(ContentSamples.ItemsByType[i]);
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if ((item.wingSlot <= 0) || existingItems.Contains(item) || (item.ModItem != null && item.ModItem.Mod != CurMod) || item.shoeSlot > 0)
                    continue;
                items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
            {
                ret += "\n" + items[i].Name;
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }

        /// <summary>
        /// Dumps out a list of food items not in the Any Food recipe group
        /// </summary>
        public static void FoodItems()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> items = new List<Item>();
            List<Item> existingItems = new List<Item>();
            path = $@"{exportPath}\FoodItems.txt";
            list.Add(path);
            string ret = "Missing Food items:";
            int key = -1;
            foreach (var r in RecipeGroup.recipeGroups)
            {
                if (r.Value.ContainsItem(ItemID.BananaSplit))
                {
                    key = r.Key;
                    break;
                }
            }
            foreach (int i in RecipeGroup.recipeGroups[key].ValidItems)
            {
                existingItems.Add(ContentSamples.ItemsByType[i]);
            }
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if ((item.buffType != BuffID.WellFed && item.buffType != BuffID.WellFed2 && item.buffType != BuffID.WellFed3) || existingItems.Contains(item) || (item.ModItem != null && item.ModItem.Mod != ModLoader.GetMod("CalamityMod")))
                    continue;
                items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
            {
                ret += "\n" + items[i].Name;
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }

        /// <summary>
        /// Dumps out a list of Calamity items with the cyan rarity
        /// </summary>
        public static void CyanRarity()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> items = new List<Item>();
            path = $@"{exportPath}\CyanCalamityItems.txt";
            list.Add(path);
            string ret = "Cyan rarity Calamity items:";
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item.rare != ItemRarityID.Cyan)
                    continue;
                if (item.ModItem != null)
                    if (item.ModItem.Mod == ModLoader.GetMod("CalamityMod"))
                        items.Add(item);
            }

            for (int i = 0; i < items.Count; i++)
            {
                ret += "\n" + items[i].Name;
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }

        /// <summary>
        /// Dumps out Calamity items with sell prices not matching their rarity
        /// </summary>
        public static void InconsistentPrices()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> items = new List<Item>();
            path = $@"{exportPath}\CalamityRarities.txt";
            list.Add(path);

            string ret = "";
            /*ret += "Current Turquoise rarity: " + ModContent.RarityType<Turquoise>() + "\n";
            ret += "Current Pure Green rarity: " + ModContent.RarityType<PureGreen>() + "\n";
            ret += "Current Dark Blue rarity: " + ModContent.RarityType<DarkBlue>() + "\n";
            ret += "Current Violet rarity: " + ModContent.RarityType<Violet>() + "\n";
            ret += "Current Hot Pink rarity: " + ModContent.RarityType<HotPink>() + "\n";
            ret += "Current Calamity Red rarity: " + ModContent.RarityType<CalamityRed>() + "\n";*/
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item.rare == ItemRarityID.Master || item.rare == ItemRarityID.Expert)
                    continue;
                if (item.maxStack == 1)
                    continue;
                if (item.value == 0)
                    continue;
                if (item.damage < 1)
                    continue;
                if (item.ModItem != null)
                    if (item.ModItem.Mod == ModLoader.GetMod("CalamityMod"))
                        items.Add(item);
            }

            items.Sort((x, y) => ContentSamples.ItemsByType[x.type].rare.CompareTo(ContentSamples.ItemsByType[y.type].rare));
            for (int i = 0; i < items.Count; i++)
            {

                if (items[i].value == GetPrice(items[i].rare))
                    continue;
                ret += "\nItem name: " + items[i].Name + ", Rarity: " + items[i].rare + ", Price: " + items[i].value + ", Expected price: " + GetPrice(items[i].rare);
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }

        /// <summary>
        /// Grabs the intended price of an item based on its rarity
        /// </summary>
        /// <param name="rare">The rarity</param>
        /// <returns></returns>
        public static int GetPrice(int rare)
        {
            /*switch (rare)
            {
                case 0:
                    return 0;
                case 1:
                    return CalamityGlobalItem.RarityBlueBuyPrice;
                case 2:
                    return CalamityGlobalItem.RarityGreenBuyPrice;
                case 3:
                    return CalamityGlobalItem.RarityOrangeBuyPrice;
                case 4:
                    return CalamityGlobalItem.RarityLightRedBuyPrice;
                case 5:
                    return CalamityGlobalItem.RarityPinkBuyPrice;
                case 6:
                    return CalamityGlobalItem.RarityLightPurpleBuyPrice;
                case 7:
                    return CalamityGlobalItem.RarityLimeBuyPrice;
                case 8:
                    return CalamityGlobalItem.RarityYellowBuyPrice;
                case 9:
                    return CalamityGlobalItem.RarityCyanBuyPrice;
                case 10:
                    return CalamityGlobalItem.RarityRedBuyPrice;
                case 11:
                    return CalamityGlobalItem.RarityPurpleBuyPrice;
            }

            if (rare == ModContent.RarityType<Turquoise>())
            {
                return CalamityGlobalItem.RarityTurquoiseBuyPrice;
            }
            if (rare == ModContent.RarityType<PureGreen>())
            {
                return CalamityGlobalItem.RarityPureGreenBuyPrice;
            }
            if (rare == ModContent.RarityType<DarkBlue>())
            {
                return CalamityGlobalItem.RarityDarkBlueBuyPrice;
            }
            if (rare == ModContent.RarityType<Violet>())
            {
                return CalamityGlobalItem.RarityVioletBuyPrice;
            }
            if (rare == ModContent.RarityType<HotPink>())
            {
                return CalamityGlobalItem.RarityHotPinkBuyPrice;
            }
            if (rare == ModContent.RarityType<CalamityRed>())
            {
                return CalamityGlobalItem.RarityCalamityRedBuyPrice;
            }*/
            return 0;
        }

        /// <summary>
        /// Grabs a list of Calamity blocks with no journey mode support
        /// </summary>
        public static void PlaceableStacks()
        {
            string path;
            List<string> list = new List<string>();
            List<Item> items = new List<Item>();
            path = $@"{exportPath}\CalamityPlaceResearch.txt";
            list.Add(path);

            string ret = "";
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item.ModItem != null)
                    if (item.ModItem.Mod == ModLoader.GetMod("CalamityMod"))
                        if (item.ResearchUnlockCount == 1 && (item.createTile > TileID.Dirt || item.createWall > 0))
                        {
                            items.Add(item);
                        }
            }
            for (int i = 0; i < items.Count; i++)
            {

                if (Main.tileFrameImportant[items[i].createTile])
                    continue;
                ret += "\n" + items[i].Name;
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }

        /// <summary>
        /// Creates a master list of Calamity projectiles, Wiki formatted
        /// </summary>
        public void MakeCalamityProjectilePage()
        {
            string path;
            List<string> list = new List<string>();
            List<Projectile> calProj = new List<Projectile>();
            path = $@"{exportPath}\CalamityProjectileDump.txt";
            list.Add(path);
            string ret = "{| class=\"terraria sortable lined\" style=\"text-align:center\"\r\n" +
                "! width=10% | Image\r\n" +
                "! width=20% | Display Name\r\n" +
                "! width=20% | Internal Name\r\n" +
                "! width=50% | Source\r\n" +
                "|-";
            for (int i = 0; i < ContentSamples.ProjectilesByType.Count; i++)
            {
                Projectile p = ContentSamples.ProjectilesByType[i];
                if (p.ModProjectile != null)
                {
                    if (p.ModProjectile.Mod == ModLoader.GetMod("CalamityMod"))
                    {
                        calProj.Add(p);
                    }
                }
            }
            Texture2D nothing = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj").Value;
            calProj.Sort((x, y) => ProjectileLoader.GetProjectile(x.type).Name.CompareTo(ProjectileLoader.GetProjectile(y.type).Name));
            for (int i = 0; i < calProj.Count; i++)
            {
                Projectile p = calProj[i];
                bool invis = TextureAssets.Projectile[p.type].Value == nothing;
                int frameCount = Main.projFrames[p.type];
                string ext = frameCount > 1 ? ".gif" : ".png";
                ret += "\r\n";
                string invistext = invis ? "| {{na}}" : "| ";
                ret += invistext + " || " + p.Name + " || <code>" + ProjectileLoader.GetProjectile(p.type).Name + "</code>" + " || ";
                ret += "\r\n|-";
            }
            File.WriteAllText(path, ret, Encoding.UTF8);
        }
    }
}
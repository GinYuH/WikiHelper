using System;
using Terraria;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static WikiHelper.WikiSystem;
using Terraria.GameContent.ItemDropRules;

namespace WikiHelper
{
    public class Utils
    {
        /// <summary>
        /// Exports a page
        /// </summary>
        /// <param name="function"></param>
        public static void ExportPage(Func<string> function, string name)
        {
            Directory.CreateDirectory(exportPath);
            string path = $@"{exportPath}\" + name + ".txt";
            if (!Directory.Exists(exportPath))
            {
                Directory.CreateDirectory(exportPath);
            }
            File.WriteAllText(path, function.Invoke(), Encoding.UTF8);
            Main.NewText("Exported!");
        }

        /// <summary>
        /// Formats an item's name to mod wiki standards
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetTaggedItemName(Item i)
        {
            int type = i.type;
            int stack = i.stack;
            i.SetDefaults(type);
            i.stack = stack;
            return i.ModItem == null || standaloneMods.Contains(i.ModItem.Mod) ? i.Name : "#" + i.Name;
        }

        /// <summary>
        /// Grabs if an item is craftable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsCraftable(int type)
        {
            foreach (Recipe r in Main.recipe)
            {
                if (r.HasResult(type))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a mod affix to image files
        /// </summary>
        /// <returns>The affix</returns>
        public static string ImageName()
        {
            if (standaloneMods.Contains(CurMod))
                return "";
            else
                return " (" + CurMod.DisplayName + ")";
        }

        /// <summary>
        /// Returns a string used for links to other pages which accounts for mod
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static string GetLinkAccountingForMod(string itemName)
        {
            if (standaloneMods.Contains(CurMod))
                return "[[" + itemName + "]]";
            else
                return "{{+|" + itemName + "}}";
        }

        /// <summary>
        /// A wiki formatted sell price for an item
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string GetValue(int num)
        {
            string finalVal = "No value";

            if (num > 0)
            {
                finalVal = "{{value";
                // platinum
                if (num > 999999 * 5)
                {
                    finalVal += "|" + (int)num / 5000000;
                    num -= (int)(num / 1000000) * 1000000;
                }
                else
                {
                    finalVal += "|0";
                }
                // gold
                if (num > 9999 * 5)
                {
                    finalVal += "|" + (int)(num / 50000);
                    num -= (int)(num / 10000) * 10000;
                }
                else
                {
                    finalVal += "|0";
                }
                // silver
                if (num > 99 * 5)
                {
                    finalVal += "|" + (int)(num / 500);
                    num -= (int)(num / 100) * 100;
                }
                else
                {
                    finalVal += "|0";
                }
                // copper
                finalVal += "|" + num / 5 + "}}";
            }

            return finalVal;
        }

        /// <summary>
        /// Grabs a string with the item's rarity
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetRarity(Item item)
        {
            if (item.master)
            {
                return "fire red";
            }
            if (item.expert)
            {
                return "rainbow";
            }
            if (item.rare < 12)
            {
                return item.rare.ToString();
                return "{{rare|" + item.rare.ToString() + "}}";
            }
            else
            {
                /*string rarityName = "";
                if (item.rare == ModContent.RarityType<CalValEX.Rarities.Aqua>())
                {
                    rarityName = "aqua";
                }
                if (item.rare == ModContent.RarityType<Turquoise>())
                {
                    rarityName = "turquoise";
                }
                if (item.rare == ModContent.RarityType<PureGreen>())
                {
                    rarityName = "pure green";
                }
                if (item.rare == ModContent.RarityType<DarkBlue>())
                {
                    rarityName = "dark blue";
                }
                if (item.rare == ModContent.RarityType<Violet>())
                {
                    rarityName = "violet";
                }
                if (item.rare == ModContent.RarityType<HotPink>())
                {
                    rarityName = "hot pink";
                }
                if (item.rare == ModContent.RarityType<CalamityRed>())
                {
                    rarityName = "calamity red";
                }
                if (item.rare == ModContent.RarityType<DarkOrange>())
                {
                    rarityName = "dark orange";
                }
                return "[[File:Rarity color " + rarityName + " (Calamity's Vanities).png]]";*/
                return "";
            }
        }

        /// <summary>
        /// Grabs an item's tooltip, Wiki formatted
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        public static string GetTooltip(Item item)
        {
            string ret = "";
            for (int i = 0; i < item.ToolTip.Lines; i++)
            {
                ret += item.ToolTip.GetLine(i);
                if (i != item.ToolTip.Lines - 1)
                {
                    ret += "<br/>";
                }
            }
            return ret;
        }

        /// <summary>
        /// Get an item's buy price if it's sold
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetBuyPrice(int type)
        {
            return "";
            /*string oracleShopName = NPCShopDatabase.GetShopName(ModContent.NPCType<OracleNPC>());
            string jellyShopName = NPCShopDatabase.GetShopName(ModContent.NPCType<JellyPriestNPC>());
            string jharimShopName = NPCShopDatabase.GetShopName(ModContent.NPCType<Jharim>());
            NPCShopDatabase.TryGetNPCShop(oracleShopName, out AbstractNPCShop oracleShop);
            NPCShopDatabase.TryGetNPCShop(jellyShopName, out AbstractNPCShop jellyShop);
            NPCShopDatabase.TryGetNPCShop(jharimShopName, out AbstractNPCShop jharShop);
            NPCShop oShop = (NPCShop)oracleShop;
            List<NPCShop.Entry> entry1 = (List<NPCShop.Entry>)oShop.GetType().GetField("entries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(oShop);
            foreach (NPCShop.Entry i in entry1)
            {
                if (i.Item.type == type)
                {
                    return "\n buy = " + GetValue(i.Item.value * 5);
                }
            }*/
            return "";
        }

        /// <summary>
        /// Converts RGB to Hex
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static string ColorToHex(Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }

        /// <summary>
        /// Gets the accessory equip slot of a given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetAccessorySlotType(Item item)
        {
            if (item.backSlot > 0)
                return "Back";
            if (item.frontSlot > 0)
                return "Front";
            if (item.balloonSlot > 0)
                return "Balloon";
            if (item.shieldSlot > 0)
                return "Shield";
            if (item.neckSlot > 0)
                return "Neck";
            return "";
        }

        /// <summary>
        /// Generates a 2D array of colors from a texture
        /// Originally from Calamity
        /// </summary>
        /// <param name="texture"></param>
        /// <returns></returns>
        public static Color[,] GetColorsFromTexture(Texture2D texture)
        {
            Color[] alignedColors = new Color[texture.Width * texture.Height];
            texture.GetData(alignedColors); // Fills the color array with all the colors in the texture

            Color[,] colors2D = new Color[texture.Width, texture.Height];
            for (int x = 0; x < texture.Width; x++)
            {
                for (int y = 0; y < texture.Height; y++)
                {
                    colors2D[x, y] = alignedColors[x + y * texture.Width];
                }
            }
            return colors2D;
        }

        /// <summary>
        /// Returns what armor slot the item falls into. Supports robes.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetArmorSlotType(Item i)
        {
            if (i.headSlot > 0)
                return "Head";
            if (i.bodySlot > 0 && i.legSlot > 0)
                return "Body and Legs";
            if (i.bodySlot > 0)
                return "Body";
            if (i.legSlot > 0)
                return "Legs";
            return "";
        }

        /// <summary>
        /// Gets the name of a tile 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetTileItemName(int type)
        {
            if (type == TileID.DemonAltar)
                return "Demon Altar";
            foreach (var v in ContentSamples.ItemsByType)
            {
                if (v.Value.createTile < TileID.Dirt)
                    continue;
                if (v.Value.createTile == type)
                {
                    return GetTaggedItemName(v.Value);
                }
            }
            return "";
        }

        /// <summary>
        /// Gets the NPC, min amount, max amount, and chance that this item drops from
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static List<(NPC, int, int, float)> GetDrops(Item i)
        {
            List<(NPC, int, int, float)> npcDropIds = new List<(NPC, int, int, float)>();
            for (int n = 0; n < ContentSamples.NpcsByNetId.Count; n++)
            {
                List<IItemDropRule> loot = Terraria.Main.ItemDropsDB.GetRulesForNPCID(n);
                for (int l = 0; l < loot.Count; l++)
                {
                    if (loot[l] is CommonDrop fuck)
                    {
                        if (fuck.itemId == i.type)
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
            return npcDropIds;
        }
    }
}
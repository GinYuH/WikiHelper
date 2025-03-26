using System;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.GameContent.ItemDropRules;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;

namespace WikiHelper
{
    public class ListPages
    {

        public static string GenerateBuffPage()
        {
            string ret = "'''Buffs''' are positive status effects granted to a player upon consuming, equipping, or activating various items. Active buffs are shown as icons below the hotbar, along with their remaining duration.\r\n\r\nThis is a comprehensive list of all buffs added by " + CurMod.DisplayName + ".\r\n\r\n{{item/options|mode=table}}\r\n{| class=\"terraria lined\"\r\n!width=\"45em\"| Icon\r\n!width=\"90em\"| Name\r\n!width=\"90em\"| From\r\n!width=\"417em\"| Effect\r\n!width=\"203em\"| Tooltip\r\n|-";
            List<ModBuff> buffs = new List<ModBuff>();
            for (int i = BuffID.Count - 1; i < BuffLoader.BuffCount; i++)
            {
                ModBuff b = BuffLoader.GetBuff(i);
                if (b == null)
                    continue;
                if (b.Mod != CurMod)
                    continue;
                if (Main.debuff[b.Type])
                    continue;
                buffs.Add(b);
            }
            buffs.Sort((x, y) => x.DisplayName.Value.CompareTo(y.DisplayName.Value));
            foreach (ModBuff b in buffs)
            {
                ret += "|{{item|" + b.DisplayName + "}}\n|SOURCE\n|" + b.Description + "\n|{{bufftip|" + b.DisplayName + "}}\n|-";
            }
            ret += "\n|}\n == See also ==\r\n*[[tgc:Buffs|Buffs]] on the vanilla wiki.\r\n*[[Debuffs]]\r\n{{Master Template Buffs\r\n| show-main = yes\r\n| show-buffs = yes\r\n}}\r\n{{Game mechanics}}\r\n[[Category:Game mechanics]]\r\n{{language info|en=Buffs}}";
            return ret;
        }

        public static string GenerateDebuffPage()
        {
            string ret = "'''Debuffs''' are status effects that affect the player or target negatively.\r\n\r\nThis is a comprehensive list of all buffs added by " + CurMod.DisplayName + "\r\n\r\n{{item/options|mode=table}}\r\n{| class=\"terraria lined\"\r\n!width=\"45em\"| Icon\r\n!width=\"90em\"| Name\r\n!width=\"90em\"| From\r\n!width=\"417em\"| Effect\r\n!width=\"203em\"| Tooltip\r\n|-";
            List<ModBuff> buffs = new List<ModBuff>();
            for (int i = BuffID.Count - 1; i < BuffLoader.BuffCount; i++)
            {
                ModBuff b = BuffLoader.GetBuff(i);
                if (b == null)
                    continue;
                if (b.Mod != CurMod)
                    continue;
                if (!Main.debuff[b.Type])
                    continue;
                buffs.Add(b);
            }

            buffs.Sort((x, y) => x.DisplayName.Value.CompareTo(y.DisplayName.Value));
            foreach (ModBuff b in buffs)
            {
                ret += "\n|{{item|" + b.DisplayName + "}}\n|SOURCE\n|" + b.Description + "\n|{{bufftip|" + b.DisplayName + "}}\n|-";
            }
            ret += "\n|}\n == See also ==\r\n*[[tgc:Debuffs|Debuffs]] on the vanilla wiki.\r\n*[[Buffs]]\r\n{{Master Template Buffs\r\n| show-main = yes\r\n| show-debuffs = yes\r\n}}\r\n{{Game mechanics}}\r\n[[Category:Game mechanics]]\r\n{{language info|en=Debuffs}}";
            return ret;
        }

        /// <summary>
        /// Generates a wing page table
        /// </summary>
        /// <returns></returns>
        public static string GenerateWingStats()
        {
            Mod Calval = CurMod;
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != Calval)
                {
                    continue;
                }
                if (item.wingSlot > 0)
                {
                    items.Add(item);
                }
            }
            string ret = "";
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = GetDrops(items[i]);

                string itemName = GetLinkAccountingForMod(items[i].Name);
                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + itemName + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (equipped)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetTooltip(items[i]) +
                   "\n| " + ArmorIDs.Wing.Sets.Stats[items[i].wingSlot].FlyTime +
                   "\n| " + Math.Round((float)ArmorIDs.Wing.Sets.Stats[items[i].wingSlot].FlyTime / 60f, 2) +
                   "\n| " + ArmorIDs.Wing.Sets.Stats[items[i].wingSlot].AccRunSpeedOverride +
                   "\n| " + ArmorIDs.Wing.Sets.Stats[items[i].wingSlot].AccRunAccelerationMult +
                   "\n| " + GetValue(items[i].value) +
                   "\n| " + GetRarity(items[i]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generates a table of blocks for a blocks page
        /// </summary>
        /// <returns></returns>
        public static string GenerateBlocks()
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
                if (item.createTile >= TileID.Dirt)
                {
                    if (Main.tileFrameImportant[item.createTile])
                        continue;
                    items.Add(item);
                }
            }
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = GetDrops(items[i]);

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (placed)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }

        /// <summary>
        /// Generates a table of walls for a walls page
        /// </summary>
        /// <returns></returns>
        public static string GenerateWalls()
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
                if (item.createWall >= 0)
                {
                    items.Add(item);
                }
            }
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
            for (int i = 0; i < items.Count; i++)
            {
                List<(NPC, int, int, float)> npcDropIds = GetDrops(items[i]);

                string craftable = IsCraftable(items[i].type) ? "\n| {{recipes/extract|result=" + GetTaggedItemName(items[i]) + "}}" : npcDropIds.Count > 0 ? "\n| " + GetLinkAccountingForMod(npcDropIds[0].Item1.FullName) + " " + npcDropIds[0].Item4 + "%" : "\n| ";
                ret += "| {{item|class=boldname|" + GetTaggedItemName(items[i]) + "}}" +
                   "\n| [[File:" + items[i].Name + " (placed)" + ImageName() + ".png|link=]]" +
                   craftable +
                   "\n| " + GetRarity(ContentSamples.ItemsByType[items[i].type]) +
                   "\n|-\n";
            }
            return ret;
        }
    }
}

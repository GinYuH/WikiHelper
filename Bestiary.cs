using Terraria;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Terraria.ID;
using System.Linq;
using System.Reflection;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using static WikiHelper.WikiSystem;

namespace WikiHelper
{
    public class Bestiary
    {

        public static void BestiaryDatabase()
        {
            FieldInfo bestKey = typeof(FlavorTextBestiaryInfoElement).GetField("_key", BindingFlags.Instance | BindingFlags.NonPublic);
            string ret = "{{#dplvar:set|_bestiary:_db|(ok)<!--\n";
            for (int i = 0; i < ContentSamples.NpcsByNetId.Count; i++)
            {
                if (!ContentSamples.NpcsByNetId.ContainsKey(i))
                {
                    continue;
                }
                NPC n = ContentSamples.NpcsByNetId[i];
                if (n.ModNPC == null)
                    continue;
                if (n.ModNPC.Mod != CurMod)
                    continue;
                if (!NPCID.Sets.NPCBestiaryDrawOffset.ContainsKey(i))
                    continue;
                if (!NPCID.Sets.NPCBestiaryDrawOffset[i].Hide)
                {

                    BestiaryEntry b = Main.BestiaryDB.FindEntryByNPCID(n.type);
                    FlavorTextBestiaryInfoElement flavor = (FlavorTextBestiaryInfoElement)b.Info.First((IBestiaryInfoElement elem) => elem is FlavorTextBestiaryInfoElement);
                    string bKey = (string)bestKey.GetValue(flavor);
                    ret += "-->|_bestiary:" + n.FullName + "|" + Language.GetText(bKey) + "<!--\n";
                }
            }
            ret += "-->}}<!--\r\n\r\n--><noinclude>This is a [[:Category:Data templates|Data template]]. It is used to store [[Bestiary]] entries for the {{tl|bestiary}} template.\r\n\r\n[[Category:Data templates]]</noinclude>";



            string path;
            path = $@"{exportPath}\Bestiary.txt";
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");

            return;
        }

        /// <summary>
        /// Dumps out a Bestiary page.
        /// </summary>
        public static void BestiaryPage()
        {
            string path;
            List<string> list = new List<string>();
            List<NPC> npcs = new List<NPC>();
            List<string> Names = new List<string>();
            path = $@"{exportPath}\Bestiary.txt";
            list.Add(path);
            string ret = "{| class=\"terraria lined sortable\" style=\"margin:auto;\"\n! Entity\r\n! Stars\n! Filters\n! Description\n|-\n";
            for (int i = 0; i < ContentSamples.NpcsByNetId.Count; i++)
            {
                if (!ContentSamples.NpcsByNetId.ContainsKey(i))
                {
                    Names.Add("");
                    continue;
                }
                NPC n = ContentSamples.NpcsByNetId[i];
                bool hasThe = n.FullName.Contains("The ");
                string noThe = hasThe ? n.FullName.Remove(0, 4) : n.FullName;
                if (n.FullName == "Ebonian Paladin")
                {
                    noThe = "Slime God2";
                }
                Names.Add(noThe);
                if (n == null)
                    continue;
                if (n.ModNPC == null)
                    continue;
                if (n.ModNPC.Mod != CurMod)
                    continue;
                /*if (n.type == ModContent.NPCType<HiveBlob>() || n.type == ModContent.NPCType<DankCreeper>() || n.type == ModContent.NPCType<DarkHeart>() || n.type == ModContent.NPCType<PhantomSpiritL>() 
                    || n.type == ModContent.NPCType<PhantomSpiritS>()
                    || n.type == ModContent.NPCType<PhantomSpiritM>()
                    || n.type == ModContent.NPCType<PlagueChargerLarge>()
                    || n.type == ModContent.NPCType<CrimulanPaladin>()
                    || n.type == ModContent.NPCType<SplitCrimulanPaladin>()
                    || n.type == ModContent.NPCType<PerforatorHeadSmall>()
                    || n.type == ModContent.NPCType<PerforatorHeadLarge>())
                    continue;*/
                npcs.Add(n);
            }

            npcs.Sort((x, y) => Names[x.type].CompareTo(Names[y.type]));
            List<BestiaryEntry> entries = Main.BestiaryDB.GetBestiaryEntriesByMod(CurMod);
            for (int i = 0; i < npcs.Count; i++)
            {
                NPC n = npcs[i];
                NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(n.type, out var value);
                if (value.Hide)
                    continue;

                BestiaryEntry bE = Main.BestiaryDB.FindEntryByNPCID(n.type);
                string name = "[[" + n.FullName + "]]";
                /*if (n.type == ModContent.NPCType<SplitEbonianPaladin>())
                {
                    name = "[[The Slime God]] (Paladins)";
                }
                if (n.type == ModContent.NPCType<SlimeGodCore>())
                {
                    name = "[[The Slime God]] (core)";
                }
                if (n.type == ModContent.NPCType<CrimsonSlimeSpawn2>())
                {
                    name = "[[Crimson Slime Spawn (spiked)]]";
                }
                if (n.type == ModContent.NPCType<SoulSeekerSupreme>())
                {
                    name = "[[Soul Seeker]] (supreme)";
                }
                if (n.type == ModContent.NPCType<PerforatorHeadMedium>())
                {
                    name = "[[The Perforators]]";
                }
                if (n.type == ModContent.NPCType<HiveEnemy>())
                {
                    name = "[[Hive (enemy)]]";
                }*/
                ret += "| " + name + "\n";

                string rarity = ContentSamples.NpcBestiaryRarityStars[n.type].ToString();
                /*if (n.type == ModContent.NPCType<PhantomSpirit>())
                {
                    rarity = "2 (Normal and Sad)<br/>3 (Angry and Happy)";
                }
                if (n.type == ModContent.NPCType<PerforatorHeadMedium>())
                {
                    rarity = "2 (Small and Medium)<br/>3 (Large)";
                }*/

                ret += "| " + rarity + "\n| ";

                bool comma = false;
                for (int j = 0; j < bE.Info.Count; j++)
                {
                    IBestiaryInfoElement element = bE.Info[j];
                    string commaText = comma ? ", " : "";
                    if (element is Terraria.GameContent.Bestiary.SpawnConditionBestiaryInfoElement biome)
                    {
                        ret += commaText;
                        FilterProviderInfoElement filterer = element as FilterProviderInfoElement;
                        ret += Language.GetText(filterer.GetDisplayNameKey()).Value;
                        comma = true;
                    }
                    if (element is Terraria.GameContent.Bestiary.SpawnConditionBestiaryOverlayInfoElement evente)
                    {
                        ret += commaText;
                        FilterProviderInfoElement filterer = element as FilterProviderInfoElement;
                        ret += Language.GetText(filterer.GetDisplayNameKey()).Value;
                        comma = true;
                    }
                    if (element is ModBiomeBestiaryInfoElement modbiome)
                    {
                        ret += commaText;
                        ret += Language.GetText(modbiome.GetDisplayNameKey()).Value;
                        comma = true;
                    }
                    if (element is BossBestiaryInfoElement)
                    {
                        ret += commaText;
                        ret += "Boss Enemy";
                        comma = true;
                    }
                    if (element is RareSpawnBestiaryInfoElement)
                    {
                        ret += commaText;
                        ret += "Rare Creature";
                        comma = true;
                    }
                }
                if (!comma)
                {
                    ret += "{{na}}";
                }

                string codeName = n.FullName;
                /*if (n.type == ModContent.NPCType<PerforatorHeadMedium>())
                {
                    codeName = "The Perforators";
                }
                if (n.type == ModContent.NPCType<CrimsonSlimeSpawn2>())
                {
                    codeName = "Crimson Slime Spawn (spiked)";
                }*/

                ret += "\n| {{#dplvar:_bestiary:" + codeName + "}}\n|-\n";
            }
            ret += "|}";
            File.WriteAllText(path, ret, Encoding.UTF8);
            Main.NewText("Exported");
        }
    }
}

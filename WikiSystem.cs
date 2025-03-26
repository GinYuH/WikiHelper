using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ModLoader;

namespace WikiHelper
{
	public class WikiSystem : ModSystem
    {
        internal static string exportPath = Path.Combine(Main.SavePath + "/WikiExports");

        public static bool InUI = false;
        public static string ModName = "WikiHelper";
        public static Mod CurMod => ModLoader.GetMod(ModName);

        public static List<Mod> standaloneMods = new List<Mod>();

        public static List<string> standaloneModNames = new List<string>()
        {
            "CalamityMod",
            "ThoriumMod",
            "CalamityFables",
        };

        public override void PostSetupContent()
        {
            foreach (string modName in standaloneModNames)
            {
                if (ModLoader.TryGetMod(modName, out Mod mod))
                {
                    standaloneMods.Add(mod);
                }
            }
        }
    }
}

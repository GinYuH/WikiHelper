using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using static WikiHelper.Utils;
using static WikiHelper.WikiSystem;

namespace WikiHelper
{

    public class WikiRegisterCommand : ModCommand
    {
        public override string Command => "SetMod";

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            if (args[0].Length <= 0)
            {
                Main.NewText("How to use command: parameter 1 should be the mod's internal name. parameter 2 is optional and sets if the mod is on the Mod's Wiki or not as true or false");
            }
            else if (ModLoader.HasMod(args[0]))
            {
                ModName = args[0];
                Mod newMod = ModLoader.GetMod(args[0]);
                Main.NewText("Set desired mod to: " + newMod.DisplayName);
                if (args.Length > 1)
                {
                    string tralse = args[1];
                    if (tralse.ToLower() == "true")
                    {
                        if (!standaloneMods.Contains(newMod))
                        {
                            standaloneMods.Add(newMod);
                            Main.NewText(newMod.DisplayName + " now counts as a standalone mod", Color.Lime);
                        }
                        else
                        {
                            Main.NewText(newMod.DisplayName + " already counts as a standalone mod", Color.Red);
                        }
                    }
                    else if (tralse.ToLower() == "false")
                    {
                        if (standaloneMods.Contains(newMod))
                        {
                            standaloneMods.Remove(newMod);
                            Main.NewText(newMod.DisplayName + " no longer counts as a standalone mod", Color.Teal);
                        }
                        else
                        {
                            Main.NewText(newMod.DisplayName + " already does not count as a standalone mod", Color.Red);
                        }
                    }
                }
            }
            else
                Main.NewText("Error: Mod not found, are you sure you used the right internal name?", Color.Red);
        }

        public override CommandType Type => CommandType.World;
    }
}
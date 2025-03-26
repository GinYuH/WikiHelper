using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;

namespace WikiHelper
{
    public class Recipes
    {

        /// <summary>
        /// Generates a recipe page for the player's current held item if it's a crafting station.
        /// If an Ebonstone block is held, it generates Demon Altar recipes.
        /// If no valid block is held, it generates all "By Hand" recipes
        /// </summary>
        /// <param name="tileType"></param>
        /// <returns></returns>
        public static string GenerateAllRecipes(int tileType)
        {
            string ret = standaloneMods.Contains(CurMod) ? "{{recipes/register/note}}" : "{{mod sub-page}}{{recipes registration subpage}}\n";
            foreach (Recipe r in Main.recipe)
            {
                if (r.Mod == null)
                    continue;
                if (tileType <= -1 && r.requiredTile.Count > 0)
                    continue;
                if (r.requiredTile.Count > 0 && ((tileType > -1 && r.requiredTile[0] != tileType) || (tileType == TileID.Ebonstone && r.requiredTile[0] != TileID.DemonAltar)))
                    continue;
                if (r.Mod != CurMod)// && !r.requiredItem.Any((Item it) => it.ModItem != null && it.ModItem.Mod == CurMod))
                    continue;
                /*if (r.createItem.ModItem is StickyRogue || r.createItem.ModItem is BouncyRogue)
                    continue;*/

                string itemName = GetTaggedItemName(r.createItem);

                string station = r.requiredTile.Count > 0 ? GetTileItemName(r.requiredTile[0]) : "By Hand";

                if (r.Conditions.Contains(Condition.InGraveyard))
                    station += " and Ecto Mist";

                foreach (Condition c in r.Conditions)
                {
                    if (c == Condition.InGraveyard) continue;
                    station += " and " + c.Description;
                }

                if (station.Contains("By Hand"))
                    continue;

                ret +=
                    "\n{{recipes/register\n|result=" + GetTaggedItemName(r.createItem) + "|amount=" + r.createItem.stack + "\r\n|station=" + station;

                Dictionary<int, RecipeGroup> groups = RecipeGroup.recipeGroups;
                foreach (var v in r.requiredItem)
                {
                    bool isGroup = false;
                    foreach (var ve in r.acceptedGroups)
                    {
                        foreach (var g in groups)
                        {
                            if (g.Value.ContainsItem(v.type))
                            {
                                if (g.Key == ve)
                                {
                                    ret += "\n|" + g.Value.GetText.Invoke() + "|" + v.stack;
                                    isGroup = true;
                                }
                            }
                        }
                    }
                    if (!isGroup)
                        ret += "\n|" + GetTaggedItemName(v) + "|" + v.stack;
                }
                ret += "\n}}\n";
            }

            return ret;

        }
    }
}

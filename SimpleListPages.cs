using Terraria;
using System.Collections.Generic;
using Terraria.ID;
using static WikiHelper.WikiSystem;
using static WikiHelper.Utils;

namespace WikiHelper
{
    public class SimpleListPages
    {
        /// <summary>
        /// Generates the Items page for a given mod, listing every single item
        /// </summary>
        /// <returns></returns>
        public static string GenerateItemsPageLikeTheBIGOne()
        {
            string ret = "";
            List<Item> items = new List<Item>();
            for (int i = 0; i < ContentSamples.ItemsByType.Count; i++)
            {
                Item item = ContentSamples.ItemsByType[i];
                if (item?.ModItem?.Mod != CurMod)
                {
                    continue;
                }
                items.Add(item);
            }
            items.Sort((x, y) => x.Name.CompareTo(y.Name));
            for (int i = 0; i < items.Count; i++)
            {
                ret += "\n| {{item|" + GetTaggedItemName(items[i]) + "}}";
            }
            return ret;
        }
    }
}
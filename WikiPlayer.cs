using System;
using Terraria;
using Terraria.ModLoader;
using System.IO;
using System.Collections.Generic;
using System.Text;
using static WikiHelper.WikiSystem;

namespace WikiHelper
{
    public class WikiPlayer : ModPlayer
    {

        public override bool HoverSlot(Item[] inventory, int context, int slot)
        {
            return false;
        }
    }
}

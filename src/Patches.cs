using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using HarmonyLib;

namespace RadialMenuUtilities
{
    [HarmonyPatch(typeof(GameManager), nameof(GameManager.Update))]
    internal class ShowMenus
    {
        private static void Postfix()
        {
            RadialMenuManager.MaybeShowMenu();
        }
    }

    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.CanPlaceFromRadial))]
    internal class PlaceAnything
    {
        private static void Postfix(ref bool __result)
        {
            __result = true;
        }
    }
}

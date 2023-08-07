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

    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.CanPlaceFromRadial), new Type[] { typeof(GearItem) })]
    internal class PlaceAnything
    {
        private static void Postfix(GearItem gi, ref bool __result)
        {
            __result = true;
        }
    }
}

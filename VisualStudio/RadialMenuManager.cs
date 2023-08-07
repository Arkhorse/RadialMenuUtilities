namespace RadialMenuUtilities
{
    internal class RadialMenuManager
    {
#nullable disable
        private static List<CustomRadialMenu> radialMenuList = new List<CustomRadialMenu>();
        internal static string[] ConflictsWith { get; set; }
#nullable enable

        public RadialMenuManager() { }

        internal static bool ContainsKeyCode(KeyCode key)
        {
            foreach(CustomRadialMenu radialMenu in radialMenuList)
            {
                if(radialMenu.enabled && radialMenu.GetKeyCode() == key)
                {
                    return true;
                }
            }
            return false;
        }

        internal static bool ContainsConflict(CustomRadialMenu customRadialMenu)
        {
            foreach (CustomRadialMenu radialMenu in radialMenuList)
            {
                bool bothEnabled = radialMenu.enabled && customRadialMenu.enabled;
                bool sameKey = radialMenu.GetKeyCode() == customRadialMenu.GetKeyCode();
                bool sameObject = radialMenu == customRadialMenu;

                if (bothEnabled && sameKey && !sameObject)
                {
                    ConflictsWith.AddItem(customRadialMenu.ModName);
                    return true;
                }
            }
            return false;
        }
        
        internal static void AddToList(CustomRadialMenu customRadialMenu)
        {
            radialMenuList.Add(customRadialMenu);
        }

        internal static void MaybeShowMenu()
        {
            foreach (CustomRadialMenu radialMenu in radialMenuList)
            {
                if (radialMenu.enabled)
                {
                    KeyCode keyCode = radialMenu.GetKeyCode();
                    if (InputManager.GetKeyDown(InputManager.m_CurrentContext, keyCode))
                    {
                        if (CanShowRadialMenu())
                        {
                            InputManager.OpenRadialMenu();
                            radialMenu.ShowGearItems();
                        }
                    }
                }
            }   
        }

        public static bool CanShowRadialMenu()
        {
            return !GameManager.GetPlayerManagerComponent().IsInPlacementMode() && !IsOverlayActive();
        }
        public static bool IsOverlayActive()
        {
            return (InterfaceManager.IsOverlayActiveCached() && !InterfaceManager.GetPanel<Panel_ActionsRadial>().IsEnabled() || uConsole.IsOn());
        }
    }
}

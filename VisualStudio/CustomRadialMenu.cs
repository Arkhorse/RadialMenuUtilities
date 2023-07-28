namespace RadialMenuUtilities
{
    public enum CustomRadialMenuType
    {
        BestOfEach,
        WorstOfEach,
        AllOfEach
    }

    public class CustomRadialMenu
    {
        private string[] GearNames { get; }
        private KeyCode keycode;
        private CustomRadialMenuType menuType;
        internal bool enabled = true;

        public CustomRadialMenu(KeyCode keyCode, CustomRadialMenuType radialMenuType, string[] gearNames, bool enabled = true)
        {
            this.keycode = keyCode;
            this.menuType = radialMenuType;
            this.GearNames = gearNames;
            this.enabled = enabled;
            RadialMenuManager.AddToList(this);
        }

        internal void ShowGearItems()
        {
            Panel_ActionsRadial radial = InterfaceManager.GetPanel<Panel_ActionsRadial>();
            radial.m_Queue.Add(new System.Action(ShowGearItems));
            GearItem[] array = GetGearItems();
            radial.Enable(true, false);
            for (int j = 0; j < array.Length; j++)
            {
                if (j >= radial.m_RadialArms.Length - 1)
                {
                    break;
                }
                if (!(array[j] == null))
                {
                    Panel_ActionsRadial.RadialGearDelegate radialGearDelegate = new System.Action<GearItem>(radial.UseItem);
                    radial.AddRadialSelectionGear(Utils.GetInventoryIconTexture(array[j]).name, radialGearDelegate, array[j]);
                }
            }

            for (int k = 0; k < radial.m_RadialArms.Length; k++)
            {
                radial.m_RadialArms[k].SetHoverColor(false);
            }
        }

        public KeyCode GetKeyCode()
        {
            return keycode;
        }
        public void SetValues(KeyCode newKeyCode, bool enabled)
        {
            this.keycode = newKeyCode;
            this.enabled = enabled;
            if (RadialMenuManager.ContainsConflict(this))
            {
                Logger.LogWarning("KeyCode already registered to another menu!", Color.yellow);
            }
        }
        
        private GearItem[] GetGearItems()
        {
            switch (menuType)
            {
                case CustomRadialMenuType.BestOfEach:
                    return RadialUtils.GetBestGearItems(GearNames);
                case CustomRadialMenuType.WorstOfEach:
                    return RadialUtils.GetWorstGearItems(GearNames);
                case CustomRadialMenuType.AllOfEach:
                    return RadialUtils.GetAllGearItems(GearNames);
                default:
                    return null;
            };
        }
    }
}

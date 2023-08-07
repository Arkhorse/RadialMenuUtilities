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
#nullable disable
        /// <summary>
        /// The gear names for the GearItem's you want to show in your menu
        /// </summary>
        private string[] GearNames { get; }
        /// <summary>
        /// The KeyCode you want to use to open your menu
        /// </summary>
        private KeyCode m_Keycode { get; set; }
        /// <summary>
        /// The type of menu you want
        /// </summary>
        /// <value><c>BestOfEach</c>, <c>WorstOfEach</c>, <c>AllOfEach</c> (default)</value>
        private CustomRadialMenuType radialMenuType { get; set; }
        /// <summary>
        /// If you want it enabled. Useful if you want it to only be enabled in given circumstances
        /// </summary>
        internal bool enabled { get; set; } = true;
        /// <summary>
        /// Your modname. This is entirely used in logging
        /// </summary>
        internal string ModName { get; set; }
#nullable enable

        public CustomRadialMenu(string[] GearNames, KeyCode m_Keycode, CustomRadialMenuType radialMenuType, bool enabled = true, string ModName = "")
        {
            this.m_Keycode          = m_Keycode;
            this.radialMenuType     = radialMenuType;
            this.GearNames          = GearNames;
            this.enabled            = enabled;
            this.ModName            = ModName;

            Initialize();
        }

        internal void Initialize()
        {
            bool GearItemsExist = VerifyGearItems();
            if (!GearItemsExist)
            {
                Logger.LogFatalError($"Fatal Error: One of the requested GearItems does not exist for {this.ModName}. This will prevent adding of radial menu for this mod");
                return;
            }
            else RadialMenuManager.AddToList(this);
        }

        internal bool VerifyGearItems()
        {
            List<bool> bools = new();
            for (int i = 0; i < this.GearNames.Length; i++)
            {
                if (ItemUtilities.GetGearItemPrefab(GearNames[i])) bools.Add(true);
                else bools.Add(false);
            }
            if (bools.Contains(false)) return false;
            return true;
        }

        internal void ShowGearItems()
        {
            Panel_ActionsRadial radial = InterfaceManager.GetPanel<Panel_ActionsRadial>();

            radial.m_Queue.Add(new Action(ShowGearItems));
            GearItem[] array = GetGearItems();
            radial.Enable(true, false);

            for (int j = 0; j < array.Length; j++)
            {
                if (j >= radial.m_RadialArms.Length - 1) break;

                if (array[j] != null)
                {
                    Panel_ActionsRadial.RadialGearDelegate radialGearDelegate = new System.Action<GearItem>(radial.UseItem);
                    radial.AddRadialSelectionGear(radialGearDelegate, array[j]);
                }
            }

            for (int k = 0; k < radial.m_RadialArms.Length; k++)
            {
                radial.m_RadialArms[k].SetHoverColor(false);
            }
        }

        public KeyCode GetKeyCode()
        {
            return m_Keycode;
        }
        public void SetValues(KeyCode newKeyCode, bool enabled)
        {
            this.m_Keycode = newKeyCode;
            this.enabled = enabled;
            if (RadialMenuManager.ContainsConflict(this))
            {
                Logger.LogWarning($"KeyCode already registered to another menu! Conflict(s):", Color.yellow);
                for (int i = 0; i < RadialMenuManager.ConflictsWith.Length; i++)
                {
                    Logger.Log($"{RadialMenuManager.ConflictsWith}");
                }
            }
        }
        
        private GearItem[] GetGearItems()
        {
            return radialMenuType switch
            {
                CustomRadialMenuType.BestOfEach => RadialUtils.GetBestGearItems(GearNames),
                CustomRadialMenuType.WorstOfEach => RadialUtils.GetWorstGearItems(GearNames),
                CustomRadialMenuType.AllOfEach => RadialUtils.GetAllGearItems(GearNames),
                _ => RadialUtils.GetAllGearItems(GearNames)
            };
        }
    }
}

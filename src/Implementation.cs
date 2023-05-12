﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using UnityEngine;

namespace RadialMenuUtilities
{
    public class Implementation : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MelonLogger.Msg($"[{Info.Name}] Version {Info.Version} loaded!");
        }
        internal static void Log(string message)
        {
            MelonLogger.Msg(message);
        }

        internal static void Log(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            Log(preformattedMessage);
        }

        internal static void LogWarning(string message)
        {
            MelonLogger.Warning(message);
        }

        internal static void LogWarning(string message, params object[] parameters)
        {
            string preformattedMessage = string.Format(message, parameters);
            LogWarning(preformattedMessage);
        }
    }
}

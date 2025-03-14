namespace RadialMenuUtilities
{
    public class Logger
    {
        // Replace <Main> with your MelonMod class name
        // These are written to be accessable from static methods
        public static void Log(string message, params object[] parameters)              => Melon<Main>.Logger.Msg($"{message}", parameters);
        public static void LogWarning(string message, params object[] parameters)       => Melon<Main>.Logger.Warning($"{message}", parameters);
        public static void LogError(string message, params object[] parameters)         => Melon<Main>.Logger.Error($"{message}", parameters);
        public static void LogFatalError(string message)                                => Melon<Main>.Logger.Error($"{message}", Color.red);

        public static void LogSeperator(params object[] parameters)                     => Melon<Main>.Logger.Msg("==============================================================================", parameters);
        public static void LogStarter()                                                 => Melon<Main>.Logger.Msg($"Mod loaded with v{BuildInfo.Version}");
    }
}
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace HighTide.SupercompactedItems
{
    [BepInPlugin(ModGuid, ModName, ModVersion)]
    [BepInDependency("com.snmodding.nautilus")]
    public class Plugin : BaseUnityPlugin
    {
        private const string ModGuid = "com.hightide.subnautica.supercompacteditems";
        private const string ModName = "HighTide's Supercompacted Items";
        private const string ModVersion = "0.0.1";

        private static readonly Harmony Harmony = new Harmony(ModGuid);

        public static ManualLogSource Log;

        private void Awake()
        {
            Harmony.PatchAll();
            Logger.LogInfo(ModName + " " + ModVersion + " " + "loaded.");
            Log = Logger;

            CustomItems.RegisterCustomItems();
            CustomBuildables.RegisterCustomBuildables();
            CustomDatabankEntries.RegisterDatabankEntries();
        }

        public static void SendLog(string message, LogLevel type = LogLevel.Info)
        {
            switch (type)
            {
                case LogLevel.Info:
                    Log.LogInfo(message);
                    break;
                case LogLevel.Warning:
                    Log.LogWarning(message);
                    break;
                case LogLevel.Error:
                    Log.LogError(message);
                    break;
                case LogLevel.Debug:
                    Log.LogDebug(message);
                    break;
            }
        }

    }
}

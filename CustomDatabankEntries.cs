using Nautilus.Handlers;

namespace HighTide.SupercompactedItems
{
    internal class CustomDatabankEntries
    {
        public static void RegisterDatabankEntries()
        {
            Plugin.SendLog("Now attempting to register all custom databank entries...", BepInEx.Logging.LogLevel.Warning);

            PDAHandler.AddEncyclopediaEntry("HTSupercompactorEntry", "Tech/Habitats", "Supercompactor", "The Supercompactor is a modified fabricator created by Altera with the soul purpose of reducing space in a cost effective way.\n\nThe Supercompactor is a specially tuned fabricator set to hyper-compress raw materials into extremely condensed and pressurized forms while keeping its original mass.\n\n<b>Please remove all bodyparts from the devices vacinity while running to prevent any injuries</b>.");
            PDAHandler.AddCustomScannerEntry(SupercompactorBuildable.Info.TechType, encyclopediaKey: "HTSupercompactorEntry");

            Plugin.SendLog("All custom databank entries should've been registered!");
        }
    }
}

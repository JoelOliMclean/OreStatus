using BepInEx.Configuration;
using static OreStatus.OreStatus;

namespace OreStatus
{
    internal class OreStatusConfig
    {
        internal static ConfigEntry<bool> modEnabled;
        internal static ConfigEntry<DisplayType> displayType;

        internal static void SetupConfig(ConfigFile config)
        {
            modEnabled = config.Bind("General", "Mod Enabled", true, "Sets whether or not the mod is enabled");
            displayType = config.Bind("General", "Display Type", DisplayType.HealthBar, "Desired display type for ore health status");

            config.Save();
        }
    }
}

using HarmonyLib;
using BepInEx;

namespace OreStatus
{
    [BepInPlugin(GUID, MOD_NAME, VERSION)]
    public class OreStatus : BaseUnityPlugin
    {
        public const string GUID = "uk.co.jowleth.valheim.orestatus";
        public const string MOD_NAME = "Ore Status";
        public const string VERSION = "0.0.1";

        public enum DisplayType { HealthBar, Percentage, Disabled }

        public void Awake()
        {
            OreStatusConfig.SetupConfig(Config);
            if (OreStatusConfig.modEnabled.Value)
            {
                Harmony.CreateAndPatchAll(typeof(PatchMineRock5), null);
            }
        }
    }
}

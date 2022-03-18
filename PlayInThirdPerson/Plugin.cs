using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using PlayInThirdPerson.Installers;
using PlayInThirdPerson.UI;
using PlayInThirdPerson.Utilities;
using SiraUtil.Zenject;
using System.Reflection;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace PlayInThirdPerson
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        internal static Config Config { get; private set; }
        internal static IPALogger Logger { get; private set; }
        internal static Harmony HarmonyID { get; private set; } = null;

        [Init]
        public Plugin(IPALogger iLogger, IPAConfig iConfig, Zenjector zenjector)
        {
            Logger = iLogger;
            Config = iConfig.Generated<Config>();
            zenjector.Install<CameraMoverInstaller>(Location.Player);
        }

        [OnStart]
        public void OnStart()
        {
            ScoreSaberUtil.Define();
            if (HarmonyID is null) HarmonyID = new Harmony("bs.Exomanz.ThirdPerson");
            HarmonyID.PatchAll(Assembly.GetExecutingAssembly());
            GameplaySetup.instance.AddTab("Third Person", "PlayInThirdPerson.UI.SettingsUI.bsml", SettingsUI.instance);
        }

        [OnExit]
        public void OnExit()
        {
            GameplaySetup.instance.RemoveTab("Third Person");
            HarmonyID.UnpatchSelf();
            HarmonyID = null;
        }
    }
}

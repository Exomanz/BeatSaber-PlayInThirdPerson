using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using PlayInThirdPerson.UI;
using PlayInThirdPerson.Utilities;
using SiraUtil.Zenject;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;

namespace PlayInThirdPerson
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        internal static PluginConfig Config { get; private set; }
        internal static IPALogger Logger { get; private set; }
        internal static Harmony harmony { get; } = new Harmony("com.exomanz.beatsaber.thirdperson");

        [Init]
        public Plugin(IPALogger logger, IPAConfig config, Zenjector zenjector)
        {
            Logger = logger;
            Config = config.Generated<PluginConfig>();

            zenjector.Install(Location.Player, Container =>
            {
                bool fpfc = Environment.GetCommandLineArgs().Any(arg => arg.ToLower() == "fpfc");

                ScoreSaberUtil.UpdateIsInReplay();
                if (ScoreSaberUtil.IsInReplay() || fpfc || !Plugin.Config.Enabled)
                    return;

                GameObject cameraMover = new GameObject("PITP - CameraMover");
                Container.Bind<CameraMover>().FromNewComponentOn(cameraMover).AsSingle().NonLazy();
            });
        }

        [OnEnable]
        public void Enable()
        {
            ScoreSaberUtil.Define();

            GameplaySetup.instance.AddTab("Third Person", "PlayInThirdPerson.UI.SettingsUI.bsml", SettingsUI.instance);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }

        [OnDisable]
        public void Disable()
        {
            GameplaySetup.instance.RemoveTab("Third Person");
            harmony.UnpatchSelf();
        }
    }
}

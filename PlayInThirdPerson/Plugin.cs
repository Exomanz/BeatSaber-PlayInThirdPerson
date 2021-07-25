using BeatSaberMarkupLanguage.GameplaySetup;
using HarmonyLib;
using IPA;
using IPA.Config.Stores;
using IPAConfig = IPA.Config.Config;
using IPALogger = IPA.Logging.Logger;
using PlayInThirdPerson.Installers;
using PlayInThirdPerson.UI;
using SiraUtil.Zenject;
using System.Reflection;

namespace PlayInThirdPerson
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		internal static Config Config { get; private set; }
		internal static IPALogger Logger { get; private set; }
		internal static Harmony HarmonyID { get; private set; } = null;

	[Init] public Plugin(IPALogger iLogger, IPAConfig iConfig, Zenjector zenjector)
        {
		Logger = iLogger;
		Config = iConfig.Generated<Config>();
		zenjector.OnGame<CameraMoverInstaller>().ShortCircuitForMultiplayer().ShortCircuitForTutorial();
        }

	[OnEnable] public void Enable()
        {
		GameplaySetup.instance.AddTab("Third Person", "PlayInThirdPerson.UI.SettingsUI.bsml", SettingsUI.instance);
		if (HarmonyID is null) HarmonyID = new Harmony("bs.Exomanz.ThirdPerson");
		HarmonyID.PatchAll(Assembly.GetExecutingAssembly());
        }

	[OnDisable] public void Disable()
        {
		GameplaySetup.instance.RemoveTab("Third Person");
		HarmonyID.UnpatchAll(HarmonyID.Id);
		HarmonyID = null;
        }
    }
}

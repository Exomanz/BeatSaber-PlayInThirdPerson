using IPA.Loader;
using UnityEngine;
using Zenject;

namespace PlayInThirdPerson.Installers
{
    internal class CameraMoverInstaller : Installer<CameraMoverInstaller>
    {
        public override void InstallBindings()
        {
            bool scoreSaber;

            PluginMetadata data = PluginManager.GetPluginFromId("ScoreSaber");
            if (data is null) scoreSaber = false;
            else
            {
                scoreSaber = true;
                GameObject.Find("ReplayPlayer");
            }

            GameObject cameraMover = new GameObject("CameraMover");
            Container.Bind<CameraMover>().FromNewComponentOn(cameraMover).AsSingle().NonLazy();
        }
    }
}

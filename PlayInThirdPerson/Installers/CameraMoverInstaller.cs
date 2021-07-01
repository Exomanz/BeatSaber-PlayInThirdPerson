using PlayInThirdPerson.Utilities;
using UnityEngine;
using UnityEngine.XR;
using Zenject;

namespace PlayInThirdPerson.Installers
{
    internal class CameraMoverInstaller : Installer<CameraMoverInstaller>
    {
        public override void InstallBindings()
        {
            ScoreSaberUtil.UpdateIsInReplay();
            if (ScoreSaberUtil.IsInReplay() || !XRDevice.isPresent) return;

            GameObject cameraMover = new GameObject("CameraMover");
            Container.Bind<CameraMover>().FromNewComponentOn(cameraMover).AsSingle().NonLazy();
        }
    }
}

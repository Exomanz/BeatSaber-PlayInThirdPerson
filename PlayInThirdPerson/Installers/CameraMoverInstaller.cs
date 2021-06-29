using UnityEngine;
using Zenject;

namespace PlayInThirdPerson.Installers
{
    internal class CameraMoverInstaller : Installer<CameraMoverInstaller>
    {
        public override void InstallBindings()
        {
            GameObject cameraMover = new GameObject("CameraMover");
            Container.Bind<CameraMover>().FromNewComponentOn(cameraMover).AsSingle().NonLazy();
        }
    }
}

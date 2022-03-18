using PlayInThirdPerson.Utilities;
using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace PlayInThirdPerson.Installers
{
    internal class CameraMoverInstaller : Installer<CameraMoverInstaller>
    {
        public override void InstallBindings()
        {
            bool fpfc = Environment.GetCommandLineArgs().Any(x => x?.ToLower() == "fpfc");

            ScoreSaberUtil.UpdateIsInReplay();
            if (ScoreSaberUtil.IsInReplay() || fpfc || !Plugin.Config.Enabled) return;

            GameObject cameraMover = new GameObject("CameraMover");
            Container.Bind<CameraMover>().FromNewComponentOn(cameraMover).AsSingle().NonLazy();
        }
    }
}

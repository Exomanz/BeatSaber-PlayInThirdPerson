using UnityEngine;
using Zenject;

namespace PlayInThirdPerson.Utilities
{
    internal class CameraMover : MonoBehaviour
    {
#pragma warning disable CS0649
        [Inject] protected MainCamera _mainCamera;
#pragma warning restore CS0649

        private void Start()
        {
            Transform mainCam = _mainCamera.transform;
            Transform offset = transform;

            offset.SetParent(mainCam.parent, false);
            mainCam.SetParent(transform, true);
        }

        private void LateUpdate()
        {
            if (Plugin.Config.Enabled) transform.localPosition = Plugin.Config.Offset;
            else transform.localPosition = Vector3.zero;
        }
    }
}

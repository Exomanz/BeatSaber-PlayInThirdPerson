using UnityEngine;
using Zenject;

namespace PlayInThirdPerson.Utilities
{
    internal class CameraMover : MonoBehaviour
    {
        [Inject] protected readonly MainCamera _mainCamera;

        public void Start()
        {
            this.transform.SetParent(_mainCamera.transform.parent, false);
            _mainCamera.transform.SetParent(this.transform, true);
        }

        public void LateUpdate()
        {
            if (Plugin.Config.Enabled) transform.localPosition = Plugin.Config.Offset;
            else transform.localPosition = Vector3.zero;
        }
    }
}

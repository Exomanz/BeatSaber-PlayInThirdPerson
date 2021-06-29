using BeatSaberMarkupLanguage.Attributes;
using UnityEngine;

namespace PlayInThirdPerson.UI
{
	internal class SettingsUI : PersistentSingleton<SettingsUI>
	{
		Config _config => Plugin.Config;

		[UIValue("boolEnable")]
		protected bool Enabled
        {
			get => _config.Enabled;
			set => _config.Enabled = value;
        }

		[UIValue("cameraOffsetY")]
		protected float CameraOffsetY
        {
			get => _config.Offset.y;
			set => _config.Offset = new Vector3(0f, value, CameraOffsetZ);
        }

		[UIValue("cameraOffsetZ")]
		protected float CameraOffsetZ
        {
			get => _config.Offset.z;
			set => _config.Offset = new Vector3(0f, CameraOffsetY, value);
        }
	}
}

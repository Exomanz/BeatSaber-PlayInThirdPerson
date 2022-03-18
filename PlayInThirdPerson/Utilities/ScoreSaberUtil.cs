#nullable enable
using System.Reflection;
using UnityEngine;

namespace PlayInThirdPerson.Utilities
{
    /// <summary>
    /// Code taken from Kinsi55's Camera2 Plugin. Needed a way to get ScoreSaber Replay Status without depending on ScoreSaber or Cam2.
    /// https://github.com/Kinsi55/CS_BeatSaber_Camera2
    /// </summary>
    internal static class ScoreSaberUtil
    {
        private static MethodBase? ScoreSaber_playbackEnabled;

        private static bool isInReplay;
        public static Camera? replayCam;

        public static void Define()
        {
            ScoreSaber_playbackEnabled = IPA.Loader.PluginManager.GetPlugin("ScoreSaber")?
                .Assembly.GetType("ScoreSaber.Core.ReplaySystem.HarmonyPatches.PatchHandleHMDUnmounted")
                .GetMethod("Prefix", BindingFlags.Static | BindingFlags.NonPublic);
        }

        public static bool IsInReplay()
        {
            try
            {
                return ScoreSaber_playbackEnabled != null && (bool)ScoreSaber_playbackEnabled.Invoke(null, null) == false;
            }
            catch { }
            return false;
        }

        public static void UpdateIsInReplay()
        {
            isInReplay = IsInReplay();
            replayCam = !isInReplay ? null : GameObject.Find("LocalPlayerGameCore/Recorder/RecorderCamera")?.GetComponent<Camera>();

            if (replayCam != null)
            {
                var x = GameObject.Find("RecorderCamera(Clone)")?.GetComponent<Camera>();

                if (x != null)
                {
                    replayCam.tag = "Untagged";

                    if (!UnityEngine.XR.XRDevice.isPresent)
                        x.enabled = false;

                    x.tag = "MainCamera";
                }
            }
        }
    }
}

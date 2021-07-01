using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace PlayInThirdPerson.Utilities
{
    /// <summary>
    /// Code taken from Kinsi55's Camera2 Plugin. Needed a way to get ScoreSaber Replay Status without depending on ScoreSaber or Cam2.
    /// </summary>
    internal static class ScoreSaberUtil
    {
        static MethodBase ScoreSaber_playbackEnabled = AccessTools.Method("ScoreSaber.Core.ReplaySystem.HarmonyPatches.PatchHandleHMDUnmounted:Prefix");

        public static bool isInReplay { get; internal set; }
        public static Camera replayCam { get; private set; }

        public static bool IsInReplay()
        {
            return ScoreSaber_playbackEnabled != null && (bool)ScoreSaber_playbackEnabled.Invoke(null, null) == false;
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

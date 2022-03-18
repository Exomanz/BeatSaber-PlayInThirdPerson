using HarmonyLib;
using IPA.Utilities;
using UnityEngine;

namespace PlayInThirdPerson.Harmony_Patches
{
    [HarmonyPatch(typeof(PlayerTransforms), "Update")]
    internal class PlayerTransformsUpdate
    {
        private static void Postfix(PlayerTransforms __instance)
        {
            if (Plugin.Config.Enabled)
            {
                Vector3 headPos = __instance.headPseudoLocalPos - Plugin.Config.Offset;
                ReflectionUtil.SetField(__instance, "_headPseudoLocalPos", headPos);
            }
        }
    }
}

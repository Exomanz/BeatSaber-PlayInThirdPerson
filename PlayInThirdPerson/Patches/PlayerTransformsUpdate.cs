using HarmonyLib;
using IPA.Utilities;
using UnityEngine;

namespace PlayInThirdPerson.Patches
{
    [HarmonyPatch(typeof(PlayerTransforms), "Update")]
    internal class PlayerTransformsUpdate
    {
        internal static readonly FieldAccessor<PlayerTransforms, Vector3>.Accessor PsuedoPosAccessor = FieldAccessor<PlayerTransforms, Vector3>.GetAccessor("_headPseudoLocalPos");

        internal static void Postfix(PlayerTransforms __instance)
        {
            if (Plugin.Config.Enabled)
            {
                Vector3 newHeadPos = __instance.headPseudoLocalPos - Plugin.Config.Offset;
                PsuedoPosAccessor(ref __instance);
            }
        }
    }
}

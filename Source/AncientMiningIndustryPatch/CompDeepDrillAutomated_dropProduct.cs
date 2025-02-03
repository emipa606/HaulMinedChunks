using AncientMining;
using HarmonyLib;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDeepDrillAutomated), nameof(CompDeepDrillAutomated.dropProduct))]
public static class CompDeepDrillAutomated_dropProduct
{
    public static bool Spawning;

    public static void Prefix()
    {
        Spawning = true;
    }

    public static void Postfix()
    {
        Spawning = false;
    }
}
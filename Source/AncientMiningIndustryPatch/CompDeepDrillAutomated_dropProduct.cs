using AncientMining;
using HarmonyLib;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDeepDrillAutomated), nameof(CompDeepDrillAutomated.dropProduct))]
public static class CompDeepDrillAutomated_dropProduct
{
    public static void Prefix()
    {
        Mineable_TrySpawnYield.Spawning = true;
    }

    public static void Postfix()
    {
        Mineable_TrySpawnYield.Spawning = false;
    }
}
using AlphaBiomes;
using HarmonyLib;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompCoreSampleDrill), "TryProducePortion")]
public static class CompCoreSampleDrill_TryProducePortion
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
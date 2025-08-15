using HarmonyLib;
using ProjectRimFactory.Industry;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Building_DeepQuarry), "GenerateChunk")]
public static class Building_DeepQuarry_GenerateChunk
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
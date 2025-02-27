using HarmonyLib;
using ProjectRimFactory.Industry;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Building_DeepQuarry), nameof(Building_DeepQuarry.GenerateChunk))]
public static class Building_DeepQuarry_GenerateChunk
{
    public static void Prefix()
    {
        ProjectRimFactoryRevivedPatch.SpawningChunk = true;
    }

    public static void Postfix()
    {
        ProjectRimFactoryRevivedPatch.SpawningChunk = false;
    }
}
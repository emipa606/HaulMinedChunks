using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(GenPlace), "SplitAndSpawnOneStackOnCell")]
public static class GenPlace_SplitAndSpawnOneStackOnCell
{
    public static void Postfix(Thing resultingThing, bool __result)
    {
        if (!__result)
        {
            return;
        }

        if (!Mineable_TrySpawnYield.Spawning)
        {
            return;
        }

        if (resultingThing?.Map == null
            || resultingThing.def.thingCategories?.Intersect(HaulMinedChunks.ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(resultingThing.Position, resultingThing.Map))
        {
            resultingThing.Map.designationManager.AddDesignation(new Designation(resultingThing,
                DesignationDefOf.Haul));
        }
    }
}
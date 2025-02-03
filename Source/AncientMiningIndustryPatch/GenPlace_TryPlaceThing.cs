using System;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(GenPlace), nameof(GenPlace.TryPlaceThing), typeof(Thing), typeof(IntVec3), typeof(Map),
    typeof(ThingPlaceMode), typeof(Action<Thing, int>), typeof(Predicate<IntVec3>), typeof(Rot4))]
public static class GenPlace_TryPlaceThing
{
    public static void Postfix(Thing thing)
    {
        if (!CompDeepDrillAutomated_dropProduct.Spawning)
        {
            return;
        }

        if (thing?.Map == null
            || thing.def.thingCategories?.Intersect(HaulMinedChunks.ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(thing.Position, thing.Map))
        {
            thing.Map.designationManager.AddDesignation(new Designation(thing, DesignationDefOf.Haul));
        }
    }
}
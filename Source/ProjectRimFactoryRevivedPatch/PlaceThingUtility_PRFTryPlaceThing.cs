using System.Linq;
using HarmonyLib;
using ProjectRimFactory;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(PlaceThingUtility), nameof(PlaceThingUtility.PRFTryPlaceThing))]
public static class PlaceThingUtility_PRFTryPlaceThing
{
    public static void Postfix(Thing t)
    {
        if (t?.Map == null
            || t.def.thingCategories?.Intersect(HaulMinedChunks.ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(t.Position, t.Map))
        {
            t.Map.designationManager.AddDesignation(new Designation(t, DesignationDefOf.Haul));
        }
    }
}
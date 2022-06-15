using HarmonyLib;
using RaiseTheRoof;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(RTRUtils), "RemoveRoof")]
internal class RTRUtils_RemoveRoof
{
    private static void Postfix(IntVec3 cell, Map map)
    {
        var possibleChunk = cell.GetFirstHaulable(map);
        if (possibleChunk == null ||
            possibleChunk.def.thingCategories?.Contains(ThingCategoryDefOf.Chunks) == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(possibleChunk.Position, map))
        {
            map.designationManager.AddDesignation(new Designation(possibleChunk, DesignationDefOf.Haul));
        }
    }
}
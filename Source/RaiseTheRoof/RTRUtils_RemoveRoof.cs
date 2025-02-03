using System.Linq;
using HarmonyLib;
using RaiseTheRoof;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(RTRUtils), nameof(RTRUtils.RemoveRoof))]
internal class RTRUtils_RemoveRoof
{
    public static void Postfix(IntVec3 cell, Map map)
    {
        var possibleChunk = cell.GetFirstHaulable(map);
        if (possibleChunk == null ||
            possibleChunk.def.thingCategories?.Cross(HaulMinedChunks.ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(possibleChunk.Position, map))
        {
            map.designationManager.AddDesignation(new Designation(possibleChunk, DesignationDefOf.Haul));
        }
    }
}
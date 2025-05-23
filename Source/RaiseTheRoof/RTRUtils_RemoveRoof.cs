using HarmonyLib;
using RaiseTheRoof;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(RTRUtils), nameof(RTRUtils.RemoveRoof))]
internal class RTRUtils_RemoveRoof
{
    public static void Postfix(IntVec3 cell, Map map)
    {
        var possibleChunk = cell.GetFirstHaulable(map);
        if (possibleChunk == null)
        {
            return;
        }

        HaulMinedChunks.MarkIfNeeded(possibleChunk);
    }
}
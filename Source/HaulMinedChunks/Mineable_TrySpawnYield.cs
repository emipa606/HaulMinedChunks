using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Mineable), "TrySpawnYield", typeof(Map), typeof(bool), typeof(Pawn))]
internal class Mineable_TrySpawnYield
{
    private static void Postfix(Mineable __instance, Map map, Pawn pawn)
    {
        if (pawn == null || !pawn.IsColonist && !pawn.IsColonyMech && !pawn.IsPrisoner)
        {
            return;
        }

        var possibleChunk = __instance.Position.GetFirstHaulable(map);
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
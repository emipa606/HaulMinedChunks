using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Mineable), "TrySpawnYield")]
internal class Mineable_TrySpawnYield
{
    private static void Postfix(Mineable __instance, Map map, Pawn pawn)
    {
        if (pawn?.IsColonist == false && pawn.def.defName != "PRFDrone")
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
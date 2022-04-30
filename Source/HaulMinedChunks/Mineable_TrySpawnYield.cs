using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Mineable), "TrySpawnYield")]
internal class Mineable_TrySpawnYield
{
    private static void Postfix(Mineable __instance, Map map, Pawn pawn)
    {
        if (pawn?.IsColonist == false)
        {
            return;
        }

        var possibleChunk = __instance.Position.GetFirstHaulable(map);
        if (possibleChunk == null ||
            possibleChunk.def.thingCategories?.Contains(ThingCategoryDefOf.StoneChunks) == false)
        {
            return;
        }

        map.designationManager.AddDesignation(new Designation(possibleChunk, DesignationDefOf.Haul));
    }
}
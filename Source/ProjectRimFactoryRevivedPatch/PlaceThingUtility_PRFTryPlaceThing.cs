using HarmonyLib;
using ProjectRimFactory;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(PlaceThingUtility), nameof(PlaceThingUtility.PRFTryPlaceThing))]
public static class PlaceThingUtility_PRFTryPlaceThing
{
    public static void Postfix(Thing t)
    {
        if (!Mineable_TrySpawnYield.Spawning)
        {
            return;
        }

        HaulMinedChunks.MarkIfNeeded(t);
    }
}
using System;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(GenPlace), "SplitAndSpawnOneStackOnCell")]
public static class GenPlace_SplitAndSpawnOneStackOnCell
{
    public static void Prefix(ref Action<Thing, int> placedAction)
    {
        if (!Mineable_TrySpawnYield.Spawning)
        {
            return;
        }

        var wrappedPlacedAction = placedAction;
        placedAction = (thing, stackCount) =>
        {
            wrappedPlacedAction?.Invoke(thing, stackCount);
            HaulMinedChunks.MarkIfNeeded(thing);
        };
    }
}

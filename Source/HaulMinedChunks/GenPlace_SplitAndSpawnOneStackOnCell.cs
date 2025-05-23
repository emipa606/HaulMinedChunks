using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(GenPlace), "SplitAndSpawnOneStackOnCell")]
public static class GenPlace_SplitAndSpawnOneStackOnCell
{
    public static void Postfix(Thing resultingThing, bool __result)
    {
        if (!__result)
        {
            return;
        }

        if (!Mineable_TrySpawnYield.Spawning)
        {
            return;
        }

        HaulMinedChunks.MarkIfNeeded(resultingThing);
    }
}
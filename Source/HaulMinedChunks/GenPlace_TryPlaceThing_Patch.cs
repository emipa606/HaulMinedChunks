using HarmonyLib;
using Verse;
using RimWorld;
using System;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(GenPlace), nameof(GenPlace.TryPlaceThing), new Type[] { typeof(Thing), typeof(IntVec3), typeof(Map), typeof(ThingPlaceMode), typeof(Action<Thing, int>), typeof(Predicate<IntVec3>), typeof(Rot4), typeof(int) })]
public static class GenPlace_TryPlaceThing_Patch
{
    public static void Postfix(Thing thing, bool __result)
    {
        if (!__result || thing == null)
        {
            return;
        }
        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
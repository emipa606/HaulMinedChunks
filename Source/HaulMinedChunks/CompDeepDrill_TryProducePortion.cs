using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDeepDrill), "TryProducePortion")]
internal class CompDeepDrill_TryProducePortion
{
    private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var makeThingMethod = AccessTools.Method(typeof(ThingMaker), nameof(ThingMaker.MakeThing),
            [typeof(ThingDef), typeof(ThingDef)]);
        var tryPlaceThingMethod = AccessTools.Method(typeof(GenPlace), nameof(GenPlace.TryPlaceThing),
        [
            typeof(Thing), typeof(IntVec3), typeof(Map), typeof(ThingPlaceMode), typeof(Action<Thing, int>),
            typeof(Predicate<IntVec3>), typeof(Rot4)
        ]);
        var markToHaulMethod = AccessTools.Method(typeof(CompDeepDrill_TryProducePortion), nameof(TryMarkToHaul));

        return new CodeMatcher(instructions)
            .MatchStartForward(
                new CodeMatch(OpCodes.Call, makeThingMethod))
            .ThrowIfInvalid("MakeThing location not found.")
            .Advance(1)
            .Insert(new CodeInstruction(OpCodes.Dup)) //Insert another reference to `thing` to use later.
            .MatchStartForward(
                new CodeMatch(OpCodes.Call, tryPlaceThingMethod),
                new CodeMatch(OpCodes.Pop))
            .ThrowIfInvalid("TryPlaceThing location not found.")
            .Advance(1)
            .SetAndAdvance(OpCodes.Call,
                markToHaulMethod) // Replace the pop with a call to our method, so we pass the bool from TryPlaceThing as a parameter, using the duped `thing` from earlier.
            .InstructionEnumeration();
    }

    public static void TryMarkToHaul(Thing thing, bool created)
    {
        if (!created)
        {
            return;
        }

        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
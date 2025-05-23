using System.Collections.Generic;
using System.Reflection.Emit;
using AnimalBehaviours;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDigPeriodically), nameof(CompDigPeriodically.CompTick))]
public static class CompDigPeriodically_CompTick
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        return new CodeMatcher(instructions)
            .MatchStartForward(
                new CodeMatch(OpCodes.Ldloc_S), // Ld `thing`
                new CodeMatch(OpCodes.Ldloc_S), // Ld `digIfRocksOrBricks`
                new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(Thing), nameof(Thing.stackCount))))
            .DuplicateInstructionAndAdvance() // Duplicate the Ld `thing`
            .Insert(CodeInstruction.Call(typeof(CompDigPeriodically_CompTick), nameof(MarkToHaulIfHaulable)))
            .InstructionEnumeration();
    }

    public static void MarkToHaulIfHaulable(Thing thing)
    {
        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
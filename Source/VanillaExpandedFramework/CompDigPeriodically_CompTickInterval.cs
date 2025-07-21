using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;
using VEF.AnimalBehaviours;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDigPeriodically), nameof(CompDigPeriodically.CompTickInterval))]
public static class CompDigPeriodically_CompTickInterval
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        CodeMatcher matcher = new CodeMatcher(instructions)
            .MatchBack(
                new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(Thing), nameof(Thing.stackCount)))
            )
            .MatchBack(
                new CodeMatch(i => i.IsLdloc())
            );

        if (!matcher.Found)
        {
            return instructions;
        }

        matcher.DuplicateInstructionAndAdvance();
        matcher.Insert(CodeInstruction.Call(typeof(CompDigPeriodically_CompTickInterval), nameof(MarkToHaulIfHaulable)));

        return matcher.InstructionEnumeration();
    }

    public static void MarkToHaulIfHaulable(Thing thing)
    {
        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
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
        var matcher = new CodeMatcher(instructions)
            .MatchStartForward(
                new CodeMatch(OpCodes.Stfld,
                    AccessTools.Field(typeof(Thing), nameof(Thing.stackCount)))); // Locate the stackCount assignment

        if (matcher.Pos < 0) // Check if the match was unsuccessful
        {
            Log.Error(
                "[HaulMinedChunks]: Could not find stackCount assignment in CompDigPeriodically.CompTickInterval transpiler.");
            return instructions; // Return original instructions if no match is found
        }

        // Ensure the Thing reference is loaded before the stackCount assignment
        matcher.MatchStartBackwards(new CodeMatch(OpCodes.Ldloc_S)); // Match the local variable load instruction
        if (matcher.Pos < 0) // Check if the backward match was unsuccessful
        {
            Log.Error(
                "[HaulMinedChunks]: Could not find Thing reference in CompDigPeriodically.CompTickInterval transpiler.");
            return instructions; // Return original instructions if no match is found
        }

        // Duplicate the Thing reference and insert the call to MarkToHaulIfHaulable
        matcher.InsertAndAdvance(new CodeInstruction(OpCodes.Dup)); // Duplicate the Thing reference
        matcher.InsertAndAdvance(CodeInstruction.Call(typeof(CompDigPeriodically_CompTickInterval),
            nameof(MarkToHaulIfHaulable)));

        return matcher.InstructionEnumeration();
    }

    public static void MarkToHaulIfHaulable(Thing thing)
    {
        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
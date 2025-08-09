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

        // Search backward to find the instruction that loads the Thing reference
        matcher.MatchStartBackwards(new CodeMatch(i =>
            i.opcode == OpCodes.Ldloc || i.opcode == OpCodes.Ldarg || i.opcode == OpCodes.Ldfld));
        if (matcher.Pos < 0) // Check if the backward match was unsuccessful
        {
            Log.Error(
                "[HaulMinedChunks]: Could not find Thing reference in CompDigPeriodically.CompTickInterval transpiler.");
            return instructions; // Return original instructions if no match is found
        }

        // Insert the call to MarkToHaulIfHaulable after the stackCount assignment
        matcher.Advance(1); // Move to the position after the stfld instruction
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
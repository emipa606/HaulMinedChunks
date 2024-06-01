using AnimalBehaviours;
using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch]
public static class CompDigPeriodically_Patches
{
    [HarmonyPatch(typeof(CompDigPeriodically), nameof(CompDigPeriodically.CompTick))]
    [HarmonyTranspiler]
    public static IEnumerable<CodeInstruction> CompDigPeriodically_CompTick_Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        return new CodeMatcher(instructions)
            .MatchStartForward(
                new CodeMatch(OpCodes.Ldloc_S), // Ld `thing`
                new CodeMatch(OpCodes.Ldloc_S), // Ld `digIfRocksOrBricks`
                new CodeMatch(OpCodes.Stfld, AccessTools.Field(typeof(Thing), nameof(Thing.stackCount))))
            .DuplicateInstructionAndAdvance() // Duplicate the Ld `thing`
            .Insert(CodeInstruction.Call(typeof(CompDigPeriodically_Patches), nameof(MarkToHaulIfHaulable)))
            .InstructionEnumeration();
    }

    public static void MarkToHaulIfHaulable(Thing thing)
    {
        if (thing == null
            || thing.Map == null
            || thing.def.thingCategories?.Cross(HaulMinedChunks.ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunks.ShouldMarkChunk(thing.Position, thing.Map))
        {
            thing.Map.designationManager.AddDesignation(new Designation(thing, DesignationDefOf.Haul));
        }
    }
}

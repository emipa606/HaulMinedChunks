using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using VEF.AnimalBehaviours;
using Verse;
// Added for FieldInfo

namespace HaulMinedChunks;

[HarmonyPatch(typeof(CompDigPeriodically), nameof(CompDigPeriodically.CompTickInterval))]
public static class CompDigPeriodically_CompTickInterval
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
    {
        var codes = new List<CodeInstruction>(instructions);
        // Find the stfld for stackCount and insert after it
        for (var i = 0; i < codes.Count; i++)
        {
            var code = codes[i];
            if (code.opcode != OpCodes.Stfld ||
                code.operand is not FieldInfo { Name: nameof(Thing.stackCount) })
            {
                continue;
            }

            // V_4 is the Thing local (see IL, always index 4)
            codes.Insert(i + 1, new CodeInstruction(OpCodes.Ldloc_S, (byte)4));
            codes.Insert(i + 2,
                CodeInstruction.Call(typeof(CompDigPeriodically_CompTickInterval), nameof(MarkToHaulIfHaulable)));
            break;
        }

        return codes;
    }

    public static void MarkToHaulIfHaulable(Thing thing)
    {
        HaulMinedChunks.MarkIfNeeded(thing);
    }
}
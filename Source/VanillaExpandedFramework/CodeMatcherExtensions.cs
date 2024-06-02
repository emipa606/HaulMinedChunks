using HarmonyLib;

namespace HaulMinedChunks;

public static class CodeMatcherExtensions
{
    public static CodeMatcher DuplicateInstruction(this CodeMatcher cm, int offset = 0)
    {
        return cm.Insert(cm.InstructionAt(offset));
    }

    public static CodeMatcher DuplicateInstructionAndAdvance(this CodeMatcher cm, int offset = 0)
    {
        return cm.InsertAndAdvance(cm.InstructionAt(offset));
    }
}
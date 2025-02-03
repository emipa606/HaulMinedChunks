using HarmonyLib;

namespace HaulMinedChunks;

public static class CodeMatcherExtensions
{
    public static CodeMatcher DuplicateInstructionAndAdvance(this CodeMatcher cm, int offset = 0)
    {
        return cm.InsertAndAdvance(cm.InstructionAt(offset));
    }
}
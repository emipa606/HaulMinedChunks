using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class HaulMinedChunks
{
    static HaulMinedChunks()
    {
        new Harmony("Mlie.HaulMinedChunks").PatchAll();
    }
}
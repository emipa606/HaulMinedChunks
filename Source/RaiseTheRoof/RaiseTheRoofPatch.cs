using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public static class RaiseTheRoofPatch
{
    static RaiseTheRoofPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.RaiseTheRoofPatch").PatchAll();
        Log.Message("[HaulMinedChunks]: Adding compatibility with Raise The Roof");
    }
}
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class VanillaExpandedFrameworkPatch
{
    static VanillaExpandedFrameworkPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.VanillaExpandedFrameworkPatch").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message("[HaulMinedChunks]: Adding compatibility with Vanilla Expanded Framework");
    }
}
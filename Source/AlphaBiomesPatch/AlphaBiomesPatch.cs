using System.Reflection;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class AlphaBiomesPatch
{
    static AlphaBiomesPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.AlphaBiomesPatch").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message("[HaulMinedChunks]: Adding compatibility with Alpha Biomes");
    }
}
using System.Reflection;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class AncientMiningIndustryPatch
{
    static AncientMiningIndustryPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.AncientMiningIndustryPatch").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message("[HaulMinedChunks]: Adding compatibility with Ancient Mining Industry");
    }
}
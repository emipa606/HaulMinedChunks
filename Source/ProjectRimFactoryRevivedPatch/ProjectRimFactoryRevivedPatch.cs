using System.Reflection;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public static class ProjectRimFactoryRevivedPatch
{
    static ProjectRimFactoryRevivedPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.ProjectRimFactoryRevivedPatch").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message("[HaulMinedChunks]: Adding compatibility with Project RimFactory Revived");
    }
}
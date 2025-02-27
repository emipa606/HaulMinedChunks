using System.Reflection;
using HarmonyLib;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public static class ProjectRimFactoryRevivedPatch
{
    public static bool SpawningChunk;

    static ProjectRimFactoryRevivedPatch()
    {
        new Harmony("Mlie.HaulMinedChunks.ProjectRimFactoryRevivedPatch").PatchAll(Assembly.GetExecutingAssembly());
        Log.Message("[HaulMinedChunks]: Adding compatibility with Project RimFactory Revived");
    }
}
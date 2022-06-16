using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class HaulMinedChunks
{
    public static readonly List<ThingCategoryDef> ChunkCategoryDefs;

    static HaulMinedChunks()
    {
        new Harmony("Mlie.HaulMinedChunks").PatchAll();
        ChunkCategoryDefs = ThingCategoryDefOf.Chunks.ThisAndChildCategoryDefs.ToList();
    }

    public static bool ShouldMarkChunk(IntVec3 position, Map map)
    {
        if (!HaulMinedChunksMod.instance.Settings.LimitToCustomArea &&
            !HaulMinedChunksMod.instance.Settings.LimitToHomeArea)
        {
            return true;
        }

        if (HaulMinedChunksMod.instance.Settings.LimitToHomeArea && map.areaManager.Home[position])
        {
            return true;
        }

        if (!HaulMinedChunksMod.instance.Settings.LimitToCustomArea)
        {
            return false;
        }

        var customArea = map.areaManager.GetLabeled(HaulMinedChunksMod.instance.Settings.CustomAreaName);
        return customArea != null && customArea[position];
    }
}
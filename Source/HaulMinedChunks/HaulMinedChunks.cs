using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
public class HaulMinedChunks
{
    public static readonly List<ThingCategoryDef> ChunkCategoryDefs;
    public static readonly bool Insects2Loaded;
    public static readonly bool ArtificialBeingsFrameworkLoaded;
    public static readonly MethodInfo IsArtificialMethodInfo;

    static HaulMinedChunks()
    {
        Insects2Loaded = ModsConfig.IsActive("OskarPotocki.VFE.Insectoid2");
        ArtificialBeingsFrameworkLoaded = ModsConfig.IsActive("Killathon.ArtificialBeings");
        if (ArtificialBeingsFrameworkLoaded)
        {
            IsArtificialMethodInfo = AccessTools.Method("ArtificialBeings.ABF_Utils:IsArtificial");
        }

        new Harmony("Mlie.HaulMinedChunks").PatchAll();
        ChunkCategoryDefs = ThingCategoryDefOf.Chunks.ThisAndChildCategoryDefs.ToList();
    }

    public static void MarkIfNeeded(Thing thing)
    {
        var map = thing.MapHeld;
        var position = thing.PositionHeld;

        if (map == null || position == IntVec3.Invalid)
        {
            return;
        }

        if (thing.def.thingCategories?.Intersect(ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (HaulMinedChunksMod.instance.Settings.LimitToHomeArea && !map.areaManager.Home[position])
        {
            return;
        }

        if (HaulMinedChunksMod.instance.Settings.LimitToCustomArea)
        {
            var customArea = map.areaManager.GetLabeled(HaulMinedChunksMod.instance.Settings.CustomAreaName);
            if (customArea == null || !customArea[position])
            {
                return;
            }
        }

        if (map.designationManager.HasMapDesignationOn(thing))
        {
            return;
        }

        map.designationManager.AddDesignation(new Designation(thing, DesignationDefOf.Haul));
    }
}
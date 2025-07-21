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
    private static readonly List<ThingCategoryDef> ChunkCategoryDefs;
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

        new Harmony("Mlie.HaulMinedChunks").PatchAll(Assembly.GetExecutingAssembly());

        ChunkCategoryDefs = ThingCategoryDefOf.Chunks.ThisAndChildCategoryDefs.ToList();
    }

    public static void MarkIfNeeded(Thing thing)
    {
        var map = thing.MapHeld;
        var position = thing.PositionHeld;

        if (map == null || position == IntVec3.Invalid || thing.Destroyed)
        {
            return;
        }

        if (thing.def.thingCategories?.Intersect(ChunkCategoryDefs).Any() == false)
        {
            return;
        }

        if (thing.def.filth != null)
        {
            return;
        }


        if (HaulMinedChunksMod.Instance.Settings.LimitToHomeArea && !map.areaManager.Home[position])
        {
            return;
        }

        if (HaulMinedChunksMod.Instance.Settings.LimitToCustomArea &&
            HaulMinedChunksMod.Instance.Settings.CustomAreaName?.ToLowerInvariant() != "unrestricted")
        {
            var customArea = map.areaManager.GetLabeled(HaulMinedChunksMod.Instance.Settings.CustomAreaName);
            if (customArea == null || !customArea[position])
            {
                return;
            }
        }

        if (thing.IsForbidden(Faction.OfPlayer))
        {
            thing.SetForbidden(false);
        }

        if (map.designationManager.DesignationOn(thing, DesignationDefOf.Haul) != null)
        {
            return;
        }

        map.designationManager.AddDesignation(new Designation(thing, DesignationDefOf.Haul));
    }
}
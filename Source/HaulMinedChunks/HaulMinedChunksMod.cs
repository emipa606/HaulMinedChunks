using Mlie;
using UnityEngine;
using Verse;

namespace HaulMinedChunks;

[StaticConstructorOnStartup]
internal class HaulMinedChunksMod : Mod
{
    /// <summary>
    ///     The instance of the settings to be read by the mod
    /// </summary>
    public static HaulMinedChunksMod Instance;

    private static string currentVersion;

    /// <summary>
    ///     The private settings
    /// </summary>
    private HaulMinedChunksSettings settings;

    /// <summary>
    ///     Constructor
    /// </summary>
    /// <param name="content"></param>
    public HaulMinedChunksMod(ModContentPack content) : base(content)
    {
        Instance = this;
        currentVersion =
            VersionFromManifest.GetVersionFromModMetaData(content.ModMetaData);
    }

    /// <summary>
    ///     The instance-settings for the mod
    /// </summary>
    internal HaulMinedChunksSettings Settings
    {
        get
        {
            settings ??= GetSettings<HaulMinedChunksSettings>();

            return settings;
        }
        set => settings = value;
    }

    /// <summary>
    ///     The title for the mod-settings
    /// </summary>
    /// <returns></returns>
    public override string SettingsCategory()
    {
        return "Haul Mined Chunks";
    }

    /// <summary>
    ///     The settings-window
    ///     For more info: https://rimworldwiki.com/wiki/Modding_Tutorials/ModSettings
    /// </summary>
    /// <param name="rect"></param>
    public override void DoSettingsWindowContents(Rect rect)
    {
        var listingStandard = new Listing_Standard();
        listingStandard.Begin(rect);
        if (!Settings.LimitToCustomArea && !Settings.LimitToHomeArea)
        {
            listingStandard.Label("HMC.AllAreas".Translate());
        }
        else
        {
            listingStandard.Label("HMC.DefinedAreas".Translate());
        }

        listingStandard.GapLine();

        listingStandard.CheckboxLabeled("HMC.HomeArea".Translate(), ref Settings.LimitToHomeArea,
            "HMC.HomeArea.Tooltip".Translate());
        listingStandard.CheckboxLabeled("HMC.CustomArea".Translate(Settings.CustomAreaName),
            ref Settings.LimitToCustomArea,
            "HMC.CustomArea.Tooltip".Translate());
        if (Settings.LimitToCustomArea)
        {
            Settings.CustomAreaName =
                listingStandard.TextEntryLabeled("HMC.AreaName".Translate(), Settings.CustomAreaName);
        }


        if (currentVersion != null)
        {
            listingStandard.Gap();
            GUI.contentColor = Color.gray;
            listingStandard.Label("HMC.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listingStandard.End();
        Settings.Write();
    }
}
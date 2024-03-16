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
    public static HaulMinedChunksMod instance;

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
        instance = this;
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
            if (settings == null)
            {
                settings = GetSettings<HaulMinedChunksSettings>();
            }

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
        var listing_Standard = new Listing_Standard();
        listing_Standard.Begin(rect);
        if (!Settings.LimitToCustomArea && !Settings.LimitToHomeArea)
        {
            listing_Standard.Label("HMC.AllAreas".Translate());
            listing_Standard.GapLine();
        }
        else
        {
            listing_Standard.Label("HMC.DefinedAreas".Translate());
            listing_Standard.GapLine();
        }

        listing_Standard.CheckboxLabeled("HMC.HomeArea".Translate(), ref Settings.LimitToHomeArea,
            "HMC.HomeArea.Tooltip".Translate());
        listing_Standard.CheckboxLabeled("HMC.CustomArea".Translate(Settings.CustomAreaName),
            ref Settings.LimitToCustomArea,
            "HMC.CustomArea.Tooltip".Translate());
        if (Settings.LimitToCustomArea)
        {
            Settings.CustomAreaName =
                listing_Standard.TextEntryLabeled("HMC.AreaName".Translate(), Settings.CustomAreaName);
        }


        if (currentVersion != null)
        {
            listing_Standard.Gap();
            GUI.contentColor = Color.gray;
            listing_Standard.Label("HMC.ModVersion".Translate(currentVersion));
            GUI.contentColor = Color.white;
        }

        listing_Standard.End();
        Settings.Write();
    }
}
using Verse;

namespace HaulMinedChunks;

/// <summary>
///     Definition of the settings for the mod
/// </summary>
internal class HaulMinedChunksSettings : ModSettings
{
    public string CustomAreaName = "Mining";
    public bool LimitToCustomArea;
    public bool LimitToHomeArea;

    /// <summary>
    ///     Saving and loading the values
    /// </summary>
    public override void ExposeData()
    {
        base.ExposeData();
        Scribe_Values.Look(ref LimitToHomeArea, "LimitToHomeArea");
        Scribe_Values.Look(ref LimitToCustomArea, "LimitToCustomArea");
        Scribe_Values.Look(ref CustomAreaName, "CustomAreaName", "Mining");
    }
}
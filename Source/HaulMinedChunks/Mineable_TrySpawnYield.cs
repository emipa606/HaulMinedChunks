using HarmonyLib;
using RimWorld;
using Verse;

namespace HaulMinedChunks;

[HarmonyPatch(typeof(Mineable), "TrySpawnYield", typeof(Map), typeof(bool), typeof(Pawn))]
public class Mineable_TrySpawnYield
{
    public static bool Spawning;

    public static void Prefix(Pawn pawn)
    {
        if (pawn == null)
        {
            return;
        }

        if (HaulMinedChunks.Insects2Loaded && pawn.RaceProps.Insect)
        {
            if (!pawn.health.hediffSet.hediffs.Any(hediff => hediff.GetType().Name == "Hediff_InsectWorker"))
            {
                return;
            }
        }
        else if (!pawn.IsColonist && !pawn.IsColonyMech && !pawn.IsPrisoner)
        {
            if (!HaulMinedChunks.ArtificialBeingsFrameworkLoaded)
            {
                return;
            }

            if (pawn.Faction != Faction.OfPlayerSilentFail ||
                !(bool)HaulMinedChunks.IsArtificialMethodInfo.Invoke(null, [pawn]))
            {
                return;
            }
        }

        Spawning = true;
    }

    public static void Postfix()
    {
        Spawning = false;
    }
}
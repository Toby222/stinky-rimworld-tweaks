using System;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace StinkyTweaks.Components;

public class SafetyTracker(World world) : WorldComponent(world)
{
    public DateTime? SafeSince;

    private static bool AllMapsSafe =>
        Find.Maps.TrueForAll(map =>
            map.mapPawns.AllPawnsSpawned.TrueForAll(pawn => !pawn.HostileTo(Faction.OfPlayer))
        );

    public override void WorldComponentUpdate()
    {
        bool allMapsSafe = AllMapsSafe;
        bool slowTime =
            Find.TickManager.CurTimeSpeed
                is TimeSpeed.Normal
                    or TimeSpeed.Fast
                    or TimeSpeed.Superfast;
        bool shouldTrackTime = allMapsSafe && slowTime;

        if (SafeSince is not null && !shouldTrackTime)
            SafeSince = null;
        else if (SafeSince is null && shouldTrackTime)
            SafeSince = DateTime.Now;

        StinkyTweaks.Log.DebugMessage($"allMapsSafe: {allMapsSafe} - slowTime ${slowTime} - SafeSince ${SafeSince} - CurTimeSpeed {Find.TickManager.CurTimeSpeed} - safe time: {DateTime.Now - SafeSince}");
        if (
            SafeSince is not null
            && (DateTime.Now - SafeSince) >= StinkyTweaks.Settings.SafetySpeedupSafeTime
        )
        {
            Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
        }
    }
}

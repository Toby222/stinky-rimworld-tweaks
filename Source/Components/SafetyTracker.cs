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
            GenHostility.AnyHostileActiveThreatToPlayer(map)
        );

    public override void WorldComponentUpdate()
    {
        if (!StinkyTweaks.Settings.SafetySpeedupEnable) return;

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

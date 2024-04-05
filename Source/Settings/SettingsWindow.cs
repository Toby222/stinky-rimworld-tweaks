using System;
using UnityEngine;
using Verse;

namespace StinkyTweaks.Settings;

public static class SettingsWindow
{
    private static Vector2 settingsScrollPosition = new();

    private static float settingsHeight;

    private static StinkyTweaksSettings Settings => StinkyTweaks.Settings;

    public static void DoSettingsWindowContents(Rect inRect)
    {
        Listing_Standard listing = new();
        Rect viewRect = new(inRect.x, inRect.y, inRect.width - 16f, settingsHeight);
        Widgets.BeginScrollView(inRect, ref settingsScrollPosition, viewRect);
        listing.Begin(new Rect(viewRect.x, viewRect.y, viewRect.width, float.PositiveInfinity));

        listing.GapLine();
        Text.Font = GameFont.Medium;
        listing.CheckboxLabeled(StinkyTweaks.Translate("SafetySpeedup"), ref Settings.SafetySpeedupEnable);
        Text.Font = GameFont.Small;
        listing.Label(StinkyTweaks.Translate("SafetySpeedup.Explanation"));
        var safeTime = listing.SliderLabeled(
            StinkyTweaks.Translate(
                "SafetySpeedup.SafeTimeLabel",
                $"{(TimeSpan)Settings.SafetySpeedupSafeTime:%m\\mss\\s}".Named("timeSpan")
            ),
            (float)((TimeSpan)Settings.SafetySpeedupSafeTime).TotalSeconds,
            0f,
            300f
        );
        Settings.SafetySpeedupSafeTime = new(new TimeSpan((long)safeTime * TimeSpan.TicksPerSecond));
        listing.GapLine();

        listing.End();
        settingsHeight = listing.CurHeight;
        Widgets.EndScrollView();
    }

    public static string SettingsCategory()
    {
        return StinkyTweaks.Translate("SettingsCategory");
    }
}

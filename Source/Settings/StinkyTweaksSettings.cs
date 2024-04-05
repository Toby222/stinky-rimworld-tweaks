using System;
using System.Collections.Generic;
using Verse;

namespace StinkyTweaks.Settings;

public class StinkyTweaksSettings : ModSettings
{
    #region Scribe Helpers
    private static void LookField<T>(ref T value, string label, T defaultValue)
        where T : struct
    {
        Scribe_Values.Look(ref value, label, defaultValue);
    }

    private static void LookHashSet<T>(
        ref HashSet<T> valueHashSet,
        string label,
        HashSet<T> defaultValues
    )
        where T : notnull
    {
        if (Scribe.mode == LoadSaveMode.Saving && valueHashSet is null)
        {
            StinkyTweaks.Log.Warn(
                label + " is null before saving. Reinitializing with default values."
            );
            valueHashSet = defaultValues;
        }
        Scribe_Collections.Look(ref valueHashSet, label, lookMode: LookMode.Value);
        if (Scribe.mode == LoadSaveMode.LoadingVars && valueHashSet is null)
        {
            StinkyTweaks.Log.Warn(
                label + " is null after loading. Reinitializing with default values."
            );
            valueHashSet = defaultValues;
        }
    }
    #endregion

    public bool SafetySpeedupEnable = true;

    public struct SafetySpeedupSafeTimeStruct
    {
        public SafetySpeedupSafeTimeStruct(TimeSpan time)
        {
            this.time = time;
        }

        private TimeSpan time = TimeSpan.FromSeconds(10d);

        public override string ToString() => time.TotalSeconds.ToString();

        public static implicit operator TimeSpan(SafetySpeedupSafeTimeStruct self) => self.time;
    }
    public SafetySpeedupSafeTimeStruct SafetySpeedupSafeTime = new();

    public override void ExposeData()
    {
        base.ExposeData();

        LookField(ref SafetySpeedupEnable, nameof(SafetySpeedupEnable), true);
        LookField(
            ref SafetySpeedupSafeTime,
            nameof(SafetySpeedupSafeTime),
            new()
        );
    }
}

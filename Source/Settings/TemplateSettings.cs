using System.Collections.Generic;
using Verse;

namespace Template.Settings;

public class TemplateSettings : ModSettings
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
            TemplateMod.Log.Warn(
                label + " is null before saving. Reinitializing with default values."
            );
            valueHashSet = defaultValues;
        }
        Scribe_Collections.Look(ref valueHashSet, label, lookMode: LookMode.Value);
        if (Scribe.mode == LoadSaveMode.LoadingVars && valueHashSet is null)
        {
            TemplateMod.Log.Warn(
                label + " is null after loading. Reinitializing with default values."
            );
            valueHashSet = defaultValues;
        }
    }
    #endregion

    public override void ExposeData()
    {
        base.ExposeData();
    }
}

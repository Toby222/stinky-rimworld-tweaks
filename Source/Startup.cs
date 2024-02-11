#nullable enable

using Verse;

// using HarmonyLib;

namespace Template;

[StaticConstructorOnStartup]
public static class Startup
{
    static Startup()
    {
        Log.Message("Mod template loaded successfully!");
    }
}

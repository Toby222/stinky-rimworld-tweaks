using System.Reflection;
using HarmonyLib;
using Verse;

namespace StinkyTweaks;

using System;
using System.Globalization;
using Settings;
using UnityEngine;

public class StinkyTweaks : Mod
{
    internal static string Translate(string key, params NamedArgument[] args)
    {
        const string TranslationKey = nameof(StinkyTweaks);
        return (TranslationKey + "." + key).Translate(args);
    }

    public StinkyTweaks(ModContentPack content)
        : base(content)
    {
#if DEBUG
        const string build = "Debug";
#else
        const string build = "Release";
#endif
        Log.Message(
            $"Running Version {Assembly.GetAssembly(typeof(StinkyTweaks)).GetName().Version} "
                + build
        );
        Log.Message(content.ModMetaData.packageIdLowerCase);

        ParseHelper.Parsers<StinkyTweaksSettings.SafetySpeedupSafeTimeStruct>.Register(
            input => new StinkyTweaksSettings.SafetySpeedupSafeTimeStruct(
                TimeSpan.FromSeconds(double.Parse(input))
            )
        );

        Settings = GetSettings<StinkyTweaksSettings>();
        WriteSettings();

        Harmony harmony = new(content.ModMetaData.packageIdLowerCase);
        harmony.PatchAll();
    }

#nullable disable // Set in constructor.

    public static StinkyTweaksSettings Settings { get; private set; }

#nullable enable

    public void ResetSettings()
    {
        Settings = new();
    }

    public override void DoSettingsWindowContents(Rect inRect) =>
        SettingsWindow.DoSettingsWindowContents(inRect);

    public override string SettingsCategory() => SettingsWindow.SettingsCategory();

    public static class Log
    {
        const string LogPrefix = "Stinky Tweaks - ";

        public static void DebugError(string message)
        {
#if DEBUG
            Error(message);
#endif
        }

        public static void Error(string message)
        {
            Verse.Log.Error(LogPrefix + message);
        }

        public static void DebugWarn(string message)
        {
#if DEBUG
            Warn(message);
#endif
        }

        public static void Warn(string message)
        {
            Verse.Log.Warning(LogPrefix + message);
        }

        public static void DebugMessage(string message)
        {
#if DEBUG
            Message(message);
#endif
        }

        public static void Message(string message)
        {
            Verse.Log.Message(LogPrefix + message);
        }
    }
}

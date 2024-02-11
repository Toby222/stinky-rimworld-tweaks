#nullable enable

using RimWorld;
using Verse;

// using HarmonyLib;

namespace Template;

public class TemplateMapComponent : MapComponent
{
    public TemplateMapComponent(Map map)
        : base(map) { }

    public override void FinalizeInit()
    {
        Messages.Message("Success", null, MessageTypeDefOf.PositiveEvent);
        Find.LetterStack.ReceiveLetter(
            "Success",
            TemplateDefOf.success_letter.description,
            TemplateDefOf.success_letter,
            null
        );
    }
}
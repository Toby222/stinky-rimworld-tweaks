using RimWorld;
using Verse;

// using HarmonyLib;

namespace Template;

public class TemplateMapComponent(Map map) : MapComponent(map)
{
    public override void FinalizeInit()
    {
        Messages.Message("Success", null, MessageTypeDefOf.PositiveEvent);
        Find.LetterStack.ReceiveLetter(
            "Success",
            StinkyTweaksDefOf.success_letter.description,
            StinkyTweaksDefOf.success_letter,
            null
        );
    }
}

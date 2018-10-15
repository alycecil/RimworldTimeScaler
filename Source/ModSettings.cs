using UnityEngine;
using Verse;

namespace TimeSlider
{
    public class TimeSlideMod : Mod
    {
        private ModSettings settings;
        public static ModSettings latest;

        public TimeSlideMod(ModContentPack content) : base(content)
        {
            settings = GetSettings<ModSettings>();
            latest = settings;
        }

        public override string SettingsCategory() => "Time-Slider!";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            settings.maxSlider = Widgets.HorizontalSlider(
                inRect.TopHalf().TopHalf().TopHalf().ContractedBy(4),
                settings.maxSlider, 3.5f, 6.5f, true,
                "How much More Slider?"+settings.maxSlider
            );

            Widgets.Label(inRect.BottomHalf().BottomHalf().BottomHalf(),
                "That's all -Alice.\nSource Code Available at https://github.com/alycecil");
        }
    }

    public class ModSettings : Verse.ModSettings
    {
        public float maxSlider = 3.5f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref maxSlider, "maxSlider", 3.5f);
        }
    }
}
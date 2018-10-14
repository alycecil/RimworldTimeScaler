using UnityEngine;
using Verse;

namespace TimeSlider
{
    public class TimeSlideMod : Mod
    {
        private ModSettings settings;
        public static ModSettings latest = null;

        public TimeSlideMod(ModContentPack content) : base(content)
        {
            this.settings = GetSettings<ModSettings>();
            latest = this.settings;
        }

        public override string SettingsCategory() => "Time-Slider!";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            this.settings.maxSlider = Widgets.HorizontalSlider(
                inRect.TopHalf().TopHalf().TopHalf().ContractedBy(4),
                this.settings.maxSlider, 3.5f, 6.5f, true,
                this.settings.maxSlider.ToString()
            );

            Widgets.Label(inRect.BottomHalf().BottomHalf().BottomHalf(),
                "That's all, restart before playing to ensure your change is there. -Alice.\nSource Code Available at https://github.com/alycecil");
        }
    }

    public class ModSettings : Verse.ModSettings
    {
        public float maxSlider = 3.5f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.maxSlider, "maxSlider", 3.5f);
        }
    }
}
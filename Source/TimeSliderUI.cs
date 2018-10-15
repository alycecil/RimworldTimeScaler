using System;
using RimWorld;
using UnityEngine;
using Verse;

namespace TimeSlider
{
    public static class TimeSliderUI
    {
        
        
        
        public static void     TickManagerUi(Rect timerRect)
        {
            TickManager tickManager = Find.TickManager;
            
            GUI.BeginGroup(timerRect);
            Rect rect = new Rect(0f, 0f, TimeControls.TimeButSize.x, TimeControls.TimeButSize.y);

            DrawSlider(rect);

            if (Find.TickManager.slower.ForcedNormalSpeed)
            {
                Widgets.DrawLineHorizontal(rect.width * 2f, rect.height / 2f, rect.width * 2f);
            }

            GUI.EndGroup();
            GenUI.AbsorbClicksInRect(timerRect);
            UIHighlighter.HighlightOpportunity(timerRect, "TimeControls");
        }

        private static void DrawSlider(Rect rect)
        {
            var old = TimeSlider.timeSetting;
            var outSpeed = TimeSpeed.Normal;

            Rect slider = new Rect(rect.x, rect.y, rect.width * 4f, rect.height);

            string label = TimeSlider.describe(TimeSlider.timeSetting, ref outSpeed);
            var newVal = Widgets.HorizontalSlider(slider, old, 0f, TimeSlideMod.latest.maxSlider, false,
                label);
            

            if (outSpeed == TimeSpeed.Paused)
            {
                Find.TickManager.Pause();
                TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Paused);
            }
            else
            {
                var curTimeSpeed = Find.TickManager.CurTimeSpeed;
                if (outSpeed != curTimeSpeed)
                {
                    Find.TickManager.CurTimeSpeed =
                        outSpeed; //carry the helper around  MVP : Able to adjusted CutTimeSpeed to nearest reference
                }
            }

            if (Math.Abs(newVal - old) > .6e-6)
            {
                TimeSlider.timeSetting = newVal;
            }

            return;
        }
    }
}
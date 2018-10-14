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

            TimeSpeed[] CachedTimeSpeedValues = {TimeSpeed.Paused};

            RenderUiIcons(tickManager, CachedTimeSpeedValues, rect);


            
            DrawSlider(rect);

            if (Find.TickManager.slower.ForcedNormalSpeed)
            {
                Widgets.DrawLineHorizontal(rect.width * 2f, rect.height / 2f, rect.width * 2f);
            }

            GUI.EndGroup();
            GenUI.AbsorbClicksInRect(timerRect);
            UIHighlighter.HighlightOpportunity(timerRect, "TimeControls");
        }

        private static float DrawSlider(Rect rect)
        {
            var old = TimeSlider.timeSetting;
            var outSpeed = TimeSpeed.Normal;

            Rect slider = new Rect(rect.x + rect.width, rect.y, rect.width * 3f, rect.height);

            string label = TimeSlider.describe(TimeSlider.timeSetting, ref outSpeed);
            var newVal = Widgets.HorizontalSlider(slider, TimeSlider.timeSetting, 0f, TimeSlideMod.latest.maxSlider, false,
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
            
            TimeSlider.timeSetting = newVal;
            return newVal;
        }

        private static void RenderUiIcons(TickManager tickManager, TimeSpeed[] cachedTimeSpeedValues, Rect rect)
        {
            for (int i = 0; i < cachedTimeSpeedValues.Length; i++)
            {
                TimeSpeed timeSpeed = cachedTimeSpeedValues[i];
                if (timeSpeed != TimeSpeed.Ultrafast)
                {
                    if (Widgets.ButtonImage(rect, TimeSliderHarmony.SpeedButtonTextures[(int) timeSpeed]))
                    {
                        if (timeSpeed == TimeSpeed.Paused)
                        {
                            tickManager.TogglePaused();
                            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause,
                                KnowledgeAmount.SpecificInteraction);
                        }
                        else
                        {
                            tickManager.CurTimeSpeed = timeSpeed;
                            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls,
                                KnowledgeAmount.SpecificInteraction);
                        }

                        TimeSlider.PlaySoundOf(tickManager.CurTimeSpeed);
                    }

                    if (tickManager.CurTimeSpeed == timeSpeed)
                    {
                        GUI.DrawTexture(rect, TexUI.HighlightTex);
                    }

                    rect.x += rect.width;
                }
            }
        }
    }
}
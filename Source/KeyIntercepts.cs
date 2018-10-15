using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace TimeSlider
{
    public class KeyIntercepts
    {
        private static float last = 0f;

        public static void KeyMashes()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                var speed = Find.TickManager.CurTimeSpeed;
                if (KeyBindingDefOf.TogglePause.KeyDownEvent)
                {
                    TogglePause();


                    PlaySound();
                    PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.Pause,
                        KnowledgeAmount.SpecificInteraction);
                    Event.current.Use();
                }

                else if (KeyBindingDefOf.TimeSpeed_Normal.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Normal;
                    TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Normal);
                    PlaySound();
                    LearnedIt();
                    Event.current.Use();
                }

                else if (KeyBindingDefOf.TimeSpeed_Fast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Fast;
                    TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Fast);
                    PlaySound();
                    LearnedIt();
                    Event.current.Use();
                }

                else if (KeyBindingDefOf.TimeSpeed_Superfast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Superfast;
                    TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Superfast);
                    PlaySound();
                    LearnedIt();
                    Event.current.Use();
                }

                else if (KeyBindingDefOf.TimeSpeed_Ultrafast.KeyDownEvent)
                {
                    Find.TickManager.CurTimeSpeed = TimeSpeed.Ultrafast;
                    TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Ultrafast);
                    PlaySound();
                    LearnedIt();
                    Event.current.Use();
                }

                else if (KeyBindingDefOf.Dev_TickOnce.KeyDownEvent && speed == TimeSpeed.Paused)
                {
                    Log.Message("TRiGgeR Single Tick");
                    Find.TickManager.DoSingleTick();
                    SoundDefOf.Clock_Stop.PlayOneShotOnCamera(null);
                }
            }
        }

        private static void TogglePause()
        {
            TimeSlider.pause = !TimeSlider.pause;
            if (TimeSlider.pause)
            {
                last = TimeSlider.timeSetting;
                TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Paused);
            }
            else
            {
                TimeSlider.timeSetting = last;
            }
                

            Find.TickManager.TogglePaused();
        }

        private static void PlaySound()
        {
            TimeSlider.PlaySoundOf(Find.TickManager.CurTimeSpeed);
        }

        private static void LearnedIt()
        {
            PlayerKnowledgeDatabase.KnowledgeDemonstrated(ConceptDefOf.TimeControls,
                KnowledgeAmount.SpecificInteraction);
        }
    }
}
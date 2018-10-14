using RimWorld;
using UnityEngine;
using Verse;
using Verse.Sound;

namespace TimeSlider
{
    public class KeyIntercepts
    {
        public static void KeyMashes()
        {
            if (Event.current.type == EventType.KeyDown)
            {
                if (KeyBindingDefOf.TogglePause.KeyDownEvent)
                {
                    Log.Message("PAUSE!");
                    Find.TickManager.TogglePaused();
                    TimeSlider.setTimeSettingForTimeSpeed(TimeSpeed.Paused);
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

                else if (KeyBindingDefOf.Dev_TickOnce.KeyDownEvent && Find.TickManager.CurTimeSpeed == TimeSpeed.Paused)
                {
                    Find.TickManager.DoSingleTick();
                    SoundDefOf.Clock_Stop.PlayOneShotOnCamera(null);
                }
            }
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
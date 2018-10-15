using System;
using System.Reflection;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;

namespace TimeSlider
{
    [StaticConstructorOnStartup]
    class TimeSliderHarmony
    {
        static TimeSliderHarmony()
        {
            //harmony
            var harmony = HarmonyInstance.Create("time.slider");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
            
        }


        public static Texture2D[] SpeedButtonTextures =
        {
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Pause", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Normal", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Fast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Superfast", true),
            ContentFinder<Texture2D>.Get("UI/TimeControls/TimeSpeedButton_Superfast", true)
        };

    }

    [HarmonyPatch(typeof(TimeControls), "DoTimeControlsGUI")]
    public static class TimeControls_DoTimeControlsGUI
    {
        [HarmonyPrefix]
        static bool Prefix(Rect timerRect)
        {
           
            KeyIntercepts.KeyMashes();
            
            TimeSliderUI.TickManagerUi(timerRect);
            

            return false;
        }
    }
    
    [HarmonyPatch(typeof(TickManager), "TickRateMultiplier", MethodType.Getter)]
    public static class TickManager_TickRateMultiplier
    {
        [HarmonyPostfix]
        static void Postfix(ref float __result, TickManager __instance)
        {
            if (__result < 0.04f || __instance.Paused || TimeSlider.timeSetting < TimeSlider.MinSetting){ 
                __result = 0f;
                return;
            }

            if (__instance.slower.ForcedNormalSpeed)
            {
                __result = Math.Min(1.0f,TimeSlider.tickRate());
            }
            else
            {
                __result = TimeSlider.tickRate();
            }

        }
    }
}
using System;
using System.Reflection;
using Harmony;
using RimWorld;
using UnityEngine;
using Verse;

namespace TimeSlider
{
    public static class TimeSlider
    {
        
#if DEBUG
        private static short i = 0;
        #endif
        //redirect to private method
        private static MethodInfo __PlaySoundOf =
            AccessTools.Method(typeof(TimeControls), "PlaySoundOf", new[] {typeof(TimeSpeed)});

        public static float MinSetting = .03f;
        public static bool pause;

        public static void PlaySoundOf(TimeSpeed s)
        {
            __PlaySoundOf?.Invoke(null, new object[] {s});
        }


        public static float timeSetting = 1f;

        // Polynomial Regression of keyPoints
        //.7528331659 x^5 - 4.02960316 x^4 + 7.70080376 x^3 - 5.721310794 x^2 + 2.340790635 x + .003955776659
        //.6x^3-x^2+1.4x

        public static readonly Vector2[] keyPoints = new[]
        {
            new Vector2(0f, 0f), //paused
            new Vector2(0.5f, 0.5f), //half
            new Vector2(1f, 1f), //normal
            new Vector2(1.5f, 2f), //fast
            new Vector2(2f, 3f), //super
            new Vector2(3f, 20f), //ultra
            new Vector2(4f, 150f) //fast af
        };

        public static void setTimeSettingForTimeSpeed(TimeSpeed timeSpeed)
        {
            if (timeSpeed == TimeSpeed.Paused)
            {
#if DEBUG
                Log.Message("Paused It");
            #endif
                timeSetting = 0f;
                return;
            }

            timeSetting = keyPoints[Math.Min(5, (int) timeSpeed) + 1].x;
        }

        /**
         *  Polynomial Regression of keyPoints
        .7528331659 x^5 - 4.02960316 x^4 + 7.70080376 x^3 - 5.721310794 x^2 + 2.340790635 x + .003955776659 
        bad alt : .6x^3-x^2+1.4x
         */
        public static float tickRate()
        {
            var setting = timeSetting;

            var up2 = setting * setting;
            var up3 = up2 * setting;
            var up4 = up3 * setting;
            var up5 = up4 * setting;

            var value = .7528331659f * up5
                        - 4.02960316f * up4
                        + 7.70080376f * up3
                        - 5.721310794f * up2
                        + 2.340790635f * setting
                        + .003955776659f;

#if DEBUG
            i += 1;
            i %= 3567;
            if(i==0)
                Log.Message("Slider at ["+setting+"] TickRate ["+ value+"]");
            #endif

            return value;
        }

        public static string describe(float timeSetting, ref TimeSpeed outSpeed)
        {
            Find.TickManager.CurTimeSpeed = TimeSpeed.Paused;
            if (timeSetting < MinSetting)
                return "paused";

            outSpeed = TimeSpeed.Normal;

            if (timeSetting < .15f)
                return "snail";

            if (timeSetting < .35f)
                return "quarter";

            if (timeSetting < .60f)
                return "half";

            if (timeSetting < .9f)
                return "slowed";


            if (timeSetting < 1.4f)
                return "normal";

            outSpeed = TimeSpeed.Fast;
            if (timeSetting < 2.1f)
                return "fast";

            outSpeed = TimeSpeed.Superfast;
            if (timeSetting < 2.5f)
                return "faster";

            outSpeed = TimeSpeed.Ultrafast;
            if (timeSetting < 3.1f)
                return "ultra";

            if (timeSetting < 3.6f) return "wow";

            if (timeSetting <= 4.4f) return "zoom";

            return "fastest.";
        }
    }
}
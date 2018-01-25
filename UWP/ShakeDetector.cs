namespace Zebble.Device
{
    using System;

    internal static class ShakeDetector
    {
        const double SHAKING_ACCELERATION_THRESHOLD = 5;
        const int SHAKEN_INTERVAL = 500;
        static Accelerometer Accelerometer;

        static DateTime WaitUntil, MeasureStart;
        static double TotalAcceleration;

        internal static void Start(Accelerometer acc)
        {
            Accelerometer = acc;
            if (!Accelerometer.IsAvailable()) return;

            Accelerometer.Start(SenrorDelay.UI).RunInParallel();
            Accelerometer.Changed.Handle((Action<MotionVector>)OnChanged);
        }

        static void OnChanged(MotionVector change)
        {
            if (DateTime.UtcNow < WaitUntil) return;

            if (MeasureStart < DateTime.UtcNow.AddMilliseconds(SHAKEN_INTERVAL))
            {
                MeasureStart = DateTime.UtcNow;
                TotalAcceleration = 0;
            }

            TotalAcceleration += Math.Pow(change.X, 2) + Math.Pow(change.Y, 2) + Math.Pow(change.Z, 2);

            if (TotalAcceleration < SHAKING_ACCELERATION_THRESHOLD) return;

            WaitUntil = DateTime.UtcNow.AddSeconds(1);
            Accelerometer.DeviceShaken.RaiseOn(Thread.Pool);
        }
    }
}
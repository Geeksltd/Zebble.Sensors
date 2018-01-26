namespace Zebble.Device
{
    using Android.Hardware;
    using Android.Runtime;
    using System;

    class ShakeRecognizer : Java.Lang.Object, ISensorEventListener
    {
        const int SHAKE_DETECTION_TIME_LAPSE = 250;
        const double SHAKE_THRESHOLD = 800;

        bool HasUpdated;
        DateTime LastUpdate, LastShakenDetected;
        float LastX, LastY, LastZ;

        void ISensorEventListener.OnAccuracyChanged(Sensor sensor, [GeneratedEnum] SensorStatus accuracy) { }

        public void OnSensorChanged(SensorEvent args)
        {
            if (LastShakenDetected > DateTime.UtcNow.Subtract(500.Milliseconds()))
                return; // Already raised

            if (args.Sensor.Type != SensorType.Accelerometer) return;

            var x = args.Values[0]; var y = args.Values[1]; var z = args.Values[2];

            var start = DateTime.Now;

            if (!HasUpdated)
            {
                HasUpdated = true;
                LastUpdate = start;
                LastX = x; LastY = y; LastZ = z;
            }
            else if ((start - LastUpdate).TotalMilliseconds > SHAKE_DETECTION_TIME_LAPSE)
            {
                var diffTime = (float)(start - LastUpdate).TotalMilliseconds;
                LastUpdate = start;
                var total = x + y + z - LastX - LastY - LastZ;
                var speed = Math.Abs(total) / diffTime * 10000;

                if (speed > SHAKE_THRESHOLD)
                {
                    LastShakenDetected = DateTime.UtcNow;
                    Device.Accelerometer.DeviceShaken.RaiseOn(Thread.Pool);
                }

                LastX = x; LastY = y; LastZ = z;
            }
        }
    }
}
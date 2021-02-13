namespace Zebble.Device
{
    using System;
    using Windows.Devices.Sensors;

    public partial class Gyroscope : Sensor<MotionVector>
    {
        const double MULTIPLIER = 1 / 360.0; // UWP is different from iOS and Android

        Gyrometer sensor;
        Gyrometer Sensor => sensor ?? (sensor = Gyrometer.GetDefault());

        public bool IsAvailable() => Sensor != null;

        protected override void DoStart(SensorDelay delay)
        {
            if (EnvironmentSimulator.Gyroscope != null)
            {
                EnvironmentSimulator.Gyroscope.Changed += OnChanged;
            }
            else
            {
                if (Sensor == null) throw new Exception("Gyroscope is not available on this device.");

                Sensor.ReportInterval = (uint)delay;
                Sensor.ReadingChanged += Sensor_ReadingChanged;
            }
        }

        void Sensor_ReadingChanged(Gyrometer sender, GyrometerReadingChangedEventArgs args)
        {
            var reading = args.Reading;
            OnChanged(new MotionVector(
                reading.AngularVelocityX * MULTIPLIER,
                reading.AngularVelocityY * MULTIPLIER,
                reading.AngularVelocityZ * MULTIPLIER));
        }

        public override void Stop()
        {
            IsActive = false;

            if (EnvironmentSimulator.Gyroscope != null) EnvironmentSimulator.Gyroscope.Changed -= OnChanged;

            if (Sensor != null) Sensor.ReadingChanged -= Sensor_ReadingChanged;
        }
    }
}
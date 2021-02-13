namespace Zebble.Device
{
    using System;
    using Windows.Devices.Sensors;

    partial class Accelerometer
    {
        Windows.Devices.Sensors.Accelerometer sensor;
        Windows.Devices.Sensors.Accelerometer Sensor => sensor ?? (sensor = Windows.Devices.Sensors.Accelerometer.GetDefault());

        public Accelerometer()
        {
            if (ShouldDetectShaking)
                ShakeDetector.Start(this);
        }

        public bool IsAvailable() => Sensor != null;

        protected override void DoStart(SensorDelay delay)
        {
            if (EnvironmentSimulator.Accelerometer != null)
                EnvironmentSimulator.Accelerometer.Changed += OnChanged;
            else
            {
                if (Sensor == null) throw new Exception("Accelerometer sensor is not available on this device.");

                Sensor.ReportInterval = (uint)delay;
                Sensor.ReadingChanged += Sensor_ReadingChanged;
            }
        }

        public override void Stop()
        {
            IsActive = false;

            if (EnvironmentSimulator.Accelerometer != null)
                EnvironmentSimulator.Accelerometer.Changed -= OnChanged;

            if (Sensor != null) Sensor.ReadingChanged -= Sensor_ReadingChanged;
        }

        void Sensor_ReadingChanged(Windows.Devices.Sensors.Accelerometer sender, AccelerometerReadingChangedEventArgs args)
        {
            var reading = args.Reading;
            OnChanged(new MotionVector(reading.AccelerationX, reading.AccelerationY, reading.AccelerationZ));
        }
    }
}

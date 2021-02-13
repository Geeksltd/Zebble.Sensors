namespace Zebble.Device
{
    using System;
    using Windows.Devices.Sensors;

    public partial class Compass : Sensor<double>
    {
        Windows.Devices.Sensors.Compass sensor;
        Windows.Devices.Sensors.Compass Sensor => sensor ?? (sensor = Windows.Devices.Sensors.Compass.GetDefault());

        public bool IsAvailable() => Sensor != null;

        protected override void DoStart(SensorDelay delay)
        {
            if (EnvironmentSimulator.Compass != null)
                EnvironmentSimulator.Compass.Changed += OnChanged;
            else
            {
                if (Sensor == null) throw new Exception("Compass sensor is not available on this device.");

                Sensor.ReportInterval = (uint)delay;
                Sensor.ReadingChanged += Sensor_ReadingChanged;
            }
        }

        void Sensor_ReadingChanged(Windows.Devices.Sensors.Compass sender, CompassReadingChangedEventArgs args)
        {
            var reading = args.Reading;
            OnChanged(reading.HeadingTrueNorth ?? reading.HeadingMagneticNorth);
        }

        public override void Stop()
        {
            IsActive = false;

            if (EnvironmentSimulator.Compass != null)
                EnvironmentSimulator.Compass.Changed -= OnChanged;

            if (sensor != null) Sensor.ReadingChanged -= Sensor_ReadingChanged;
        }
    }
}
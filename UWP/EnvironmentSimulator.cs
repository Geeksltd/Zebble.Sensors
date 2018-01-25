namespace Zebble.Device
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class EnvironmentSimulator
    {
        public static SimulatedSensor<MotionVector> Accelerometer, Gyroscope;
        public static SimulatedSensor<double> Compass;

        public static Services.GeoPosition Location;

        public static void Start()
        {
            Accelerometer = new SimulatedSensor<MotionVector>();
            Gyroscope = new SimulatedSensor<MotionVector>();
            Compass = new SimulatedSensor<double>();
        }

        public class SimulatedSensor<TReading>
        {
            public void Invoke(TReading value) => Changed?.Invoke(value);

            public event Action<TReading> Changed;
        }
    }
}
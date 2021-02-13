namespace Zebble.Device
{
    using CoreLocation;
    using CoreMotion;

    partial class Sensor<TValue>
    {
        protected static CMMotionManager MotionManager = new CMMotionManager();
        protected static CLLocationManager LocationManager = new CLLocationManager
        {
            DesiredAccuracy = CLLocation.AccuracyBest,
            HeadingFilter = 0.1
        };

        public abstract void Stop();

        protected abstract void DoStart(SensorDelay delay);
    }
}
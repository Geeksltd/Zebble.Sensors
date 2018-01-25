namespace Zebble.Device
{
    using Android.Hardware;

    public partial class Gyroscope : Sensor<MotionVector>
    {
        public Gyroscope() : base(SensorType.Gyroscope) { }

        public override void OnSensorChanged(SensorEvent args)
        {
            OnChanged(new MotionVector(args.Values[0], args.Values[1], args.Values[2]));
        }
    }
}
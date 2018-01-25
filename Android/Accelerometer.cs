namespace Zebble.Device
{
    using Android.Hardware;

    partial class Accelerometer
    {
        public Accelerometer() : base(SensorType.Accelerometer) { }

        public override void OnSensorChanged(SensorEvent args)
        {
            var xAngle = args.Values[0] * 0.1;
            var yAngle = args.Values[1] * 0.1;
            var zAngle = args.Values[2] * 0.1;

            OnChanged(new MotionVector(xAngle, yAngle, zAngle));
        }
    }
}
namespace Zebble.Device
{
    using Android.Hardware;

    public partial class Compass : Sensor<double>
    {
        public Compass() : base(SensorType.Orientation) { }
        public override void OnSensorChanged(SensorEvent args) => OnChanged(args.Values[0]);
    }
}
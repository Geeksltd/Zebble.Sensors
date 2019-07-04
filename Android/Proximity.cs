namespace Zebble.Device
{
    using Android.Hardware;

    public partial class Proximity : Sensor<bool>
    {
        public Proximity() : base(SensorType.Proximity) { }
        public override void OnSensorChanged(SensorEvent args) => OnChanged(args.Values[0] < args.Sensor.MaximumRange);
    }
}
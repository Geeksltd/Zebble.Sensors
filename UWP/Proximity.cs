namespace Zebble.Device
{
    public partial class Proximity : Sensor<bool>
    {
        public override void Stop() { }

        protected override void DoStart(SensorDelay delay)
        {
            OnChanged(false);
        }
    }
}
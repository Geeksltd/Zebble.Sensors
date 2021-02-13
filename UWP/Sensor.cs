namespace Zebble.Device
{
    partial class Sensor<TValue>
    {
        public abstract void Stop();

        protected abstract void DoStart(SensorDelay delay);
    }
}
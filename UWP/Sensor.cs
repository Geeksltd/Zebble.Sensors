namespace Zebble.Device
{
    using System;
    using Windows.Devices.Sensors;

    partial class Sensor<TValue>
    {
        public abstract void Stop();

        protected abstract void DoStart(SenrorDelay delay);
    }
}
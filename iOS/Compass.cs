namespace Zebble.Device
{
    using CoreLocation;
    using System;

    public partial class Compass : Sensor<double>
    {
        public bool IsAvailable() => CLLocationManager.HeadingAvailable;

        protected override void DoStart(SenrorDelay _)
        {
            if (!IsAvailable()) throw new Exception("Compass is not available on this device.");

            LocationManager.UpdatedHeading += HandleChanged;
            LocationManager.StartUpdatingHeading();
        }

        public override void Stop()
        {
            if (!IsAvailable()) return;

            IsActive = false;
            LocationManager.UpdatedHeading -= HandleChanged;
            LocationManager.StopUpdatingHeading();
        }

        void HandleChanged(object _, CLHeadingUpdatedEventArgs e) => OnChanged(e.NewHeading.MagneticHeading);
    }
}
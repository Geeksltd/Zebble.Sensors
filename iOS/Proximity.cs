namespace Zebble.Device
{
    using Foundation;
    using System;
    using UIKit;

    public partial class Proximity : Sensor<bool>
    {
        public bool IsAvailable() => true;

        public override void Stop()
        {
            UIDevice.CurrentDevice.ProximityMonitoringEnabled = false;
            NSNotificationCenter.DefaultCenter.RemoveObserver(UIDevice.ProximityStateDidChangeNotification);
        }

        protected override void DoStart(SenrorDelay delay)
        {
            UIDevice.CurrentDevice.ProximityMonitoringEnabled = true;
            NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.ProximityStateDidChangeNotification, (a) => OnChanged(UIDevice.CurrentDevice.ProximityState));
        }
    }
}
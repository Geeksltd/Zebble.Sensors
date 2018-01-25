namespace Zebble.Device
{
    using System;
    using Foundation;
    using CoreMotion;

    public partial class Gyroscope : Sensor<MotionVector>
    {
        public bool IsAvailable() => MotionManager.GyroAvailable;

        protected override void DoStart(SenrorDelay delay)
        {
            if (!IsAvailable()) throw new Exception("Gyroscope is not available on this device.");

            MotionManager.GyroUpdateInterval = (int)delay * 0.001;
            MotionManager.StartGyroUpdates(NSOperationQueue.CurrentQueue, HandleChanged);
        }

        public override void Stop()
        {
            if (IsAvailable())
            {
                IsActive = false;
                MotionManager.StopGyroUpdates();
            }
        }

        void HandleChanged(CMGyroData data, NSError _)
        {
            OnChanged(new MotionVector(data.RotationRate.x, data.RotationRate.y, data.RotationRate.z));
        }
    }
}
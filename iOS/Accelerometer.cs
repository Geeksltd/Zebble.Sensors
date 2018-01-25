﻿namespace Zebble.Device
{
    using System;
    using Foundation;
    using CoreMotion;

    partial class Accelerometer
    {
        public bool IsAvailable() => MotionManager.AccelerometerAvailable;

        protected override void DoStart(SenrorDelay delay)
        {
            if (!IsAvailable()) throw new Exception("Accelerometer sensor is not available on this device.");

            MotionManager.AccelerometerUpdateInterval = (int)delay * 0.001;
            MotionManager.StartAccelerometerUpdates(NSOperationQueue.CurrentQueue, HandleChanged);
        }

        public override void Stop()
        {
            if (IsAvailable())
            {
                IsActive = false;
                MotionManager.StopAccelerometerUpdates();
            }
        }

        void HandleChanged(CMAccelerometerData data, NSError _)
        {
            OnChanged(new MotionVector(data.Acceleration.X, data.Acceleration.Y, data.Acceleration.Z));
        }
    }
}
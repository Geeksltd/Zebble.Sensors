namespace Zebble.Device
{
    using UIKit;

    public static partial class Sensors
    {
        static Sensors()
        {
            UIRuntime.OnViewMotionEnded.Handle(motion =>
            {
                if (motion == UIEventSubtype.MotionShake && Accelerometer.ShouldDetectShaking)
                    Accelerometer.DeviceShaken.RaiseOn(Thread.Pool);
            });

            UIRuntime.OnFinishedLaunching.Handle(() =>
            {
                UIApplication.SharedApplication.ApplicationSupportsShakeToEdit = Accelerometer.ShouldDetectShaking;
            });
        }
    }
}
namespace Zebble.Device
{
    using Android.Content;
    using Android.Hardware;

    public static partial class Sensors
    {
        static ShakeRecognizer ShakeRecognizer;

        static Sensors()
        {
            UIRuntime.OnResume.Handle(() =>
            {
                if (Accelerometer.ShouldDetectShaking)
                    Thread.Pool.RunActionOnNewThread(RegisterShakeHandler);
            });

            UIRuntime.OnPause.Handle(() =>
            {
                if (Accelerometer.ShouldDetectShaking)
                {
                    UIRuntime.GetService<SensorManager>(Context.SensorService).UnregisterListener(ShakeRecognizer);
                }
            });
        }

        static void RegisterShakeHandler()
        {
            var sensorManager = UIRuntime.GetService<SensorManager>(Context.SensorService);
            var sensor = sensorManager.GetDefaultSensor(SensorType.Accelerometer);
            ShakeRecognizer = new ShakeRecognizer();
            sensorManager.RegisterListener(ShakeRecognizer, sensor, SensorDelay.Ui);
        }
    }
}
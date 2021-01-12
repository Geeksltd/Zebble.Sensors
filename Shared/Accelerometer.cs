namespace Zebble.Device
{
    using Olive;

    public partial class Accelerometer : Sensor<MotionVector>
    {
        /// <summary>
        /// This event is raised when the device is shaken.
        /// Due to battery usage overheads, this is disabled by default, 
        /// unless you set Device.System.DetectShaking to true in Config.xml.
        /// </summary>
        public static readonly AsyncEvent DeviceShaken = new AsyncEvent();

        internal static bool ShouldDetectShaking => Config.Get("Device.System.DetectShaking", defaultValue: false);
    }
}

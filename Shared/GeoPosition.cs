namespace Zebble.Device
{
    using Olive.GeoLocation;

    public class GeoPosition : GeoLocation
    {
        /// <summary>The altitude relative to sea level in meters.</summary>
        public double? Altitude { get; set; }

        /// <summary>The potential error radius in meters.</summary>
        public double? Accuracy { get; set; }

        /// <summary>The potential altitude error range in meters.</summary>
        public double? AltitudeAccuracy { get; set; }

        /// <summary>The speed in meters per second.</summary>
        public double? Speed { get; set; }
    }
}
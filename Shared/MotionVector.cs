namespace Zebble
{
    using Olive;

    [EscapeGCop("X, Y, and Z are actually good names here.")]
    public struct MotionVector
    {
        public MotionVector(double x, double y, double z) { X = x; Y = y; Z = z; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public override string ToString() => $"X={X.Round(2)}, Y={Y.Round(2)}, Z={Z.Round(2)}";
    }
}
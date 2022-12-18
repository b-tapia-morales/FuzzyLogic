namespace FuzzyLogic.Utils;

public static class TrigonometricUtils
{
    public static double Distance((double X1, double Y1) v1, (double X2, double Y2) v2) =>
        Math.Sqrt(Math.Pow(v2.X2 - v1.X1, 2) + Math.Pow(v2.Y2 - v1.Y1, 2));

    public static double CalculateTriangleArea(double x1, double x2, double h) =>
        (1 / 2.0) * Math.Abs(x1 - x2) * h;
    
    public static double CalculateTrapezoidArea(double x1, double x2, double x3, double x4, double h)
    {
        var a = Math.Abs(x2 - x3);
        var b = Math.Abs(x1 - x4);
        return (1 / 2.0) * (a + b) * h;
    }
}
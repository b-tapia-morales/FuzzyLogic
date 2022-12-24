namespace FuzzyLogic.Utils;

public static class TrigonometricUtils
{
    public static double Distance((double X1, double Y1) v1, (double X2, double Y2) v2) =>
        Math.Sqrt(Math.Pow(v2.X2 - v1.X1, 2) + Math.Pow(v2.Y2 - v1.Y1, 2));

    public static double CalculateTriangleArea(double b, double h) =>
        (1 / 2.0) * b * h;

    public static double CalculateTrapezoidArea(double a, double b, double h) => 
        (1 / 2.0) * (a + b) * h;
}
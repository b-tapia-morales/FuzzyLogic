using static System.Math;

namespace FuzzyLogic.Utils;

public static class TrigonometricUtils
{
    public static double Distance((double X1, double Y1) v1, (double X2, double Y2) v2) =>
        Sqrt(Pow(v2.X2 - v1.X1, 2) + Pow(v2.Y2 - v1.Y1, 2));

    public static double TriangleArea(double @base, double height) =>
        @base * (height / 2);

    public static double RightTriangleHypotenuse(double @base, double height) =>
        Sqrt(Pow(@base, 2) + Pow(height, 2));

    public static double TrapezoidArea(double a, double b, double c, double d, double height) =>
        TrapezoidArea(d - a, c - b, height / 2);

    public static double TrapezoidArea(double a, double b, double height) =>
        (height / 2) * (a + b);

    public static double TriangleXCentroid(double x1, double x2, double x3) => (x1 + x2 + x3) / 3;

    public static double TriangleYCentroid(double y1, double y2, double y3) => (y1 + y2 + y3) / 3;

    public static double TriangleYCentroid(double h) => h / 3;

    public static (double X, double Y) TriangleCentroid(double x1, double x2, double x3, double height) =>
        (TriangleXCentroid(x1, x2, x3), TriangleYCentroid(height));

    public static (double X, double Y) TriangleCentroid((double X, double Y) point1, (double X, double Y) point2, (double X, double Y) point3) =>
        (TriangleXCentroid(point1.X, point2.X, point3.X), TriangleYCentroid(point1.Y, point2.Y, point3.Y));

    public static double TrapezoidXCentroid(double a, double b, double height) =>
        (height / 3) * ((2 * a + b) / (a + b));

    public static double TrapezoidYCentroid(double height) =>
        height / 2;

    public static (double X, double Y) TrapezoidCentroid(double a, double b, double height) =>
        (TrapezoidXCentroid(a, b, height), TrapezoidYCentroid(height));
}
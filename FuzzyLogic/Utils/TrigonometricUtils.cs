using static System.Math;

namespace FuzzyLogic.Utils;

public static class TrigonometricUtils
{
    public static double Distance((double X1, double Y1) v1, (double X2, double Y2) v2) =>
        Sqrt(Pow(v2.X2 - v1.X1, 2) + Pow(v2.Y2 - v1.Y1, 2));

    public static double TriangleArea(double b, double h) =>
        (h / 2) * b;

    public static double RightTriangleHypotenuse(double b, double h) =>
        Sqrt(Pow(b, 2) + Pow(h, 2));

    public static double TrapezoidArea(double a, double b, double h) =>
        (h / 2) * (a + b);

    public static (double X, double Y) TriangleCentroid(double x1, double x2, double x3, double h) =>
        ((x1 + x2 + x3) / 3, h / 3);

    public static (double X, double Y) TrapezoidCentroid(double a, double b, double c, double d, double h) =>
        ((b / 2) + ((2 * a + b) * (Pow(c, 2) - Pow(d, 2))) / (6 * (Pow(b, 2) - Pow(a, 2))),
            (h / 3) * ((2 * a + b) / (a + b)));
}
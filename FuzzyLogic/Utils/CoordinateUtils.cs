namespace FuzzyLogic.Utils;

public static class CoordinateUtils
{
    public static double Distance((double X1, double Y1) v1, (double X2, double Y2) v2) =>
        Math.Sqrt(Math.Pow(v2.X2 - v1.X1, 2) + Math.Pow(v2.Y2 - v1.Y1, 2));

    public static double CalculateTriangleArea((double X1, double Y1) v1, (double X2, double Y2) v2,
        (double X3, double Y3) v3)
    {
        var p1 = v1.X1 * (v2.Y2 - v3.Y3);
        var p2 = v2.X2 * (v3.Y3 - v1.Y1);
        var p3 = v3.X3 * (v1.Y1 - v2.Y2);
        return (1 / 2.0) * Math.Abs(p1 + p2 + p3);
    }

    public static double CalculateTriangleArea(double x1, double x2, double x3, double y) =>
        CalculateTriangleArea((x1, 0), (x2, y), (x3, 0));
}
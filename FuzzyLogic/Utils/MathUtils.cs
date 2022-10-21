namespace FuzzyLogic.Utils;

public static class MathUtils
{
    public static double Distance(double x0, double x1, double y0, double y1) =>
        Math.Sqrt(Math.Pow(x1 - x0, 2) + Math.Pow(y1 - y0, 2));
}
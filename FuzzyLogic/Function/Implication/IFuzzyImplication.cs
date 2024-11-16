using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Implication;

public interface IFuzzyImplication : IClosedShape
{
    /// <summary>
    ///     <para>
    ///         Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate, originating
    ///         from performing a Lambda-cut over the original membership function at the height point <i>y</i>,
    ///         represented as a <see cref="IFuzzyNumber{TNumber}" />.
    ///     </para>
    ///     <para>
    ///         <b>Special cases</b>:
    ///     </para>
    ///     <list type="bullet">
    ///         <item>
    ///             <description>μ(x) = 0 ⇒ The zero function.</description>
    ///         </item>
    ///         <item>
    ///             <description>μ(x) = 1 ⇒ The original membership function.</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="y">
    ///     The height point at which the Lambda-cut is performed, represented as a
    ///     <see cref="IFuzzyNumber{TNumber}" />.
    /// </param>
    /// <returns>The new membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    /// <seealso cref="IMembershipFunction{T}.AsFunction()" />
    Func<double, double> LambdaCutFunction<T>(T y) where T : struct, IFuzzyNumber<T>;

    /// <summary>
    ///     <para>
    ///         Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate, originating
    ///         from performing a Lambda-cut over the original membership function at the height point <i>y</i>,
    ///         represented as a <see cref="IFuzzyNumber{TNumber}" />.
    ///     </para>
    ///     <para>
    ///         This Lambda-cut differs from the original
    ///         <see cref="LambdaCutFunction{TNumber}(TNumber)">LambdaCutFunction</see> in that it also performs
    ///         horizontal cuts over the membership function. This can be specially relevant if the function
    ///         <see cref="IMembershipFunction{T}.IsClosed">is not closed</see>, since it could have no lower or upper
    ///         boundaries for <i>x</i> values other than -∞ and +∞.
    ///     </para>
    ///     <list type="bullet">
    ///         <item>
    ///             <description>x &lt; x₀ ∨ x &gt; x₁ ⇒ The zero function.</description>
    ///         </item>
    ///         <item>
    ///             <description>μ(x) = 0 ⇒ The zero function.</description>
    ///         </item>
    ///         <item>
    ///             <description>μ(x) = 1 ⇒ The original membership function.</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="y">
    ///     The height point at which the Lambda-cut is performed, represented as a
    ///     <see cref="IFuzzyNumber{TNumber}" />.
    /// </param>
    /// <param name="x0">The lower bound for the left horizontal cut.</param>
    /// <param name="x1">The upper bound for the right horizontal cut.</param>
    /// <returns>A <see cref="Func{T,TResult}" /> delegate</returns>
    /// <seealso cref="LambdaCutFunction{TNumber}(TNumber)">LambdaCutFunction</seealso>
    /// <seealso cref="IMembershipFunction{T}.IsClosed">IsClosed</seealso>
    Func<double, double> LambdaCutFunction<T>(T y, double x0, double x1) where T : struct, IFuzzyNumber<T> => x =>
        x < x0 || x > x1 ? 0.0 : LambdaCutFunction(y).Invoke(x);

    double? LambdaCutLeftEndpoint<T>(T y) where T : struct, IFuzzyNumber<T>;

    double? LambdaCutRightEndpoint<T>(T y) where T : struct, IFuzzyNumber<T>;

    /// <summary>
    ///     <para>
    ///         Returns the left and right sided coordinates, represented as a <see cref="ValueTuple" />, for a
    ///         Lambda-cut performed at the height point <i>y</i>, represented as a <see cref="IFuzzyNumber{T}" />.
    ///     </para>
    ///     <para>
    ///         Note that it should not be assumed that the lower or upper boundaries of this interval necessarily exist.
    ///         It will depend on whether the function itself is
    ///         <see cref="Interface.IMembershipFunction{T}.IsOpenLeft">Open Left</see>
    ///         or <see cref="Interface.IMembershipFunction{T}.IsOpenRight">Open Right</see>.
    ///     </para>
    /// </summary>
    /// <param name="y">The height point at which the Lambda-cut is performed, represented as a <see cref="FuzzyNumber" />.</param>
    /// <returns>The interval, represented as a <see cref="ValueTuple" /></returns>
    (double X1, double X2)? LambdaCutInterval<T>(T y) where T : struct, IFuzzyNumber<T> =>
        y > H
            ? null
            : (LambdaCutLeftEndpoint(y).GetValueOrDefault(), LambdaCutRightEndpoint(y).GetValueOrDefault());

    /// <summary>
    ///     <para>
    ///         Returns the left and right sided coordinates, represented as a <see cref="ValueTuple" />, for a
    ///         Lambda-cut performed at the the crossover points.
    ///     </para>
    ///     <para>
    ///         This method is equivalent to calling the <see cref="LambdaCutInterval{TNumber}(TNumber)">LambdaCutInterval</see>
    ///         method with <i>0.5</i> as a parameter, which represents the height point at which the Lambda-cut is
    ///         performed.
    ///     </para>
    /// </summary>
    /// <returns>The crossover points interval, represented as a <see cref="ValueTuple" /></returns>
    (double X1, double X2)? CrossoverCutInterval<T>() where T : struct, IFuzzyNumber<T> =>
        H >= 0.5 ? LambdaCutInterval<T>(0.5).GetValueOrDefault() : null;

    double CalculateArea<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>;

    double CentroidXCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidXCoordinate(this, y, errorMargin);

    double CentroidYCoordinate<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> => CentroidYCoordinate(this, y, errorMargin);

    (double X, double Y) CalculateCentroid<T>(T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T> =>
        (CentroidXCoordinate(y, errorMargin), CentroidYCoordinate(y, errorMargin));

    static double CentroidXCoordinate<T>(IFuzzyImplication function, T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the centroid coordinates of the zero-function");
        if (y >= function.H)
            return (function as IClosedShape).CentroidXCoordinate(errorMargin);
        var (x1, x2) = function.ClosedInterval();
        var area = function.CalculateArea(y, errorMargin);
        return (1 / area) * Integrate(Integral, x1, x2, errorMargin);
        double Integral(double x) => x * function.LambdaCutFunction(y).Invoke(x);
    }

    static double CentroidYCoordinate<T>(IFuzzyImplication function, T y, double errorMargin = DefaultErrorMargin)
        where T : struct, IFuzzyNumber<T>
    {
        if (y == 0)
            throw new ArgumentException("Can't calculate the centroid coordinates of the zero-function");
        if (y >= function.H)
            return (function as IClosedShape).CentroidYCoordinate(errorMargin);
        double Integral(double x) => function.LambdaCutFunction(y).Invoke(x) * function.LambdaCutFunction(y).Invoke(x);
        var (x1, x2) = function.ClosedInterval();
        var area = function.CalculateArea(y, errorMargin);
        return (1 / (2.0 * area)) * Integrate(Integral, x1, x2, errorMargin);
    }
}
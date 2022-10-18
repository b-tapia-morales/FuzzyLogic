using System.Numerics;
using FuzzyLogic.Number;

namespace FuzzyLogic.MembershipFunctions.Base;

/// <summary>
///     <para>
///         Represents the Membership Function that allows the creation of linguistic values that belong to a linguistic
///         variable.
///     </para>
///     <para>
///         Note that Membership Functions must not implement this class directly. They must inherit from
///         <see cref="BaseMembershipFunction{T}" /> instead.
///     </para>
/// </summary>
/// <typeparam name="T">
///     The type must be either <see cref="int" /> or <see cref="double" /> (the reason behind it being
///     defined as <see langword="unmanaged" />). Support for other types other than the strictly aforementioned is not
///     allowed.
/// </typeparam>
public interface IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
    /// <summary>
    ///     The name of the function.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     <para>
    ///         Returns the minimum value allowed for an <i>x</i> value that belongs to the support of the Membership
    ///         Function (that is, the region of the universe that is characterized by nonzero membership:
    ///         0 &lt; μ(X) &lt; 1).
    ///     </para>
    ///     <para>
    ///         Note that the nullability represents the existence of such value, and, as such, is not a replacement
    ///         for mathematical concepts such as <see cref="Double.PositiveInfinity" />,
    ///         <see cref="Double.NegativeInfinity" /> or <see cref="Double.NaN" />. In case there's a need to represent
    ///         the boundary using such concepts, this method needs to be overriden by the implementing class.
    ///     </para>
    /// </summary>
    /// <returns>
    ///     The minimum value allowed for an <i>x</i> value that belongs to the support of the Membership Function.
    /// </returns>
    T? LowerBoundary() => null;

    /// <summary>
    ///     <para>
    ///         Returns the maximum value allowed for an <i>x</i> value that belongs to the support of the Membership
    ///         Function (that is, the region of the universe that is characterized by nonzero membership: 0 &lt; μ(X) &lt; 1).
    ///     </para>
    ///     <para>
    ///         Note that the nullability represents the existence of such value, and, as such, is not a replacement
    ///         for mathematical concepts such as <see cref="Double.PositiveInfinity" />,
    ///         <see cref="Double.NegativeInfinity" /> or <see cref="Double.NaN" />. In case there's a need to use such
    ///         concepts, this method needs to be overriden by the implementing class.
    ///     </para>
    /// </summary>
    /// <returns>
    ///     The maximum value allowed for an <i>x</i> value that belongs to the support of the Membership Function.
    /// </returns>
    T? UpperBoundary() => null;

    /// <summary>
    ///     <para>
    ///         Returns the minimum and maximum values allowed for an <i>x</i> value that belongs to the support of the
    ///         Membership Function as an interval, represented as a <see cref="System.ValueTuple" />.
    ///     </para>
    ///     <para>
    ///         The check for the existence of such values is delegated to the consumer of the class (see
    ///         <see cref="LowerBoundary()" /> and <see cref="UpperBoundary()" /> for reference).
    ///     </para>
    /// </summary>
    /// <returns>The interval, represented as a <see cref="System.ValueTuple" />.</returns>
    (T? X0, T? X1) BoundaryInterval() => (LowerBoundary(), UpperBoundary());

    /// <summary>
    ///     Returns the membership function itself, represented as a <see cref="Func{T,TResult}" /> delegate.
    /// </summary>
    /// <returns>The membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    Func<T, double> SimpleFunction();

    /// <summary>
    ///     <para>
    ///         Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate, originating
    ///         from performing a Lambda-cut over the original membership function at the height point <i>y</i>,
    ///         represented as a <see cref="FuzzyNumber" />.
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
    /// <param name="y">The height point at which the Lambda-cut is performed, represented as a <see cref="FuzzyNumber" />.</param>
    /// <returns>The new membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    /// <seealso cref="SimpleFunction" />
    Func<T, double> LambdaCutFunction(FuzzyNumber y) => x =>
    {
        if (y == 0) return 0.0;
        if (y == 1) return SimpleFunction().Invoke(x);
        var (leftCut, rightCut) = LambdaCutInterval(y);
        return x.ToDouble(null) < leftCut || x.ToDouble(null) > rightCut ? SimpleFunction().Invoke(x) : y;
    };

    /// <summary>
    ///     <para>
    ///         Returns the membership degree, represented as a <see cref="FuzzyNumber" />, of the <i>x</i> value provided as a
    ///         parameter.
    ///     </para>
    ///     <para>
    ///         This method is equivalent to using the <see cref="Func{TResult}.Invoke" /> method on the resulting delegate
    ///         of the <see cref="SimpleFunction" /> method.
    ///     </para>
    /// </summary>
    /// <param name="x">The <i>x</i> value</param>
    /// <returns>The membership degree, represented as a <see cref="FuzzyNumber" /></returns>
    /// <seealso cref="SimpleFunction" />
    FuzzyNumber MembershipDegree(T x) => SimpleFunction().Invoke(x);

    /// <summary>
    ///     Returns the <i>x</i> value provided as a parameter and its membership degree <i>y</i> value as a two-dimensional
    ///     point, represented by a <see cref="System.ValueTuple" />.
    /// </summary>
    /// <param name="x">The <i>x</i> value.</param>
    /// <returns>
    ///     The <i>x</i> value and its membership degree <i>y</i> value as a two-dimensional point, represented by
    ///     a <see cref="System.ValueTuple" />.
    /// </returns>
    (T x, FuzzyNumber Y) ToPoint(T x) => (x, MembershipDegree(x));

    /// <summary>
    /// </summary>
    /// <param name="y"></param>
    /// <returns></returns>
    (double X1, double X2) LambdaCutInterval(FuzzyNumber y);

    (double X1, double X2) CrossoverCutInterval() => LambdaCutInterval(0.5);
}
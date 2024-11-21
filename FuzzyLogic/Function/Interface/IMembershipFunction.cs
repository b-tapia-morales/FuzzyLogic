using FuzzyLogic.Function.Real;
using FuzzyLogic.Number;
using static System.Math;

namespace FuzzyLogic.Function.Interface;

/// <summary>
/// <para>
/// Represents the Membership Function that allows the creation of linguistic entries that belong to a linguistic
/// variable.
/// </para>
/// <para>
/// Note that Membership Functions must not implement this class directly.
/// They must inherit from <see cref="MembershipFunction" /> instead.
/// </para>
/// </summary>
public interface IMembershipFunction
{
    /// <summary>
    /// Represents the smallest possible difference for which two <i>x</i> values are considered to represent
    /// the same <i>x</i>-coordinate.
    /// </summary>
    /// <remarks>
    /// Two <i>x</i> values are considered to represent the same <i>x</i>-coordinate
    /// if the absolute difference between equal to or below this threshold.
    /// This constant accounts for potential numerical inaccuracies or rounding errors
    /// during comparisons.
    /// </remarks>
    const double DeltaX = 1e-3;

    /// <summary>
    /// The name of the function.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// The maximum membership degree of the function.
    /// </summary>
    double UMax { get; }

    /// <summary>
    /// Determines whether the function is Open Left.
    /// A function is said to be open left if the membership degree for <i>x</i> values
    /// as <i>x</i> approaches -∞ and +∞ is 1 and 0 respectively.
    /// </summary>
    /// <returns>true if the function is Open Left; otherwise, false</returns>
    bool IsOpenLeft();

    /// <summary>
    /// Determines whether the function is Open Right.
    /// A function is said to be open right if the membership degree for
    /// <i>x</i> values as <i>x</i> approaches -∞ and +∞ is 0 and 1 respectively.
    /// </summary>
    /// <returns>true if the function is Open Right; otherwise, false</returns>
    bool IsOpenRight();

    /// <summary>
    /// Determines whether the function is Closed.
    /// A function is said to be closed if the membership degree for
    /// <i>x</i> values as <i>x</i> approaches both -∞ and +∞ is 0; in other words,
    /// if it is neither <see cref="IsOpenLeft">Open Left</see> nor <see cref="IsOpenRight">Open Right</see>.
    /// </summary>
    /// <returns>true if the function is Closed; otherwise, false</returns>
    bool IsClosed() => !IsOpenLeft() && !IsOpenRight();

    /// <summary>
    /// Determines whether the function is Symmetric.
    /// A function is said to be symmetric if μ(x + c) = μ(x - c), ∀x ∈ X when evaluated around a center point c.
    /// </summary>
    /// <returns>true if the function is Symmetric; otherwise, false</returns>
    bool IsSymmetric();

    /// <summary>
    /// Determines whether the function is Normal.
    /// A function is said to be normal if ∃x: μ(x) = 1; in other words,
    /// if there's at least one <i>x</i> value such that its membership degree equals to one.
    /// </summary>
    /// <returns>true if the function is Normal; otherwise, false</returns>
    bool IsNormal() => Abs(UMax - 1) < FuzzyNumber.Epsilon;

    bool IsSingleton();

    /// <summary>
    /// <para>
    /// Returns the leftmost <i>x</i> value of the Membership Function's support boundary
    /// (that is, the region of the universe that is characterized by nonzero membership: 0 &lt; μ(X) &lt; 1).
    /// </para>
    /// <para>
    /// In case that the function is <see cref="IsOpenLeft"/>,
    /// the value will be <see cref="Double.NegativeInfinity" />.
    /// In case that the leftmost value asymptotically approaches zero,
    /// the <i>x</i> value will be x: |μ(x) - <see cref="FuzzyNumber.Epsilon"/>| &#8805; 0
    /// </para>
    /// </summary>
    /// <returns>
    /// The minimum value allowed for an <i>x</i> value that belongs to the Membership Function's support boundary.
    /// </returns>
    double SupportLeft();

    /// <summary>
    /// <para>
    /// Returns the rightmost <i>x</i> value of the Membership Function's support boundary
    /// (that is, the region of the universe that is characterized by nonzero membership: 0 &lt; μ(X) &lt; 1).
    /// </para>
    /// <para>
    /// In case that the function is <see cref="IsOpenRight"/>,
    /// the value will be <see cref="Double.PositiveInfinity" />.
    /// In case that the rightmost value asymptotically approaches zero,
    /// the <i>x</i> value will be x: |μ(x) - <see cref="FuzzyNumber.Epsilon"/>| &#8805; 0
    /// </para>
    /// </summary>
    /// <returns>
    /// The maximum value allowed for an <i>x</i> value that belongs to the Membership Function's support boundary.
    /// </returns>
    double SupportRight();

    /// <summary>
    /// Returns the leftmost and rightmost values of the support boundary as an interval,
    /// represented by a <see cref="ValueTuple" />.
    /// </summary>
    /// <returns>The interval, represented as a <see cref="ValueTuple" />.</returns>
    (double X0, double X1) SupportBoundary() => (SupportLeft(), SupportRight());

    (double X0, double X1) FiniteSupportBoundary();

    /// <summary>
    /// <para>
    /// Returns the leftmost <i>x</i> value of the Membership Function's core boundary
    /// (that is, the region of the universe that where the membership degree reaches its maximum value).
    /// </para>
    /// <para>
    /// In case that the function is <see cref="IsOpenLeft"/> and monotonically decreasing,
    /// the value will be x: |μ(x) - <see cref="UMax"/>| &lt; <see cref="FuzzyNumber.Epsilon"/>.
    /// </para>
    /// </summary>
    /// <returns>
    /// The minimum value allowed for an <i>x</i> value that belongs to the Membership Function's core boundary.
    /// </returns>
    double? CoreLeft();

    /// <summary>
    /// <para>
    /// Returns the rightmost <i>x</i> value of the Membership Function's core boundary
    /// (that is, the region of the universe that where the membership degree reaches its maximum value).
    /// </para>
    /// <para>
    /// In case that the function is <see cref="IsOpenRight"/> and monotonically increasing,
    /// the value will be x: |μ(x) - <see cref="UMax"/>| &lt; <see cref="FuzzyNumber.Epsilon"/>.
    /// </para>
    /// </summary>
    /// <returns>
    /// The minimum value allowed for an <i>x</i> value that belongs to the Membership Function's core boundary.
    /// </returns>
    double? CoreRight();

    /// <summary>
    /// Returns the leftmost and rightmost values of the core boundary as an interval,
    /// represented by a <see cref="ValueTuple" />.
    /// </summary>
    /// <returns>The interval, represented as a <see cref="ValueTuple" />.</returns>
    (double? X0, double? X1) CoreBoundary() => (CoreLeft(), CoreRight());

    double? AlphaCutLeft(FuzzyNumber cut);

    double? AlphaCutRight(FuzzyNumber cut);

    (double? X0, double? X1) AlphaCutBoundary(FuzzyNumber cut) => (AlphaCutLeft(cut), AlphaCutRight(cut));

    double? LeftBandwidth() => AlphaCutLeft(0.5);

    double? RightBandwidth() => AlphaCutRight(0.5);

    (double? X0, double? X1) BandwidthBoundary => (LeftBandwidth(), RightBandwidth());

    /// <summary>
    /// Returns the membership function itself, represented as a <see cref="Func{T,TResult}" /> delegate.
    /// </summary>
    /// <returns>The membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    Func<double, double> PureFunction() => LarsenProduct(UMax);

    /// <summary>
    /// <para>
    /// Returns a new membership function generated by performing the Larsen Product
    /// over the original membership function with the scaling factor <i>λ</i>,
    /// represented by a <see cref="FuzzyNumber"/>.
    /// The resulting function is represented as a <see cref="Func{T,TResult}" /> delegate.
    /// </para>
    /// <para>
    /// The Larsen Product modifies the original membership function
    /// by scaling down each membership degree proportionally by the specified scaling factor <i>λ</i>,
    /// resulting in a new membership function: <c>μ'(x) = λ * μ(x)</c>.
    /// </para>
    /// <list type="bullet">
    /// <listheader>Special cases:</listheader>
    /// <item>
    /// <description><i>λ ≈ 0</i> ⇒ The zero function.</description>
    /// </item>
    /// <item>
    /// <description><i>λ ≈ μMax</i> ⇒ The original membership function, unmodified.</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="lambda">
    /// The cut value at which the horizontal cut is performed, represented as a <see cref="FuzzyNumber" />.
    /// </param>
    /// <returns>
    /// A new membership function represented as a <see cref="Func{T,TResult}" /> delegate,
    /// which performs a horizontal cut over the original membership function at the given cut value <i>λ</i>.
    /// </returns>
    /// <seealso cref="IMembershipFunction.PureFunction" />
    Func<double, double> LarsenProduct(FuzzyNumber lambda);

    /// <summary>
    /// <para>
    /// Returns a new membership function generated by performing the Mamdani Minimum
    /// over the original membership function at the cut value <i>α</i>,
    /// represented by a <see cref="FuzzyNumber"/>.
    /// The resulting function is represented as a <see cref="Func{T,TResult}" /> delegate.
    /// </para>
    /// <para>
    /// The Mamdani Minimum performs a horizontal cut at the specified cut value <i>α</i>,
    /// generating a new function that either evaluates to the original membership function
    /// or the cut value itself depending on whether <i>x</i> is outside or inside the cut value's range,
    /// respectively.
    /// </para>
    /// <list type="bullet">
    /// <listheader>Special cases:</listheader>
    /// <item>
    /// <description><i>α ≈ 0</i> ⇒ The zero function.</description>
    /// </item>
    /// <item>
    /// <description><i>α ≈ μMax</i> ⇒ The original membership function, unmodified.</description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="alpha">
    /// The cut value at which the horizontal cut is performed, represented as a <see cref="FuzzyNumber" />.
    /// </param>
    /// <returns>
    /// A new membership function represented as a <see cref="Func{T,TResult}" /> delegate,
    /// which performs a horizontal cut over the original membership function at the given cut value <i>α</i>.
    /// </returns>
    /// <seealso cref="IMembershipFunction.PureFunction" />
    Func<double, double> MamdaniMinimum(FuzzyNumber alpha) => PrecomputedMamdaniMinimum(this, alpha);

    /// <summary>
    /// <para>
    /// Returns the membership degree, represented as a <see cref="FuzzyNumber" />, of the <i>x</i> value provided as a
    /// parameter.
    /// </para>
    /// <para>
    /// This method is equivalent to using the <see cref="Func{TResult}.Invoke" /> method on the resulting delegate
    /// of the <see cref="PureFunction" /> method.
    /// </para>
    /// </summary>
    /// <param name="x">The <i>x</i> value</param>
    /// <returns>The membership degree, represented as a <see cref="FuzzyNumber" /></returns>
    /// <seealso cref="PureFunction" />
    FuzzyNumber MembershipDegree(double x) => PureFunction()(x);

    /// <summary>
    /// Returns the <i>x</i> value provided as a parameter and its membership degree <i>y</i> value as a two-dimensional
    /// point, represented by a <see cref="ValueTuple" />.
    /// </summary>
    /// <param name="x">The <i>x</i> value.</param>
    /// <returns>
    /// The <i>x</i> value and its membership degree <i>y</i> value as a two-dimensional point, represented by
    /// a <see cref="ValueTuple" />.
    /// </returns>
    (double x, FuzzyNumber Y) ToPoint(double x) => (x, MembershipDegree(x));

    private static Func<double, double> PrecomputedMamdaniMinimum(IMembershipFunction function, FuzzyNumber alpha)
    {
        if (Abs(alpha.Value) <= FuzzyNumber.Epsilon)
            return _ => 0;
        if (alpha.Value >= function.UMax)
            return function.PureFunction();
        var leftMost = function.AlphaCutLeft(alpha)!.Value;
        var rightMost = function.AlphaCutRight(alpha)!.Value;
        return x => x <= leftMost && x >= rightMost ? alpha.Value : function.PureFunction()(x);
    }
}
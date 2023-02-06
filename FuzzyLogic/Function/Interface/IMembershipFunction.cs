using System.Numerics;
using FuzzyLogic.Function.Base;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Interface;

/// <summary>
///     <para>
///         Represents the Membership Function that allows the creation of linguistic entries that belong to a linguistic
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

    T H { get; }

    /// <summary>
    ///     Determines whether the function is Open Left. A function is said to be open left if the membership degree for
    ///     <i>x</i> values as <i>x</i> approaches -∞ and +∞ is 1 and 0 respectively.
    /// </summary>
    /// <returns>true if the function is Open Left; otherwise, false</returns>
    bool IsOpenLeft();

    /// <summary>
    ///     Determines whether the function is Open Right. A function is said to be open right if the membership degree for
    ///     <i>x</i> values as <i>x</i> approaches -∞ and +∞ is 0 and 1 respectively.
    /// </summary>
    /// <returns>true if the function is Open Right; otherwise, false</returns>
    bool IsOpenRight();

    /// <summary>
    ///     Determines whether the function is Closed. A function is said to be closed if the membership degree for
    ///     <i>x</i> values as <i>x</i> approaches both -∞ and +∞ is 0; in other words, if it is neither
    ///     <see cref="IsOpenLeft">Open Left</see> nor <see cref="IsOpenRight">Open Right</see>.
    /// </summary>
    /// <returns>true if the function is Closed; otherwise, false</returns>
    bool IsClosed() => !IsOpenLeft() && !IsOpenRight();

    /// <summary>
    ///     Determines whether the function is Symmetric. A function is said to be symmetric if μ(x + c) = μ(x - c), ∀x ∈ X
    ///     when evaluated around a center point c.
    /// </summary>
    /// <returns>true if the function is Symmetric; otherwise, false</returns>
    bool IsSymmetric();

    /// <summary>
    ///     Determines whether the function is Normal. A function is said to be normal if ∃x: μ(x) = 1; in other words, if
    ///     there's at least one <i>x</i> value such that its membership degree equals to one.
    /// </summary>
    /// <returns>true if the function is Normal; otherwise, false</returns>
    bool IsNormal() => H == T.One;

    bool IsSingleton();

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
    T LeftSupportEndpoint();

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
    T RightSupportEndpoint();

    /// <summary>
    ///     <para>
    ///         Returns the minimum and maximum values allowed for an <i>x</i> value that belongs to the support of the
    ///         Membership Function as an interval, represented as a <see cref="ValueTuple" />.
    ///     </para>
    ///     <para>
    ///         The check for the existence of such values is delegated to the consumer of the class (see
    ///         <see cref="LeftSupportEndpoint" /> and <see cref="RightSupportEndpoint" /> for reference).
    ///     </para>
    /// </summary>
    /// <returns>The interval, represented as a <see cref="ValueTuple" />.</returns>
    (T X0, T X1) SupportInterval() => (LeftSupportEndpoint(), RightSupportEndpoint());

    T MaxHeightLeftEndpoint();

    T MaxHeightRightEndpoint();

    (T X0, T X1) MaxHeightInterval() => (MaxHeightLeftEndpoint(), MaxHeightRightEndpoint());

    (T X0, T X1) ClosedInterval() => ClosedInterval(this);

    /// <summary>
    ///     Returns the membership function itself, represented as a <see cref="Func{T,TResult}" /> delegate.
    /// </summary>
    /// <returns>The membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    Func<T, double> AsFunction();

    Func<T, double> HeightFunction<TNumber>(TNumber y) where TNumber : struct, IFuzzyNumber<TNumber>;

    /// <summary>
    ///     <para>
    ///         Returns the membership degree, represented as a <see cref="FuzzyNumber" />, of the <i>x</i> value provided as a
    ///         parameter.
    ///     </para>
    ///     <para>
    ///         This method is equivalent to using the <see cref="Func{TResult}.Invoke" /> method on the resulting delegate
    ///         of the <see cref="AsFunction" /> method.
    ///     </para>
    /// </summary>
    /// <param name="x">The <i>x</i> value</param>
    /// <returns>The membership degree, represented as a <see cref="FuzzyNumber" /></returns>
    /// <seealso cref="AsFunction" />
    TNumber MembershipDegree<TNumber>(T x) where TNumber : struct, IFuzzyNumber<TNumber> =>
        AsFunction().Invoke(x);

    /// <summary>
    ///     Returns the <i>x</i> value provided as a parameter and its membership degree <i>y</i> value as a two-dimensional
    ///     point, represented by a <see cref="ValueTuple" />.
    /// </summary>
    /// <param name="x">The <i>x</i> value.</param>
    /// <returns>
    ///     The <i>x</i> value and its membership degree <i>y</i> value as a two-dimensional point, represented by
    ///     a <see cref="ValueTuple" />.
    /// </returns>
    (T x, TNumber Y) ToPoint<TNumber>(T x) where TNumber : struct, IFuzzyNumber<TNumber> =>
        (x, MembershipDegree<TNumber>(x));

    private static (T X0, T X1) ClosedInterval(IMembershipFunction<T> function)
    {
        return function is IAsymptoteFunction<T> asymptoteFunction
            ? asymptoteFunction.ApproximateBoundaryInterval()
            : function.SupportInterval();
    }
}
namespace FuzzyLogic.MembershipFunctions;

/// <summary>
/// <para>Represents the Membership Function that allows the creation of linguistic values that belong to a linguistic
/// variable.</para>
/// <para>Note that Membership Functions must not implement this class directly. They must inherit from
/// <see cref="BaseMembershipFunction{T}"/> instead.</para>
/// </summary>
/// <typeparam name="T">The type must be either <see cref="int"/> or <see cref="double"/> (the reason behind it being
/// defined as <see langword="unmanaged"/>). Support for other types other than the strictly aforementioned is not
/// allowed.</typeparam>
public interface IMembershipFunction<T> where T : unmanaged, IConvertible
{
    /// <summary>
    /// The name of the function.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// <para>Returns the minimum value allowed for an <i>x</i> value that belongs to the support of the Membership
    /// Function (that is, the region of the universe that is characterized by nonzero membership: 0 &lt; μ(X) &lt; 1).
    /// </para>
    /// <para>Note that the nullability represents the existence of such value, and, as such, is not a replacement
    /// for mathematical concepts such as <see cref="Double.PositiveInfinity"/>, <see cref="Double.NegativeInfinity"/>
    /// or <see cref="Double.NaN"/>. In case there's a need to use such concepts, this method needs to be overriden by
    /// the implementing class.</para>
    /// </summary>
    /// <returns>The minimum value allowed for an <i>x</i> value that belongs to the support of the Membership Function.
    /// </returns>
    T? LowerBoundary() => null;

    /// <summary>
    /// <para>Returns the maximum value allowed for an <i>x</i> value that belongs to the support of the Membership
    /// Function (that is, the region of the universe that is characterized by nonzero membership: 0 &lt; μ(X) &lt; 1).
    /// </para>
    /// <para>Note that the nullability represents the existence of such value, and, as such, is not a replacement
    /// for mathematical concepts such as <see cref="Double.PositiveInfinity"/>, <see cref="Double.NegativeInfinity"/>
    /// or <see cref="Double.NaN"/>. In case there's a need to use such concepts, this method needs to be overriden by
    /// the implementing class.</para>
    /// </summary>
    /// <returns>The maximum value allowed for an <i>x</i> value that belongs to the support of the Membership Function.
    /// </returns>
    T? UpperBoundary() => null;

    /// <summary>
    /// <para>Returns the minimum and maximum values allowed for an <i>x</i> value that belongs to the support of the
    /// Membership Function as an interval, represented as a <see cref="System.ValueTuple"/>.</para>
    /// <para>The check for the existence of such values is delegated to the consumer of the class (see
    /// <see cref="LowerBoundary()"/> and <see cref="UpperBoundary()"/> for reference.</para>
    /// </summary>
    /// <returns>The interval, represented as a <see cref="System.ValueTuple"/>.</returns>
    (T? X0, T? X1) BoundaryInterval() => (LowerBoundary(), UpperBoundary());

    /// <summary>
    /// Returns the membership degree of the <i>x</i> value provided as a parameter as a <see cref="FuzzyNumber"/>
    /// </summary>
    /// <param name="x">The <i>x</i> value</param>
    /// <returns>Its membership degree as a <see cref="FuzzyNumber"/></returns>
    FuzzyNumber MembershipDegree(T x);

    /// <summary>
    /// Returns the <i>x</i> value provided as a parameter and its membership degree as a two-dimensional point,
    /// represented by a <see cref="System.ValueTuple"/>.
    /// </summary>
    /// <param name="x">The <i>x</i> value.</param>
    /// <returns>The <i>x</i> value and its membership degree as a two-dimensional point, represented by a
    /// <see cref="System.ValueTuple"/>.</returns>
    (T x, FuzzyNumber Y) ToPoint(T x) => (x, MembershipDegree(x));
}
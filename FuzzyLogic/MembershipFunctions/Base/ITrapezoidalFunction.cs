namespace FuzzyLogic.MembershipFunctions;

/// <summary>
/// <para>Represents the special case of Trapezoidal functions that derive from the <see cref="IMembershipFunction{T}"/>
/// base class.</para>
/// <para>Note that Trapezoidal Functions must not implement this class directly. They must inherit from
/// <see cref="BaseTrapezoidalFunction{T}"/> instead.</para>
/// </summary>
/// <typeparam name="T">The type must be either <see cref="int"/> or <see cref="double"/>, as originally defined
/// in <see cref="IMembershipFunction{T}"/>.</typeparam>
public interface ITrapezoidalFunction<T> : IMembershipFunction<T> where T : unmanaged, IConvertible
{
    /// <summary>
    /// Returns the minimum and maximum for <i>x</i> values that belong to the core of the Membership Function
    /// (that is, the region of the universe that is characterized by full membership: μ(X) = 1) as an interval,
    /// represented as a <see cref="System.ValueTuple"/>.
    /// </summary>
    /// <returns>The interval, represented as a <see cref="System.ValueTuple"/>.</returns>
    (T X0, T X1) CoreInterval();

    /// <summary>
    /// Returns the minimum and maximum for <i>x</i> values that belong to left side of the support of the Membership
    /// Function (that is, the region of the universe to the left of the core that is characterized by nonzero
    /// membership: 0 &lt; μ(X) &lt; 1) as an interval, represented as a <see cref="System.ValueTuple"/>.
    /// </summary>
    /// <returns>The interval, represented as a <see cref="System.ValueTuple"/>.</returns>
    (T? X0, T X1) LeftSupportInterval();
    
    /// <summary>
    /// Returns the minimum and maximum for <i>x</i> values that belong to right side of the support of the Membership
    /// Function (that is, the region of the universe to the left of the core that is characterized by nonzero
    /// membership: 0 &lt; μ(X) &lt; 1) as an interval, represented as a <see cref="System.ValueTuple"/>.
    /// </summary>
    /// <returns>The interval, represented as a <see cref="System.ValueTuple"/>.</returns>
    (T X0, T? X1) RightSupportInterval();
}
using System.Collections;
using System.Globalization;

// ReSharper disable HeapView.PossibleBoxingAllocation

namespace FuzzyLogic.Number;

/// <summary>
///     <para>
///         Embodies the core mathematical concepts that define a membership degree, such as its truth value range
///         μ(x) ∈ [0, 1]; its basic operations - A ∧ B, A ∨ B, ¬A ⇒ 1 - A, and others.
///     </para>
///     <para>
///         In terms of its actual implementation, it's merely a wrapper class of a <see cref="double" /> value, on
///         which the aforementioned operators are applied.
///     </para>
/// </summary>
public interface IFuzzyNumber : IComparable, IComparer, IEqualityComparer, IEquatable
{
    /// <summary>
    /// <para>
    /// Represents the smallest possible difference for which x comparison between two Fuzzy Numbers yields
    /// equality.
    /// In other words, two fuzzy numbers are considered to be equal
    /// if the difference between them is below this threshold.
    /// </para>
    /// <para>This field is constant.</para>
    /// </summary>
    public const double Tolerance = 1e-5;

    /// <summary>
    /// The property for the <see cref="double" /> value on which the <see cref="IFuzzyNumber" /> operates.
    /// </summary>
    double Value { get; }

    /// <summary>
    ///     <para>
    ///         Creates an instance of a Fuzzy Number. The <see cref="double" /> value must be in the range μ(x) ∈
    ///         [0, 1]; otherwise, an <exception cref="ArgumentException" /> will be thrown.
    ///     </para>
    ///     <para>
    ///         <b>Special case</b>: |0 - μ(x)| &lt; <see cref="Tolerance" /> ∨ |1 - μ(x)| &lt; <see cref="Tolerance" />
    ///         ⇒ Defaults to either 0 or 1 respectively.
    ///     </para>
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <returns>An instance of a Fuzzy Number.</returns>
    /// <exception cref="ArgumentException">Thrown if the value is not in the range μ(X) ∈ [0, 1].</exception>
    static abstract IFuzzyNumber Of(double value);

    /// <summary>
    ///     <para>Creates an instance of a Fuzzy Number in a failsafe way.</para>
    ///     <para>μ(x) &lt; 0 ⇒ 0; conversion is unsuccessful.</para>
    ///     <para>μ(x) &gt; 1 ⇒ 1; conversion is unsuccessful.</para>
    ///     <para>0 ≤ μ(x) ≤ 1 ⇒ The <see cref="double" /> value itself; conversion is successful.</para>
    ///     <para>
    ///         <b>Special case</b>: |0 - μ(x)| &lt; <see cref="Tolerance" /> ∨ |1 - μ(x)| &lt; <see cref="Tolerance" /> ⇒
    ///         Defaults to either 0 or 1 respectively; conversion is successful.
    ///     </para>
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <param name="number">An instance of a Fuzzy Number</param>
    /// <returns><see langword="true" /> if the conversion is successful; <see langword="false" />, otherwise.</returns>
    static abstract bool TryCreate(double value, out IFuzzyNumber number);

    /// <summary>
    ///     Represents a Fuzzy Number's smallest possible value.
    /// </summary>
    /// <returns>A Fuzzy Number's smallest possible value.</returns>
    static abstract IFuzzyNumber MinValue();

    /// <summary>
    ///     Represents a Fuzzy Number's biggest possible value.
    /// </summary>
    /// <returns>A Fuzzy Number's biggest possible value.</returns>
    static abstract IFuzzyNumber MaxValue();

    /// <summary>
    /// Represents the negation operation NOT: ¬A.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the NOT operator.</returns>
    static abstract IFuzzyNumber operator !(IFuzzyNumber x);

    /// <summary>
    /// Represents the T-Conorm OR: A ∨ B.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <param name="y">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the OR operator.</returns>
    static abstract IFuzzyNumber operator &(IFuzzyNumber x, IFuzzyNumber y);

    /// <summary>
    /// Represents the T-Norm AND: A ∧ B.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <param name="y">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the AND operator.</returns>
    static abstract IFuzzyNumber operator |(IFuzzyNumber x, IFuzzyNumber y);

    /// <summary>
    /// Represents the residuum operation THEN: A ⇒ B.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <param name="y">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the THEN operator.</returns>
    static abstract IFuzzyNumber Then(IFuzzyNumber x, IFuzzyNumber y);

    static abstract bool operator ==(IFuzzyNumber x, IFuzzyNumber y);

    static abstract bool operator !=(IFuzzyNumber x, IFuzzyNumber y);

    public static virtual bool operator <(IFuzzyNumber x, IFuzzyNumber y) => x != y && x.Value < y.Value;

    public static virtual bool operator <=(IFuzzyNumber x, IFuzzyNumber y) => x == y || x.Value < y.Value;

    public static virtual bool operator >(IFuzzyNumber x, IFuzzyNumber y) => x != y && x.Value > y.Value;

    public static virtual bool operator >=(IFuzzyNumber x, IFuzzyNumber y) => x == y || x.Value > y.Value;

    /// <summary>
    /// Defines an implicit conversion from a <see cref="IFuzzyNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="IFuzzyNumber" /> value.</returns>
    public static virtual implicit operator double(IFuzzyNumber x) => x.Value;

    /// <summary>
    /// Defines an implicit conversion from a <see cref="double" /> value to a <see cref="IFuzzyNumber" />.
    /// Note that this value must be in the range μ(x) ∈ [0, 1].
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="IFuzzyNumber" /> value.</returns>
    public static virtual implicit operator IFuzzyNumber(double x) => IFuzzyNumber.Of(x);

    bool IEqualityComparer<IFuzzyNumber>.Equals(IEquatable<IFuzzyNumber>? x, IEquatable<IFuzzyNumber>? y)
    {
        if (object.Equals(x, default(IFuzzyNumber)) && object.Equals(y, default(IFuzzyNumber)))
            return true;
        if (object.Equals(x, default(IFuzzyNumber)) || object.Equals(y, default(IFuzzyNumber)))
            return false;
        return x == y;
    }

    bool IEquatable<IFuzzyNumber>.Equals(IEquatable<IFuzzyNumber>? other) => Equals(this, other);

    int IEqualityComparer<IFuzzyNumber>.GetHashCode(IEquatable<IFuzzyNumber>? obj) => (obj ?? 0.0).GetHashCode();

    int IComparer<IFuzzyNumber>.Compare(IEquatable<IFuzzyNumber>? x, IEquatable<IFuzzyNumber>? y)
    {
        if (object.Equals(x, default(IFuzzyNumber)) && object.Equals(y, default(IFuzzyNumber)))
            return 0;
        if (object.Equals(x, default(IFuzzyNumber)))
            return -1;
        if (object.Equals(y, default(IFuzzyNumber)))
            return +1;
        return x!.Value.CompareTo(y!.Value);
    }

    int IComparable<IFuzzyNumber>.CompareTo(IFuzzyNumber? other) => other == null ? 1 : Value.CompareTo(other.Value);

    string ToString() => Value.ToString(CultureInfo.InvariantCulture);

    internal protected static void RangeCheck(double value)
    {
        if (value is < 0 or > 1)
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
    }
}
using System.Globalization;

namespace FuzzyLogic;

/// <summary>
///     <para>
///         Embodies the core mathematical concepts that define a membership degree, such as: its truth value range
///         μ(x) ∈ [0, 1]; its basic operations - A ∧ B ⇒ Min(A, B), A ∨ B ⇒ Max(A, B), ¬A ⇒ 1 - A, and others.
///     </para>
///     <para>
///         In terms of its actual implementation, it's merely a wrapper class of a <see cref="double" /> value, on
///         which the aforementioned operators are applied.
///     </para>
/// </summary>
public sealed class FuzzyNumber : IComparable<FuzzyNumber>, IEquatable<FuzzyNumber>
{
    /// <summary>
    ///     <para>
    ///         Represents the smallest possible difference for which a comparison between two Fuzzy Numbers yields
    ///         equality. In other words, two fuzzy numbers are considered to be equal if the difference between them
    ///         is below this threshold.
    ///     </para>
    ///     <para>This field is constant.</para>
    /// </summary>
    public const double Tolerance = 1e-5;

    /// <summary>
    ///     Represents the smallest possible value of a Fuzzy Number.
    /// </summary>
    public static readonly FuzzyNumber MinValue = Of(0);

    /// <summary>
    ///     Represents the largest possible value of a Fuzzy Number.
    /// </summary>
    public static readonly FuzzyNumber MaxValue = Of(1);

    /// <summary>
    ///     The base constructor of a Fuzzy Number. To create instances of a Fuzzy Number, <see cref="Of(double)" />
    ///     should be used instead.
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    private FuzzyNumber(double value) => Value = value;

    /// <summary>
    ///     The property for the <see cref="double" /> value on which the <see cref="FuzzyNumber" /> operates.
    /// </summary>
    public double Value { get; }

    /// <summary>
    ///     <para>
    ///         Creates an instance of a Fuzzy Number. The <see cref="double" /> value must be in the range μ(x) ∈
    ///         [0, 1]; otherwise, an <exception cref="ArgumentException" /> will be thrown.
    ///     </para>
    ///     <para>
    ///         <b>Special case</b>: |0 - μ(x)| &lt; <see cref="Tolerance"/> ∨ |1 - μ(x)| &lt; <see cref="Tolerance"/>
    ///         ⇒ Defaults to either 0 or 1 respectively.
    ///     </para>
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <returns>A new instance of a Fuzzy Number.</returns>
    /// <exception cref="ArgumentException">Thrown if the value is not in the range μ(X) ∈ [0, 1].</exception>
    public static FuzzyNumber Of(double value)
    {
        if (Math.Abs(0 - value) < Tolerance) return 0;
        if (Math.Abs(1 - value) < Tolerance) return 1;
        RangeCheck(value);
        return value;
    }

    /// <summary>
    ///     <para>Creates an instance of a Fuzzy Number in a failsafe way.</para>
    ///     <para>μ(x) &lt; 0 ⇒ 0; conversion is unsuccessful.</para>
    ///     <para>μ(x) &gt; 1 ⇒ 1; conversion is unsuccessful.</para>
    ///     <para>0 ≤ μ(x) ≤ 1 ⇒ The <see cref="double" /> value itself; conversion is successful.</para>
    ///     <para>
    ///         <b>Special case</b>: |0 - μ(x)| &lt; <see cref="Tolerance"/> ∨ |1 - μ(x)| &lt; <see cref="Tolerance"/> ⇒
    ///         Defaults to either 0 or 1 respectively; conversion is successful.
    ///     </para>
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <param name="fuzzyNumber">An instance of a Fuzzy Number</param>
    /// <returns><see langword="true" /> if the conversion is successful; <see langword="false" />, otherwise.</returns>
    public static bool TryCreate(double value, out FuzzyNumber fuzzyNumber)
    {
        try
        {
            fuzzyNumber = Of(value);
            return true;
        }
        catch (ArgumentException)
        {
            fuzzyNumber = value < 0 ? Math.Max(0, value) : Math.Min(1, value);
            return false;
        }
    }

    /// <summary>
    ///     Represents the basic operation OR: A ∨ B ⇒ Max{μA(x), μB(x)}.
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the OR operator.</returns>
    public static FuzzyNumber operator |(FuzzyNumber a, FuzzyNumber b) => new(Math.Max(a.Value, b.Value));

    /// <summary>
    ///     Represents the basic operation AND: A ∧ B ⇒ Min{μA(x), μB(x)}.
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the AND operator.</returns>
    public static FuzzyNumber operator &(FuzzyNumber a, FuzzyNumber b) => new(Math.Min(a.Value, b.Value));

    /// <summary>
    ///     Represents the basic operation NOT: ¬A ⇒ 1 - A.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the NOT operator.</returns>
    public static FuzzyNumber operator !(FuzzyNumber x) => new(1 - x.Value);

    /// <summary>
    ///     Represents the sum operation: A + B ⇒ μA(x) + μB(x) - μA(x) ∙ μB(x)
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the sum operation.</returns>
    public static FuzzyNumber operator +(FuzzyNumber a, FuzzyNumber b) => new(a.Value + b.Value - a.Value * b.Value);

    /// <summary>
    ///     Represents the difference operation: A - B ⇒ A ∧ ¬B
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the difference operation.</returns>
    public static FuzzyNumber operator -(FuzzyNumber a, FuzzyNumber b) => a & !b;

    /// <summary>
    ///     Represents the scalar product operation: α × A ⇒ α ∙ μA(x)
    /// </summary>
    /// <param name="a">A double number</param>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the scalar product operation.</returns>
    public static FuzzyNumber operator *(double a, FuzzyNumber x) => ScalarProduct(a, x);

    /// <summary>
    ///     Represents the scalar product operation: α × A ⇒ α ∙ μA(x)
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <param name="a">A double number</param>
    /// <returns>The resulting fuzzy number after applying the scalar product operation.</returns>
    public static FuzzyNumber operator *(FuzzyNumber x, double a) => ScalarProduct(a, x);

    /// <summary>
    ///     Represents the bounded sum operation: |μA(x) ⊕ μB(x)| ⇒ Min{1, μA(x) + μB(x)}
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the disjunctive sum operation.</returns>
    public static FuzzyNumber BoundedSum(FuzzyNumber a, FuzzyNumber b) => new(Math.Min(1, a.Value + b.Value));

    /// <summary>
    ///     Represents the bounded difference operation: |μA(x) ⊖ μB(x)| ⇒ Max{0, μA(x) - μB(x)}
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the disjunctive sum operation.</returns>
    public static FuzzyNumber BoundedDifference(FuzzyNumber a, FuzzyNumber b) =>
        new(Math.Max(0, a.Value + b.Value - 1));

    /// <summary>
    ///     Represents the disjunctive sum operation: A ⊕ B ⇒ (¬A ∧ B) ∨ (A ∧ ¬B)
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the disjunctive sum operation.</returns>
    public static FuzzyNumber DisjunctiveSum(FuzzyNumber a, FuzzyNumber b) => (!a & b) | (a & !b);

    public static bool operator <(FuzzyNumber a, FuzzyNumber b) => a != b && a.Value < b.Value;

    public static bool operator <=(FuzzyNumber a, FuzzyNumber b) => a == b || a.Value <= b.Value;

    public static bool operator >(FuzzyNumber a, FuzzyNumber b) => a != b && a.Value > b.Value;

    public static bool operator >=(FuzzyNumber a, FuzzyNumber b) => a == b || a.Value >= b.Value;

    /// <summary>
    ///     Represents the equals operator. Two fuzzy numbers are considered to be equal if the difference between their
    ///     values is below the threshold defined by the <see cref="Tolerance"/>.
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>
    ///     <see langword="true" /> if the difference between their values is below the threshold;
    ///     <see langword="false" />, otherwise.
    /// </returns>
    public static bool operator ==(FuzzyNumber a, FuzzyNumber b) => Math.Abs(a.Value - b.Value) < Tolerance;

    public static bool operator !=(FuzzyNumber a, FuzzyNumber b) => !(a == b);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="double" /> value to a <see cref="FuzzyNumber" />. Note that
    ///     this value must be in the range μ(x) ∈ [0, 1] (see <see cref="FuzzyNumber(double)" /> for reference).
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="FuzzyNumber" /> value.</returns>
    public static implicit operator FuzzyNumber(double x) => new(x);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="FuzzyNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns>The <see cref="FuzzyNumber" /> value.</returns>
    public static implicit operator double(FuzzyNumber x) => x.Value;

    /// <summary>
    ///     <para>Defines a implicit conversion from a <see cref="bool" /> to a <see cref="FuzzyNumber" /> value.</para>
    ///     <para>
    ///         Note that this operation is unidirectional. There is no possible conversion from a Fuzzy Number whose
    ///         <see cref="FuzzyNumber.Value" /> isn't either 0 or 1 to a Boolean.
    ///     </para>
    /// </summary>
    /// <param name="b">The <see cref="bool" /> value.</param>
    /// <returns>The <see cref="FuzzyNumber" /> value.</returns>
    public static implicit operator FuzzyNumber(bool b) => new(b ? 1.0 : 0.0);

    /// <inheritdoc />
    public bool Equals(FuzzyNumber? other) => Value.Equals(other?.Value ?? 0);

    /// <inheritdoc />
    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || (obj is FuzzyNumber other && Equals(other));

    /// <inheritdoc />
    public override int GetHashCode() => Value.GetHashCode();

    /// <inheritdoc />
    public int CompareTo(FuzzyNumber? other) => Value.CompareTo(other?.Value ?? 0);

    /// <inheritdoc />
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);

    private static FuzzyNumber ScalarProduct(double a, FuzzyNumber x)
    {
        if (Math.Abs(0 - a) < Tolerance) return 0;
        if (Math.Abs(1 - a) < Tolerance) return x;
        RangeCheck(a);
        return a * x.Value;
    }

    private static void RangeCheck(double value)
    {
        if (value is < 0.0 or > 1.0)
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
    }
}
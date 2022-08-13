using System.Globalization;

namespace FuzzyLogic;

/// <summary>
///     <param>
///         Embodies the core mathematical concepts that define a membership degree, such as: its truth value range
///         μ(X) ∈ [0, 1]; its basic operations - A ∧ B ⇒ Min(A, B), A ∨ B ⇒ Max(A, B), ¬A ⇒ 1 - A, and others (to be
///         added later if needed).
///     </param>
///     <param>
///         In terms of its actual implementation, it's merely a wrapper class of a <see cref="double" /> value, on
///         which the aforementioned operators are applied.
///     </param>
/// </summary>
public sealed class FuzzyNumber : IComparable<FuzzyNumber>, IEquatable<FuzzyNumber>
{
    /// <summary>
    ///     <param>
    ///         Represents the smallest possible difference for which a comparison between two Fuzzy Numbers yields
    ///         equality. In other words, two fuzzy numbers are considered to be equal if the difference between them
    ///         is below this threshold.
    ///     </param>
    ///     <param>This field is constant.</param>
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
    private FuzzyNumber(double value)
    {
        Value = value;
    }

    /// <summary>
    ///     The property for the <see cref="double" /> value on which the <see cref="FuzzyNumber" /> operates.
    /// </summary>
    public double Value { get; }

    /// <summary>
    ///     Creates an instance of a Fuzzy Number. The <see cref="double" /> value must be in the range μ(X) ∈ [0, 1],
    ///     otherwise, an <exception cref="ArgumentException" /> will be thrown.
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if the value is not in the range μ(X) ∈ [0, 1].</exception>
    public static FuzzyNumber Of(double value)
    {
        if (Math.Abs(0 - value) < Tolerance) return new FuzzyNumber(0);
        if (Math.Abs(1 - value) < Tolerance) return new FuzzyNumber(1);
        if (value is < 0.0 or > 1.0)
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");

        return new FuzzyNumber(value);
    }

    /// <summary>
    ///     <param>Creates an instance of a Fuzzy Number in a failsafe way.</param>
    ///     <param>μ(X) &lt; 0 ⇒ 0; conversion is unsuccessful.</param>
    ///     <param>μ(X) &gt; 1 ⇒ 1; conversion is unsuccessful.</param>
    ///     <param>0 ≤ μ(X) ≤ 1 ⇒ The <see cref="double" /> value itself; conversion is successful.</param>
    /// </summary>
    /// <param name="value">The <see cref="double" /> value.</param>
    /// <param name="fuzzyNumber">An uninitialized instance of a Fuzzy Number</param>
    /// <returns><see langword="true" /> if the conversion is successful, <see langword="false" /> otherwise.</returns>
    public static bool TryCreate(double value, out FuzzyNumber fuzzyNumber)
    {
        switch (value)
        {
            case < 0.0:
                fuzzyNumber = Math.Max(0.0, value);
                return false;
            case > 1.0:
                fuzzyNumber = Math.Min(1.0, value);
                return false;
            default:
                fuzzyNumber = value;
                return true;
        }
    }

    /// <summary>
    ///     Represents the basic operation OR: A ∨ B ⇒ Max(A, B).
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the OR operator.</returns>
    public static FuzzyNumber operator |(FuzzyNumber a, FuzzyNumber b) => Math.Max(a.Value, b.Value);

    /// <summary>
    ///     Represents the basic operation AND: A ∧ B ⇒ Min(A, B).
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the AND operator.</returns>
    public static FuzzyNumber operator &(FuzzyNumber a, FuzzyNumber b) => Math.Min(a.Value, b.Value);

    /// <summary>
    ///     Represents the basic operation NOT: ¬A ⇒ 1 - A.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the NOT operator.</returns>
    public static FuzzyNumber operator !(FuzzyNumber x) => (1 - x.Value);

    public static bool operator <(FuzzyNumber a, FuzzyNumber b) => a != b && a.Value < b.Value;

    public static bool operator <=(FuzzyNumber a, FuzzyNumber b) => a == b || a.Value <= b.Value;

    public static bool operator >(FuzzyNumber a, FuzzyNumber b) => a != b && a.Value > b.Value;

    public static bool operator >=(FuzzyNumber a, FuzzyNumber b) => a == b || a.Value >= b.Value;

    public static bool operator ==(FuzzyNumber a, FuzzyNumber b) => Math.Abs(a.Value - b.Value) < Tolerance;

    public static bool operator !=(FuzzyNumber a, FuzzyNumber b) => !(a == b);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="double" /> value to a <see cref="FuzzyNumber" />. Note that
    ///     this value must be in the range μ(X) ∈ [0, 1] (see <see cref="FuzzyNumber(double)" /> for reference).
    /// </summary>
    /// <param name="x">The <see cref="double" /> value.</param>
    /// <returns></returns>
    public static implicit operator FuzzyNumber(double x) => new(x);

    /// <summary>
    ///     Defines a implicit conversion from a <see cref="FuzzyNumber" /> to a <see cref="double" /> value.
    /// </summary>
    /// <param name="x">The <see cref="FuzzyNumber" /> value.</param>
    /// <returns></returns>
    public static implicit operator double(FuzzyNumber x) => x.Value;

    /// <summary>
    ///     <param>Defines a implicit conversion from a <see cref="bool" /> to a <see cref="FuzzyNumber" /> value.</param>
    ///     <param>
    ///         Note that this operation is unidirectional. There is no possible conversion from a
    ///         <see cref="FuzzyNumber" /> whose <see cref="FuzzyNumber.Value" /> isn't either 0 or 1 to a
    ///         <see cref="bool" />.
    ///     </param>
    /// </summary>
    /// <param name="b">The <see cref="bool" /> value.</param>
    /// <returns></returns>
    public static implicit operator FuzzyNumber(bool b) => (b ? 1.0 : 0.0);

    public bool Equals(FuzzyNumber? other) => Value.Equals(other?.Value ?? 0);

    public override bool Equals(object? obj) =>
        ReferenceEquals(this, obj) || (obj is FuzzyNumber other && Equals(other));

    public override int GetHashCode() => Value.GetHashCode();

    public int CompareTo(FuzzyNumber? other) => Value.CompareTo(other?.Value ?? 0);

    /// <inheritdoc />
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
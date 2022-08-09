using System.Globalization;

namespace FuzzyLogic;

/// <summary>
/// <param>Embodies the core mathematical concepts that represent a membership degree, such as its truth value range
/// μ(X) ∈ [0, 1]; its basic operations: A ∧ B ⇒ Min(A, B), A ∨ B ⇒ Max(A, B), ¬A ⇒ 1 - A; and others (to be added
/// later if needed).</param>
/// <param>In terms of its actual implementation, it's merely a wrapper class of a <see cref="Double"/> value, on
/// which the aforementioned operators are applied.</param>
/// </summary>
public class FuzzyNumber
{
    /// <summary>
    /// Creates an instance of a Fuzzy Number.
    /// </summary>
    /// <param name="value">The <see cref="Double"/> value.</param>
    /// <exception cref="ArgumentException">Thrown if the value is not in the range μ(X) ∈ [0, 1].</exception>
    public FuzzyNumber(double value)
    {
        if (value is < 0.0 or > 1.0)
        {
            throw new ArgumentException(
                $"Value can't be lesser than 0 or greater than 1 (Value provided was: {value})");
        }

        Value = value;
    }

    public double Value { get; }

    /// <summary>
    /// Represents the basic operation OR: A ∨ B ⇒ Max(A, B).
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the OR operator.</returns>
    public static FuzzyNumber operator |(FuzzyNumber a, FuzzyNumber b) => new(Math.Max(a.Value, b.Value));

    /// <summary>
    /// Represents the basic operation AND: A ∧ B ⇒ Min(A, B).
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the AND operator.</returns>
    public static FuzzyNumber operator &(FuzzyNumber a, FuzzyNumber b) => new(Math.Min(a.Value, b.Value));

    /// <summary>
    /// Represents the basic operation NOT: ¬A ⇒ 1 - A.
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the NOT operator.</returns>
    public static FuzzyNumber operator !(FuzzyNumber x) => new(1 - x.Value);

    public static FuzzyNumber operator *(FuzzyNumber a, FuzzyNumber b) => new(a.Value * b.Value);

    /// <summary>
    /// Defines a implicit conversion of a <see cref="Double"/> value into a <see cref="FuzzyNumber"/>. Note that this
    /// value must be in the range μ(X) ∈ [0, 1] (see <see cref="FuzzyNumber(Double)"/>. for reference).
    /// </summary>
    /// <param name="x">The <see cref="Double"/> value.</param>
    /// <returns></returns>
    public static implicit operator FuzzyNumber(double x) => new(x);

    /// <summary>
    /// Defines a implicit conversion of a <see cref="FuzzyNumber"/> into a <see cref="Double"/> value.
    /// </summary>
    /// <param name="x">The <see cref="FuzzyNumber"/> value.</param>
    /// <returns></returns>
    public static implicit operator double(FuzzyNumber x) => x.Value;

    /// <inheritdoc/>
    public override string ToString() => Value.ToString(CultureInfo.InvariantCulture);
}
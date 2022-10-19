namespace FuzzyLogic.Number;

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
public interface IFuzzyNumber<T> where T : IFuzzyNumber<T>
{
    /// <summary>
    ///     <para>
    ///         Represents the smallest possible difference for which x comparison between two Fuzzy Numbers yields
    ///         equality. In other words, two fuzzy numbers are considered to be equal if the difference between them
    ///         is below this threshold.
    ///     </para>
    ///     <para>This field is constant.</para>
    /// </summary>
    public const double Tolerance = 1e-5;

    /// <summary>
    ///     The property for the <see cref="double" /> value on which the <see cref="IFuzzyNumber{T}" /> operates.
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
    static abstract T Of(double value);

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
    /// <param name="number">The <see cref="double" /> value.</param>
    /// <param name="fuzzyNumber">An instance of a Fuzzy Number</param>
    /// <returns><see langword="true" /> if the conversion is successful; <see langword="false" />, otherwise.</returns>
    static abstract bool TryCreate(double number, out T fuzzyNumber);

    /// <summary>
    ///     Represents a Fuzzy Number's smallest possible value.
    /// </summary>
    /// <returns>A Fuzzy Number's smallest possible value.</returns>
    static abstract T MinValue();

    /// <summary>
    ///     Represents a Fuzzy Number's biggest possible value.
    /// </summary>
    /// <returns>A Fuzzy Number's biggest possible value.</returns>
    static abstract T MaxValue();

    /// <summary>
    ///     Represents the basic operation NOT: ¬A.
    ///     <para>
    ///         This operator isn't necessarily defined explicitly. If that's the case, the operation defaults to the
    ///         standard negation: ¬A ≡ 1 - A.
    ///     </para>
    /// </summary>
    /// <param name="x">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the NOT operator.</returns>
    static abstract T operator !(T x);

    /// <summary>
    ///     Represents the basic operation OR: A ∨ B.
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the OR operator.</returns>
    static abstract T operator &(T a, T b);

    /// <summary>
    ///     Represents the basic operation AND: A ∧ B.
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the AND operator.</returns>
    static abstract T operator |(T a, T b);

    /// <summary>
    ///     <para>Represents the operation of residuum THEN: A ⇒ B.</para>
    ///     <para>
    ///         This operator isn't necessarily defined explicitly. If that's the case, the operation defaults to the
    ///         Zadeh implication: A ⇒ B ≡ max{1 − a, min{a, b}}
    ///     </para>
    /// </summary>
    /// <param name="a">A fuzzy number</param>
    /// <param name="b">A fuzzy number</param>
    /// <returns>The resulting fuzzy number after applying the THEN operator.</returns>
    static abstract T Implication(T a, T b);
}
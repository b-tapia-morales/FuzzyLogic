using System.Numerics;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;

namespace FuzzyLogic.Function.Implication;

public interface IFuzzyImplication<T> : IMembershipFunction<T> where T : unmanaged, INumber<T>, IConvertible
{
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
    /// <seealso cref="IMembershipFunction{T}.AsFunction()" />
    Func<T, double> LambdaCutFunction(FuzzyNumber y);

    /// <summary>
    ///     <para>
    ///         Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate, originating
    ///         from performing a Lambda-cut over the original membership function at the height point <i>y</i>,
    ///         represented as a <see cref="FuzzyNumber" />.
    ///     </para>
    ///     <para>
    ///         This Lambda-cut differs from the original <see cref="LambdaCutFunction(FuzzyNumber)">LambdaCutFunction</see>
    ///         in that it also performs horizontal cuts over the membership function. This can be specially relevant if
    ///         the function <see cref="IMembershipFunction{T}.IsClosed">is not closed</see>, since it could have no
    ///         lower or upper boundaries for <i>x</i> values other than -∞ and +∞.
    ///     </para>
    ///     <list type="bullet">
    ///         <item>
    ///             <description>x &lt; x₀ ∨ x &gt; x₁ ⇒ The zero function.</description>
    ///         </item>
    ///         <item>
    ///             <description>μ(x) = 0 ⇒ The zero function.</description>
    ///         </item>
    ///         <item>
    ///             <description>μ(x) = 1 ⇒ The original membership function.</description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="y">The height point at which the Lambda-cut is performed, represented as a <see cref="FuzzyNumber" />.</param>
    /// <param name="x0">The lower bound for the left horizontal cut.</param>
    /// <param name="x1">The upper bound for the right horizontal cut.</param>
    /// <returns></returns>
    /// <seealso cref="LambdaCutFunction(FuzzyNumber)">LambdaCutFunction</seealso>
    /// <seealso cref="IMembershipFunction{T}.IsClosed">IsClosed</seealso>
    Func<T, double> LambdaCutFunction(FuzzyNumber y, double x0, double x1);
}
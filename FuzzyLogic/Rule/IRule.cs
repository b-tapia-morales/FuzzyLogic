using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Rule;

/// <summary>
///     <para>
///         A class representation for a fuzzy rule. Fuzzy rules are built from existing
///         <see cref="IProposition{T}">Propositions</see> and <see cref="Connective{T}">Connectives</see>.
///     </para>
///     <para>
///         According to propositional logic, in order to be considered valid, a rule must have both an
///         <see cref="Antecedent" /> and a <see cref="Consequent" />. <see cref="IProposition{T}">Propositions</see> using
///         the <see cref="Connective{T}.And">Disjunctive</see> and <see cref="Connective{T}.Or">Conjunctive</see> operators in
///         between are considered optional.
///     </para>
///     <para>
///         In addition to the conditions written above, the rule creation process must comply with the following
///         policies:
///     </para>
///     <list type="number">
///         <item>
///             <description>
///                 There can be one and only one proposition with the <see cref="Connective{T}.If" /> connective.
///             </description>
///         </item>
///         <item>
///             <description>
///                 In order to append propositions with the connectives:
///                 <see cref="Connective{T}.And" />, <see cref="Connective{T}.Or" />, <see cref="Connective{T}.Then" />,
///                 there must already be a proposition appended with the <see cref="Connective{T}.If" /> connective.
///             </description>
///         </item>
///         <item>
///             <description>
///                 There cannot be a proposition with the connective <see cref="Connective{T}.Then" /> using the
///                 <see cref="Literal{T}.IsNot"/> literal. In other words, the consequent cannot be in negated form.
///             </description>
///         </item>
///         <item>
///             <description>
///                 After a proposition with the <see cref="Connective{T}.Then" /> connective is appended, no further
///                 propositions can be appended, because the rule is considered to be <see cref="IsFinalized">Finalized</see>.
///             </description>
///         </item>
///     </list>
/// </summary>
/// <seealso cref="MissingAntecedentException" />
/// <seealso cref="DuplicatedAntecedentException" />
/// <seealso cref="NegatedConsequentException"/>
/// <seealso cref="FinalizedRuleException" />
public interface IRule<T> : IComparable<IRule<T>>, IComparer<IRule<T>> where T : struct, IFuzzyNumber<T>
{
    public IProposition<T>? Antecedent { get; set; }
    public ICollection<IProposition<T>> Connectives { get; }
    public IProposition<T>? Consequent { get; set; }
    public bool IsFinalized { get; set; }
    public RulePriority Priority { get; }

    /// <summary>
    ///     Appends a <see cref="IProposition{T}">Proposition</see> with the <see cref="Proposition.Enums.Connective{T}.If" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective{T}.If" /> part of the rule appended.</returns>
    /// <exception cref="DuplicatedAntecedentException">
    ///     A proposition with the <see cref="Proposition.Enums.Connective{T}.If" /> connective already exists.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule<T> If(IProposition<T> proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition{T}">Proposition</see> with the <see cref="Proposition.Enums.Connective{T}.And" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective{T}.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Proposition.Enums.Connective{T}.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule<T> And(IProposition<T> proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition{T}">Proposition</see> with the <see cref="Connective{T}.Or" /> connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Connective{T}.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Connective{T}.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule<T> Or(IProposition<T> proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition{T}">Proposition</see> with the <see cref="Proposition.Enums.Connective{T}.Then" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Connective{T}.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Connective{T}.If" /> connective.
    /// </exception>
    /// <exception cref="NegatedConsequentException">
    ///     The proposition is in negated form.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule<T> Then(IProposition<T> proposition);

    /// <summary>
    ///     Determines whether the rule is valid according to propositional logic; that is, it must have both an
    ///     <see cref="Antecedent" /> and a <see cref="Consequent" />.
    /// </summary>
    /// <returns>true if the rule is valid; otherwise, null</returns>
    bool IsValid();

    /// <summary>
    ///     Determines whether the rule is applicable. In order to be determined as such, the following two conditions must be
    ///     met:
    ///     <list type="number">
    ///         <item>
    ///             <description>
    ///                 The rule is valid according to propositional logic.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 All variable names used to form <see cref="IProposition{T}">Propositions</see> in the premise part of
    ///                 the rule are contained as <see cref="IDictionary{TKey,TValue}.Keys">Keys</see> in the dictionary of
    ///                 facts.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>true if the rule is applicable; otherwise, null</returns>
    bool IsApplicable(IDictionary<string, double> facts);

    /// <summary>
    ///     Determines whether the rule uses the Linguistic Variable, which is uniquely identifiable by the name provided as
    ///     a parameter, as a part of its premise.
    /// </summary>
    /// <param name="variableName">The name of the Linguistic Variable</param>
    /// <returns>true if the rule uses the Linguistic Variable as a part of its premise; otherwise, false</returns>
    bool PremiseContainsVariable(string variableName);

    /// <summary>
    ///     Determines whether the rule uses the Linguistic Variable, which is uniquely identifiable by the name provided as
    ///     a parameter, as a part of its conclusion.
    /// </summary>
    /// <param name="variableName">The name of the Linguistic Variable</param>
    /// <returns>true if the rule uses the Linguistic Variable as a part of its conclusion; otherwise, false</returns>
    bool ConclusionContainsVariable(string variableName);

    /// <summary>
    ///     Gets the number of propositions contained in the premise part of the rule.
    /// </summary>
    /// <returns>The number of elements contained in the premise part of the rule.</returns>
    int PremiseLength();

    /// <summary>
    ///     <para>
    ///         Applies all the unary operators to each proposition in the premise part of the rule. A unary operator
    ///         only needs of an atomic term in order to be applied. In this context, examples of a unary operator are the
    ///         <see cref="Literal{T}.Is">Affirmation</see> and the <see cref="Literal{T}.IsNot">Negation</see> of a proposition.
    ///     </para>
    ///     <para>The process complies with the following order of precedence:</para>
    ///     <list type="number">
    ///         <item>
    ///             <description>
    ///                 The <see cref="Double">Crisp number</see> (given as a fact from the function parameter) is
    ///                 transformed to a <see cref="FuzzyNumber" /> by evaluating its
    ///                 <see cref="IMembershipFunction{T}.MembershipDegree{TNumber}">Membership Degree.</see>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 The <see cref="LinguisticHedge{T}">Hedge function</see> is applied over it.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 The <see cref="Literal{T}">Literal function</see> is applied over it.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     In order to operate, it must be proven first that the rule <see cref="IsApplicable">Is Applicable</see>;
    ///     otherwise, returns an empty collection.
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    ///     A collection of fuzzy numbers if the rule <see cref="IsApplicable">Is Applicable</see>;
    ///     otherwise, an empty collection.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    IEnumerable<T> ApplyOperators(IDictionary<string, double> facts);

    /// <summary>
    ///     <para>
    ///         Applies all the binary operators to each <see cref="FuzzyNumber" /> in the premise
    ///         part of the rule and aggregates them into a single Fuzzy Number, which can be understood as the weight of the
    ///         premise itself. This process succeeds the application of unary operators in the
    ///         <see cref="ApplyOperators">ApplyOperators</see> method, which is why it operates directly over fuzzy numbers
    ///         instead of propositions.
    ///     </para>
    ///     <para>
    ///         A binary operator needs of two atomic terms and a <see cref="Connective{T}" /> between them in order to be
    ///         applied. In this context, examples of a unary operator are the <see cref="Connective{T}.Or">Conjunction</see>
    ///         and the <see cref="Connective{T}.And">Disjunction</see> operators.
    ///     </para>
    ///     <para>
    ///         In order to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    ///         otherwise, returns null.
    ///     </para>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    ///     A <see cref="FuzzyNumber" /> if the rule is <see cref="IsApplicable">Applicable</see>;
    ///     otherwise, null.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    T EvaluatePremiseWeight(IDictionary<string, double> facts);

    /// <summary>
    ///     <para>
    ///         Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate, originating
    ///         from applying the implication method, which uses the premise weight determined in the
    ///         <see cref="EvaluatePremiseWeight">EvaluatePremiseWeight</see> method, as the height point to apply a Lambda Cut
    ///         over the original membership function in the consequent part of the rule.
    ///     </para>
    ///     <para>
    ///         In order to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    ///         otherwise, returns null.
    ///     </para>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <param name="method"></param>
    /// <returns>The new membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    /// <seealso cref="EvaluatePremiseWeight">EvaluatePremiseWeight</seealso>
    /// <seealso cref="IFuzzyImplication.LambdaCutFunction{T}(TNumber)">LambdaCutFunction</seealso>
    Func<double, double>? ApplyImplication(IDictionary<string, double> facts, InferenceMethod method = Mamdani);

    T EvaluateConclusionWeight(IDictionary<string, double> facts);

    T EvaluateRuleWeight(IDictionary<string, double> facts);

    double? CalculateArea(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani, double errorMargin = IClosedShape.DefaultErrorMargin);

    (double X, double Y)? CalculateCentroid(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani, double errorMargin = IClosedShape.DefaultErrorMargin);
}
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using static FuzzyLogic.Function.Implication.InferenceMethod;

namespace FuzzyLogic.Rule;

/// <summary>
/// <para>
/// A class representation for a fuzzy rule.
/// Fuzzy rules are built from existing <see cref="IProposition">Propositions</see> and <see cref="Connective">Connectives</see>.
/// </para>
/// <para>
/// According to propositional logic, to be considered valid, a rule must have both an
/// <see cref="Antecedent" /> and a <see cref="Consequent" />.
/// <see cref="IProposition">Propositions</see> using the <see cref="Connective.And">Disjunctive</see> and <see cref="Connective.Or">Conjunctive</see>
/// operators in between are considered optional.
/// </para>
/// <para>
/// In addition to the conditions written above, the rule creation process must comply with the following policies:
/// </para>
/// <list type="number">
/// <item>
/// <description>
/// There can be one and only one proposition with the <see cref="Connective.If" /> connective.
/// </description>
/// </item>
/// <item>
/// <description>
/// To append propositions with the connectives:
/// <see cref="Connective.And" />, <see cref="Connective.Or" />, <see cref="Connective.Then" />,
/// there must already be a proposition appended with the <see cref="Connective.If" /> connective.
/// </description>
/// </item>
/// <item>
/// <description>
/// There cannot be a proposition with the connective <see cref="Connective.Then" /> using the
/// <see cref="Literal.IsNot"/> literal.
/// In other words, the consequent cannot be in negated form.
/// </description>
/// </item>
/// <item>
/// <description>
/// After a proposition with the <see cref="Connective.Then" /> connective is appended, no further
/// propositions can be appended, because the rule is considered to be <see cref="IsFinalized">Finalized</see>.
/// </description>
/// </item>
/// </list>
/// </summary>
/// <seealso cref="MissingAntecedentException" />
/// <seealso cref="DuplicatedAntecedentException" />
/// <seealso cref="NegatedConsequentException"/>
/// <seealso cref="FinalizedRuleException" />
public interface IRule : IComparable<IRule>, IComparer<IRule>
{
    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; }
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; }
    public RulePriority Priority { get; }

    /// <summary>
    /// Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.If" />
    /// connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="DuplicatedAntecedentException">
    /// A proposition with the <see cref="Proposition.Enums.Connective.If" /> connective already exists.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    /// The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule If(IProposition proposition);

    /// <summary>
    /// Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.And" />
    /// connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    /// There is no existing proposition with the <see cref="Proposition.Enums.Connective.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    /// The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule And(IProposition proposition);

    /// <summary>
    /// Appends a <see cref="IProposition">Proposition</see> with the <see cref="Connective.Or" /> connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    /// There is no existing proposition with the <see cref="Connective.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    /// The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule Or(IProposition proposition);

    /// <summary>
    /// Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.Then" />
    /// connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    /// There is no existing proposition with the <see cref="Connective.If" /> connective.
    /// </exception>
    /// <exception cref="NegatedConsequentException">
    /// The proposition is in negated form.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    /// The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule Then(IProposition proposition);

    /// <summary>
    /// Determines whether the rule is valid or not according to propositional logic; that is,
    /// it must have both an <see cref="Antecedent" /> and a <see cref="Consequent" />.
    /// </summary>
    /// <returns><see langword="true" />, if the rule is valid; otherwise, <see langword="false"/>.</returns>
    bool IsValid();

    /// <summary>
    /// Determines whether the rule is applicable.
    /// To be determined as such, the following two conditions must be met:
    /// <list type="number">
    /// <item>
    /// <description>
    /// The rule is valid, according to propositional logic.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// All variable names used to form <see cref="IProposition">Propositions</see> in the premise part of
    /// the rule are contained as <see cref="IDictionary{TKey,TValue}.Keys">Keys</see> in the dictionary
    /// of facts provided as a parameter.
    /// </description>
    /// </item>
    /// </list>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns><see langword="true" /> if the rule is applicable;
    /// otherwise, <see langword="false" /></returns>
    bool IsApplicable(IDictionary<string, double> facts);

    /// <summary>
    /// Determines whether the rule uses the Linguistic Variable, which is uniquely identifiable by the name provided as
    /// a parameter, as a part of its premise.
    /// </summary>
    /// <param name="variableName">The name of the Linguistic Variable</param>
    /// <returns><see langword="true" /> if the rule uses the Linguistic Variable as a part of its premise;
    /// otherwise, <see langword="false" />.</returns>
    bool PremiseContainsVariable(string variableName);

    /// <summary>
    /// Determines whether the rule uses the Linguistic Variable, which is uniquely identifiable by the name provided as
    /// a parameter, as a part of its conclusion.
    /// </summary>
    /// <param name="variableName">The name of the Linguistic Variable</param>
    /// <returns><see langword="true" /> if the rule uses the Linguistic Variable as a part of its conclusion;
    /// otherwise, <see langword="false" />.</returns>
    bool ConclusionContainsVariable(string variableName);

    /// <summary>
    /// Gets the number of propositions contained in the premise part of the rule.
    /// </summary>
    /// <returns>The number of elements contained in the premise part of the rule.</returns>
    int PremiseLength();

    /// <summary>
    /// <para>
    /// Applies all the unary operators to each proposition in the premise part of the rule.
    /// A unary operator only needs of an atomic term to be applied.
    /// In this context, examples of a unary operator are the
    /// <see cref="Literal.Is">Affirmation</see> and the <see cref="Literal.IsNot">Negation</see> of a proposition.
    /// </para>
    /// <list type="number">
    /// <listheader>The process applies the following order of precedence:</listheader>
    /// <item>
    /// <description>
    /// The <see cref="Double">Crisp number</see> (given as a fact from the dictionary provided as a parameter)
    /// is transformed to a <see cref="FuzzyNumber">Fuzzy number</see> by evaluating its
    /// <see cref="IMembershipFunction{T}.MembershipDegree">Membership Degree.</see>
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The <see cref="LinguisticHedge">Hedge function</see> is applied over it.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The <see cref="Literal">Literal function</see> is applied over it.
    /// </description>
    /// </item>
    /// </list>
    /// To be able to operate, it must be proven first that the rule <see cref="IsApplicable">Is Applicable</see>;
    /// otherwise, returns an empty collection.
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    /// A collection of fuzzy numbers if the rule <see cref="IsApplicable">Is Applicable</see>;
    /// otherwise, an empty collection.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    IEnumerable<FuzzyNumber> ApplyOperators(IDictionary<string, double> facts);

    /// <summary>
    /// <para>
    /// Applies all the unary operators to each <see cref="FuzzyNumber">Fuzzy number</see> in the premise
    /// part of the rule and aggregates them into a single Fuzzy number,
    /// which can be understood as the weight of the premise itself.
    /// This process succeeds the application of unary operators in the <see cref="ApplyOperators">ApplyOperators</see> method,
    /// which is why it operates directly over fuzzy numbers instead of propositions.
    /// </para>
    /// <para>
    /// A unary operator needs of two atomic terms and a <see cref="Connective" /> between them to be applied.
    /// In this context, examples of a unary operator are the <see cref="Connective.Or">Conjunction</see>
    /// and the <see cref="Connective.And">Disjunction</see> operators.
    /// </para>
    /// <para>
    /// To be able to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    /// otherwise, returns null.
    /// </para>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    /// A <see cref="FuzzyNumber" /> if the rule is <see cref="IsApplicable">Applicable</see>;
    /// otherwise, null.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    FuzzyNumber? EvaluatePremiseWeight(IDictionary<string, double> facts);

    /// <summary>
    /// <para>
    /// Returns a new membership function, represented as a <see cref="Func{T,TResult}" /> delegate,
    /// originating from applying the implication method, which uses the premise weight determined in the
    /// <see cref="EvaluatePremiseWeight">EvaluatePremiseWeight</see> method,
    /// as the height point to apply a Lambda Cut over the original membership function in the consequent part of the rule.
    ///     </para>
    ///     <para>
    ///         To be able to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    ///         otherwise, returns null.
    ///     </para>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <param name="method"></param>
    /// <returns>The new membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    /// <seealso cref="EvaluatePremiseWeight">EvaluatePremiseWeight</seealso>
    /// <seealso cref="IFuzzyImplication.AlphaCut">LambdaCutFunction</seealso>
    Func<double, double>? ApplyImplication(IDictionary<string, double> facts, InferenceMethod method = Mamdani);

    FuzzyNumber EvaluateConclusionWeight(IDictionary<string, double> facts);

    FuzzyNumber EvaluateRuleWeight(IDictionary<string, double> facts);

    double? CalculateArea(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani, double errorMargin = IClosedShape.ErrorMargin);

    (double X, double Y)? CalculateCentroid(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani, double errorMargin = IClosedShape.ErrorMargin);
}
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Rule;

/// <summary>
/// <para>
/// A class representation for a fuzzy rule.
/// Fuzzy rules are built from existing <see cref="IProposition">Propositions</see> and <see cref="Connective">Connectives</see>.
/// </para>
/// <para>
/// According to propositional logic, to be considered valid, a rule must have both an
/// <see cref="Conditional" /> and a <see cref="Consequent" />.
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
public interface IRule : IComparable<IRule>, IComparer<IRule>, IEquatable<IRule>
{
    public ILinguisticBase LinguisticBase { get; }
    public IProposition? Conditional { get; set; }
    public ICollection<IProposition> Connectives { get; }
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; }
    public RulePriority Priority { get; }

    IRule If(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    IRule IfNot(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    IRule And(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    IRule AndNot(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    public IRule Or(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    public IRule OrNot(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    public IRule Then(string variableName, string termName, HedgeType hedgeType = HedgeType.None);

    /// <summary>
    /// Determines whether the rule is valid or not according to propositional logic; that is,
    /// it must have both an <see cref="Conditional" /> and a <see cref="Consequent" />.
    /// </summary>
    /// <returns><see langword="true" />, if the rule is valid; otherwise, <see langword="false"/>.</returns>
    bool IsValid();

    /// <summary>
    /// <para>
    /// Determines whether the rule is applicable.
    /// </para>
    /// <list type="number">
    /// <listheader>To be determined as such, the following two conditions must be met:</listheader>
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
    bool PremiseContains(string variableName);

    /// <summary>
    /// Determines whether the rule uses the referenced <see cref="Variable.IVariable">Linguistic Variable</see>
    /// as a part of its conclusion.
    /// </summary>
    /// <param name="variableName">The name of the Linguistic Variable</param>
    /// <returns><see langword="true" /> if the rule uses the Linguistic Variable as a part of its conclusion;
    /// otherwise, <see langword="false" />.</returns>
    bool ConsequentContains(string variableName);

    /// <summary>
    /// Gets the number of propositions contained in the premise part of the rule.
    /// </summary>
    /// <returns>The number of elements contained in the premise part of the rule.</returns>
    int PremiseLength();

    /// <summary>
    /// <para>
    /// Applies all unary operators to each proposition in the premise part of the rule.
    /// In this context, unary operators are the <see cref="Literal.Is">Affirmation</see>
    /// and the <see cref="Literal.IsNot">Negation</see> of a proposition.
    /// </para>
    /// <list type="number">
    /// <listheader>The method processes the unary operations in the following order of precedence:</listheader>
    /// <item>
    /// <description>
    /// A <see cref="double"/> crisp value is transformed into a <see cref="FuzzyNumber">Fuzzy Number</see>
    /// by evaluating its <see cref="IMembershipFunction.MembershipDegree">Membership Degree</see>.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The <see cref="LinguisticHedge">Linguistic Hedge function</see> is applied over the resulting Fuzzy Number.
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// The <see cref="Literal">Literal function</see> is applied to finalize the operation.
    /// </description>
    /// </item>
    /// </list>
    /// The rule must satisfy the <see cref="IsApplicable"/> condition for the method to operate;
    /// otherwise, it returns an empty collection.
    /// </summary>
    /// <param name="facts">
    ///     A dictionary where each <see cref="string"/> key represents the name of a
    ///     <see cref="Variable.IVariable">Linguistic Variable</see>, and each associated
    ///     <see cref="double"/> value is its crisp value.
    /// </param>
    /// <param name="negation">Specifies the negation operator to be used.</param>
    /// <returns>
    /// A collection of <see cref="FuzzyNumber">Fuzzy Numbers</see> after applying all unary operators
    /// if the rule <see cref="IsApplicable">Is Applicable</see>;
    /// otherwise, an empty collection.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    IEnumerable<FuzzyNumber> ApplyUnaryOperators(IDictionary<string, double> facts, INegation negation);

    IEnumerable<FuzzyNumber> ApplyUnaryOperators(IDictionary<string, double> facts) =>
        ApplyUnaryOperators(facts, Negation.Standard);

    public FuzzyNumber EvaluatePremiseWeight(IDictionary<string, double> facts, INegation negation, INorm tNorm, IConorm tConorm);

    public FuzzyNumber EvaluatePremiseWeight(IDictionary<string, double> facts, INegation negation, IOperatorFamily operatorFamily) =>
        EvaluatePremiseWeight(facts, negation, operatorFamily.Norm, operatorFamily.Conorm);

    public FuzzyNumber EvaluatePremiseWeight(IDictionary<string, double> facts, IOperatorFamily operatorFamily) =>
        EvaluatePremiseWeight(facts, operatorFamily.Negation, operatorFamily.Norm, operatorFamily.Conorm);

    FuzzyNumber EvaluateConclusionWeight(IDictionary<string, double> facts, INegation negation);

    FuzzyNumber EvaluateConclusionWeight(IDictionary<string, double> facts) =>
        EvaluateConclusionWeight(facts, Negation.Standard);

    FuzzyNumber EvaluateRuleWeight(IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, IResiduum residuum);

    FuzzyNumber EvaluateRuleWeight(IDictionary<string, double> facts, INegation negation, IOperatorFamily operatorFamily) =>
        EvaluatePremiseWeight(facts, negation, operatorFamily.Norm, operatorFamily.Conorm);

    FuzzyNumber EvaluateRuleWeight(IDictionary<string, double> facts, IOperatorFamily operatorFamily) =>
        EvaluatePremiseWeight(facts, Negation.Standard, operatorFamily.Norm, operatorFamily.Conorm);
}
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;

namespace FuzzyLogic.Rule;

/// <summary>
///     <para>
///         A class representation for a fuzzy rule. Fuzzy rules are built from existing
///         <see cref="IProposition">Propositions</see> and <see cref="Proposition.Enums.Connective">Connectives</see>.
///     </para>
///     <para>
///         According to propositional logic, in order to be considered valid, a rule must have both an
///         <see cref="Antecedent" /> and a <see cref="Consequent" />. <see cref="IProposition">Propositions</see> using
///         the <see cref="Proposition.Enums.Connective.And">Disjunctive</see> and
///         <see cref="Proposition.Enums.Connective.Or">Conjunctive</see> operators in between are considered optional.
///     </para>
///     <para>
///         In addition to the conditions written above, the rule creation process must comply with the following
///         policies:
///     </para>
///     <list type="number">
///         <item>
///             <description>
///                 There can be one and only one proposition with the <see cref="Proposition.Enums.Connective.If" />
///                 connective.
///             </description>
///         </item>
///         <item>
///             <description>
///                 In order to append propositions with the connectives:
///                 <see cref="Proposition.Enums.Connective.And" />, <see cref="Proposition.Enums.Connective.Or" />,
///                 <see cref="Proposition.Enums.Connective.Then" />, there must already be a proposition with the
///                 <see cref="Proposition.Enums.Connective.If" /> connective
///             </description>
///         </item>
///         <item>
///             <description>
///                 After a proposition with the <see cref="Proposition.Enums.Connective.Then" /> connective is appended,
///                 the rule is considered to be <see cref="IsFinalized">Finalized</see>, meaning that no further propositions
///                 can be appended.
///             </description>
///         </item>
///     </list>
/// </summary>
/// <seealso cref="MissingAntecedentException" />
/// <seealso cref="DuplicatedAntecedentException" />
/// <seealso cref="FinalizedRuleException" />
public interface IRule
{
    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; }
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; }

    /// <summary>
    ///     Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.If" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="DuplicatedAntecedentException">
    ///     A proposition with the <see cref="Proposition.Enums.Connective.If" /> connective already exists.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule If(IProposition proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.And" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Proposition.Enums.Connective.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule And(IProposition proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.Or" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Proposition.Enums.Connective.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule Or(IProposition proposition);

    /// <summary>
    ///     Appends a <see cref="IProposition">Proposition</see> with the <see cref="Proposition.Enums.Connective.Then" />
    ///     connective to the rule.
    /// </summary>
    /// <param name="proposition">The proposition</param>
    /// <returns>The rule itself with the <see cref="Proposition.Enums.Connective.If" /> part of the rule appended.</returns>
    /// <exception cref="MissingAntecedentException">
    ///     There is no existing proposition with the <see cref="Proposition.Enums.Connective.If" /> connective.
    /// </exception>
    /// <exception cref="FinalizedRuleException">
    ///     The rule has already been <see cref="IsFinalized">Finalized</see>.
    /// </exception>
    IRule Then(IProposition proposition);

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
    ///                 All variable names used to form <see cref="IProposition">Propositions</see> in the premise part of
    ///                 the rule are contained as <see cref="IDictionary{TKey,TValue}.Keys">Keys</see> in the dictionary of
    ///                 facts.
    ///             </description>
    ///         </item>
    ///     </list>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>true if the rule is applicable; otherwise, null</returns>
    bool IsApplicable(IDictionary<string, double> facts);

    bool PremiseContainsVariable(string variableName);

    bool ConclusionContainsVariable(string variableName);

    int PremiseLength();

    /// <summary>
    ///     <para>
    ///         Applies all the unary operators to each proposition in the premise part of the rule. A unary operator
    ///         only needs of an atomic term in order to be applied. In this context, examples of a unary operator are the
    ///         <see cref="Proposition.Enums.Literal.Is">Affirmation</see> and the
    ///         <see cref="Proposition.Enums.Literal.IsNot">Negation</see> of a proposition.
    ///     </para>
    ///     <para>The process complies with the following order of precedence:</para>
    ///     <list type="number">
    ///         <item>
    ///             <description>
    ///                 The <see cref="Double">Crisp number</see> (given as a fact from the function parameter) is
    ///                 transformed to a <see cref="FuzzyLogic.Number.FuzzyNumber" /> by evaluating its
    ///                 <see cref="IMembershipFunction{T}.MembershipDegree">
    ///                     Membership Degree.
    ///                 </see>
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 The <see cref="FuzzyLogic.Proposition.Enums.LinguisticHedge">Hedge function</see> is applied over it.
    ///             </description>
    ///         </item>
    ///         <item>
    ///             <description>
    ///                 The <see cref="FuzzyLogic.Proposition.Enums.Literal">Literal function</see> is applied over it.
    ///             </description>
    ///         </item>
    ///     </list>
    ///     In order to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    ///     otherwise, returns an empty collection.
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    ///     A collection of fuzzy numbers if the rule is <see cref="IsApplicable">Applicable</see>;
    ///     otherwise, an empty collection.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    IEnumerable<FuzzyNumber> ApplyOperators(IDictionary<string, double> facts);

    /// <summary>
    ///     <para>
    ///         Applies all the binary operators to each <see cref="FuzzyLogic.Number.FuzzyNumber" /> in the premise
    ///         part of the rule and aggregates them into a single Fuzzy Number, which can be understood as the weight of the
    ///         premise itself. This process succeeds the application of unary operators in the
    ///         <see cref="ApplyOperators">ApplyOperators</see> method, which is why it operates directly over fuzzy numbers
    ///         instead of propositions.
    ///     </para>
    ///     <para>
    ///         A binary operator needs of two atomic terms and a <see cref="FuzzyLogic.Proposition.Enums.Connective" />
    ///         between them in order to be applied. In this context, examples of a unary operator are the
    ///         <see cref="Proposition.Enums.Connective.Or">Conjunction</see> and the
    ///         <see cref="Proposition.Enums.Connective.And">Disjunction</see> operators.
    ///     </para>
    ///     <para>
    ///         In order to operate, it must be proven first that the rule is <see cref="IsApplicable">Applicable</see>;
    ///         otherwise, returns null.
    ///     </para>
    /// </summary>
    /// <param name="facts">A <see cref="IDictionary{TKey,TValue}">Dictionary</see> of facts</param>
    /// <returns>
    ///     A <see cref="FuzzyLogic.Number.FuzzyNumber" /> if the rule is <see cref="IsApplicable">Applicable</see>;
    ///     otherwise, null.
    /// </returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    FuzzyNumber? EvaluatePremiseWeight(IDictionary<string, double> facts);

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
    /// <returns>The new membership function, represented as a <see cref="Func{T,TResult}" /> delegate.</returns>
    /// <seealso cref="IsApplicable">IsApplicable</seealso>
    /// <seealso cref="ApplyOperators">ApplyOperators</seealso>
    /// <seealso cref="EvaluatePremiseWeight">EvaluatePremiseWeight</seealso>
    /// <seealso cref="IMembershipFunction{T}.LambdaCutFunction(FuzzyNumber)">LambdaCutFunction</seealso>
    Func<double, double>? ApplyImplication(IDictionary<string, double> facts);

    FuzzyNumber? EvaluateConclusionWeight(IDictionary<string, double> facts);

    FuzzyNumber? EvaluateRuleWeight(IDictionary<string, double> facts);

    double? CalculateArea(IDictionary<string, double> facts, double errorMargin = IClosedSurface.DefaultErrorMargin);

    (double X, double Y)? CalculateCentroid(IDictionary<string, double> facts,
        double errorMargin = IClosedSurface.DefaultErrorMargin);
}
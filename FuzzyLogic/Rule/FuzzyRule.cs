using System.Collections.Immutable;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using static System.StringComparison;
using static FuzzyLogic.Proposition.Enums.Literal;
using static FuzzyLogic.Rule.RulePriority;

namespace FuzzyLogic.Rule;

public class FuzzyRule : IRule
{
    private static readonly IComparer<IRule> DefaultComparer = new HighestPriority();

    private FuzzyRule(RulePriority priority = Normal) => Priority = priority;

    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; } = new List<IProposition>();
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; } = false;
    public RulePriority Priority { get; }

    public override string ToString() =>
        $"{Antecedent} {(Connectives.Any() ? $"{string.Join(' ', Connectives)} " : string.Empty)}{Consequent}";

    public int Compare(IRule? x, IRule? y) => DefaultComparer.Compare(x, y);

    public int CompareTo(IRule? other) => DefaultComparer.Compare(this, other);

    public IRule If(IProposition proposition) => If(this, proposition);

    public IRule And(IProposition proposition) => And(this, proposition);

    public IRule Or(IProposition proposition) => Or(this, proposition);

    public IRule Then(IProposition proposition) => Then(this, proposition);

    public bool IsValid() => Antecedent != null && Consequent != null;

    public bool IsApplicable(IDictionary<string, double> facts) =>
        Antecedent != null && Consequent != null &&
        facts.ContainsKey(Antecedent.LinguisticVariable.Name) &&
        Connectives.All(e => facts.ContainsKey(e.LinguisticVariable.Name));

    public bool PremiseContainsVariable(string variableName) =>
        Antecedent != null &&
        string.Equals(Antecedent.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase) ||
        Connectives.Any(e => string.Equals(e.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase));

    public bool ConclusionContainsVariable(string variableName) =>
        Consequent != null &&
        string.Equals(Consequent.LinguisticVariable.Name, variableName, InvariantCultureIgnoreCase);

    public int PremiseLength() => (Antecedent == null ? 1 : 0) + Connectives.Count;

    public IEnumerable<FuzzyNumber> ApplyOperators(IDictionary<string, double> facts) =>
        !IsApplicable(facts)
            ? ImmutableList<FuzzyNumber>.Empty
            : Connectives
                .Prepend(Antecedent!)
                .Select(e => e.ApplyUnaryOperators(facts[e.LinguisticVariable.Name]));

    public FuzzyNumber? EvaluatePremiseWeight(IDictionary<string, double> facts)
    {
        var numbers = new Queue<FuzzyNumber>(ApplyOperators(facts));
        if (!numbers.Any())
            return null;
        if (numbers.Count == 1)
            return numbers.First();

        var connectives = new Queue<Connective>(Connectives.Select(e => e.Connective));
        while (numbers.Count > 1)
        {
            var firstNumber = numbers.Dequeue();
            var secondNumber = numbers.Dequeue();
            var connective = connectives.Dequeue().Function!;
            numbers.Enqueue(connective(firstNumber, secondNumber));
        }

        return numbers.Dequeue();
    }

    public Func<double, double>? ApplyImplication(IDictionary<string, double> facts)
    {
        var ruleWeight = EvaluatePremiseWeight(facts);
        return ruleWeight == null ? null : Consequent!.Function.LambdaCutFunction(ruleWeight.GetValueOrDefault());
    }

    public FuzzyNumber? EvaluateConclusionWeight(IDictionary<string, double> facts)
    {
        if (Consequent == null || !facts.ContainsKey(Consequent.LinguisticVariable.Name))
            return null;
        var crispNumber = facts[Consequent.LinguisticVariable.Name];
        return Consequent.ApplyUnaryOperators(crispNumber);
    }

    public FuzzyNumber? EvaluateRuleWeight(IDictionary<string, double> facts)
    {
        if (!IsApplicable(facts)) return null;
        return FuzzyNumber.Implication(EvaluatePremiseWeight(facts).GetValueOrDefault(),
            EvaluateConclusionWeight(facts).GetValueOrDefault());
    }

    public double? CalculateArea(IDictionary<string, double> facts,
        double errorMargin = IClosedShape.DefaultErrorMargin)
    {
        if (!IsApplicable(facts)) return null;
        var function = Consequent!.Function;
        if (function is not IClosedShape) return null;
        var surface = (IClosedShape) Consequent!.Function;
        var cutPoint = EvaluatePremiseWeight(facts).GetValueOrDefault();
        return cutPoint == 0 ? null : surface.CalculateArea(cutPoint, errorMargin);
    }

    public (double X, double Y)? CalculateCentroid(IDictionary<string, double> facts,
        double errorMargin = IClosedShape.DefaultErrorMargin)
    {
        if (!IsApplicable(facts)) return null;
        var function = Consequent!.Function;
        if (function is not IClosedShape) return null;
        var surface = (IClosedShape) Consequent!.Function;
        var cutPoint = EvaluatePremiseWeight(facts).GetValueOrDefault();
        return cutPoint == 0 ? null : surface.CalculateCentroid(cutPoint, errorMargin);
    }

    public static IRule Create(RulePriority priority = Normal) => new FuzzyRule(priority);

    private static IRule If(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent != null)
            throw new DuplicatedAntecedentException();

        proposition.Connective = Connective.If;
        rule.Antecedent = proposition;
        return rule;
    }

    private static IRule And(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();

        proposition.Connective = Connective.And;
        rule.Connectives.Add(proposition);
        return rule;
    }

    private static IRule Or(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();

        proposition.Connective = Connective.Or;
        rule.Connectives.Add(proposition);
        return rule;
    }

    public static IRule Then(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();
        if (proposition.Literal == IsNot)
            throw new NegatedConsequentException();

        proposition.Connective = Connective.Then;
        rule.Consequent = proposition;
        rule.IsFinalized = true;
        return rule;
    }
}
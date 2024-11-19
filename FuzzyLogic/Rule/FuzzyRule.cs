using System.Collections.Immutable;
using FuzzyLogic.Function.Implication;
using FuzzyLogic.Function.Interface;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using static System.StringComparison;
using static FuzzyLogic.Function.Implication.InferenceMethod;
using static FuzzyLogic.Rule.RulePriority;

namespace FuzzyLogic.Rule;

public class FuzzyRule<T> : IRule<T> where T : struct, IFuzzyNumber<T>
{
    private static readonly IComparer<IRule<T>> DefaultComparer = new HighestPriority<T>();

    private FuzzyRule(RulePriority priority = Normal) => Priority = priority;

    public IProposition<T>? Antecedent { get; set; }
    public ICollection<IProposition<T>> Connectives { get; } = new List<IProposition<T>>();
    public IProposition<T>? Consequent { get; set; }
    public bool IsFinalized { get; set; } = false;
    public RulePriority Priority { get; }

    public override string ToString() =>
        $"{Antecedent} {(Connectives.Count != 0 ? $"{string.Join(' ', Connectives)} " : string.Empty)}{Consequent}";

    public int Compare(IRule<T>? x, IRule<T>? y) => DefaultComparer.Compare(x, y);

    public int CompareTo(IRule<T>? other) => DefaultComparer.Compare(this, other);

    public IRule<T> If(IProposition<T> proposition) => If(this, proposition);

    public IRule<T> And(IProposition<T> proposition) => And(this, proposition);

    public IRule<T> Or(IProposition<T> proposition) => Or(this, proposition);

    public IRule<T> Then(IProposition<T> proposition) => Then(this, proposition);

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

    public IEnumerable<T> ApplyOperators(IDictionary<string, double> facts) =>
        !IsApplicable(facts)
            ? ImmutableList<T>.Empty
            : Connectives
                .Prepend(Antecedent!)
                .Select(e => e.ApplyUnaryOperators(facts[e.LinguisticVariable.Name]));

    public T EvaluatePremiseWeight(IDictionary<string, double> facts)
    {
        var numbers = new Queue<T>(ApplyOperators(facts));
        switch (numbers.Count)
        {
            case 0:
                throw new InvalidOperationException();
            case 1:
                return numbers.First();
        }

        var connectives = new Queue<Connective<T>>(Connectives.Select(e => e.Connective));
        while (numbers.Count > 1)
        {
            var firstNumber = numbers.Dequeue();
            var secondNumber = numbers.Dequeue();
            var connective = connectives.Dequeue().Function!;
            numbers.Enqueue(connective(firstNumber, secondNumber));
        }

        return numbers.Dequeue();
    }

    public Func<double, double> ApplyImplication(IDictionary<string, double> facts, InferenceMethod method = Mamdani)
    {
        var function = Consequent!.Function;
        if (function is not IFuzzyInference surface)
            throw new InvalidOperationException();
        var ruleWeight = EvaluatePremiseWeight(facts);
        return surface.LambdaCutFunction(ruleWeight, method);
    }

    public T EvaluateConclusionWeight(IDictionary<string, double> facts)
    {
        if (Consequent == null || !facts.TryGetValue(Consequent.LinguisticVariable.Name, out var crispNumber))
            throw new InvalidOperationException();
        return Consequent.ApplyUnaryOperators(crispNumber);
    }

    public T EvaluateRuleWeight(IDictionary<string, double> facts)
    {
        if (!IsApplicable(facts))
            throw new InvalidOperationException();
        return T.Then(EvaluatePremiseWeight(facts), EvaluateConclusionWeight(facts));
    }

    public double? CalculateArea(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani,
        double errorMargin = IClosedShape.ErrorMargin)
    {
        if (!IsApplicable(facts))
            throw new InvalidOperationException();
        var function = Consequent!.Function;
        if (function is not IFuzzyInference surface)
            throw new InvalidOperationException();
        var cutPoint = EvaluatePremiseWeight(facts);
        return cutPoint == 0 ? null : surface.CalculateArea(cutPoint, errorMargin, method);
    }

    public (double X, double Y)? CalculateCentroid(IDictionary<string, double> facts,
        InferenceMethod method = Mamdani,
        double errorMargin = IClosedShape.ErrorMargin)
    {
        if (!IsApplicable(facts))
            throw new InvalidOperationException();
        var function = Consequent!.Function;
        if (function is not IFuzzyInference surface)
            throw new InvalidOperationException();
        var cutPoint = EvaluatePremiseWeight(facts);
        return cutPoint == 0 ? null : surface.CalculateCentroid(cutPoint, errorMargin, method);
    }

    public static IRule<T> Create(RulePriority priority = Normal) => new FuzzyRule<T>(priority);

    private static IRule<T> If(IRule<T> rule, IProposition<T> proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent != null)
            throw new DuplicatedAntecedentException();

        proposition.Connective = Connective<T>.If;
        rule.Antecedent = proposition;
        return rule;
    }

    private static IRule<T> And(IRule<T> rule, IProposition<T> proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();

        proposition.Connective = Connective<T>.And;
        rule.Connectives.Add(proposition);
        return rule;
    }

    private static IRule<T> Or(IRule<T> rule, IProposition<T> proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();

        proposition.Connective = Connective<T>.Or;
        rule.Connectives.Add(proposition);
        return rule;
    }

    public static IRule<T> Then(IRule<T> rule, IProposition<T> proposition)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Antecedent == null)
            throw new MissingAntecedentException();
        if (proposition.Literal == Literal<T>.IsNot)
            throw new NegatedConsequentException();

        proposition.Connective = Connective<T>.Then;
        rule.Consequent = proposition;
        rule.IsFinalized = true;
        return rule;
    }
}
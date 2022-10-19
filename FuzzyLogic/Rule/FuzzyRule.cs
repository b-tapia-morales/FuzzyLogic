using System.Collections.Immutable;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;

namespace FuzzyLogic.Rule;

public class FuzzyRule : IRule
{
    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; } = new List<IProposition>();
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; } = false;

    public override string ToString() =>
        $"{Antecedent} {string.Join(' ', Connectives)} {Consequent}";

    public IRule If(IProposition proposition) => If(this, proposition);

    public IRule And(IProposition proposition) => And(this, proposition);

    public IRule Or(IProposition proposition) => Or(this, proposition);

    public IRule Then(IProposition proposition) => Then(this, proposition);

    public bool IsValid() => Antecedent != null && Consequent != null;

    public bool IsApplicable(IDictionary<string, double> facts) =>
        Antecedent != null && Consequent != null &&
        facts.ContainsKey(Antecedent.LinguisticVariable.Name) &&
        Connectives.All(e => facts.ContainsKey(e.LinguisticVariable.Name));

    public IEnumerable<FuzzyNumber> ApplyOperators(IDictionary<string, double> facts)
    {
        if (!IsApplicable(facts))
            return ImmutableList<FuzzyNumber>.Empty;

        var propositions = Connectives.Prepend(Antecedent!);

        return from proposition in propositions
            let crispNumber = facts[proposition.LinguisticVariable.Name]
            select proposition.ApplyUnaryOperators(crispNumber);
    }

    public FuzzyNumber? EvaluateAntecedentWeight(IDictionary<string, double> facts)
    {
        var numbers = new Queue<FuzzyNumber>(ApplyOperators(facts));
        if (!numbers.Any())
            return null;
        if (numbers.Count == 1)
            return numbers.First();

        var connectives = new Queue<Connective>(Connectives.Select(e => e.Connective));
        while (numbers.Count > 1)
        {
            var first = numbers.Dequeue();
            var second = numbers.Dequeue();
            var connectorFunction = connectives.Dequeue().Function!;
            numbers.Enqueue(connectorFunction(first, second));
        }

        return numbers.Dequeue();
    }

    public Func<double, double>? ApplyImplication(IDictionary<string, double> facts)
    {
        var ruleWeight = EvaluateAntecedentWeight(facts);
        return ruleWeight == null ? null : Consequent!.Function.LambdaCutFunction(ruleWeight.GetValueOrDefault());
    }

    public FuzzyNumber? EvaluateConsequentWeight(IDictionary<string, double> facts)
    {
        if (Consequent == null || !facts.ContainsKey(Consequent.LinguisticVariable.Name))
            return null;
        var crispNumber = facts[Consequent.LinguisticVariable.Name];
        return Consequent.ApplyUnaryOperators(crispNumber);
    }

    public FuzzyNumber? EvaluateRuleWeight(IDictionary<string, double> facts)
    {
        if (!IsApplicable(facts)) return null;
        return FuzzyNumber.Implication(EvaluateAntecedentWeight(facts).GetValueOrDefault(),
            EvaluateConsequentWeight(facts).GetValueOrDefault());
    }

    public static IRule Create() => new FuzzyRule();

    private static IRule If(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent != null) throw new DuplicatedAntecedentException();

        proposition.Connective = Connective.If;
        rule.Antecedent = proposition;
        return rule;
    }

    private static IRule And(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.And;
        rule.Connectives.Add(proposition);
        return rule;
    }

    private static IRule Or(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.Or;
        rule.Connectives.Add(proposition);
        return rule;
    }

    public static IRule Then(IRule rule, IProposition proposition)
    {
        if (rule.IsFinalized) throw new FinalizedRuleException();

        if (rule.Antecedent == null) throw new MissingAntecedentException();

        proposition.Connective = Connective.Then;
        rule.Consequent = proposition;
        rule.IsFinalized = true;
        return rule;
    }
}
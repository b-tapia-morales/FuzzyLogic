using System.Collections.Immutable;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Proposition;
using FuzzyLogic.Proposition.Enums;
using static System.StringComparison;

// ReSharper disable NonReadonlyMemberInGetHashCode

namespace FuzzyLogic.Rule;

public sealed class FuzzyRule : IRule
{
    private static readonly IComparer<IRule> DefaultComparer = new HighestPriority();

    public ILinguisticBase LinguisticBase { get; }
    public IProposition? Conditional { get; set; }
    public ICollection<IProposition> Connectives { get; } = new List<IProposition>();
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; } = false;
    public RulePriority Priority { get; }

    private FuzzyRule(ILinguisticBase linguisticBase, RulePriority priority = RulePriority.Normal)
    {
        LinguisticBase = linguisticBase;
        Priority = priority;
    }

    public override string ToString() =>
        $"{Conditional} {(Connectives.Count != 0 ? $"{string.Join(' ', Connectives)} " : string.Empty)}{Consequent}";

    public bool Equals(IRule? other)
    {
        if (other is null)
            return false;
        if (ReferenceEquals(this, other))
            return true;
        return Conditional != null && Conditional.Equals(other.Conditional) &&
               Connectives.SequenceEqual(other.Connectives) &&
               Consequent != null && Consequent.Equals(other.Consequent);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((FuzzyRule) obj);
    }

    public override int GetHashCode() => HashCode.Combine(Conditional, Connectives, Consequent);

    public int Compare(IRule? x, IRule? y) => DefaultComparer.Compare(x, y);

    public int CompareTo(IRule? other) => DefaultComparer.Compare(this, other);

    public static IRule Create(ILinguisticBase linguisticBase, RulePriority priority = RulePriority.Normal) => new FuzzyRule(linguisticBase, priority);

    public bool IsValid() => Conditional != null && Consequent != null;

    public bool IsApplicable(IDictionary<string, double> facts) =>
        Conditional != null && Consequent != null &&
        facts.ContainsKey(Conditional.VariableName) &&
        Connectives.Count == 0 || Connectives.All(e => facts.ContainsKey(e.VariableName));

    public bool PremiseContains(string variableName) =>
        Conditional != null &&
        string.Equals(Conditional.VariableName, variableName, InvariantCultureIgnoreCase) ||
        Connectives.Any(e => string.Equals(e.VariableName, variableName, InvariantCultureIgnoreCase));

    public bool ConsequentContains(string variableName) =>
        Consequent != null &&
        string.Equals(Consequent.VariableName, variableName, InvariantCultureIgnoreCase);

    public int PremiseLength() => (Conditional == null ? 1 : 0) + Connectives.Count;

    public IEnumerable<FuzzyNumber> ApplyUnaryOperators(IDictionary<string, double> facts, INegation negation) =>
        !IsApplicable(facts)
            ? ImmutableList<FuzzyNumber>.Empty
            : Connectives
                .Prepend(Conditional!)
                .Select(e => e.ApplyUnaryOperators(facts[e.VariableName], negation));

    public FuzzyNumber EvaluatePremiseWeight(IDictionary<string, double> facts,
        INegation negation, INorm tNorm, IConorm tConorm)
    {
        var numbers = new Queue<FuzzyNumber>(ApplyUnaryOperators(facts, negation));
        switch (numbers.Count)
        {
            case 0:
                throw new InvalidOperationException();
            case 1:
                return numbers.First();
        }

        var connectives = new Queue<Connective>(Connectives.Select(e => e.Connective));
        while (numbers.Count > 1)
        {
            var a = numbers.Dequeue();
            var b = numbers.Dequeue();
            var operation = connectives.Dequeue() == Connective.And ? tNorm.Intersection(a, b) : tConorm.Union(a, b);
            numbers.Enqueue(operation);
        }

        return numbers.Dequeue();
    }

    public FuzzyNumber EvaluateConclusionWeight(IDictionary<string, double> facts, INegation negation)
    {
        if (Consequent == null || !facts.TryGetValue(Consequent.VariableName, out var crispNumber))
            throw new InvalidOperationException();
        return Consequent.ApplyUnaryOperators(crispNumber, negation);
    }

    public FuzzyNumber EvaluateRuleWeight(IDictionary<string, double> facts, INegation negation,
        INorm tNorm, IConorm tConorm, IResiduum residuum)
    {
        if (!IsApplicable(facts))
            throw new InvalidOperationException();

        return residuum.Implication(EvaluatePremiseWeight(facts, negation, tNorm, tConorm), EvaluateConclusionWeight(facts, negation));
    }

    public IRule If(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddAntecedent(variableName, Literal.Is, linguisticHedge, termName);

    public IRule IfNot(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddAntecedent(variableName, Literal.IsNot, linguisticHedge, termName);

    public IRule And(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddConnective(Connective.And, variableName, Literal.Is, linguisticHedge, termName);

    public IRule AndNot(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddConnective(Connective.And, variableName, Literal.IsNot, linguisticHedge, termName);

    public IRule Or(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddConnective(Connective.Or, variableName, Literal.Is, linguisticHedge, termName);

    public IRule OrNot(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddConnective(Connective.Or, variableName, Literal.IsNot, linguisticHedge, termName);

    public IRule Then(string variableName, LinguisticHedge linguisticHedge, string termName) =>
        this.AddConsequent(variableName, Literal.Is, linguisticHedge, termName);
}

file static class FuzzyRuleExtensions
{
    public static IRule AddAntecedent(this IRule rule, string variableName, Literal literal, LinguisticHedge linguisticHedge, string termName)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Conditional != null)
            throw new DuplicatedAntecedentException();

        if (!rule.LinguisticBase.ContainsFunction(variableName, termName))
            throw new InvalidOperationException();

        var membershipFunction = rule.LinguisticBase.RetrieveFunction(variableName, termName)!;
        rule.Conditional = new FuzzyProposition(variableName, Connective.If, literal, linguisticHedge, membershipFunction);
        return rule;
    }

    public static IRule AddConnective(this IRule rule, Connective connective, string variableName, Literal literal, LinguisticHedge linguisticHedge, string termName)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Conditional == null)
            throw new MissingAntecedentException();

        if (!rule.LinguisticBase.ContainsFunction(variableName, termName))
            throw new InvalidOperationException();

        var membershipFunction = rule.LinguisticBase.RetrieveFunction(variableName, termName)!;
        rule.Connectives.Add(new FuzzyProposition(variableName, connective, literal, linguisticHedge, membershipFunction));
        return rule;
    }

    public static IRule AddConsequent(this IRule rule, string variableName, Literal literal, LinguisticHedge linguisticHedge, string termName)
    {
        if (rule.IsFinalized)
            throw new FinalizedRuleException();
        if (rule.Conditional == null)
            throw new MissingAntecedentException();
        if (literal == Literal.IsNot)
            throw new NegatedConsequentException();

        if (!rule.LinguisticBase.ContainsFunction(variableName, termName))
            throw new InvalidOperationException();

        var membershipFunction = rule.LinguisticBase.RetrieveFunction(variableName, termName)!;
        rule.Consequent = new FuzzyProposition(variableName, Connective.Then, literal, linguisticHedge, membershipFunction);
        return rule;
    }
}
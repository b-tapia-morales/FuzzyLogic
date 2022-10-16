using FuzzyLogic.Proposition;

namespace FuzzyLogic.Rule;

public interface IRule
{
    public IProposition? Antecedent { get; set; }
    public ICollection<IProposition> Connectives { get; }
    public IProposition? Consequent { get; set; }
    public bool IsFinalized { get; set; }

    IRule If(IProposition proposition);

    IRule And(IProposition proposition);

    IRule Or(IProposition proposition);

    IRule Then(IProposition proposition);
}
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Number;

namespace FuzzyLogic.Enum.Family;

public interface IOperatorFamily : INegation, INorm, IConorm, IResiduum
{
    INegation INegation { get; set; }
    INorm Norm { get; set; }
    IConorm Conorm { get; set; }
    IResiduum Residuum { get; set; }

    FuzzyNumber INegation.Complement(FuzzyNumber x) => INegation.Complement(x);

    FuzzyNumber INorm.Intersection(FuzzyNumber x, FuzzyNumber y) => Norm.Intersection(x, y);

    FuzzyNumber IConorm.Union(FuzzyNumber x, FuzzyNumber y) => Conorm.Union(x, y);

    FuzzyNumber IResiduum.Implication(FuzzyNumber x, FuzzyNumber y) => Residuum.Implication(x, y);
}
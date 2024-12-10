using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;

namespace FuzzyLogic.Enum.Family;

public class OperatorFamily(INegation negation, INorm norm, IConorm conorm, IResiduum residuum) : IOperatorFamily
{
    public INegation Negation { get; set; } = negation;
    public INorm Norm { get; set; } = norm;
    public IConorm Conorm { get; set; } = conorm;
    public IResiduum Residuum { get; set; } = residuum;
}

public enum CanonicalType
{
    Godel,
    Product,
    Lukasiewicz,
    Nilpotent
}

public enum ParameterizedType
{
    Hamacher,
    Sugeno
}
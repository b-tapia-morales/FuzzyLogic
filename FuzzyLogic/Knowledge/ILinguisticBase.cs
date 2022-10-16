using FuzzyLogic.Linguistics;
using FuzzyLogic.MembershipFunctions.Real;

namespace FuzzyLogic.Knowledge;

public interface ILinguisticBase
{
    public IDictionary<string, IVariable> LinguisticVariables { get; }

    ILinguisticBase AddLinguisticVariable(IVariable variable);

    ILinguisticBase AddAllLinguisticVariables(ICollection<IVariable> variables);

    bool ContainsLinguisticVariable(string name);

    IVariable? RetrieveLinguisticVariable(string name);

    bool ContainsLinguisticEntry(string variableName, string entryName) =>
        RetrieveLinguisticEntry(variableName, entryName) != null;

    IRealFunction? RetrieveLinguisticEntry(string variableName, string entryName) =>
        RetrieveLinguisticVariable(variableName)?.RetrieveLinguisticEntry(entryName);

    static abstract ILinguisticBase Create();
}
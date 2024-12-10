﻿using FuzzyLogic.Function.Interface;
using FuzzyLogic.Variable;
using static System.StringComparer;

namespace FuzzyLogic.Knowledge.Linguistic;

public class LinguisticBase : ILinguisticBase
{
    public IDictionary<string, IVariable> LinguisticVariables { get; } = new Dictionary<string, IVariable>(InvariantCultureIgnoreCase);

    public static ILinguisticBase Create() => new LinguisticBase();

    public static ILinguisticBase Create(params IEnumerable<IVariable> variables) => Create(variables.ToList());

    public static ILinguisticBase Create(ICollection<IVariable> variables)
    {
        if (new HashSet<string>(variables.Select(e => e.Name)).Count != variables.Count)
            throw new InvalidOperationException();

        var @base = new LinguisticBase();
        @base.AddAll(variables);
        return @base;
    }

    public bool ContainsVariable(string name) =>
        LinguisticVariables.ContainsKey(name);

    public IVariable? RetrieveVariable(string name) => LinguisticVariables.TryGetValue(name, out var variable) ? variable : null;

    public bool ContainsTerm(string variable, string term) => RetrieveTerm(variable, term) != null;

    public IMembershipFunction? RetrieveTerm(string variable, string term) =>
        RetrieveVariable(variable)?.RetrieveFunction(term);

    public void Add(IVariable variable)
    {
        if (!LinguisticVariables.TryAdd(variable.Name, variable))
            throw new InvalidOperationException();
    }

    public void AddAll(params IEnumerable<IVariable> variables) => this.AddMultiple(variables);

    public void AddAll(ICollection<IVariable> variables) => this.AddRange(variables);

    public override string ToString() => $"{string.Join(Environment.NewLine, LinguisticVariables.Values)}";
}

file static class BaseExtensions
{
    public static void AddMultiple(this LinguisticBase @base, params IEnumerable<IVariable> variables) =>
        AddRange(@base, variables.ToList());

    public static void AddRange(this LinguisticBase @base, ICollection<IVariable> variables)
    {
        if (variables.Any(e => @base.ContainsVariable(e.Name)))
            throw new InvalidOperationException();

        foreach (var variable in variables)
            @base.LinguisticVariables.Add(variable.Name, variable);
    }
}
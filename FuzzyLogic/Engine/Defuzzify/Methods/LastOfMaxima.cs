﻿using FuzzyLogic.Rule;
using static FuzzyLogic.Engine.Defuzzify.IDefuzzifier;

namespace FuzzyLogic.Engine.Defuzzify.Methods;

public class LastOfMaxima : IDefuzzifier
{
    public double? Defuzzify(ICollection<IRule> rules, IDictionary<string, double> facts)
    {
        RulesCheck(rules, facts);
        var tuple = rules
            .Select(e => (Function: e.Consequent!.Function, Weight: e.EvaluatePremiseWeight(facts).GetValueOrDefault()))
            .MaxBy(e => e.Weight);
        if (tuple.Weight == 0)
            return null;
        var (function, weight) = tuple;
        return function.LambdaCutInterval(weight).X2;
    }
}
using System.Collections;
using FuzzyLogic.Function.Real;
using FuzzyLogic.Knowledge.Linguistic;
using FuzzyLogic.Number;
using FuzzyLogic.Variable;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

// ReSharper disable RedundantExplicitTupleComponentName

namespace FuzzyLogic.Tests.LinguisticVariableTests;

public class LinguisticBaseTests
{
    private static readonly ServiceProvider ServiceProvider = TipProblemProvider.ConfigureTipProblemProvider();
    private static ILinguisticBase LinguisticBaseExample => ServiceProvider.GetService<ILinguisticBase>() ?? throw new NullReferenceException();

    [Theory]
    [InlineData("food quality")]
    [InlineData("service quality")]
    [InlineData("tip")]
    public void VariablesAreRecognizedByTheirNames(string variableName)
    {
        Assert.True(LinguisticBaseExample.ContainsVariable(variableName));
        Assert.True(LinguisticBaseExample.TryGetVariable(variableName, out var variable));
        Assert.NotNull(variable);
        Assert.Equal(LinguisticBaseExample.RetrieveVariable(variableName), variable);
    }

    [Fact]
    public void AdditionOfDuplicatedNamesFails()
    {
        Assert.Throws<InvalidOperationException>(() => LinguisticBaseExample.Add(LinguisticVariable.Create("food quality")));
        Assert.Throws<InvalidOperationException>(() => LinguisticBaseExample.Add(LinguisticVariable.Create("service quality")));
        Assert.Throws<InvalidOperationException>(() => LinguisticBaseExample.Add(LinguisticVariable.Create("tip")));
    }

    [Theory]
    [ClassData(typeof(VariableData))]
    public void TermsAreRecognizedByTheirNames(string variable, string term)
    {
        Assert.True(LinguisticBaseExample.ContainsFunction(variable, term));
        Assert.True(LinguisticBaseExample.TryGetFunction(variable, term, out var function));
        Assert.NotNull(function);
        Assert.Equal(LinguisticBaseExample.RetrieveFunction(variable, term), function);
        Assert.IsType<TriangularFunction>(function);
    }

    [Theory]
    [ClassData(typeof(VariableData))]
    public void TriangularFunctionPropertiesAreCorrect(string variable, string term)
    {
        Assert.True(LinguisticBaseExample.TryGetFunction(variable, term, out var function));
        Assert.NotNull(function);
        Assert.True(function.IsClosed());
        Assert.True(function.IsNormal());
        Assert.True(function.IsPrototypical());
        Assert.Equal(function.PeakLeft().GetValueOrDefault(), function.PeakRight().GetValueOrDefault(), FuzzyNumber.Epsilon);
        Assert.Equal(function.CoreLeft().GetValueOrDefault(), function.CoreRight().GetValueOrDefault(), FuzzyNumber.Epsilon);
        Assert.Equal(function.PeakLeft().GetValueOrDefault(), function.CoreLeft().GetValueOrDefault(), FuzzyNumber.Epsilon);
        Assert.Equal(function.PeakRight().GetValueOrDefault(), function.CoreRight().GetValueOrDefault(), FuzzyNumber.Epsilon);
    }
}

file class VariableData : IEnumerable<object[]>
{
    private static readonly IList<(string Variable, IList<string> Terms)> UnflattenedData =
    [
        ("food quality", ["bad", "decent", "great"]),
        ("service quality", ["poor", "acceptable", "amazing"]),
        ("tip", ["low", "medium", "high"])
    ];

    private static IEnumerable<object[]> FlattenedData => UnflattenedData
        .SelectMany(tuple => tuple.Terms.Select(term => (Variable: tuple.Variable, Term: term)))
        .Select(tuple => new[] {tuple.Variable, tuple.Term});

    public IEnumerator<object[]> GetEnumerator() => FlattenedData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
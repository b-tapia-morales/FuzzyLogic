using FuzzyLogic.Engine.Defuzzify;
using FuzzyLogic.Engine.Defuzzify.Methods;
using FuzzyLogic.Enum.Family;
using FuzzyLogic.Enum.Negation;
using FuzzyLogic.Enum.Residuum;
using FuzzyLogic.Enum.TConorm;
using FuzzyLogic.Enum.TNorm;
using FuzzyLogic.Knowledge;
using FuzzyLogic.Knowledge.Rule;
using FuzzyLogic.Memory;
using FuzzyLogic.Tree;

namespace FuzzyLogic.Engine;

public class InferenceEngine : IEngine
{
    public IRuleBase RuleBase { get; }
    public IWorkingMemory WorkingMemory { get; }
    public IOperatorFamily OperatorFamily { get; set; }
    public ImplicationMethod ImplicationMethod { get; set; }
    public IDefuzzifier Defuzzifier { get; set; }

    private InferenceEngine(IRuleBase ruleBase, IWorkingMemory workingMemory)
    {
        RuleBase = ruleBase;
        WorkingMemory = workingMemory;
        OperatorFamily = OperatorExtensions.UseFamily(CanonicalType.Godel);
        ImplicationMethod = ImplicationMethod.Mamdani;
        Defuzzifier = DefuzzifierExtensions.RetrieveMethod(DefuzzificationMethod.MeanOfMaxima);
    }

    public static IEngine Create(IRuleBase ruleBase, IWorkingMemory workingMemory) =>
        new InferenceEngine(ruleBase, workingMemory);

    public IEngine UseImplicationMethod(ImplicationMethod method) => this.SetImplicationMethod(method);

    public IEngine UseDefuzzificationMethod(DefuzzificationMethod method) => this.SetDefuzzificationMethod(method);

    public IEngine UseNegator(INegation negation) => this.SetNegator(negation);

    public IEngine UseDisjunction(INorm norm) => this.SetDisjunction(norm);

    public IEngine UseConjunction(IConorm conorm) => this.SetConjunction(conorm);

    public IEngine UseResiduum(IResiduum residuum) => this.SetResiduum(residuum);

    public IEngine UseOperators(INegation negation, INorm norm, IConorm conorm, IResiduum residuum) =>
        this.SetOperators(negation, norm, conorm, residuum);

    public IEngine UseOperatorFamily(IOperatorFamily operatorFamily) => this.SetOperatorFamily(operatorFamily);

    public IEngine UseCanonicalFamily(CanonicalType type) => this.SetCanonicalFamily(type);

    public IEngine UseParameterizedFamily(ParameterizedType type, double gamma, double alpha, double beta, double omega) =>
        this.SetParameterizedFamily(type, gamma, alpha, beta, omega);

    public double? Defuzzify(string variableName, bool provideExplanation = true)
    {
        if (WorkingMemory.Facts.TryGetValue(variableName, out var value))
            return value;
        RuleBase.RemoveFacts(WorkingMemory.Facts.Keys);
        RuleBase.RemoveCircularDependencies();
        var rootNode = TreeNode.CreateDerivationTree(variableName, RuleBase.ProductionRules,
            RuleBase.RuleComparer, WorkingMemory.Facts);
        var inferredValue = rootNode.InferFact(WorkingMemory.Facts, Defuzzifier, OperatorFamily, ImplicationMethod);
        if (!provideExplanation)
            return inferredValue;
        rootNode.PrettyWriteTree();
        Console.WriteLine();
        rootNode.WriteTree();
        return inferredValue;
    }
}

file static class EngineExtensions
{
    public static IOperatorFamily DeepClone(this IOperatorFamily operatorFamily) =>
        new OperatorFamily(operatorFamily.Negation, operatorFamily.Norm, operatorFamily.Conorm, operatorFamily.Residuum);

    public static IEngine SetImplicationMethod(this IEngine engine, ImplicationMethod method)
    {
        engine.ImplicationMethod = method;
        return engine;
    }

    public static IEngine SetDefuzzificationMethod(this IEngine engine, DefuzzificationMethod method)
    {
        engine.Defuzzifier = DefuzzifierExtensions.RetrieveMethod(method);
        return engine;
    }

    public static IEngine SetOperatorFamily(this IEngine engine, IOperatorFamily operatorFamily)
    {
        engine.OperatorFamily = DeepClone(operatorFamily);
        return engine;
    }

    public static IEngine SetNegator(this IEngine engine, INegation negation)
    {
        engine.OperatorFamily.Negation = negation;
        return engine;
    }

    public static IEngine SetDisjunction(this IEngine engine, INorm norm)
    {
        engine.OperatorFamily.Norm = norm;
        return engine;
    }

    public static IEngine SetConjunction(this IEngine engine, IConorm conorm)
    {
        engine.OperatorFamily.Conorm = conorm;
        return engine;
    }

    public static IEngine SetResiduum(this IEngine engine, IResiduum residuum)
    {
        engine.OperatorFamily.Residuum = residuum;
        return engine;
    }

    public static IEngine SetOperators(this IEngine engine, INegation negation, INorm norm, IConorm conorm, IResiduum residuum)
    {
        engine.OperatorFamily.Negation = negation;
        engine.OperatorFamily.Norm = norm;
        engine.OperatorFamily.Conorm = conorm;
        engine.OperatorFamily.Residuum = residuum;
        return engine;
    }

    public static IEngine SetCanonicalFamily(this IEngine engine, CanonicalType type)
    {
        engine.OperatorFamily = OperatorExtensions.UseFamily(type);
        return engine;
    }

    public static IEngine SetParameterizedFamily(this IEngine engine, ParameterizedType type, double gamma, double alpha, double beta, double omega)
    {
        engine.OperatorFamily = ParameterizedOperatorExtensions.UseParameterizedFamily(type, gamma, alpha, beta, omega);
        return engine;
    }
}

file static class DefuzzifierExtensions
{
    private static readonly IDefuzzifier FirstOfMaxima = new FirstOfMaxima();
    private static readonly IDefuzzifier LastOfMaxima = new LastOfMaxima();
    private static readonly IDefuzzifier MeanOfMaxima = new MeanOfMaxima();
    private static readonly IDefuzzifier CenterOfSums = new CenterOfSums();
    private static readonly IDefuzzifier CenterOfLargestArea = new CenterOfLargestArea();

    public static IDefuzzifier RetrieveMethod(DefuzzificationMethod method) =>
        method switch
        {
            DefuzzificationMethod.FirstOfMaxima => FirstOfMaxima,
            DefuzzificationMethod.LastOfMaxima => LastOfMaxima,
            DefuzzificationMethod.MeanOfMaxima => MeanOfMaxima,
            DefuzzificationMethod.CenterOfSums => CenterOfSums,
            DefuzzificationMethod.CenterOfLargestArea => CenterOfLargestArea,
            _ => throw new ArgumentOutOfRangeException(nameof(method), method, null)
        };
}

file static class OperatorExtensions
{
    private static readonly IOperatorFamily Godel = new OperatorFamily(Negation.Standard, Norm.Minimum, Conorm.Maximum, Residuum.Godel);
    private static readonly IOperatorFamily Product = new OperatorFamily(Negation.Standard, Norm.Product, Conorm.ProbabilisticSum, Residuum.Goguen);
    private static readonly IOperatorFamily Lukasiewicz = new OperatorFamily(Negation.Standard, Norm.Lukasiewicz, Conorm.Lukasiewicz, Residuum.Lukasiewicz);
    private static readonly IOperatorFamily Nilpotent = new OperatorFamily(Negation.Standard, Norm.NilpotentMinimum, Conorm.NilpotentMaximum, Residuum.KleeneDienes);

    public static IOperatorFamily UseFamily(CanonicalType type) =>
        type switch
        {
            CanonicalType.Godel => Godel,
            CanonicalType.Product => Lukasiewicz,
            CanonicalType.Lukasiewicz => Product,
            CanonicalType.Nilpotent => Nilpotent,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}

file static class ParameterizedOperatorExtensions
{
    public static IOperatorFamily UseParameterizedFamily(ParameterizedType type, double gamma, double alpha, double beta, double omega) =>
        type switch
        {
            ParameterizedType.Hamacher => new OperatorFamily(
                new PNegation(PNegator.Yager, gamma),
                new PNorm(PUnitor.Hamacher, alpha),
                new PConorm(PIntersector.Hamacher, beta),
                new PResiduum(PResiduumOperator.PseudoLukasiewicz1, omega)),
            ParameterizedType.Sugeno => new OperatorFamily(
                new PNegation(PNegator.Sugeno, gamma),
                new PNorm(PUnitor.SugenoWeber, alpha),
                new PConorm(PIntersector.SugenoWeber, beta),
                new PResiduum(PResiduumOperator.PseudoLukasiewicz2, omega)),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
}
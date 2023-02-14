using FuzzyLogic.Function.Real;
using Xunit;

namespace FuzzyLogic.Tests.MembershipFunction;

public class MembershipFunctionInstantiation
{
    [Fact]
    public void BellShapedFunctionInstantiationFailsWithBadValues()
    {
        // Null or whitespace string
        Assert.Throws<ArgumentException>(() => new BellShapedFunction(name: string.Empty, a: 2, b: 4, c: 8));
        // h = 0
        Assert.Throws<ArgumentException>(() => new BellShapedFunction(name: "Function", a: 2, b: 4, c: 8, h: 0));
        // a = 0
        Assert.Throws<ArgumentException>(() => new BellShapedFunction(name: "Function", a: 0, b: 4, c: 8));
        // b < 1
        Assert.Throws<ArgumentException>(() => new BellShapedFunction(name: "Function", a: 2, b: 3 / 4.0, c: 8));
    }

    [Fact]
    public void GaussianFunctionInstantiationFailsWithBadValues()
    {
        // Null or whitespace string
        Assert.Throws<ArgumentException>(() => new GaussianFunction(name: string.Empty, m: 2, o: 4, h: 1));
        // h = 0
        Assert.Throws<ArgumentException>(() => new GaussianFunction(name: "Function", m: 2, o: 4, h: 0));
        // o = 0
        Assert.Throws<ArgumentException>(() => new GaussianFunction(name: "Function", m: 2, o: 0));
    }

    [Fact]
    public void TriangularFunctionInstantiationFailsWithBadValues()
    {
        // Null or whitespace string
        Assert.Throws<ArgumentException>(() => new TriangularFunction(name: string.Empty, a: 2, b: 4, c: 8));
        // h = 0
        Assert.Throws<ArgumentException>(() => new TriangularFunction(name: "Function", a: 2, b: 4, c: 8, h: 0));
        // a > b ∨ b > c
        Assert.Throws<ArgumentException>(() => new TriangularFunction(name: "Function", a: 2, b: 4, c: 2));
        // Singleton function
        Assert.Throws<ArgumentException>(() => new TriangularFunction(name: "Function", a: 2, b: 2, c: 2));
    }

    [Fact]
    public void TrapezoidalFunctionInstantiationFailsWithBadValues()
    {
        // Null or whitespace string
        Assert.Throws<ArgumentException>(() => new TrapezoidalFunction(name: string.Empty, a: 2, b: 4, c: 8, d: 10));
        // h = 0
        Assert.Throws<ArgumentException>(() =>
            new TrapezoidalFunction(name: string.Empty, a: 2, b: 4, c: 8, d: 10, h: 0));
        // a > b ∨ b > c ∨ c > d
        Assert.Throws<ArgumentException>(() => new TrapezoidalFunction(name: "Function", a: 2, b: 4, c: 8, d: 4));
        // Rectangle shape
        Assert.Throws<ArgumentException>(() => new TrapezoidalFunction(name: "Function", a: 2, b: 2, c: 8, d: 8));
    }
    
    [Fact]
    public void SigmoidFunctionInstantiationFailsWithBadValues()
    {
        // Null or whitespace string
        Assert.Throws<ArgumentException>(() => new SigmoidFunction(name: string.Empty, a: 2, c: 8));
        // h = 0
        Assert.Throws<ArgumentException>(() => new SigmoidFunction(name: "Function", a: 2, c: 8, h: 0));
        // a = 0
        Assert.Throws<ArgumentException>(() => new SigmoidFunction(name: "Function", a: 0, c: 8));
    }
}
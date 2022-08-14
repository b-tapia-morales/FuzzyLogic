using FuzzyLogic.Linguistics;
using FuzzyLogic.Memory;
using FuzzyLogic.Rule;

var water = new LinguisticVariable("Water");
water.AddTrapezoidFunction("Cold", 0, 0, 20, 40);
water.AddTriangularFunction("Warm", 30, 50, 70);
water.AddTrapezoidFunction("Hot", 50, 80, 100, 100);
water.AddGaussianFunction("Sorta hot", 16, 4);
Console.WriteLine(water.Is("Cold"));
Console.WriteLine(water.IsNot("Sorta hot"));
var random = new Random();
foreach (var function in water.LinguisticValues)
{
    var randomInt = random.Next(12, 48);
    Console.WriteLine(function.BoundaryInterval());
    Console.WriteLine(function.ToPoint(randomInt));
    Console.WriteLine($"Random int: {randomInt} - Membership degree: {function.MembershipDegree(randomInt)}");
}

var rule = FuzzyRule
    .Initialize()
    .If(water.Is("Hot"))
    .Or(water.Is("Sorta hot"))
    .Then(water.IsNot("Cold"));
Console.WriteLine(rule);

var workingMemory = WorkingMemory.InitializeFromFile();
Console.WriteLine(workingMemory);
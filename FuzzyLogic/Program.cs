using FuzzyLogic.Linguistics;

var water = new LinguisticVariable<int>("Water");
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
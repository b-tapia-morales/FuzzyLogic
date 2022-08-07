using FuzzyLogic.MembershipFunctions;
using FuzzyLogic.Set;

var water = new FuzzySet<double>("Water");
water.AddTrapezoidFunction("Cold", 0, 0, 20, 40);
water.AddTriangularFunction("Warm", 30, 50, 70);
water.AddTrapezoidFunction("Hot", 50, 80, 100, 100);
var random = new Random();
foreach (var function in water.MembershipFunctions)
{
    var randomInt = random.Next(100);
    Console.WriteLine(((BaseTrapezoidalFunction<double>) function).CoreBoundaries());
    Console.WriteLine(function.ToPoint(randomInt));
    Console.WriteLine($"Random int: {randomInt} - Membership degree: {function.MembershipDegree(randomInt)}");
}
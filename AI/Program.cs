using System;
using AIContinuous;

double FuncA(double x){ return x + 1; }

double MyFunction(double x) { return x * x; }

double MyDer(double x) { return 2.00 * x; }

double MySqrt(double x) { return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10; }

double MyNewDer(double x) { return (1 / (2 * Math.Sqrt(Math.Abs(x))) - 5) * x + Math.Sqrt(Math.Abs(x));}

double Function(double x ) { return Math.Pow(x - 1, 2) + Math.Sin(Math.Pow(x,3));}

double sol;

var date = DateTime.Now;
date = DateTime.Now;
// sol = Root.Bisection(MySqrt, -10.0, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.FalsePosition(MySqrt, -10.0, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.Newton(MyFunction, MyDer, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.Newton(MySqrt, MyNewDer, 10.0);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Root.Newton(MySqrt, double (double x) => Diff.Differentiate(MySqrt, x), 10.0 );
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
sol = Optmize.Newton(MySqrt, 10.00);
Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Optmize.Newton(Function, 1.00);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

date = DateTime.Now;
sol = Optmize.GradientDescent(MySqrt, 10.00);
Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");

// date = DateTime.Now;
// sol = Optmize.Newton(Function, 1.00);
// Console.WriteLine($"Solution: {sol} | Time: {(DateTime.Now - date).TotalMilliseconds}");









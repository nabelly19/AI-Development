using System;
using AIContinuous;

double FuncA(double x){ return x + 1; }

double MyFunction(double x) { return x * x; }

double MyDer(double x) { return 2.00 * x; }

double MySqrt(double x) { return (Math.Sqrt(Math.Abs(x)) - 5) * x + 10; }

double MyNewDer(double x) { return (1 / (2 * Math.Sqrt(Math.Abs(x))) - 5) * x + Math.Sqrt(Math.Abs(x));}

double Function(double x ) { return Math.Pow(x - 1, 2) + Math.Sin(Math.Pow(x,3));}

// double GradientFunction(double[] x) { return Math.Pow(x[0],2) + Math.Pow(x[1],2); }

double GradientFunction(double[] x) { return Math.Pow(x[0] + 2.00 * x[1],2) + Math.Pow( 2.00 * x[0] + x[1] - 5, 2); }

double RosenBrockFunction(double[] x) {

    double hellyeah = 0;
    var n = x.Length;
    int ni = n - 1;
    for( int i = 0; i < ni; i ++)
    {
        hellyeah += 100 * (x[i+1] - x[0] * x[0] ) * (x[i+1] - x[0] * x[0] ) + (1 - x[i]) * (1 - x[i]);
    }
    return hellyeah;
}

double[] sol;

var date = DateTime.Now;
date = DateTime.Now;

var num = new double[] {10,10};
double lr;
double atol;

date = DateTime.Now;
sol = Optmize.GradientDescent(RosenBrockFunction, num, lr = 1e-5, atol = 1e-9);
Console.WriteLine($"Solution: {sol[0]} {sol[1]} | Time: {(DateTime.Now - date).TotalMilliseconds}");










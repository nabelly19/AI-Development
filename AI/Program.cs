using System;
using System.Collections.Generic;
using AIContinuous;

// double[] sol;
// var num = new double[] {10,10};

List<double[]> bounds = new() {
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
    new double[] {-10.0, 10.0},
};

double Restriction(double[] x)
{
    return -1.0;
}

double RosenBrockFunction(double[] x)
{

    double hellyeah = 0;
    var n = x.Length;
    int ni = n - 1;
    for (int i = 0; i < ni; i++)
    {
        hellyeah += 100 * (x[i + 1] - x[0] * x[0]) * (x[i + 1] - x[0] * x[0]) + (1 - x[i]) * (1 - x[i]);
    }
    return hellyeah;
}

var date = DateTime.Now;

var diffEvolution = new DiffEvolution(RosenBrockFunction, bounds, 100, Restriction);
var res = diffEvolution.Optimize(1000);

date = DateTime.Now;

Console.WriteLine($"Solution: {res[0]} {res[1]} | Time: {(DateTime.Now - date).TotalMilliseconds}");










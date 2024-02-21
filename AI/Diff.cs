using System;

namespace AIContinuous;

public static class Diff
{
    public static double Differentiate(Func<double, double> function, double x, double h = 1e-2)
        => (function(x + h) - function(x - h) )/(2.00 * h);

    public static double Differentiate5P(Func<double, double> function, double x, double h = 1e-2)
        => (function(x - 2.00 * h) - 8.0 * function(x - h) + 8.00 * function(x + h) - function(x + 2.00 * h))/(12.00 * h);
}
using System;

namespace AIContinuous;

public static class Optmize
{
    public static double Newton(Func<double, double> function, double x0,double atol = 1e-4, double h = 1e-2, int maxIter= 10000)
    {
        Func<double, double> diffFunction = x => Diff.Differentiate(function, x, h);
        Func<double, double> diffSecondFunction = x => Diff.Differentiate(diffFunction, x, h);

        return Root.Newton(diffFunction, diffSecondFunction, x0, atol, maxIter);
    }

    public static double GradientDescent(
        Func<double, double> function,
        double x0,
        double lr = 1e-2,
        double atol = 1e-4
    )
    {
        double xp = x0;
        double diff = Diff.Differentiate(function, xp);

        while(Math.Abs(diff) > atol)
        {
            diff = Diff.Differentiate(function, xp);
            xp -= lr * diff;
        }

        return xp;
    }

    public static double[] GradientDescent(
        Func<double[], double> function,
        double[] x0,
        double lr = 1e-2,
        double atol = 1e-4
    )
    {
        var dim = x0.Length;
        var xp = (double[])x0.Clone();
        var diff = Diff.Gradient(function, xp);
        double diffNorm = double.MaxValue;

        do
        {
            diffNorm = 0.0;
            diff =  Diff.Gradient(function, xp);

            for (int i = 0; i < dim; i++)
            {
                xp[i] -= lr * diff[i];
                diffNorm += Math.Abs(diff[i]);
            }
        }  while (diffNorm > dim * atol);

        return xp;
    }
}
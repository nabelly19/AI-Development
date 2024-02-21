using System;

namespace AIContinuous;

public static class Root
{
    public static double Bisection(Func<double, double> function,
    double a, double b, int maxiter = 10000, double tol = 1e-4)
    {
        double c = 0;

        for (int i = 0; i < maxiter; i++)
        {
            var fa = function(a);
            var fc = function(c);

            if (fa * fc < 0)
                b = c;
            else
                a = c;

            if (fc < tol)
                break;

            c = a + b / 2.0;
        }
        return c;
    }

    public static double FalsePosition(Func<double, double> function,
    double a, double b, int maxiter = 10000, double tol = 1e-4)
    {
        double c = 0;

        for (int i = 0; i < maxiter; i++)
        {
            var fa = function(a);
            var fb = function(b);
            var newa = fa * ((b - a) /( fb - fa)) + a;
            var m = ((fa - fb) / ( a - b ));
            var k = fb + m * (-b);
            var newPbValue = - k / m; 
            var fc = function(c);

            if (fa * fc < 0)
                b = c;
            else
                a = c;

            if (fc < tol)
                break;

            c = newPbValue;
        }
        return c;
    }

    public static double Newton(
        Func<double, double> function,
        Func<double, double> der,
        double x0,
        double atol = 1e-4,
        int maxIter = 10000
    )
    {
        double xp = x0;

        for(int i = 0; i < maxIter; i ++)
        {
            var fx = function(xp);
            var deri = der(xp);
            xp -= fx / deri;

            if(Math.Abs(fx) < atol)
                break;
        }
        return xp;
    }

}
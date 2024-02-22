using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace AIContinuous;

public class DiffEvolution
{
    protected Func<double[], double> Fitness { get; }
    protected int Dimension { get; set; }
    protected int Npop { get; set; }
    protected List<double[]> Individuals { get; set; }
    protected List<double[]> Bounds { get; set; }
    protected int BestIndividualIndex { get; set; }
    protected double BestIndividualFitness { get; set; } = double.MaxValue;

    public DiffEvolution(
        Func<double[], double> fitness,
        List<double[]> bounds,
        int npop
    )
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.Npop = npop;
        Individuals = new List<double[]>(Npop);
        this.Bounds = bounds;

    }

    private void GeneratePopulation()
    {

        var dimension = Dimension;
        for (int i = 0; i < Npop; i++)
        {
            Individuals[i] = new double[Dimension];
            for (int j = 0; j < Dimension; j++)
            {
                Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][0]);
            }
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = BestIndividualFitness;
        for (int i = 0; i < Npop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        BestIndividualFitness = fitnessBest;
    }

    private double[] Mutate(double[] individual)
    {
        var newIndividual = new double[Dimension];
        newIndividual = Individuals[BestIndividualIndex];
        for(int i = 0; i < Dimension; i ++)
        {
            newIndividual[i] += Individuals[Random.Shared.Next()][i] - Individuals[Random.Shared.Next(Npop)][i];
        }

        return individual;
    }
    public double[] Optimize()
    {
        GeneratePopulation();
        FindBestIndividual();

        return Individuals[BestIndividualIndex];
    }
}

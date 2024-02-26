using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace AIContinuous;

public class DiffEvolution
{
    protected Func<double[], double> Fitness { get; }
    protected Func<double[], double> Restriction { get; }
    protected int Dimension { get; set; }
    protected double MutationMin { get; set; }
    protected double MutationMax { get; set; }
    protected double Recombination { get; set; }
    protected int Npop { get; set; }
    protected List<double[]> Individuals { get; set; }
    protected List<double[]> Bounds { get; set; }
    private double[] IndividualsRestriction { get; set; }
     private double[] IndividualsFitness { get; set; }
    protected int BestIndividualIndex { get; set; }

    public DiffEvolution(
        Func<double[], double> fitness,
        List<double[]> bounds,
        int npop,
        Func<double[], double> restriction,
        double mutationMin = 0.5,
        double mutationMax = 0.9,
        double recombination = 0.8

    )
    {
        this.Fitness = fitness;
        this.Dimension = bounds.Count;
        this.Npop = npop;
        this.Individuals = new List<double[]>(Npop);
        this.Bounds = bounds;
        this.MutationMin = mutationMin;
        this.MutationMax = mutationMax;
        this.Recombination = recombination;
        this.Restriction = restriction;
        this.IndividualsRestriction = new double[Npop];
        this.IndividualsFitness = new double[Npop];

    }

    private void GeneratePopulation()
    {
        var dimension = Dimension;
        for (int i = 0; i < Npop; i++)
        {
            Individuals.Add(new double[dimension]);
            for (int j = 0; j < Dimension; j++)
            {
                Individuals[i][j] = Utils.Rescale(Random.Shared.NextDouble(), Bounds[j][0], Bounds[j][1]);
            }
            IndividualsRestriction[i] = Restriction(Individuals[i]);

            IndividualsFitness[i] =  IndividualsRestriction[i] <= 0.0 ? Fitness(Individuals[i]) : double.MaxValue;
        }
    }

    private void FindBestIndividual()
    {
        var fitnessBest = IndividualsFitness[BestIndividualIndex];
        for (int i = 0; i < Npop; i++)
        {
            var fitnessCurrent = Fitness(Individuals[i]);

            if (fitnessCurrent < fitnessBest)
            {
                BestIndividualIndex = i;
                fitnessBest = fitnessCurrent;
            }
        }
        IndividualsFitness[BestIndividualIndex] = fitnessBest;
    }

    private double[] Mutate(int index)
    {
        var newIndividual = new double[Dimension];

        int individualRand1;
        int individualRand2;

        do individualRand1 = Random.Shared.Next(Npop);
        while (individualRand1 == index);

        do individualRand2 = Random.Shared.Next(Npop);
        while (individualRand2 == individualRand1);


        newIndividual = (double[])Individuals[BestIndividualIndex].Clone();
        for (int i = 0; i < Dimension; i++)
        {
            newIndividual[i] += Utils.Rescale(Random.Shared.NextDouble(), MutationMin, MutationMax) * (Individuals[individualRand1][i] - Individuals[individualRand2][i]);
        }

        return newIndividual;
    }

    protected double[] Crossover(int index)
    {
        var trial = Mutate(index);
        var trial2 = (double[])Individuals[index].Clone();

        for (int i = 0; i < Dimension; i++)
        {
            // if(!((Random.Shared.NextDouble() < Recombination) || (i == Random.Shared.Next(Dimension))))
            if ((Random.Shared.NextDouble() > Recombination) && (i != Random.Shared.Next(Dimension)))
                trial2[i] = trial[i];
        }

        return trial2;
    }

    protected void Iterate()
    {
        for (int i = 0; i < Npop; i++)
        {
            var trial = Crossover(i);

            var restTrial = Restriction(trial);
            var restIndividual = Restriction(Individuals[i]);
            var fitnessTrial = restTrial <= 0.0 ? Fitness(trial) : double.MaxValue;

            if ((restIndividual > 0.0 && (restTrial < restIndividual))
                || (restTrial <= 0.0 && restIndividual > 0.0)
                || (restTrial <= 0.0 && fitnessTrial < IndividualsFitness[i]))
            {
                Individuals[i] = trial;
                IndividualsRestriction[i] =restTrial;
                IndividualsFitness[i] = fitnessTrial;

            }
        }
        FindBestIndividual();
    }

    public double[] Optimize(int n)
    {
        GeneratePopulation();

        for (int i = 0; i < n; i++)
            Iterate();

        return Individuals[BestIndividualIndex];
    }
}

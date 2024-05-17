using System;

class Program
{
    static void Main(string[] args)
    {
        Trainer trainer = new Trainer(
            maxEpisodes: 10000,
            maxStepsPerEpisode: 100,
            learningRate: 0.1,
            discountFactor: 0.99,
            explorationRate: 1.0,
            solvedReward: 1.0,
            stepPenalty: -0.01
        );

        trainer.Train();
    }
}

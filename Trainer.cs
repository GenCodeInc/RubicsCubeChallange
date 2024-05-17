using System;

public class Trainer
{
    private QLearning qLearning;
    private int maxEpisodes;
    private int maxStepsPerEpisode;
    private double solvedReward;
    private double stepPenalty;

    public Trainer(int maxEpisodes, int maxStepsPerEpisode, double learningRate, double discountFactor, double explorationRate, double solvedReward, double stepPenalty)
    {
        this.maxEpisodes = maxEpisodes;
        this.maxStepsPerEpisode = maxStepsPerEpisode;
        this.solvedReward = solvedReward;
        this.stepPenalty = stepPenalty;
        qLearning = new QLearning((float)learningRate, (float)discountFactor, (float)explorationRate);
    }

    public void Train()
    {
        for (int episode = 0; episode < maxEpisodes; episode++)
        {
            RubiksCube cube = new RubiksCube();
            cube.Scramble(20);  // Scramble the cube with 20 random moves
            Console.WriteLine($"Episode {episode} started with scrambled cube:");
            cube.PrintCube();  // Print the scrambled cube to verify

            if (cube.IsSolved())
            {
                Console.WriteLine("Scramble failed. Cube is still in the solved state.");
                continue;
            }

            for (int step = 0; step < maxStepsPerEpisode; step++)
            {
                int action = qLearning.ChooseAction(cube);
                RubiksCube nextCube = new RubiksCube(cube); // Copy current state
                PerformAction(nextCube, action);

                double reward = nextCube.IsSolved() ? solvedReward : stepPenalty;

                qLearning.UpdateQValue(cube, action, nextCube, reward);

                cube = nextCube;

                if (cube.IsSolved())
                {
                    Console.WriteLine($"Solved in episode {episode}, step {step}");
                    break;
                }
            }
        }

        Console.WriteLine("Training completed.");
    }

    private void PerformAction(RubiksCube cube, int action)
    {
        // Implement action logic here
        switch (action)
        {
            case 0: cube.RotateFaceClockwise(0); break;
            case 1: cube.RotateFaceCounterClockwise(0); break;
            case 2: cube.RotateFaceClockwise(1); break;
            case 3: cube.RotateFaceCounterClockwise(1); break;
            case 4: cube.RotateFaceClockwise(2); break;
            case 5: cube.RotateFaceCounterClockwise(2); break;
            case 6: cube.RotateFaceClockwise(3); break;
            case 7: cube.RotateFaceCounterClockwise(3); break;
            case 8: cube.RotateFaceClockwise(4); break;
            case 9: cube.RotateFaceCounterClockwise(4); break;
            case 10: cube.RotateFaceClockwise(5); break;
            case 11: cube.RotateFaceCounterClockwise(5); break;
        }
    }
}

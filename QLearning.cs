using System;
using System.Collections.Generic;

public class QLearning
{
    private float learningRate;
    private float discountFactor;
    private float explorationRate;
    private Dictionary<string, float[]> qTable;

    public QLearning(float learningRate, float discountFactor, float explorationRate)
    {
        this.learningRate = learningRate;
        this.discountFactor = discountFactor;
        this.explorationRate = explorationRate;
        qTable = new Dictionary<string, float[]>();
    }

    public int ChooseAction(RubiksCube cube)
    {
        string state = cube.GetStateString();
        if (!qTable.ContainsKey(state) || new Random().NextDouble() < explorationRate)
        {
            return new Random().Next(12);
        }
        float[] qValues = qTable[state];
        int maxIndex = 0;
        for (int i = 1; i < qValues.Length; i++)
        {
            if (qValues[i] > qValues[maxIndex])
            {
                maxIndex = i;
            }
        }
        return maxIndex;
    }

    public void UpdateQValue(RubiksCube currentCube, int action, RubiksCube nextCube, double reward)
    {
        string currentState = currentCube.GetStateString();
        string nextState = nextCube.GetStateString();

        if (!qTable.ContainsKey(currentState))
        {
            qTable[currentState] = new float[12];
        }
        if (!qTable.ContainsKey(nextState))
        {
            qTable[nextState] = new float[12];
        }

        float[] currentQValues = qTable[currentState];
        float[] nextQValues = qTable[nextState];
        float maxNextQValue = 0;
        foreach (float qValue in nextQValues)
        {
            if (qValue > maxNextQValue)
            {
                maxNextQValue = qValue;
            }
        }

        currentQValues[action] += learningRate * ((float)reward + discountFactor * maxNextQValue - currentQValues[action]);
    }
}

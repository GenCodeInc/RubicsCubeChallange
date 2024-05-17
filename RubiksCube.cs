using System;

public class RubiksCube
{
    private char[,,] cube;
    private static readonly char[] colors = { 'W', 'G', 'R', 'B', 'O', 'Y' };

    public RubiksCube()
    {
        cube = new char[6, 3, 3];
        InitializeCube();
    }

    public RubiksCube(RubiksCube other)
    {
        cube = new char[6, 3, 3];
        for (int face = 0; face < 6; face++)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    cube[face, row, col] = other.cube[face, row, col];
                }
            }
        }
    }

    private void InitializeCube()
    {
        for (int face = 0; face < 6; face++)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    cube[face, row, col] = colors[face];
                }
            }
        }
    }

    public void Scramble(int moves)
    {
        Random rand = new Random();
        for (int i = 0; i < moves; i++)
        {
            int face = rand.Next(6);
            bool clockwise = rand.Next(2) == 0;
            if (clockwise)
            {
                RotateFaceClockwise(face);
            }
            else
            {
                RotateFaceCounterClockwise(face);
            }
        }
    }

    public void RotateFaceClockwise(int face)
    {
        RotateEdges(face, true);
        char[,] faceColors = new char[3, 3];
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                faceColors[row, col] = cube[face, row, col];
            }
        }
        char[,] rotatedColors = RotateMatrixClockwise(faceColors);
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                cube[face, row, col] = rotatedColors[row, col];
            }
        }
    }

    public void RotateFaceCounterClockwise(int face)
    {
        RotateFaceClockwise(face);
        RotateFaceClockwise(face);
        RotateFaceClockwise(face);
    }

    private void RotateEdges(int face, bool clockwise)
    {
        int[] edges = GetEdges(face);
        char[] temp = new char[12];
        if (clockwise)
        {
            for (int i = 0; i < 12; i++)
            {
                temp[i] = cube[edges[i] / 9, (edges[i] / 3) % 3, edges[i] % 3];
            }
            for (int i = 0; i < 12; i++)
            {
                int index = (i + 9) % 12;
                cube[edges[i] / 9, (edges[i] / 3) % 3, edges[i] % 3] = temp[index];
            }
        }
        else
        {
            for (int i = 0; i < 12; i++)
            {
                temp[i] = cube[edges[i] / 9, (edges[i] / 3) % 3, edges[i] % 3];
            }
            for (int i = 0; i < 12; i++)
            {
                int index = (i + 3) % 12;
                cube[edges[i] / 9, (edges[i] / 3) % 3, edges[i] % 3] = temp[index];
            }
        }
    }

    private int[] GetEdges(int face)
    {
        switch (face)
        {
            case 0: // White
                return new int[] { 2, 1, 0, 9, 10, 11, 20, 19, 18, 33, 34, 35 };
            case 1: // Green
                return new int[] { 6, 7, 8, 27, 28, 29, 38, 37, 36, 15, 16, 17 };
            case 2: // Red
                return new int[] { 8, 5, 2, 36, 30, 24, 47, 46, 45, 18, 12, 6 };
            case 3: // Blue
                return new int[] { 24, 25, 26, 15, 14, 13, 44, 43, 42, 29, 23, 17 };
            case 4: // Orange
                return new int[] { 26, 25, 24, 42, 41, 40, 20, 14, 8, 0, 3, 6 };
            case 5: // Yellow
                return new int[] { 35, 32, 29, 11, 10, 9, 45, 44, 43, 26, 23, 20 };
            default:
                throw new ArgumentException("Invalid face value");
        }
    }

    private char[,] RotateMatrixClockwise(char[,] matrix)
    {
        char[,] result = new char[3, 3];
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                result[col, 2 - row] = matrix[row, col];
            }
        }
        return result;
    }

    public void PrintCube()
    {
        string[] faceNames = { "White", "Green", "Red", "Blue", "Orange", "Yellow" };
        for (int face = 0; face < 6; face++)
        {
            Console.WriteLine($"{faceNames[face]} Face:");
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    Console.Write(cube[face, row, col] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }

    public bool IsSolved()
    {
        for (int face = 0; face < 6; face++)
        {
            char color = cube[face, 0, 0];
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (cube[face, row, col] != color)
                    {
                        return false;
                    }
                }
            }
        }
        return true;
    }

    public string GetStateString()
    {
        char[] state = new char[54];
        for (int face = 0; face < 6; face++)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    state[face * 9 + row * 3 + col] = cube[face, row, col];
                }
            }
        }
        return new string(state);
    }
}

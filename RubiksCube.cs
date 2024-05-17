using System;

public class RubiksCube
{
    private char[,,] faces;

    public RubiksCube()
    {
        faces = new char[6, 3, 3] {
            { { 'W', 'W', 'W' }, { 'W', 'W', 'W' }, { 'W', 'W', 'W' } },
            { { 'G', 'G', 'G' }, { 'G', 'G', 'G' }, { 'G', 'G', 'G' } },
            { { 'R', 'R', 'R' }, { 'R', 'R', 'R' }, { 'R', 'R', 'R' } },
            { { 'B', 'B', 'B' }, { 'B', 'B', 'B' }, { 'B', 'B', 'B' } },
            { { 'O', 'O', 'O' }, { 'O', 'O', 'O' }, { 'O', 'O', 'O' } },
            { { 'Y', 'Y', 'Y' }, { 'Y', 'Y', 'Y' }, { 'Y', 'Y', 'Y' } }
        };
    }

    public RubiksCube(RubiksCube cube)
    {
        faces = new char[6, 3, 3];
        Array.Copy(cube.faces, faces, cube.faces.Length);
    }

    public void RotateFaceClockwise(int face)
    {
        char[,] newFace = new char[3, 3];

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newFace[j, 2 - i] = faces[face, i, j];
            }
        }

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                faces[face, i, j] = newFace[i, j];
            }
        }
    }

    public void RotateFaceCounterClockwise(int face)
    {
        RotateFaceClockwise(face);
        RotateFaceClockwise(face);
        RotateFaceClockwise(face);
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

    public bool IsSolved()
    {
        for (int face = 0; face < 6; face++)
        {
            char color = faces[face, 0, 0];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (faces[face, i, j] != color)
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
        string state = "";
        for (int face = 0; face < 6; face++)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    state += faces[face, i, j];
                }
            }
        }
        return state;
    }

    public void PrintCube()
    {
        for (int face = 0; face < 6; face++)
        {
            Console.WriteLine($"Face {face}:");
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(faces[face, i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}

using System.Diagnostics;
using System.Linq;
public static class Part01
{
    public static void Execute()
    {
        string[] inputFile = File.ReadAllLines("input.txt");
        int[,] board = new int[inputFile[0].Length,inputFile.Length];
        calcSideLeft(inputFile, ref board);
        calcSideRight(inputFile, ref board);
        calcSideTop(inputFile, ref board);
        calcSideBottom(inputFile, ref board);
        CountArray(ref board);

    }

    public static void CountArray(ref int[,] board)
    {
        int totalCount = 0;
        System.Console.WriteLine();

        for (int i = 0; i < board.GetLength(1); i++)
        {
            for (int j = 0; j < board.GetLength(0); j++)
            {
                System.Console.Write(board[j,i]);
                totalCount+=board[j,i];
            }
            System.Console.WriteLine();
        }

        System.Console.WriteLine($"totalCount = {totalCount}");
    }

    public static void calcSideBottom(string[] inputFile, ref int[,] board)
    {
        int totalCount = 0;

        for (int i = 0; i < inputFile.Length; i++)
        {
             int rowCount = 0;
            int highestNum = 0;
            for (int j = inputFile[0].Length-1; j >= 0; j--)
            {
                int numberCurr = inputFile[j][i] - 48;
                if (j == inputFile[0].Length-1 || numberCurr > highestNum) {
                    rowCount++;
                    highestNum = numberCurr;
                    board[i, j]=1;
                }
                totalCount+=rowCount;
            }
        }

        System.Console.WriteLine($"Bottom = {totalCount}");
    }

    public static void calcSideTop(string[] inputFile, ref int[,] board)
    {
        int totalCount = 0;

        for (int i = 0; i < inputFile.Length; i++)
        {
             int rowCount = 0;
            int highestNum = 0;
            for (int j = 0; j < inputFile[0].Length; j++)
            {
                int numberCurr = inputFile[j][i] - 48;
                if (j == 0 || numberCurr > highestNum) {
                    rowCount++;
                    highestNum = numberCurr;
                    board[i,j]=1;
                }
                totalCount+=rowCount;
            }
        }

        System.Console.WriteLine($"Top = {totalCount}");
    }
    
    public static void calcSideRight(string[] inputFile, ref int[,] board)
    {
        int totalCount = 0;

        for (int i = 0; i < inputFile.Length; i++)
        {
             int rowCount = 0;
            int highestNum = 0;
            for (int j = inputFile[0].Length-1; j >= 0; j--)
            {
                int numberCurr = inputFile[i][j] - 48;
                if (j == inputFile[0].Length-1 || numberCurr > highestNum) {
                    rowCount++;
                    highestNum = numberCurr;
                    board[j,i]=1;
                }
                totalCount+=rowCount;
            }
        }

        System.Console.WriteLine($"Right = {totalCount}");
    }

    public static void calcSideLeft(string[] inputFile, ref int[,] board)
    {
        int totalCount = 0;

        for (int i = 0; i < inputFile.Length; i++)
        {
             int rowCount = 0;
            int highestNum = 0;
            for (int j = 0; j < inputFile[0].Length; j++)
            {
                int numberCurr = inputFile[i][j] - 48;
                if (j == 0 || numberCurr > highestNum) {
                    rowCount++;
                    highestNum = numberCurr;
                    board[j,i]=1;
                }
                totalCount+=rowCount;
            }
        }

        System.Console.WriteLine($"Left = {totalCount}");
    }

}


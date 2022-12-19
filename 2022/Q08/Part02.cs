using System.Diagnostics;
using System.Linq;
public static class Part02
{
    public static string[] inputFile { get; set; }
    public static void Execute()
    {
        inputFile = File.ReadAllLines("input.txt");
        int[,] board = new int[inputFile[0].Length,inputFile.Length];
        loopBoard(ref board);
        //calcSideLeft(inputFile, ref board);
        //calcSideRight(inputFile, ref board);
        //calcSideTop(inputFile, ref board);
        //calcSideBottom(inputFile, ref board);
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
                if (totalCount<board[j,i]) totalCount=board[j,i];
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

    public static void loopBoard(ref int[,] board)
    {

        for (int i = 0; i < inputFile.Length; i++)
        {
            for (int j = 0; j < inputFile[0].Length; j++)
            {
                int total = 0;
                int numberCurr = inputFile[i][j] - 48;
                total = CheckUp(i, j, numberCurr,  ref board);
                total *= CheckDown(i, j, numberCurr,  ref board);
                total *= CheckLeft(i, j, numberCurr,  ref board);
                total *= CheckRight(i, j, numberCurr,  ref board);
                board[j,i] = total;
            }
        }

        // int total = 0;
        // total = CheckUp(1,2, 5,  ref board);
        // total *= CheckDown(1,2, 5,  ref board);
        // total *= CheckLeft(1,2, 5,  ref board);
        // total *= CheckRight(1,2, 5,  ref board);
        // board[] = total;
        // System.Console.WriteLine(total);
    }

    private static int CheckUp(int ii, int j, int checkNumber, ref int[,] board)
    {
        int count = 0;
        for (int i = ii-1; i >= 0; i--)
        {
            int numberCurr = inputFile[i][j] - 48;
            if (numberCurr==checkNumber) {
                count++;
                break;
            } 
            else if (numberCurr<=checkNumber) {
                count++;
            }
            else
            {
                count++;
                break;
            }
        }
        return count;
    }

    private static int CheckDown(int ii, int j, int checkNumber, ref int[,] board)
    {
        int count = 0;
        for (int i = ii+1; i < inputFile[0].Length; i++)
        {
            int numberCurr = inputFile[i][j] - 48;
            if (numberCurr==checkNumber) {
                count++;
                break;
            } 
            else if (numberCurr<=checkNumber) {
                count++;
            }
            else
            {
                count++;
                break;
            }
        }
        return count;
    }

    private static int CheckLeft(int i, int jj, int checkNumber, ref int[,] board)
    {
        int count = 0;
        for (int j = jj+1; j < inputFile[0].Length; j++)
        {
            int numberCurr = inputFile[i][j] - 48;
            if (numberCurr==checkNumber) {
                count++;
                break;
            } 
            else if (numberCurr<=checkNumber) {
                count++;
            }
            else
            {
                count++;
                break;
            }
        }
        return count;
    }

    private static int CheckRight(int i, int jj, int checkNumber, ref int[,] board)
    {
        int count = 0;
        for (int j = jj-1; j >=0; j--)
        {
            int numberCurr = inputFile[i][j] - 48;
            if (numberCurr==checkNumber) {
                count++;
                break;
            } 
            else if (numberCurr<=checkNumber) {
                count++;
            }
            else
            {
                count++;
                break;
            }
        }
        return count;
    }
}


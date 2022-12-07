// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
new Part02().Execute();

public class Part02
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            int totalVal = 0;


            for (int i = 0; i < inputFile.Length; i++)
            {
                string[] strValue = inputFile[i].Split(' ');
                int val = 0, val2 = 0;
                switch(strValue[1])
                {
                    case "X": val = 1; break;
                    case "Y": val = 2; break;
                    case "Z": val = 3; break;
                }
                switch(strValue[0])
                {
                    case "A": val2 = 1; break;
                    case "B": val2 = 2; break;
                    case "C": val2 = 3; break;
                }

                if (val == val2)
                    val += 3;
                else if (
                    (strValue[1] == "X" && strValue[0] == "C")
                    || (strValue[1] == "Y" && strValue[0] == "A")
                    || (strValue[1] == "Z" && strValue[0] == "B")
                    )
                    val += 6;

                    //System.Console.WriteLine($"{strValue[0]} {strValue[1]} = {val}");
                totalVal += val;
            }

            System.Console.WriteLine($"Question 01: {totalVal}");

            totalVal = 0;

            for (int i = 0; i < inputFile.Length; i++)
            {
                string[] strValue = inputFile[i].Split(' ');
                int val = 0, val2 = 0, val3 = 0;
                switch(strValue[0])
                {
                    case "A": val2 = 1; break;
                    case "B": val2 = 2; break;
                    case "C": val2 = 3; break;
                }

                switch(strValue[0])
                {
                    case "A": val3 = 3; break;
                    case "B": val3 = 1; break;
                    case "C": val3 = 2; break;
                }

                switch(strValue[0])
                {
                    case "A": val = 2; break;
                    case "B": val = 3; break;
                    case "C": val = 1; break;
                }

                if (strValue[1] == "Y")
                    val = val2 + 3;
                else if (strValue[1] == "Z") {
                    val += 6;
                } else {
                    val = val3;
                }
                    

                System.Console.WriteLine($"{strValue[0]} {strValue[1]} = {val}");
                totalVal += val;
            }
            System.Console.WriteLine($"Question 02: {totalVal}");
        }
    }
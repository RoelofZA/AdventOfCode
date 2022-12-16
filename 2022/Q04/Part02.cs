using System.Diagnostics;
using System.Linq;
public static class Part02
    {
        public static void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            List<char> resultsList = new List<char>();
            int total = 0;

            for (int i = 0; i < inputFile.Length; i++)
            {
                string[] sections = inputFile[i].Split(',');
                string[] sectionA = sections[0].Split('-');
                string[] sectionB = sections[1].Split('-');

                if (
                    (int.Parse(sectionA[0]) <= int.Parse(sectionB[0]) && int.Parse(sectionA[1]) >= int.Parse(sectionB[0])) ||
                    (int.Parse(sectionA[0]) >= int.Parse(sectionB[0]) && int.Parse(sectionA[0]) <= int.Parse(sectionB[1])) //||
                    //(int.Parse(sectionB[0]) <= int.Parse(sectionA[0]) && int.Parse(sectionB[1]) <= int.Parse(sectionA[0])) ||
                    //(int.Parse(sectionB[0]) >= int.Parse(sectionA[0]) && int.Parse(sectionB[1]) <= int.Parse(sectionA[1]))
                    )
                    {
                        Debug.WriteLine($"{inputFile[i]} = {(int.Parse(sectionA[0]) <= int.Parse(sectionB[0]) && int.Parse(sectionA[1]) <= int.Parse(sectionB[1]))} {(int.Parse(sectionA[0]) >= int.Parse(sectionB[0]) && int.Parse(sectionA[1]) <= int.Parse(sectionB[1]))} {(int.Parse(sectionB[0]) <= int.Parse(sectionA[0]) && int.Parse(sectionB[1]) <= int.Parse(sectionA[1]))} {(int.Parse(sectionB[0]) >= int.Parse(sectionA[0]) && int.Parse(sectionB[1]) <= int.Parse(sectionA[1]))}");
                        total++;
                    }
            }

            System.Console.WriteLine($"Question 04: {total}");
        }
    }
using System.Diagnostics;
using System.Linq;
public class Part01
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            List<char> resultsList = new List<char>();

            for (int i = 0; i < inputFile.Length; i++)
            {
                int halfway = inputFile[i].Length/2;
                string[] rugsack = {inputFile[i].Substring(0, halfway), inputFile[i].Substring(halfway, halfway)};
                

                var result = rugsack[0].Intersect(rugsack[1]);
                Debug.WriteLine($"{inputFile[i]} - {rugsack[0]} - {rugsack[1]} - Intersect ");
                foreach (var item in result)
                {
                    resultsList.Add(item);
                }
            }

            int total = 0;

            //Do Scoring
            foreach (var item in resultsList)
            {
                total += ((short)item > 96? (short)item - 96: (short)item - 64+26);
            }
            
            System.Console.WriteLine($"Question 03: {total}");
        }
    }
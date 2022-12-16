using System.Diagnostics;
using System.Linq;
public class Part02
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            List<char> resultsList = new List<char>();

            for (int i = 0; i < inputFile.Length; i++)
            {
                string rugsack1 = inputFile[i++];
                string rugsack2 = inputFile[i++];
                string rugsack3 = inputFile[i];
                                
                var result1 = rugsack1.Intersect(rugsack2);
                var result2 = rugsack2.Intersect(rugsack3);
                var result3 = result1.Intersect(result2);


                Debug.Write($"{inputFile[i]} - {result2} - {2} - Intersect ");
                foreach (var item in result3)
                {
                    Debug.Write(item);
                    resultsList.Add(item);
                }
                Debug.WriteLine("");
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
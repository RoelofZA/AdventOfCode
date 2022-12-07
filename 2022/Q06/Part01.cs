public class Part01
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            int totalVal = 0;
            List<string> recent = new List<string>();
            inputFile = inputFile[0].Split("");

            for (int i = 0; i < 14; i++)
            {
                recent.Add("");
            }

            for (int i = 0; i < inputFile[0].Length; i++)
            {
                recent.Add(inputFile[0][i].ToString());
                recent.RemoveAt(0);
                
                //Check for duplicates
                if(!recent.Any(x=>x == "") && !recent.GroupBy(x=>x).Select(n => new
                          {
                               MetricName = n.Key,
                               MetricCount = n.Count()
                          }).Any(x=>x.MetricCount>1))
                          {
                            Console.WriteLine($"{i + 1}");
                            return;
                          }
            }

            System.Console.WriteLine($"Question 01: {totalVal}");

            
            System.Console.WriteLine($"Question 02: {totalVal}");
        }
    }
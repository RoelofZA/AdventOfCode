// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
new Part01().Execute();

public class Part01
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");

            int horizontal = 0, depth = 0;
            List<int> elfsCalories = new List<int>();

            int calorieCount = 0;
            foreach (var item in inputFile)
            {
                if (string.IsNullOrEmpty(item)) {
                    elfsCalories.Add(calorieCount);
                    calorieCount = 0;
                }
                else
                {
                    calorieCount+=int.Parse(item);
                }
            }
            elfsCalories.Add(calorieCount);
            System.Console.WriteLine(elfsCalories.Max());
            System.Console.WriteLine(elfsCalories.OrderByDescending(x=>x).Take(3).Sum());

            //System.Console.WriteLine($"Question 01: {depth*horizontal}");
        }
    }
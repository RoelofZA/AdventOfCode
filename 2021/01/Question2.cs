namespace Q02
{
    public class Question2
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            int increase = 0, decrease = 0, previousVal = int.Parse(inputFile[0]);

            for (int i = 0; i < inputFile.Length-2; i++)
            {
                int currentVal = 0;
                for (int j = 0; j < 3; j++)
                {
                    currentVal += int.Parse(inputFile[i+j]);
                }
                if (i>0 && currentVal > previousVal)
                     increase++;
                previousVal = currentVal;
            }

            System.Console.WriteLine($"Question 2 = {increase}");
        }
    }
}
namespace Q01
{
    public class Question1
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            int increase = 0, decrease = 0, previousVal = int.Parse(inputFile[0]);
           

            foreach (var item in inputFile)
            {
                int currentVal = int.Parse(item);
                if (currentVal>previousVal)
                    increase++;
                else if (currentVal<previousVal)
                    decrease++;
                previousVal = currentVal;
            }

            System.Console.WriteLine($"Question 1 = {increase}");
        }
    }
}
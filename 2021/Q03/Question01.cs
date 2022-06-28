namespace Q03
{
    public class Question01
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");

            int rowLength = inputFile[0].Length;
            int[] bitCounter = new int[rowLength];
            int[] bitFinal = new int[rowLength];
            int[] bitFinalRev = new int[rowLength];

            foreach (var item in inputFile)
            {
                for (int i = 0; i < rowLength; i++)
                {
                    bitCounter[i] += int.Parse(item[i].ToString());
                }
            }

            for (int i = 0; i < rowLength; i++)
            {
                if (bitCounter[i] > inputFile.Length / 2)
                    bitFinal[i] = 1;
                else
                    bitFinalRev[i] = 1;
            }

            System.Console.WriteLine($"Question 01: {Convert.ToInt32(string.Join(string.Empty, bitFinal), 2) * Convert.ToInt32(string.Join(string.Empty, bitFinalRev), 2)}");
        }
    }
}
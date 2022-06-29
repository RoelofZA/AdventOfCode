namespace Q03
{
    public class Question02
    {

        public string recursiveSearch(string[] inputFile, string final, bool positive = true) {
            int rowLength = inputFile[0].Length;

            int[] bitCounter = new int[rowLength];
            int[] bitFinal = new int[rowLength];
            int[] bitFinalRev = new int[rowLength];
            string commonValNew = "";

            foreach (var item in inputFile)
            {
                bitCounter[0] += int.Parse(item[0].ToString());
            }

            for (int i = 0; i < 1; i++)
            {
                if (inputFile.Length == 1)
                        return inputFile[0];

                if (positive) {
                    if ((decimal)bitCounter[i] >= (decimal)(inputFile.Length) / (decimal)2)
                    commonValNew = "1";
                else
                    commonValNew = "0";
                } else {
                    if (inputFile.Length == 1)
                        return inputFile[0];
                    if ((decimal)bitCounter[i] > (decimal)(inputFile.Length) / (decimal)2 || (decimal)bitCounter[i] == (decimal)(inputFile.Length) / (decimal)2)
                        commonValNew = "0";
                    else
                        commonValNew = "1";
                }
                
            }

            final += commonValNew;
            //System.Console.WriteLine(final);

            List<string> inputFileNew = new List<string>();
            // Build new inputFile
            foreach (var item in inputFile)
            {
                if (item[0].ToString() == commonValNew){
                    inputFileNew.Add(item.Substring(1, rowLength-1));
                }
            }

            if (inputFileNew.Count>0 && inputFileNew[0].Length > 0)
                return commonValNew + recursiveSearch(inputFileNew.ToArray(), final, positive);

            return commonValNew;
        }

        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");

            int rowLength = inputFile[0].Length;
            int[] bitCounter = new int[rowLength];
            int[] bitFinal = new int[rowLength];
            int[] bitFinalRev = new int[rowLength];

            string str1 = recursiveSearch(inputFile, "", true), str2 =recursiveSearch(inputFile, "", false);
            

            //bit criteria
            System.Console.WriteLine(str1);
            System.Console.WriteLine(str2);

            // oxygen generator rating
            // foreach (var item in inputFile)
            // {
            //     for (int i = 0; i < rowLength; i++)
            //     {
            //         bitCounter[i] += int.Parse(item[i].ToString());
            //     }
            // }

            // for (int i = 0; i < rowLength; i++)
            // {
            //     if (bitCounter[i] > inputFile.Length / 2)
            //         bitFinal[i] = 1;
            //     else
            //         bitFinalRev[i] = 1;
            // }

            // oxygen generator rating



            System.Console.WriteLine($"Question 02: {Convert.ToInt32(str1, 2) * Convert.ToInt32(str2, 2)}");
        }
    }
}
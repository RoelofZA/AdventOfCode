namespace Q02
{
    public class Question02
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");

            int horizontal = 0, depth = 0, aim = 0;

            foreach (var item in inputFile)
            {
                string[] rowSplit = item.Split(' ');
                switch(rowSplit[0]) {
                    case "forward":
                        horizontal += int.Parse(rowSplit[1]);
                        depth += aim * int.Parse(rowSplit[1]);
                        break;
                    case "up":
                        aim -= int.Parse(rowSplit[1]);
                        break;
                    case "down":
                        aim += int.Parse(rowSplit[1]);
                        break;
                }
            }

            System.Console.WriteLine($"Question 02: {depth*horizontal}");
        }
    }
}
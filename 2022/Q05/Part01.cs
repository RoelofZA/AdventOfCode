public class Part01
    {
        public void Execute() {
            string[] inputFile = File.ReadAllLines("input.txt");
            Dictionary<int,Stack<string>> values = new Dictionary<int, Stack<string>>();

            //Build Stacks
            for (int i = 0; i < inputFile.Length; i++)
            {
                if (string.IsNullOrEmpty(inputFile[i]))
                    break;
                System.Console.WriteLine(inputFile[i]);
                string newString = inputFile[i];

                for (int t = 1; t < newString.Length; t = t + 4)
                {
                    if (newString[t] == ' ')
                        continue;

                    if (newString[t] == '1')
                        break;

                    if (!values.ContainsKey((t-1)/4))
                    {
                        values.Add((t-1)/4,new Stack<string>());
                    }
                    values[(t-1)/4].Push(newString[t].ToString());
                }
            }

            //reverse items
            foreach (var item in values)
            {
                var newQueue = new Stack<string>();

                while(values[item.Key].Count > 0)
                {
                    string box = values[item.Key].Pop();
                    newQueue.Push(box);
                }
                
                values[item.Key] = newQueue;
            }

            printValues(values);

            // Time to move things
            for (int i = 0; i < inputFile.Length; i++)
            {
                if (!inputFile[i].StartsWith("move"))
                    continue;

                System.Console.WriteLine(inputFile[i]);
                string newString = inputFile[i].Replace("move ", "");
                string[] moveSplit = newString.Split(" from ");
                int numberToMove = int.Parse(moveSplit[0]);
                string[] moveLocations = moveSplit[1].Split(" to ");
                int sourceIndex = int.Parse(moveLocations[0]) -1;
                int destinationIndex = int.Parse(moveLocations[1]) -1;

                for (int t = 0; t < numberToMove; t++)
                {
                    string box = values[sourceIndex].Pop();
                    values[destinationIndex].Push(box);
                }
                printValues(values);
            }

            foreach (var item in values.OrderBy(x=>x.Key))
            {
                System.Console.Write(item.Value.Peek());
            }
            System.Console.WriteLine();

            

            System.Console.WriteLine($"Question 02: {""}");
        }

        void printValues(Dictionary<int,Stack<string>> values) {
            foreach (var item in values.OrderBy(x=>x.Key))
            {
                System.Console.Write($"{item.Key + 1} - ");
                item.Value.ToList().ForEach(x=>System.Console.Write(x));
                System.Console.WriteLine();
            }
            System.Console.WriteLine();
        }
    }
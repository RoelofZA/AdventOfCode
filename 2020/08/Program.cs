using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _08
{
    class Program
    {
        
        static (bool, int) GameRun(string[] content) {
            int global = 0;
            HashSet<int> visited = new HashSet<int>();

            for (int i = 0; i < content.Count(); i++)
            {
                string[] line = content[i].Split(" ");
                int numberVal = int.Parse(line[1].Substring(1));

                if (visited.Contains(i)) {
                    return (false, global);
                }
                visited.Add(i);

                switch(line[0]) {
                    case "acc":
                        global+= ((line[1][0]=='+'? 1 : -1) * numberVal);
                        break;
                    case "jmp":
                        i += ((line[1][0]=='+'? 1 : -1) * numberVal)-1;
                        break;
                    case "nop ":
                        break;
                }
            }
            return (true, global);
        }
        
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("content.txt");

            Console.WriteLine($"Part 1  - {GameRun(content).Item2}");

            for (int i = 0; i < content.Count(); i++)
            {
                string[] contentTemp = new string[content.Count()];
                (bool, int) checkRun = (false, 0);
                switch (content[i].Split(" ")[0]) {
                    case "nop":
                        content.CopyTo(contentTemp,0);
                        contentTemp[i] = content[i].Replace("nop", "jmp");
                        checkRun = GameRun(contentTemp);
                        break;
                    case "jmp":
                        content.CopyTo(contentTemp,0);
                        contentTemp[i] = content[i].Replace("jmp", "nop");
                        checkRun = GameRun(contentTemp);
                        break;
                }

                if (checkRun.Item1) {
                    Console.WriteLine($"Part 2 - {checkRun.Item2}");
                    return;
                }
            }

            

        }
    }
}

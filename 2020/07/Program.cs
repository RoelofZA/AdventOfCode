using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;

namespace _07
{
    class Program
    {
        static Dictionary<string, Dictionary<string, int>> dicChildern = new Dictionary<string, Dictionary<string, int>>();
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("content.txt");
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();

            //Build Rules
            foreach (string item in content)
            {
                string cleanedLine = Regex.Replace(item, "([0-9]( ))|( bags)|( bag)|(\\.)", "");
                string[] itemSplit = cleanedLine.Split(" contain ");
                string[] children = itemSplit[1].Split(",");

                foreach (var child in children)
                {
                    if (dic.ContainsKey(child.Trim()))
                    {
                        if (!dic[child.Trim()].Contains(itemSplit[0].Trim()))
                            dic[child.Trim()].Add(itemSplit[0].Trim());
                    }
                    else
                    {
                        dic[child.Trim()] = new List<string>() {itemSplit[0].Trim() };
                    }
                }                
            }

            Stack<string> stack = new Stack<string>();
            stack.Push("shiny gold");
            int counter = 0;
            HashSet<string> hs = new HashSet<string>();

            while(stack.Count > 0){
                string current = stack.Pop();
                
                if (dic.ContainsKey(current))
                    foreach (var item in dic[current])
                    {
                        Console.WriteLine(item);
                        stack.Push(item);
                        counter++;
                        hs.Add(item);
                    }
            }
            Console.WriteLine($"Part 1 - {counter} {hs.Count}");


            dic = new Dictionary<string, List<string>>();
            //Dictionary<string, Dictionary<string, int>> dicChildern = new Dictionary<string, Dictionary<string, int>>();

            //Build Rules
            foreach (string item in content)
            {
                string cleanedLine = Regex.Replace(item, "( bags)|( bag)|(\\.)", "");
                string[] itemSplit = cleanedLine.Split(" contain ");
                string[] children = itemSplit[1].Split(",");

                foreach (var child in children)
                {
                    string cleanedNumber = Regex.Replace(child, "[^0-9]", "");
                    string cleanedChild = Regex.Replace(child, "[0-9]", "");

                    if (!dicChildern.ContainsKey(itemSplit[0].Trim()))
                        dicChildern.Add(itemSplit[0].Trim(), new Dictionary<string, int>());
                    
                    dicChildern[itemSplit[0].Trim()].Add(cleanedChild.Trim(),cleanedNumber==""? 1 : int.Parse(cleanedNumber));                    
                }
            }

            stack = new Stack<string>();
            stack.Push("shiny gold");
            counter = 0;

            Console.WriteLine(RecursiveMagic(1, "shiny gold"));
        }

        static int RecursiveMagic(int multi, string bag) {
            int sum = 0;
            if (!dicChildern.ContainsKey(bag))
            {
                Console.WriteLine($" last - {bag} ");
                sum = 0;
            }
            else{
                foreach (var item in dicChildern[bag])
                {
                    Console.WriteLine($" {item.Value} - {item.Key} ");
                    if (item.Key!="no other") sum += item.Value + RecursiveMagic(item.Value, item.Key);
                }
            }
            return multi * sum;
        }
    }
}

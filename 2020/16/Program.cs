using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _16
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");

            int section = 0;
            string myTicket = "";
            Dictionary<string,List<(int,int)>> rules = new Dictionary<string, List<(int, int)>>();
            List<int> invalid = new List<int>();
            List<int[]> validTickets = new List<int[]>();

            foreach (var item in collection)
            {
                if (item =="")
                    continue;
                else if (item == "your ticket:") {
                    section = 1;
                    continue;
                } else if (item == "nearby tickets:") {
                    section = 2;
                    continue;
                }

                if (section == 0) {
                    string[] split1 = item.Split(":");
                    string[] split2 = split1[1].Replace(" ", "").Split("or");
                    rules.Add(split1[0], new List<(int, int)>());
                    foreach (var splitItem in split2)
                    {
                        string[] split3 = splitItem.Split("-");
                        rules[split1[0]].Add((int.Parse(split3[0]), int.Parse(split3[1])));
                    }
                }

                if (section == 1) {
                    myTicket = item;
                }

                if (section == 2) {
                    int[] split1 = item.Split(",").ToList().Select(x=>int.Parse(x)).ToArray();
                    bool ticketValid = true;
                    foreach (var splitItem in split1)
                    {
                        bool valid = false;
                        foreach (var rule in rules.Values)
                        {
                            foreach (var range in rule)
                            {
                                if (splitItem>=range.Item1 && splitItem<=range.Item2)
                                {
                                    valid = true;
                                    break;
                                }
                            }
                            if (valid)
                                break;
                        }
                        if (!valid){
                            invalid.Add(splitItem);
                            ticketValid = false;
                        }
                    }
                    if (ticketValid) validTickets.Add(split1);
                }
            }

            Console.WriteLine($"Part 1 - {invalid.Sum()}");

            Dictionary<string,List<int>> options = new Dictionary<string, List<int>>();

            // Find the columns
            for (int j = 0; j < validTickets[0].Length; j++)
                foreach (var rule in rules)
                {
                    bool valid = true;
                    for (int i = 0; i < validTickets.Count; i++)
                    {
                        int splitItem = validTickets[i][j];
                        if ( (splitItem>=rule.Value[0].Item1 && splitItem<=rule.Value[0].Item2) || 
                            (splitItem>=rule.Value[1].Item1 && splitItem<=rule.Value[1].Item2)
                        )
                        {

                        }
                        else
                        {
                            valid = false;
                            break;
                        }
                    }
                    if (valid){
                        if (!options.ContainsKey(rule.Key))
                            options.Add(rule.Key, new List<int>());
                        options[rule.Key].Add(j);
                        //Console.WriteLine($"{j} - {rule.Key}");
                    }
                }

            //Ok Magic
            for (int i = 0; i < 20; i++)
            {
                var cleanup = options.Where(x=>x.Value.Count == 1).ToList();
                foreach (var item in cleanup)
                {
                    foreach (var ops in options)
                    {
                        if (ops.Key == item.Key)
                            continue;
                        ops.Value.Remove(item.Value[0]);
                    }                    
                }

            }

            var departures = options.Where(x=>x.Key.StartsWith("departure")).ToList();
            int[] myTicketArr = myTicket.Split(",").ToList().Select(x=>int.Parse(x)).ToArray();
            long sum = 1;
            foreach (var item in departures)
            {
                Console.WriteLine($"{item.Value[0]} {myTicketArr[item.Value[0]]}");
                sum*=myTicketArr[item.Value[0]];
            }

            Console.WriteLine($"Part 2 - {sum}");
        }
    }
}

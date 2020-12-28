using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace _19
{
    class Program
    {
        public static Dictionary<int, List<string>> _ruleCache = new Dictionary<int, List<string>>();
        public static HashSet<string> _combinations = new HashSet<string>();
        public static Queue<string[]> _ReducedRules = new Queue<string[]>();
        public static Dictionary<int, string> _Rules = new Dictionary<int, string>();
        public static List<string> _rawMessages = new List<string>();

        public static long CheckForMatches()
        {
            long count = 0;

            foreach (var item in _combinations)
            {
                if (_rawMessages.Contains(item))
                    count++;
            }

            return count;
        }

        public static bool CheckForMatch(string start)
        {
            foreach (var item in _rawMessages)
            {
                if (item.StartsWith(start))
                    return true;
            }

            return false;
        }

        static void ReduceRules()
        {

            

            bool found = false;
            do
            {
                found = false;
                foreach (var item in _Rules)
                {
                    if (item.Value.Split(" ").Count() == 1
                        || Regex.IsMatch(item.Value, "^[^0-9|]*$"))
                    {
                        string ruleId = item.Key.ToString();
                        string ruleValue = item.Value;

                        foreach (var key in _Rules.Keys)
                        {
                            string[] subruleSplit = _Rules[key].Split(" ");
                            for (int z = 0; z < subruleSplit.Length; z++)
                            {
                                if (subruleSplit[z]  == ruleId)
                                {
                                    found = true;
                                    subruleSplit[z] = ruleValue;
                                    _Rules[key] = string.Join(" ", subruleSplit);
                                }    
                            }
                        }
                    }
                }
            } while (found);

            
            // Rule Perants
            //bool foundAll = false;
            

            // foreach (var item in _Rules.OrderBy(x=>x.Key))
            // {
            //     Console.WriteLine($"{item.Key} - {item.Value}");
            // }

            // int cacheCnt = -1;
            // bool done = false;
            // while(_ruleCache.Count < 85 && _Rules.Count > _ruleCache.Count) 
            // {
            //     cacheCnt = _ruleCache.Count;
            
            //     foreach (var item in _Rules.OrderBy(x=>x.Key)) {
            //         if (_ruleCache.ContainsKey(item.Key)) {
            //             continue;
            //         }
            //         if (Regex.IsMatch(item.Value, "^[^0-9]*$")) {
            //             List<string> tmpList = new List<string>();
            //             foreach (var splitItem in item.Value.Split(" | "))
            //             {
            //                 tmpList.Add(splitItem.Replace(@" ", ""));
            //             }
            //             _ruleCache.Add(item.Key, tmpList);
            //             continue;
            //         }
                    
            //         string[] numbersOnly = Regex.Replace(item.Value, "[^0-9 ]", "").Split(" ");
            //         bool rulesAreCached = true;
            //         foreach (var subItem in numbersOnly)
            //         {
            //             if (subItem == "")
            //                 continue;
                    

            //             if (!_ruleCache.ContainsKey(int.Parse(subItem)))
            //             {
            //                 rulesAreCached = false;
            //                 break;
            //             }                    
            //         }

            //         if (!rulesAreCached) {
            //             continue;
            //         } else {
            //             // Build new Rule

            //             string[] orRules = item.Value.Split(" | ");
            //             List<string> tmpList = new List<string>() { "" };

            //             foreach (var orRule in orRules)
            //             {
            //                 List<string> tmpORList = new List<string>() { "" };
            //                 string[] splitOrRule = orRule.Split(" ");
            //                 foreach (var splitItem in splitOrRule)
            //                 {
            //                     if (Regex.IsMatch(splitItem, "^[^0-9]*$")) {
            //                         List<string> tmpAltList = new List<string>();
            //                         foreach (var tmpItem in tmpORList)
            //                         {
            //                             tmpAltList.Add((tmpItem + splitItem).Trim());
            //                         }
            //                         tmpORList = tmpAltList;
            //                     } else {
            //                         List<string> fectedRule = _ruleCache[int.Parse(splitItem)];
            //                         List<string> tmpAltList = new List<string>();
            //                         foreach (var tmpItem in tmpORList)
            //                         {
            //                             foreach (var fectchItem in fectedRule)
            //                             {
            //                                 tmpAltList.Add((tmpItem + fectchItem).Trim());
            //                             }
            //                         }
            //                         tmpORList = tmpAltList;
            //                     }

            //                     List<string> tmpAltList2 = new List<string>();
            //                     foreach (var tmpItem in tmpList)
            //                     {
            //                         foreach (var Oritem in tmpORList)
            //                         {
            //                             tmpAltList2.Add((tmpItem + Oritem).Trim());
            //                         }
            //                     }
            //                     tmpList = tmpAltList2;
            //                 }
            //             }

                        

            //             // foreach (var splitItem in item.Value.Split(" | "))
            //             // {
            //             //     tmpList.Add(splitItem);
            //             // }
            //             _ruleCache.Add(item.Key, tmpList);


            //         }
            //     }
            // }

            //
            long count = 0;

            foreach (var item in _ruleCache)
            {
                foreach (var zitem in item.Value)
                {                
                    if (_rawMessages.Contains(zitem))
                        count++;
                }
            }

           // count;

            Console.WriteLine();
            _ReducedRules.Enqueue(_Rules[0].Split(" "));
            RecursiveRule();
        }

        static string RecursiveRule()
        {
            HashSet<string> history = new HashSet<string>();
            while (_ReducedRules.Count > 0)
            {
                if (_ReducedRules.Count % 10000 == 0) Console.WriteLine(_ReducedRules.Count);

                string[] currentRule = _ReducedRules.Dequeue();

                if (currentRule.Length > 1) {
                    Match regMat = Regex.Match(string.Join("", currentRule), "^[ab]*");
                    if (regMat.Success && regMat.Length > 0 &&!CheckForMatch(regMat.Value)) {
                        continue;
                    }
                }

                string[] rulez = currentRule;
                bool perfect = true;
                for (int i = 0; i < rulez.Length; i++)
                {
                    if (Regex.IsMatch(rulez[i], "^[ab| ]*$"))
                    {
                        continue;
                    }

                    //FetchRule
                    string[] orRules = _Rules[int.Parse(rulez[i])].Split(" | ");
                    foreach (var item in orRules)
                    {
                        string serializedStr = "";
                        rulez[i] = item;
                        serializedStr = string.Join(" ", rulez);

                         if (
                             !history.Contains(serializedStr))// && 
                        //     !_ReducedRules.Contains(rulez))
                        {
                            history.Add(serializedStr);
                            _ReducedRules.Enqueue(serializedStr.Split(" "));
                        }
                    }
                    perfect = false;
                }
                if (perfect)
                {
                    string tmp = string.Join(" ", rulez).Replace(@"""", "").Replace(" ", "");
                    Match regMat = Regex.Match(tmp, "^[ab]*$");
                    if (regMat.Success && regMat.Length > 0 &&!CheckForMatch(tmp)) {
                        Console.WriteLine("Error");
                    }
                    else
                    {
                        _combinations.Add(string.Join(" ", rulez).Replace(@"""", "").Replace(" ", ""));
                    }
                }
            }

            return null;
        }


        static void ReduceRulesPart2()
        {
            bool found = false;
            do
            {
                found = false;
                foreach (var item in _Rules)
                {
                    if (item.Value.Split(" ").Count() == 1
                        || Regex.IsMatch(item.Value, "^[^0-9|]*$"))
                    {
                        string ruleId = item.Key.ToString();
                        string ruleValue = item.Value;

                        foreach (var key in _Rules.Keys)
                        {
                            string[] subruleSplit = _Rules[key].Split(" ");
                            for (int z = 0; z < subruleSplit.Length; z++)
                            {
                                if (subruleSplit[z]  == ruleId)
                                {
                                    found = true;
                                    subruleSplit[z] = ruleValue;
                                    _Rules[key] = string.Join(" ", subruleSplit);
                                }    
                            }
                        }
                    }
                }
            } while (found);
            _ReducedRules.Enqueue(_Rules[0].Split(" "));
            RecursiveRulePart2();
        }

        static string RecursiveRulePart2()
        {
            HashSet<string> history = new HashSet<string>();
            while (_ReducedRules.Count > 0)
            {
                if (_ReducedRules.Count % 10000 == 0) Console.WriteLine(_ReducedRules.Count);

                string[] currentRule = _ReducedRules.Dequeue();

                if (currentRule.Length > 1) {
                    Match regMat = Regex.Match(string.Join("", currentRule), "^[ab]*");
                    if (regMat.Success && regMat.Length > 0 &&!CheckForMatch(regMat.Value)) {
                        continue;
                    }
                }

                string[] rulez = currentRule;
                bool perfect = true;
                for (int i = 0; i < rulez.Length; i++)
                {
                    if (Regex.IsMatch(rulez[i], "^[ab| ]*$"))
                    {
                        continue;
                    }

                    //FetchRule
                    string[] orRules = _Rules[int.Parse(rulez[i])].Split(" | ");
                    foreach (var item in orRules)
                    {
                        string serializedStr = "";
                        rulez[i] = item;
                        serializedStr = string.Join(" ", rulez);

                         if (
                             !history.Contains(serializedStr))
                        {
                            history.Add(serializedStr);
                            _ReducedRules.Enqueue(serializedStr.Split(" "));
                        }
                    }
                    perfect = false;
                    break;
                }
                if (perfect)
                {
                    string tmp = string.Join(" ", rulez).Replace(@"""", "").Replace(" ", "");
                    Match regMat = Regex.Match(tmp, "^[ab]*$");
                    if (regMat.Success && regMat.Length > 0 &&!CheckForMatch(tmp)) {
                        Console.WriteLine("Error");
                    }
                    else
                    {
                        _combinations.Add(string.Join(" ", rulez).Replace(@"""", "").Replace(" ", ""));
                    }
                }
            }

            return null;
        }


        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");

            bool rules = true;
            foreach (var item in collection)
            {
                if (item == "")
                {
                    rules = false;
                    continue;
                }
                if (rules)
                {
                    var splitVal = item.Split(": ");
                    _Rules.Add(int.Parse(splitVal[0]), splitVal[1].Replace(@"""", ""));
                }
                else
                {
                    _rawMessages.Add(item);
                }
            }

            // Update Rules
            _Rules[8] = "42 | 42 8";
            _Rules[11] = "42 31 | 42 11 31";

            /*
                8: 42 | 42 8
                11: 42 31 | 42 11 31
            */

            ReduceRulesPart2();

            long numberMatches = CheckForMatches();

            Console.WriteLine($"Part 1 - {numberMatches}");
        }
    }
}

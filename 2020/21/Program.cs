using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace _21
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");

            //Solve();
            
            Dictionary<string, List<string>> options = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> optionsSimple = new Dictionary<string, List<string>>();
            Dictionary<string, List<string>> optionsSimpleC = new Dictionary<string, List<string>>();
            
            Dictionary<string, List<string>> optionsInverse = new Dictionary<string, List<string>>();

            List<string> allerz = new List<string>();
            List<string> all = new List<string>();
            
            foreach (var item in collection)
            {
                string[] split1 = item.Replace("peanuts", "peanutz").Replace("shellfish", "shellfizh").Replace("(", "").Replace(")", "").Split(" contains ");
                string[] split2 = split1[0].Split(" ");
                string[] split3 = split1[1].Split(" ");
                

                foreach (var ingItem in split2)
                {
                    if (!options.ContainsKey(ingItem))
                    {
                        options.Add(ingItem, new List<string>());
                        optionsSimple.Add(ingItem, new List<string>());
                        optionsSimpleC.Add(ingItem, new List<string>());

                    }
                    if (!optionsInverse.ContainsKey(split1[1])) optionsInverse.Add(split1[1], new List<string>());
                    options[ingItem].AddRange(split1[1].Split(", "));
                    optionsSimple[ingItem].Add(split1[1]);
                    optionsSimpleC[ingItem].Add(split1[1]);
                    optionsInverse[split1[1]].Add(ingItem);
                    allerz.AddRange(split1[1].Split(", "));
                    all.Add(ingItem);
                }
            }

            allerz = allerz.Distinct().ToList();

            List<string> sharedGlobal = new List<string>();

            foreach (var item in allerz)
            {
                List<string> shared = new List<string>();
                shared.AddRange(optionsInverse.Where(y => y.Key.Contains(item)).ToList()[0].Value);

                foreach (var InvItem in optionsInverse.Where(y => y.Key.Contains(item)))
                {
                    List<string> tmpShared = new List<string>();
                    foreach (var share in shared)
                    {
                        if (InvItem.Value.Contains(share))
                            tmpShared.Add(share);
                    }
                    shared = tmpShared;
                }

                sharedGlobal.AddRange(shared);
            }

            sharedGlobal.Remove("grqppvx");

            all = all.Distinct().ToList();
            
            sharedGlobal.ForEach(x=>all.Remove(x));
            all.ForEach(x=>optionsSimple.Remove(x));

            int sum = 0;
            List<string> complex = new List<string>();
            foreach (var item in optionsSimple)
            {
                complex = new List<string>();
                complex.AddRange(item.Value[0].Split(", "));

                Console.WriteLine($"{item.Key} - ");

                foreach (var subitem in item.Value)
                {
                    string[] splitty = subitem.Split(", ");
                    complex.AddRange(splitty);
                }
                complex.GroupBy(x=>x).Select(Group=> new {key=Group.Key, cnt=Group.Count()} ).OrderByDescending(x=>x.cnt).Take(30).ToList().ForEach(x=>Console.WriteLine($"{x.key} {x.cnt}"));
                Console.WriteLine();
            }


            Console.WriteLine($"Part 1 - { sum }");
        }

        static List<(string[], string[])> parseInput()
        {
            var foods = new List<(string[], string[])>();

            // Not gonna bother with regexes for this one!
            foreach (string line in File.ReadLines("content.txt"))
            {
                var ingredients = line.Substring(0, line.IndexOf('(') - 1).Split(' ');
                var allergens = line.Substring(line.IndexOf('(') + 10, line.IndexOf(')') - line.IndexOf('(') - 10).Split(", ");
                foods.Add((ingredients, allergens));                
            }

            return foods;
        }

        static void eliminateIngedient(string ingredient, Dictionary<string, HashSet<string>> allergens)
        {
            foreach (string allergen in allergens.Keys)
                allergens[allergen].Remove(ingredient);            
        }

        static void Solve()
        {
            var foods = parseInput();

            var occurrences = new Dictionary<string, int>();
            var allergens = new Dictionary<string, HashSet<string>>();
            foreach (var food in foods)
            {
                foreach (var allergen in food.Item2)
                {
                    if (!allergens.ContainsKey(allergen))
                        allergens.Add(allergen, new HashSet<string>(food.Item1));
                    else
                        allergens[allergen] = new HashSet<string>(allergens[allergen].Intersect(new HashSet<string>(food.Item1)));
                }

                foreach (var ingredient in food.Item1)
                {
                    if (!occurrences.ContainsKey(ingredient))
                        occurrences.Add(ingredient, 1);
                    else
                        occurrences[ingredient] += 1;
                }
            }

            HashSet<string> identifiedAllergens = new HashSet<string>();
            HashSet<string> ingredientsWithAllergens = new HashSet<string>();
            HashSet<(string, string)> ingredientAllergenPair = new HashSet<(string, string)>();
            bool ambiguous;
            while (true)
            {
                ambiguous = false;
                foreach (string allergen in allergens.Keys.Where(k => !identifiedAllergens.Contains(k)))
                {
                    if (allergens[allergen].Count == 1)
                    {
                        var ingredient = allergens[allergen].First();
                        ingredientsWithAllergens.Add(ingredient);
                        eliminateIngedient(ingredient, allergens);
                        identifiedAllergens.Add(allergen);
                        ingredientAllergenPair.Add((ingredient, allergen));
                    }
                    else
                        ambiguous = true;
                }
            
                if (!ambiguous)
                    break;
            }

            var p1 = occurrences.Keys.Where(k => !ingredientsWithAllergens.Contains(k))
                            .Select(k => occurrences[k]).Sum();
            Console.WriteLine($"P1: {p1}");

            var p2 = string.Join(',', ingredientAllergenPair.OrderBy(i => i.Item2).Select(i => i.Item1));
            Console.WriteLine($"P2 {p2}");
        }
    }
}

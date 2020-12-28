using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace _10
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> collection = File.ReadAllLines("content.txt").Select(int.Parse).OrderBy(x=>x).ToList();
            collection.Add(collection.Max()+3);
            int currentJolt = 0, lastValue = 0, startValue = 0;
            List<int> uniqueVal = new List<int>();
            List<int> uniqueGroups = new List<int>();
            
            foreach (var item in collection)
            {
                //Console.WriteLine($"{item}");
                if (currentJolt<= item && item - currentJolt <= 3) {
                    uniqueVal.Add(item-currentJolt);
                    currentJolt = item;
                }
                else
                {
                    break;
                }
            }
            var test = uniqueVal.GroupBy(x=>x).Select(x=>new {Key = x.Key, Val = x.Count() }).ToList();
            Console.WriteLine($"Part 1 - {test[0].Val * test[1].Val}");

            for (int i = 0; i < collection.Count-1; i++)
            {
                if (collection[i]+1 == collection[i+1] && (i == 0 ||collection[i]-1 == collection[i-1])) {
                    uniqueGroups.Add(collection[i]);
                }
            }

            HashSet<string> uniqueStrings = new HashSet<string>();
            RecSmart(0, uniqueGroups.Count-1, collection, uniqueGroups, ref uniqueStrings);            
            Console.WriteLine($"Count {uniqueStrings.Count}");
        }

        static bool TestSequence(List<int> collection) {
            int currentJolt = 0;
            
            foreach (var item in collection)
            {
                if (currentJolt<= item && item - currentJolt <= 3) {
                    currentJolt = item;
                }
                else
                {
                    break;
                }
            }
            return currentJolt == collection.Max();
        }

        static void RecReplace(int depth, int start, List<int> collection, List<int> uni, ref HashSet<string> uniqueStrings) {
            
            string baseStr = string.Join(",", collection);
            //if (TestSequence(collection))
            uniqueStrings.Add(baseStr);
            // else {
            //     Console.WriteLine(baseStr);
            //     return;
            // }
            //RecReplace(start+1, collection, uni, ref uniqueStrings);
            
            for (int i = start; i >= 0; i--)
            {
                //if (depth==0) Console.WriteLine($"i={i}");
                int indexItem = collection.IndexOf(uni[i]);
                int colMax = collection.Count-1;

                if (
                    (indexItem>0 && collection[indexItem]-collection[indexItem-1]!=1) && 
                    (indexItem>1 && collection[indexItem]-collection[indexItem-2]!=2) && 
                    (indexItem>2 && collection[indexItem]-collection[indexItem-3]!=3))
                    continue;

                if (
                    (indexItem<colMax-1 && indexItem>0 && collection[indexItem+1]-collection[indexItem-1]>3) ||
                    (indexItem == 0 && collection[indexItem+1]>3)
                )
                    continue;

                var newCollection = collection.Where(x=>x!=uni[i]).ToList();
                
                //if (TestSequence(newCollection))
                {
                    uniqueStrings.Add(string.Join(",", newCollection));
                    RecReplace(depth + 1, i-1, newCollection, uni, ref uniqueStrings);
                }
                // else
                //     Console.WriteLine(string.Join(",", newCollection));

                
            }
        }

        static void RecReplace2(int depth, int start, List<int> collection, List<int> uni, ref HashSet<string> uniqueStrings) {
            
            string baseStr = string.Join(",", collection);
            uniqueStrings.Add(baseStr);
            
            for (int i = start; i >= 0; i--)
            {
                int indexItem = collection.IndexOf(uni[i]);
                int colMax = collection.Count-1;

                if (
                    (indexItem>0 && collection[indexItem]-collection[indexItem-1]!=1) && 
                    (indexItem>1 && collection[indexItem]-collection[indexItem-2]!=2) && 
                    (indexItem>2 && collection[indexItem]-collection[indexItem-3]!=3))
                    continue;

                if (
                    (indexItem<colMax-1 && indexItem>0 && collection[indexItem+1]-collection[indexItem-1]>3) ||
                    (indexItem == 0 && collection[indexItem+1]>3)
                )
                    continue;

                var newCollection = collection.Where(x=>x!=uni[i]).ToList();
                
                //if (TestSequence(newCollection))
                {
                    uniqueStrings.Add(string.Join(",", newCollection));
                    RecReplace2(depth + 1, i-1, newCollection, uni, ref uniqueStrings);
                }
                // else
                //     Console.WriteLine(string.Join(",", newCollection));

                
            }
        }

        static void RecSmart(int depth, int start, List<int> collection, List<int> uni, ref HashSet<string> uniqueStrings) {
            
            //string baseStr = string.Join(",", collection);
            //uniqueStrings.Add(baseStr);
            List<List<int>> subCollection = new List<List<int>>();


            int current = 0;

            // Build small groups
            for (int i = 0; i < uni.Count; i++)
            {
                List<int> newList = new List<int>();
                int indexItem = collection.IndexOf(uni[i]);
                if (indexItem > 1) newList.Add(collection[indexItem-2]); 
                if (indexItem > 0) newList.Add(collection[indexItem-1]); 
                newList.Add(collection[indexItem]);
                i++;
                while (collection[indexItem+1] - collection[indexItem] == 1) {
                    indexItem++;
                    newList.Add(collection[indexItem]);
                    if (i<uni.Count-1 && uni[i] == collection[indexItem]) i++;
                }
                newList.Add(collection[indexItem+1]);
                subCollection.Add(newList);
            }

            subCollection.ForEach(x=>Console.WriteLine($"{string.Join(",", x)}"));

            long sumOfCollections = 1;

            foreach (var item in subCollection)
            {
                HashSet<string> tmpUniqueStrings = new HashSet<string>();
                var uni2 = uni.Where(x=>x>=item.Min() && x<=item.Max()).ToList();

                RecReplace2(0, uni2.Count-1, item, uni2, ref tmpUniqueStrings); 

                //Console.WriteLine();
                //tmpUniqueStrings.ToList().ForEach(Console.WriteLine);
                //Console.WriteLine();

                Console.WriteLine($"Count {tmpUniqueStrings.Count}");   
                sumOfCollections*=tmpUniqueStrings.Count;             
            }
            Console.WriteLine($"{sumOfCollections} {sumOfCollections*2}");

            
            // for (int i = 0; i < uni.Count; i++)
            // {
            //     if (depth==0) Console.WriteLine($"i={i}");


            //     int indexItem = collection.IndexOf(uni[i]);
            //     int colMax = collection.Count-1;

            //     if (
            //         (indexItem>0 && collection[indexItem]-collection[indexItem-1]!=1) && 
            //         (indexItem>1 && collection[indexItem]-collection[indexItem-2]!=2) && 
            //         (indexItem>2 && collection[indexItem]-collection[indexItem-3]!=3))
            //         continue;

            //     if (
            //         (indexItem<colMax-1 && indexItem>0 && collection[indexItem+1]-collection[indexItem-1]>3) ||
            //         (indexItem == 0 && collection[indexItem+1]>3)
            //     )
            //         continue;

            //     var newCollection = collection.Where(x=>x!=uni[i]).ToList();
                
            //     uniqueStrings.Add(string.Join(",", newCollection));
            //     RecReplace(depth + 1, i-1, newCollection, uni, ref uniqueStrings);                
            // }
        }
    }
}

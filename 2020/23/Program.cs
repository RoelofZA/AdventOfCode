using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace _23
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        static void Part1() {
            string input = "284573961";
            int lengthInput = input.Length;
            List<int> inputArr = input.Select(x=>int.Parse(x.ToString())).ToList();
            string current = input.Substring(0,1);

            for (int i = 0; i < 100; i++)
            {
                // Pick
                string selItems = input.Substring(1,3);
                string newInput = current + input.Substring(4, 5);
                int searchVal = int.Parse(current) - 1;

                int searchIndex = newInput.IndexOf(searchVal.ToString());
                if (searchIndex<0) {
                    int currentSearchVal = searchVal;
                    while(searchIndex<0) {
                        currentSearchVal--;
                        if (currentSearchVal<1) currentSearchVal = 9;
                        searchIndex = newInput.IndexOf(currentSearchVal.ToString());
                    }

                }

                newInput = newInput.Insert(searchIndex+1, selItems);
                current = newInput.Substring(1,1);
                searchIndex = newInput.IndexOf(current);
                input = current + newInput.Substring(searchIndex+1, newInput.Length-(searchIndex+1)) + newInput.Substring(0, searchIndex);

                //Console.WriteLine(input);
                Debug.Assert(lengthInput == input.Length);
            }

            int searchFinal = input.IndexOf("1");
            string final = input.Substring(searchFinal+1, input.Length-(searchFinal+1)) + input.Substring(0, searchFinal);

            Console.WriteLine($"Part 1 {final}");
        }

        static void Part2() {
            string input = "284573961";
            Hashtable ht = new Hashtable();
            List<int> cups = new List<int>();

            LinkedList<int> cupsLL = new LinkedList<int>();

            for (int i = 0; i < input.Length; i++)
            {
                ht[int.Parse(input[i].ToString())] = cupsLL.AddLast(int.Parse(input[i].ToString()));
            }

            int cnt = 10;
            while(cnt<=1000000){
                ht[cnt] = cupsLL.AddLast(cnt);
                cnt++;
            }

            LinkedListNode<int> currentNode = cupsLL.First;

            for (int i = 0; i < 10000000; i++)
            {
                int location = currentNode.Value - 1;

                LinkedListNode<int> oneNode = currentNode.Next ?? currentNode.List.First;
                LinkedListNode<int> twoNode = oneNode.Next ?? oneNode.List.First;
                LinkedListNode<int> threeNode = twoNode.Next ?? twoNode.List.First;
                LinkedListNode<int> nextNode = threeNode.Next ?? threeNode.List.First;
                
                int one = oneNode.Value;
                int two = twoNode.Value;
                int three = threeNode.Value;

                LinkedListNode<int> locationNode = null;
                location++;

                while(locationNode == null) {
                    location--;
                    if (location<1) location = 1000000;
                    if (location == one || location == two || location == three)
                        continue;
                    
                    locationNode = (LinkedListNode<int>)ht[location];
                }

                cupsLL.Remove(oneNode);
                cupsLL.Remove(twoNode);
                cupsLL.Remove(threeNode);

                cupsLL.AddAfter(locationNode, oneNode);
                cupsLL.AddAfter(oneNode, twoNode);
                cupsLL.AddAfter(twoNode, threeNode);

                currentNode = nextNode;
            }

            var cupOne = (LinkedListNode<int>)ht[1];

            Console.WriteLine($"Part 2 {cupOne.Next.Value} {cupOne.Next.Next.Value} = {(long)cupOne.Next.Value * (long)cupOne.Next.Next.Value}");
        }
    }
}

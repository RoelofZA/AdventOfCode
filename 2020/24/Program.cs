using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace _24
{
    class Program
    {
        static string[] _input;
        static List<List<int>> _tiles = new List<List<int>>();
        static void Main(string[] args)
        {
            ReadInput();
            ProcessTiles();
        }

        static void ProcessTiles() {
            HashSet<(int,int)> locations = new HashSet<(int, int)>();
            Dictionary<(int,int), bool> flipped = new Dictionary<(int, int), bool>();             
            foreach (var tile in _tiles)
            {
                (int,int) location = (0,0);
                foreach (var directions in tile)
                {
                    switch(directions) {
                        case 6:
                            location = (location.Item1-1,location.Item2+1);
                            break;
                        case 5:
                            location = (location.Item1,location.Item2+1);
                            break;
                        case 4:
                            location = (location.Item1,location.Item2-1);
                            break;
                        case 3:
                            location = (location.Item1+1,location.Item2-1);
                            break;
                        case 2:
                            location = (location.Item1+1,location.Item2);
                            break;
                        case 1:
                            location = (location.Item1-1,location.Item2);
                            break;
                        default:
                            Debug.Assert(false);
                            break;
                    }
                    
                }
                if (!locations.Contains(location)){
                    locations.Add(location);
                    flipped.Add(location, true);
                }
                else {
                    flipped[location] = !flipped[location];
                }
            }

            Console.WriteLine($"Part 1 - {flipped.Where(x=>x.Value).Count()}");
            
            //fill Floor
            // for (int y = -50; y < 50; y++)
            // {
            //     for (int x = -50; x < 50; x++)
            //     {
            //         if (!locations.Contains((x,y)))
            //         {
            //             locations.Add((x,y));
            //             //flipped[(x,y)] = true;
            //         }
            //     }
            // }

            Console.WriteLine($"Part 2 - Debug {flipped.Where(x=>x.Value).Count()}");
            for (int i = 0; i < 100; i++)
            {
                HashSet<(int,int)> locationsNew = new HashSet<(int, int)>();
                Dictionary<(int,int), bool> flippedNew = new Dictionary<(int, int), bool>();
                foreach (var location in locations) {
                    locationsNew.Add(location);
                    if (!flipped.ContainsKey((location.Item1-1,location.Item2+1))) {
                        (int,int) tmp = (location.Item1-1,location.Item2+1);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                    if (!flipped.ContainsKey((location.Item1,location.Item2+1))) {
                        (int,int) tmp = (location.Item1,location.Item2+1);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                    if (!flipped.ContainsKey((location.Item1,location.Item2-1))) {
                        (int,int) tmp = (location.Item1,location.Item2-1);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                    if (!flipped.ContainsKey((location.Item1+1,location.Item2-1))) {
                        (int,int) tmp = (location.Item1+1,location.Item2-1);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                    if (!flipped.ContainsKey((location.Item1+1,location.Item2))) {
                        (int,int) tmp = (location.Item1+1,location.Item2);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                    if (!flipped.ContainsKey((location.Item1-1,location.Item2))) {
                        (int,int) tmp = (location.Item1-1,location.Item2);
                        flipped.Add(tmp, false);
                        locationsNew.Add(tmp);
                    }
                }
                locations = locationsNew;
                foreach (var location in locations)
                {
                    // check tiles
                    int sum = 0;
                    if (!flipped.ContainsKey((location.Item1-1,location.Item2+1)) || !flipped[(location.Item1-1,location.Item2+1)]) sum++;
                    if (!flipped.ContainsKey((location.Item1,location.Item2+1)) || !flipped[(location.Item1,location.Item2+1)]) sum++;
                    if (!flipped.ContainsKey((location.Item1,location.Item2-1)) || !flipped[(location.Item1,location.Item2-1)]) sum++;
                    if (!flipped.ContainsKey((location.Item1+1,location.Item2-1)) || !flipped[(location.Item1+1,location.Item2-1)]) sum++;
                    if (!flipped.ContainsKey((location.Item1+1,location.Item2)) || !flipped[(location.Item1+1,location.Item2)]) sum++;
                    if (!flipped.ContainsKey((location.Item1-1,location.Item2)) || !flipped[(location.Item1-1,location.Item2)]) sum++;

                    if (!flipped[location]) // Is White
                    {
                        if (sum == 4)
                            flippedNew.Add(location, true);
                        else
                            flippedNew.Add(location, false);
                    } else { // Black
                        if (sum == 6 || sum < 4)
                            flippedNew.Add(location, false);
                        else
                            flippedNew.Add(location, true);
                    }
                }
                flipped = flippedNew;
                //Console.WriteLine($"Debug - {i} - {flipped.Where(x=>x.Value).Count()}");
            }
            Console.WriteLine($"Part 2 - {flipped.Where(x=>x.Value).Count()}");
        }

        static void ReadInput() {
            _input = File.ReadAllLines("content.txt");

            //Translate e, se, sw, w, nw, and ne
            foreach (var item in _input)
            {
                List<int> array1 = item.Replace("sw","6").Replace("se","5").Replace("nw","4").Replace("ne","3").Replace("e","2").Replace("w","1").ToCharArray().Select(x=>Convert.ToInt32(x.ToString())).ToList();
                _tiles.Add(array1);
            }
        }
    }
}

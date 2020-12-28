using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");
            Dictionary<(int,int,int), string> space = new Dictionary<(int,int,int), string>();
            
            for (int i = 0; i < collection.Count(); i++)
            {
                for (int x = 0; x < collection[i].Length; x++)
                {
                    if (collection[i][x].ToString() == "#") space.Add((x-1, i-1, 0),collection[i][x].ToString());
                }
            }

            for (int i = 1; i < 7; i++)
            {
                Dictionary<(int,int,int), string> newSpace = new Dictionary<(int,int,int), string>();
                Queue<(int,int,int)> tempStack = new Queue<(int, int, int)>();
                foreach (var item in space)
                    tempStack.Enqueue(item.Key);

                while(tempStack.Count > 0)
                {
                    string value = ".";
                    (int,int,int) ik = tempStack.Dequeue();
                    if (space.ContainsKey(ik)) {
                        value = space[ik];
                    }
                    
                    int cntActive = 0;

                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            for (int z = -1; z <= 1; z++)
                            {
                                if (x==0 && y== 0 && z == 0)
                                    continue;

                                if (space.ContainsKey((x+ik.Item1,y+ik.Item2,z+ik.Item3)) && space[(x+ik.Item1,y+ik.Item2,z+ik.Item3)] == "#"){
                                    cntActive++;
                                }
                                else {
                                    if (value=="#" && !space.ContainsKey((x+ik.Item1,y+ik.Item2,z+ik.Item3))) {
                                        
                                        if (!tempStack.Contains((x+ik.Item1,y+ik.Item2,z+ik.Item3))) 
                                            tempStack.Enqueue((x+ik.Item1,y+ik.Item2,z+ik.Item3));
                                    }
                                        
                                }
                            }
                        }
                    }

                    if (value == "#") {
                        if (cntActive >= 2 && cntActive <= 3){
                            newSpace.Add(ik, "#");
                        }
                    } else {
                        if (cntActive == 3){
                            newSpace.Add(ik, "#");
                        }
                    }
                }

                space = newSpace;
                Console.WriteLine($"Part 1 - {space.Count()}");
            }
            Console.WriteLine($"Part 1 - {space.Count()}");

            Part2();
        }

        public static void Part2() {
            string[] collection = File.ReadAllLines("content.txt");
            Dictionary<(int,int,int,int), string> space = new Dictionary<(int,int,int,int), string>();
            
            for (int i = 0; i < collection.Count(); i++)
            {
                for (int x = 0; x < collection[i].Length; x++)
                {
                    if (collection[i][x].ToString() == "#") space.Add((x-1, i-1, 0, 0),collection[i][x].ToString());
                }
            }

            for (int i = 1; i < 7; i++)
            {
                Dictionary<(int,int,int,int), string> newSpace = new Dictionary<(int,int,int,int), string>();
                Queue<(int,int,int,int)> tempStack = new Queue<(int, int, int,int)>();
                foreach (var item in space)
                    tempStack.Enqueue(item.Key);

                while(tempStack.Count > 0)
                {
                    string value = ".";
                    (int,int,int,int) ik = tempStack.Dequeue();
                    if (space.ContainsKey(ik)) {
                        value = space[ik];
                    }
                    
                    int cntActive = 0;

                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            for (int z = -1; z <= 1; z++)
                            {
                                for (int w = -1; w <= 1; w++)
                                {
                                    if (x==0 && y== 0 && z == 0 && w == 0)
                                        continue;

                                    (int,int,int,int) checkVal = (x+ik.Item1,y+ik.Item2,z+ik.Item3,w+ik.Item4);

                                    if (space.ContainsKey(checkVal) && space[checkVal] == "#"){
                                        cntActive++;
                                    }
                                    else {
                                        if (value=="#" && !space.ContainsKey(checkVal)) {
                                            
                                            if (!tempStack.Contains(checkVal)) 
                                                tempStack.Enqueue(checkVal);
                                        }
                                            
                                    }
                                }
                            }
                        }
                    }

                    if (value == "#") {
                        if (cntActive >= 2 && cntActive <= 3){
                            newSpace.Add(ik, "#");
                        }
                    } else {
                        if (cntActive == 3){
                            newSpace.Add(ik, "#");
                        }
                    }
                }

                space = newSpace;
                Console.WriteLine($"Part 2 - {space.Count()}");
            }
            Console.WriteLine($"Part 2 - {space.Count()}");            
        }

        public static void DrawSpace(int i, Dictionary<(int,int,int), string> spaceToDraw) {
            Console.WriteLine($"I: {i}");

            for (int zz = -i ; zz <= i; zz++)
            {
                Console.WriteLine();
                Console.WriteLine($"Z: {zz}");

                for (int yy = -i; yy <= i; yy++) {
                    
                    for (int xx = -i; xx <= i; xx++) {
                        
                        if (spaceToDraw.ContainsKey((xx,yy,zz))) {
                            Console.Write(spaceToDraw[(xx,yy,zz)]);
                        }
                        else {
                            Console.Write(".");
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

    }

    
}

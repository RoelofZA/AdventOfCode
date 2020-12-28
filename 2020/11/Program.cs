using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace _11
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");

            string[,] baseArray = new string[collection[0].Length+2, collection.Count()+2];

            int line = 0;
            foreach (var item in collection)
            {
                for (int i = 0; i < item.Length; i++)
                {
                    baseArray[i+1, line+1] = item[i].ToString();
                }
                line++;
            }

            int cnt = 0;
            int cntNew = 0;
            do {
                cnt = baseArray.Cast<string>().ToArray().Count(x=>x=="#");
                baseArray = ApplyRulesPart2(baseArray);
                cntNew = baseArray.Cast<string>().ToArray().Count(x=>x=="#");
            }
            while (cnt != cntNew);
            Console.WriteLine($"Part 1 - {cntNew}");
        }

        static string[,] ApplyRules(string[,] board) {
            string[,] applied = new string[board.GetUpperBound(0)+1, board.GetUpperBound(1)+1];

            for (int y = 1; y < board.GetUpperBound(1)+1; y++)
            {
                for (int x = 1; x < board.GetUpperBound(0)+1; x++)
                {
                    applied[x,y] = board[x,y];
                    switch(board[x,y])
                    {
                        case "L":
                            if (
                                board[x-1,y-1] != "#" &&
                                board[x,y-1] != "#" &&
                                board[x+1,y-1] != "#" &&

                                board[x-1,y] != "#" &&
                                board[x+1,y] != "#" &&

                                board[x-1,y+1] != "#" &&
                                board[x,y+1] != "#" &&
                                board[x+1,y+1] != "#"
                            )
                                applied[x,y] = "#";
                            break;
                        case "#":
                            int sumOccupied = 0;
                            for (int yy = -1; yy < 2; yy++)
                            {
                                for (int xx = -1; xx < 2; xx++)
                                {
                                    if (!(xx == 0 && yy == 0) && board[xx+x,yy+y]=="#")
                                        sumOccupied++;
                                }
                            }
                            if (sumOccupied>=4)
                                applied[x,y] = "L";
                            break;
                        default:
                            break;
                    }
                }
            }

            return applied;
        }


        static int RangeSearch(string[,] board, int x, int y) {
            
            int foundNo = 0;
                            
            for (int yy = -1; yy < 2; yy++)
            {
                for (int xx = -1; xx < 2; xx++)
                {
                    bool foundL = false;
                    bool found = false;
                    for (int i = 1; i < board.GetUpperBound(0)+1; i++)
                    {
                        if (foundL || found) break;

                        if (
                            (board.GetUpperBound(0) >= (xx*i)+x &&
                            board.GetUpperBound(1) >= (yy*i)+y &&
                            0 <= (xx*i)+x && 0 <= (yy*i)+y)
                            )
                        {
                            if ((xx == 0 && yy == 0) || board[(xx*i)+x,(yy*i)+y] == null)
                                break;

                            switch(board[(xx*i)+x,(yy*i)+y]) {
                                case "#":
                                    found = true;
                                    foundNo++;
                                    break;
                                case "L":
                                    foundL = true;
                                    break;
                            }
                        }
                        else
                            break;

                    }
                }
            }
                
            return foundNo;
        }

        static string[,] ApplyRulesPart2(string[,] board) {
            string[,] applied = new string[board.GetUpperBound(0)+1, board.GetUpperBound(1)+1];

            for (int y = 1; y < board.GetUpperBound(1)+1; y++)
            {
                for (int x = 1; x < board.GetUpperBound(0)+1; x++)
                {
                    applied[x,y] = board[x,y];
                    switch(board[x,y])
                    {
                        case "L":

                            bool found = false;
                            
                            for (int yy = -1; yy < 2; yy++)
                            {
                                for (int xx = -1; xx < 2; xx++)
                                {
                                    bool foundL = false;
                                    for (int i = 1; i < board.GetUpperBound(0)+1; i++)
                                    {
                                        if (foundL || found) break;

                                        if (
                                            (board.GetUpperBound(0) >= (xx*i)+x && board.GetUpperBound(1) >= (yy*i)+y &&
                                            0 <= (xx*i)+x && 0 <= (yy*i)+y) &&
                                            !(xx == 0 && yy == 0))
                                        {
                                            switch(board[(xx*i)+x,(yy*i)+y]) {
                                                case "#":
                                                    found = true;
                                                    break;
                                                case "L":
                                                    foundL = true;
                                                    break;
                                            }
                                        }

                                    }
                                    if (found) break; 
                                }
                                if (found) break; 
                            }
                             
                            if (!found) 
                                applied[x,y] = "#";
                            break;
                        case "#":
                            int sumOccupied = RangeSearch(board, x, y);
                            if (sumOccupied>=5)
                                applied[x,y] = "L";
                            break;
                        default:
                            break;
                    }
                }
            }

            // for (int y = 1; y < board.GetUpperBound(1)+1; y++)
            // {
            //     Console.WriteLine();
            //     for (int x = 1; x < board.GetUpperBound(0)+1; x++)
            //     {
            //         Console.Write(applied[x,y]);
            //     }
            // }

            Console.WriteLine();


            return applied;
        }
    }
}

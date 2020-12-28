using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllLines("content.txt");
            long maxSeatId = 0;
            List<long> listSeats = new List<long>();
            
            foreach (var item in content)
            {
                long seatId = Seat(item);
                listSeats.Add(seatId);
                maxSeatId = Math.Max(seatId,maxSeatId);
            }

            Console.WriteLine($"Part 1 - {maxSeatId}");

            listSeats = listSeats.OrderBy(x=>x).ToList();
            int ten = (int)(listSeats.Count*0.1);

            for (int i = ten; i < listSeats.Count-ten-1; i++)
            {
                if (listSeats[i]+1!=listSeats[i+1])
                {
                    Console.WriteLine($"Part 2 - {listSeats[i]+1}");
                }
            }
        }

        static long Seat(string line) {
            string detailrow = line.Substring(0, 7).Replace("B", "1").Replace("F", "0");
            string detailcolumn = line.Substring(7, 3).Replace("R", "1").Replace("L", "0");

            long row = Convert.ToInt64(detailrow, 2);
            long column = Convert.ToInt64(detailcolumn, 2);

            return (row * 8) + column;
        } 
    }
}

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace _25
{
    class Program
    {
        static void Main(string[] args)
        {
            long card = 6929599, cardLoop = 0; //6929599;
            long door = 2448427, doorLoop = 0; //2448427;

            bool foundCard = false, foundDoor = false;

            long current = 1;
            long counter = 1;
            while(!foundCard || !foundDoor) {
                current*=7;
                current = current%20201227;

                if (current == card)
                {
                    Console.WriteLine($"Card {counter}");
                    foundCard = true;
                    cardLoop = counter;
                }
                if (current == door)
                {
                    Console.WriteLine($"Door {counter}");
                    foundDoor = true;
                    doorLoop = counter;
                }
                counter++;
            }

            current = 1;
            for (int i = 0; i < doorLoop; i++)
            {
                current*=card;
                current = current%20201227;
            }
            Console.WriteLine(current);

            current = 1;
            for (int i = 0; i < cardLoop; i++)
            {
                current*=door;
                current = current%20201227;
            }
            Console.WriteLine(current);



        }
    }
}

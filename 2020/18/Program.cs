using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace _18
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");
            long result = 0;

            foreach (var item in collection)
            {
                result += Deconstruct(item);
            }

            Console.WriteLine($"Part 1 - {result}");
        }

        public static long Deconstruct(string calc) {
            //calc = calc.Replace(" ", "");
            while(calc.Contains('(')) {
                int lastBracket = calc.LastIndexOf('(')+1;
                string bracket = calc.Substring(lastBracket, calc.IndexOf(')',lastBracket)-lastBracket);
                calc = calc.Replace($"({bracket})", ProcessString(bracket).ToString()); 
            }
            return ProcessString(calc);
        }

        public static long ProcessString(string calc) {
            string[] working = ProcessStringAdv(calc).Split(" ");
            long calcValue = long.Parse(working[0]);

            for (long i = 1; i < working.Length; i++)
            {
                switch(working[i]) {
                    case "+":
                        calcValue+= long.Parse(working[++i]);
                        break;
                    case "-":
                        calcValue-= long.Parse(working[++i]);
                        break;
                    case "*":
                        calcValue*= long.Parse(working[++i]);
                        break;
                    case "/":
                        calcValue/= long.Parse(working[++i]);
                        break;
                }
            }

            return calcValue;
        }

        public static string ProcessStringAdv(string calc) {
            string[] working = calc.Split(" ");

            for (long i = 1; i < working.Length-1; i++)
            {
                switch(working[i]) {
                    case "+":
                        long newVal = long.Parse(working[i-1]) + long.Parse(working[i+1]);
                        working[i-1] = "1";
                        working[i] = "*";
                        working[i+1] = newVal.ToString();
                        break;
                }
            }

            // /Console.WriteLine(string.Join(" ", working));
            return string.Join(" ", working);
        }


    }
}

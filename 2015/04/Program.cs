using System;
using System.Security.Cryptography;
using System.Text;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            long number = 0;
            while(0==0) {
                number++;
                string hash = BitConverter.ToString(MD5.Create().ComputeHash(Encoding.ASCII.GetBytes("bgvyzdsv" + number.ToString()))).Replace("-","");
                if (hash.StartsWith("000000")){
                    Console.WriteLine($"{hash} {number}");
                    return;
                }
            }
        }
    }
}

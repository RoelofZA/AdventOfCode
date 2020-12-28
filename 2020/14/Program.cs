using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace _14
{
    class Program
    {
        public static string _mask = "";
        public static List<(bool, double)> maskList = new List<(bool, double)>();
        public static List<(int, string)> maskAltList = new List<(int, string)>();
        public static Dictionary<long, double> _memory = new Dictionary<long, double>();
        public static void CW(string item) => Console.WriteLine(item);

        public static void CalcMask() {
            string smallMask = Regex.Replace(_mask, "^[Xx]*", "");
            maskList = new List<(bool, double)>();
            maskAltList = new List<(int, string)>();
            for (int i = smallMask.Length-1; i >= 0; i--)
            {
                if (smallMask[i]!='X')
                {
                    maskList.Add((smallMask[i]=='1'?true:false, Math.Pow(2, (smallMask.Length-1 - i))));
                    maskAltList.Add((smallMask.Length-1 - i, smallMask[i].ToString()));
                }
            }
        }
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");

            // foreach (var item in collection)
            // {
            //     string[] sections = item.Replace(" ", "").Split("=");
            //     //CW($"{sections[0]} = {sections[1]}");

            //     switch(sections[0]) {
            //         case "mask":
            //             _mask = sections[1];
            //             CalcMask();
            //             break;
            //         default:
            //             double memLocation = double.Parse(Regex.Replace(sections[0], "[\\[\\]a-z]", ""));
            //             double newValue = double.Parse(sections[1]);

            //             string binary = Convert.ToString((int)newValue, 2).PadLeft(36, '0');

            //             var charArr = binary.ToCharArray();

            //             foreach (var item2 in maskAltList)
            //             {
            //                 charArr[binary.Length - 1 -item2.Item1] = char.Parse(item2.Item2);
            //             }

            //             binary = string.Join("", charArr);
            //             newValue = Convert.ToInt64(binary,2);

            //             if (_memory.ContainsKey((int)memLocation))
            //                 _memory[(int)memLocation] = newValue;
            //             else
            //                 _memory.Add((int)memLocation, newValue);
            //             break;
            //     }

            // }

            // CW($"Part 1 - {_memory.Values.Sum()}");

            foreach (var item in collection)
            {
                string[] sections = item.Replace(" ", "").Split("=");

                switch(sections[0]) {
                    case "mask":
                        _mask = sections[1];
                        CalcMask();
                        break;
                    default:
                        double memLocation = double.Parse(Regex.Replace(sections[0], "[\\[\\]a-z]", ""));
                        double newValue = double.Parse(sections[1]);
                        string newAddress = Convert.ToString((int)memLocation, 2); //.PadLeft(36, '0')
                        var charArr = newAddress.ToCharArray();
                        var charArrMask = _mask.ToCharArray();
                        var newCharArray = new char[charArrMask.Length];
                        
                        for (int i = 0; i < charArrMask.Length; i++)
                        {
                            newCharArray[charArrMask.Length-1-i] = '0';
                            switch(_mask[_mask.Length - 1 - i]) {
                                case '1':
                                    newCharArray[charArrMask.Length-1-i] = '1';
                                    break;
                                case 'X':
                                    newCharArray[charArrMask.Length-1-i] = 'X';
                                    break;
                                default:
                                    if (charArr.Length-1-i >= 0)
                                        newCharArray[newCharArray.Length-1-i] = charArr[charArr.Length-1-i];
                                    break;
                            }
                        }

                        newAddress = string.Join("", newCharArray);
                        //CW(newAddress);

                        List<string> addresses = new List<string>();
                        addresses.Add("0");
                        int startPos = 0;

                        while (startPos<newAddress.Length) {
                            List<string> newEntries = new List<string>();

                            if (newAddress[startPos]=='X') {
                                foreach (var address in addresses)
                                {
                                    newEntries.Add(address + "0");
                                    newEntries.Add(address + "1");
                                }
                            }
                            else
                                foreach (var address in addresses)
                                {
                                    newEntries.Add(address + newAddress[startPos].ToString());
                                }
                            
                            addresses = newEntries;

                            startPos++;
                        }
                        //addresses.ForEach(CW);

                        foreach (var address in addresses)
                        {
                            long addressNo = Convert.ToInt64(address,2);
                            if (_memory.ContainsKey(addressNo))
                                _memory[addressNo] = newValue;
                            else
                                _memory.Add(addressNo, newValue);
                        }
                        break;
                }

            }

            //_memory.ToList().ForEach(x=>CW($"{x.Key}  {x.Value.ToString()}"));

            CW($"Part 2 - {_memory.Values.Sum()}");
        }
    }
}
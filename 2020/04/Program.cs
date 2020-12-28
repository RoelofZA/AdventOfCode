using System;
using System.IO;
using System.Text.RegularExpressions;

namespace _04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] content = File.ReadAllText("content.txt").Split(Environment.NewLine + Environment.NewLine);
            int count = 0, countValid = 0;

            foreach (var item in content)
            {
                if (item.Contains("byr:") && item.Contains("iyr:") && item.Contains("eyr:") && item.Contains("hgt:") && item.Contains("hcl:") && item.Contains("ecl:") && item.Contains("pid:")                    ) {
                        count++;
                        string[] spaceSplit = item.Replace(Environment.NewLine, " ").Split(" ");
                        bool valid = true;

                        foreach (var subItem in spaceSplit)
                        {
                            string[] colonSplit = subItem.Split(":");
                            switch(colonSplit[0]) {
                                case "byr":
                                    valid = Regex.IsMatch(colonSplit[1], "^(200[0-2]{1}|19[2-9]{1}[0-9]{1}){1}$");
                                    break;
                                case "iyr":
                                    valid = Regex.IsMatch(colonSplit[1], "^((201[0-9])|(2020)){1}$");
                                    break;
                                case "eyr":
                                    valid = Regex.IsMatch(colonSplit[1], "^((202)[0-9]{1}|(2030)){1}$");
                                    break;
                                case "hgt":
                                    valid = Regex.IsMatch(colonSplit[1], "^((1[5-8]{1}[0-9]{1}|(19[0-3]{1}))cm|((59)|(7[0-6]{1})|(6[0-9]{1}))in){1}$");
                                    break;
                                case "hcl":
                                    valid = Regex.IsMatch(colonSplit[1], "^((#)[0-9a-f]{6}){1}$");
                                    break;
                                case "ecl":
                                    valid = Regex.IsMatch(colonSplit[1], "^(amb|blu|brn|gry|grn|hzl|oth){1}$");
                                    break;
                                case "pid":
                                    valid = Regex.IsMatch(colonSplit[1], "^[0-9]{9}$");
                                    break;
                            }
                            if (!valid)
                                break;
                        }
                        if (valid) countValid++;
                }
            }
            Console.WriteLine($"Part 1 - {count}");
            Console.WriteLine($"Part 2 - {countValid}");
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace _03
{
    class Program
    {
        static void Main(string[] args)
        {
            //Main2();

            string content = File.ReadAllText("content.txt");
            long currentPosX = 0, currentPosY = 0, cnt = 0;
            Dictionary<Tuple<long, long>, int> ht = new Dictionary<Tuple<long, long>, int>();
            HashSet<Tuple<long, long>> setVisit = new HashSet<Tuple<long, long>>();
            setVisit.Add(new Tuple<long, long>(currentPosX, currentPosY));
            
            foreach (var item in content.ToCharArray())
            {
                switch(item) {
                    case '^':
                        currentPosY = currentPosY+1;
                        break;
                    case 'v':
                        currentPosY = currentPosY-1;
                        break;
                    case '>':
                        currentPosX = currentPosX + 1;
                        break;
                    case '<':
                        currentPosX = currentPosX - 1;
                        break;
                    default:
                        Console.WriteLine(item.ToString());
                        break;
                }
                setVisit.Add(new Tuple<long, long>(currentPosX, currentPosY));

                if (cnt%2==0)
                    setVisit.Add(new Tuple<long, long>(currentPosX, currentPosY));
            }

            Console.WriteLine($"Part 1 - {setVisit.Count}");
            HashSet<Tuple<long, long>> setVisit2 = new HashSet<Tuple<long, long>>();
            DeliverPresents(content,0, ref setVisit2);
            DeliverPresents(content,1, ref setVisit2);
            Console.WriteLine($"Part 2 - {setVisit2.Count}");
        }
        
        static int DeliverPresents(string content, int step, ref HashSet<Tuple<long, long>> setVisit) {
            //HashSet<Tuple<long, long>> setVisit = new HashSet<Tuple<long, long>>();
            long currentPosX = 0, currentPosY = 0, cnt = 0;
            setVisit.Add(new Tuple<long, long>(currentPosX, currentPosY));

            foreach (var item in content.ToCharArray())
            {
                cnt++;
                if ((cnt-1)%2 != step)
                    continue;

                switch(item) {
                    case '^':
                        currentPosY = currentPosY+1;
                        break;
                    case 'v':
                        currentPosY = currentPosY-1;
                        break;
                    case '>':
                        currentPosX = currentPosX + 1;
                        break;
                    case '<':
                        currentPosX = currentPosX - 1;
                        break;
                    default:
                        Console.WriteLine(item.ToString());
                        break;
                }
                setVisit.Add(new Tuple<long, long>(currentPosX, currentPosY));
            }
            return setVisit.Count;
        }

    }
}

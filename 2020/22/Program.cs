using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace _22
{
    class Program
    {
        static List<Queue<int>> queues = new List<Queue<int>>();
        static HashSet<string> _history = new HashSet<string>();
        static void Main(string[] args)
        {
            LoadContent();
            //PlayGame();
            

            PlayGame2();

            Part01();
        }

        static void Part01() {
            long sum = 0;
            List<int> winingQueue = (queues[0].Count>0?queues[0]:queues[1]).ToList();

            for (int i = 1; i <= winingQueue.Count; i++)
            {
                sum += (winingQueue[winingQueue.Count - i] * i);
            }
            Console.WriteLine($"Part 1 - {sum}");
        }

        

        static void PlayGame() {

            while( queues[0].Count>0 && queues[1].Count>0 ) {
                int player1 = queues[0].Dequeue();
                int player2 = queues[1].Dequeue();

                if (player1>player2) {
                    queues[0].Enqueue(player1);
                    queues[0].Enqueue(player2);
                } else {
                    queues[1].Enqueue(player2);
                    queues[1].Enqueue(player1);
                }
            }

        }

        
        static bool SubGame(int player1, int player2) {

            // Copy Queues
            List<Queue<int>> subQueues = new List<Queue<int>>();
            HashSet<string> history = new HashSet<string>();
            subQueues.Add(new Queue<int>());
            subQueues.Add(new Queue<int>());

            var arrQueue1 = queues[0].ToArray();
            var arrQueue2 = queues[1].ToArray();

            for (int i = 0; i < player1; i++)
            {
                subQueues[0].Enqueue(arrQueue1[i]);
            }
            for (int i = 0; i < player2; i++)
            {
                subQueues[1].Enqueue(arrQueue2[i]);
            }

            

            while( subQueues[0].Count>0 && subQueues[1].Count>0 ) {
                player1 = subQueues[0].Dequeue();
                player2 = subQueues[1].Dequeue();

                string historyItem = string.Join(",", subQueues[0].ToList()) + ":" + string.Join(",", subQueues[1].ToList());
                if (!history.Contains(historyItem))
                    history.Add(historyItem);
                else {
                    Console.WriteLine("Player 01 - Wins");
                    return true;
                }

                if (player1>player2) {
                    subQueues[0].Enqueue(player1);
                    subQueues[0].Enqueue(player2);
                } else {
                    subQueues[1].Enqueue(player2);
                    subQueues[1].Enqueue(player1);
                }
            }

            return subQueues[0].Count > 0;
        }
        static void PlayGame2() {

            while( queues[0].Count>0 && queues[1].Count>0 ) {
                int player1 = queues[0].Dequeue();
                int player2 = queues[1].Dequeue();

                //Add to history
                string historyItem = string.Join(",", queues[0].ToList()) + ":" + string.Join(",", queues[1].ToList());
                if (!_history.Contains(historyItem))
                    _history.Add(historyItem);
                else {
                    Console.WriteLine("Player 01 - Wins");
                    queues[1].Clear();
                    return;
                }

                // Check for recursive games
                if (player1 <= queues[0].Count && player2 <= queues[1].Count) {
                    bool player1Won = SubGame(player1, player2);

                    if (player1Won) {
                        queues[0].Enqueue(player1);
                        queues[0].Enqueue(player2);
                    } else {
                        queues[1].Enqueue(player2);
                        queues[1].Enqueue(player1);
                    }

                } 
                // Normal Game
                else if (player1>player2) {
                    queues[0].Enqueue(player1);
                    queues[0].Enqueue(player2);
                } else {
                    queues[1].Enqueue(player2);
                    queues[1].Enqueue(player1);
                }
            }

        }

        static void LoadContent() {
            string[] content = File.ReadAllLines("content.txt");

            int player = 0;
            foreach (var item in content)
            {
                if (string.IsNullOrEmpty(item))
                    continue;
                else if (item.StartsWith("Player")){
                    player = int.Parse(item.Replace("Player ","").Replace(":",""));
                    queues.Add(new Queue<int>());
                }
                else {
                    queues[player-1].Enqueue(int.Parse(item));
                } 
            }
        }

    }
}
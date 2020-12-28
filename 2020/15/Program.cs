using System;
using System.Linq;
using System.Collections.Generic;

namespace _15
{
    class Program
    {
        public static int _turn = 0, _lastValue = 0;
        public static int[] input = {16,12,1,0,15,7,11};
        public static Dictionary<int,List<int>> _memory = new Dictionary<int, List<int>>();
        public static void CW(string item) => Console.WriteLine(item);
        static void Main(string[] args)
        {
            for (int i = 0; i < 30000000; i++) //30000000
            {
                _turn++;
                turnLogic();
                // if (_turn == 99 || 
                // _turn == 999 || 
                // _turn == 9999 || 
                // _turn == 99999) 
                //CW(_lastValue.ToString());
                //  if ((_turn+1) % 1000 == 0) {
                //       _memory.GroupBy(x=>x.Item1).Select(x=>new {name = x.Key, Cnt = x.Count()}).OrderByDescending(x=>x.Cnt).Take(10).ToList().ForEach(x=>CW($"{x.name} = {x.Cnt}"));
                //       CW("");
                //  }

                //     var cleanup = _memory.GroupBy(x=>x.Item1).Select(x=>new {name = x.Key, Cnt = x.Count()}).Where(x=>x.Cnt > 2).ToList();
                    
                    
                // }
                
            }
            Console.WriteLine($"Part 1 - {_lastValue}");
        }

        public static void turnLogic() {
            if (_turn-1 < input.Length)
            {
                _lastValue = input[_turn-1];
                _memory.Add(_lastValue, new List<int>() { _turn });
                return;
            }
            
            if (_memory[_lastValue].Count() == 1) {
                // var list = _memory.Where(x=>x.Item1 == _lastValue).OrderByDescending(y=>y.Item2).Take(2).Select(x=>x.Item2).ToList();
                // _lastValue = _turn - list[0];
                _lastValue = 0;
            } else {
                var list = _memory[_lastValue].TakeLast(2).ToList();
                var difference = list[1]-list[0];
                _lastValue = difference;
            }
            if (_memory.ContainsKey(_lastValue)) {
                _memory[_lastValue].Add(_turn);
            }
            else {
                _memory.Add(_lastValue, new List<int>() { _turn });
            }
        }
    }
}

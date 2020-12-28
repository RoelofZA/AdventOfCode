using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

namespace _20
{
    class Program
    {
        static int[,] _orderedBoard = new int[12,12];
        static Dictionary<int, int[]> _board = new Dictionary<int, int[]>();
        static Dictionary<int, string[,]> _tilesComplex = new Dictionary<int, string[,]>();
        static Dictionary<int, (bool, int)> _adjustments = new Dictionary<int, (bool, int)>();
        static Dictionary<int, string[]> _tiles = new Dictionary<int, string[]>();

        static void PopulateTiles(string[] raw)
        {
            bool[,] tiles = new bool[10, 10];
            bool[,] tilesInv = new bool[10, 10];

            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i].StartsWith("Tile "))
                {
                    int tileId = int.Parse(Regex.Replace(raw[i++], "[^0-9]*", ""));
                    string[] sides = new string[8];
                    sides[0] = raw[i]; // Top;
                    sides[1] = raw[i + 9]; // Bottom;
                    for (int down = 0; down < 10; down++)
                    {
                        sides[2] += raw[i][0]; // Left;
                        sides[3] += raw[i++][9]; // Right;
                    }

                    //Invert Tiles
                    sides[4] = string.Join("", sides[0].ToCharArray().Reverse());
                    sides[5] = string.Join("", sides[1].ToCharArray().Reverse());
                    sides[6] = string.Join("", sides[2].ToCharArray().Reverse());
                    sides[7] = string.Join("", sides[3].ToCharArray().Reverse());
                    _tiles.Add(tileId, sides);
                }
            }
        }

        static void PopulateTiles2(string[] raw)
        {
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i].StartsWith("Tile "))
                {
                    int tileId = int.Parse(Regex.Replace(raw[i++], "[^0-9]*", ""));
                    
                    string[,] tile = new string[10,10];

                    for (int y = 0; y < 10; y++)
                    {
                        for (int x = 0; x < 10; x++)
                        {
                            tile[x,y] = raw[i+y][x].ToString();
                        }
                    }
                    i=i+10;
                    
                    _tilesComplex.Add(tileId, tile);
                }
            }
        }

        static void CalculateOrder()
        {
            foreach (var item in _tiles.OrderBy(x => x.Key))
            {
                int[] neighbors = new int[4];
                foreach (var searchItem in _tiles.Where(x => x.Key != item.Key))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int ii = 0; ii < 8; ii++)
                            if (item.Value[i] == searchItem.Value[ii])
                            {
                                neighbors[i] = searchItem.Key;
                            }
                    }
                }
                _board.Add(item.Key, neighbors);
            }

        }

        static void OrderBuilder() {
            Queue<int> nextTile = new Queue<int>();
            nextTile.Enqueue(_tiles.First().Key);
            HashSet<int> completed = new HashSet<int>();

            while(nextTile.Count > 0) {
                int key = nextTile.Dequeue();
                string[,] tile = _tilesComplex[key];
                string top = "", bottom = "", left = "", right = "";
                string[] stuff = new string[4];

                // Edges
                for (int i = 0; i < 10; i++)
                {
                    top+=tile[i,0];
                    bottom+=tile[i,9];
                    left+=tile[0,i];
                    right+=tile[9,i];
                }

                stuff[0] = top;
                stuff[1] = bottom;
                stuff[2] = left;
                stuff[3] = right;

                // Find Neighbors
                int[] neighbors = new int[4];
                foreach (var searchItem in _tiles.Where(x => x.Key != key))
                {
                    for (int i = 0; i < 4; i++)
                    {
                        for (int ii = 0; ii < 8; ii++) {
                            if (stuff[i] == searchItem.Value[ii])
                            {
                                if (!completed.Contains(key)) {
                                    neighbors[i] = searchItem.Key;
                                    nextTile.Enqueue(searchItem.Key);
                                    
                                    //Check Rotation.
                                    //Just Rotate
                                    RotateTarget(key, searchItem.Key, i, stuff[i]);
                                }
                                
                            }
                        }
                    }
                }
                completed.Add(key);
                if (!_board.ContainsKey(key)) _board.Add(key, neighbors);                
            }
        }

        static void transpose2dArray(ref string[,] matrix){
            int  nLen = matrix.GetLength(0);
            int  mLen = matrix.GetLength(1);    
            for(int n=0; n<nLen; n++){
                for(int m=0; m<mLen; m++){
                    if(n>m){
                        string tmp = matrix[n,m];
                        matrix[n,m] = matrix[m,n];
                        matrix[m,n] = tmp;
                    }
                }
            }
        }

        static void reverse2dArray(ref string[,] matrix){
            int  nLen = matrix.GetLength(0);
            int  mLen = matrix.GetLength(1);
            for(int n=0; n<nLen; n++){
                for(int m=0; m<mLen/2; m++){                
                    string tmp = matrix[n,m];
                    matrix[n,m] = matrix[n, mLen-1-m];
                    matrix[n,mLen-1-m] = tmp;
                }
            }
        }
        static void RotateTarget(int source, int target, int sourceSide, string sourceStr) {

            string[,] sourceTile = _tilesComplex[source];
            string[,] targetTile = _tilesComplex[target];

            bool compareSides() {

                switch(sourceSide) {
                    case 0:
                        for (int i = 0; i < 10; i++)
                        {
                            if (targetTile[i,9] != sourceStr[i].ToString())
                                return false;
                        }
                        break;
                    case 1:
                        for (int i = 0; i < 10; i++)
                        {
                            if (targetTile[i,0] != sourceStr[i].ToString())
                                return false;
                        }
                        break;
                    case 2:
                        for (int i = 0; i < 10; i++)
                        {
                            if (targetTile[9,i] != sourceStr[i].ToString())
                                return false;
                        }
                        break;
                    case 3:
                        for (int i = 0; i < 10; i++)
                        {
                            if (targetTile[0,i] != sourceStr[i].ToString())
                                return false;
                        }
                        break;
                }

                return true;
            }

            
            void printArr(string[,] matrix) {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < matrix.GetLength(0); x++)
                    {
                        Console.Write(matrix[x,y]);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            bool done = false;
            for (int rotate = 0; rotate < 4; rotate++)
            {
                if (compareSides()) {
                    done = true;
                    break;
                } else {
                    //Rotate
                    //printArr(targetTile);
                    transpose2dArray(ref targetTile);
                    reverse2dArray(ref targetTile);
                    //printArr(targetTile);
                }
            }

            if (!done) {
                transpose2dArray(ref targetTile);
                for (int rotate = 0; rotate < 4; rotate++)
                {
                    if (compareSides()) {
                        done = true;
                        break;
                    } else {
                        //Rotate
                        //printArr(targetTile);
                        transpose2dArray(ref targetTile);
                        reverse2dArray(ref targetTile);
                        //printArr(targetTile);
                    }
                }
            }

            System.Diagnostics.Debug.Assert(done);
            if (done) {
                _tilesComplex[target] = targetTile;

                //printArr(sourceTile);
                //printArr(targetTile);
            }
        }
        static void Guess()
        {
            long sum = 1;
            foreach (var item in _board)
            {
                if (item.Value.Where(x => x == 0).Count() > 1)
                {
                    Console.WriteLine($"{item.Key} {item.Value.Where(x => x == 0).Count()}");
                    sum *= item.Key;
                }
            }
            Console.WriteLine($"Part 1 - {sum}");
        }

        static void BuildBoard()
        {

            // Start With 3209
            List<List<int>> boardMock = new List<List<int>>();
            int current = 3209, next = 0, oldItem = 0, currentOld = 0;

            while (current != 0)
            {
                List<int> sideways = new List<int>();
                int leftItem = 0;
                sideways.Add(current);

                if (boardMock.Count == 0)
                {
                    next = _board[current][1];
                    leftItem = _board[current][3];
                }
                else
                {
                    // Turn board
                    int turns = 0;
                    for (int x = 0; x < 4; x++)
                    {
                        if (_board[current][0] != currentOld)
                        {
                            turns++;
                            _board[current] = new int[4] { _board[current][1], _board[current][2], _board[current][3], _board[current][0] };
                        }
                        else
                        {
                            break;
                        }
                    }
                    next = _board[current][1];
                    leftItem = _board[current][3];
                    _adjustments.Add(current, (false, turns));
                }

                oldItem = current;
                currentOld = current;
                // leftItem = _board[current][leftIndex]; 

                while (leftItem != 0)
                {
                    // for (int zz = 0; zz < 4; zz++)
                    // {
                    if (leftItem != 0) sideways.Add(leftItem);
                    int turns = 0;
                    for (int x = 0; x < 4; x++)
                    {
                        if (_board[leftItem][2] != oldItem)
                        {
                            turns++;
                            _board[leftItem] = new int[4] { _board[leftItem][1], _board[leftItem][2], _board[leftItem][3], _board[leftItem][0] };
                        }
                        else
                        {
                            _adjustments.Add(leftItem, (false, turns));
                            break;
                        }
                    }
                    oldItem = leftItem;
                    leftItem = _board[leftItem][3];

                    // if (_board[leftItem][zz] == oldItem) {
                    //     oldItem = leftItem;
                    //     switch(zz) {
                    //         case 0:
                    //             leftItem = _board[leftItem][1];
                    //             break;
                    //         case 1:
                    //             leftItem = _board[leftItem][0];
                    //             break;
                    //         case 2:
                    //             leftItem = _board[leftItem][3];
                    //             break;
                    //         case 3:
                    //             leftItem = _board[leftItem][2];
                    //             break;
                    //     }

                    //    break;
                    //}
                    //}
                }
                boardMock.Add(sideways);

                current = next;
            }

            Console.WriteLine();
            foreach (var item in boardMock)
            {

                item.ForEach(x => Console.Write($"{x} "));
                Console.WriteLine();

            }

        }

        static void PrintCompleted() {
            int downPos = _board.Where(x=>x.Value[0] == 0 && x.Value[2]==0).ToList().First().Key;
            int nextPos = downPos;
            int x = 0, y=0;

            Console.WriteLine();
            while(downPos != 0) {
                
                while(nextPos != 0) {
                    //Console.Write($"{nextPos} ");
                    _orderedBoard[x,y] = nextPos;
                    nextPos = _board[nextPos][3];
                    x++;
                }
                //Console.WriteLine();
                downPos = _board[downPos][1];
                nextPos = downPos;
                y++;
                x=0;
            }
        }

        static void SanitizeBoard() {
            foreach (var item in _tilesComplex)
            {
                string[,] cleaned = new string[item.Value.GetLength(0)-2, item.Value.GetLength(0)-2];
                for (int y = 1; y < item.Value.GetLength(0)-1; y++)
                {
                    for (int x = 1; x < item.Value.GetLength(0)-1; x++)
                    {
                        cleaned[x-1,y-1] = item.Value[x,y];
                    }
                }
                _tilesComplex[item.Key] = cleaned;
            }
        }

        static void flattenTiles() {
            StringBuilder db = new StringBuilder();
            string[,] matrix = new string[_tilesComplex[_orderedBoard[0,0]].GetLength(1)*12, _tilesComplex[_orderedBoard[0,0]].GetLength(1)*12];
            for (int orderY = 0; orderY < _orderedBoard.GetLength(0); orderY++)
            {
                for (int internalY = 0; internalY < _tilesComplex[_orderedBoard[0,0]].GetLength(1); internalY++)
                {
                    for (int orderX = 0; orderX < _orderedBoard.GetLength(0); orderX++)
                    {
                        string[,] tile = _tilesComplex[_orderedBoard[orderX, orderY]];
                        for (int internalX = 0; internalX < _tilesComplex[_orderedBoard[0,0]].GetLength(1); internalX++)
                        {
                            //Console.Write(tile[internalX, internalY]);
                            db.Append(tile[internalX, internalY].Replace(".", " "));
                        }
                    }
                    //Console.WriteLine();
                    db.Append(Environment.NewLine);
                }
            }

            File.WriteAllText("output.txt", db.ToString());

            string[] collection = File.ReadAllLines("output.txt");
            
            for (int i = 0; i < collection[0].Length; i++)
            {
                for (int x = 0; x < collection[i].Length; x++)
                {
                    matrix[x,i] = collection[i][x].ToString();
                }
            }

            transpose2dArray(ref matrix);
            reverse2dArray(ref matrix);

            db.Clear();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    db.Append(matrix[x,i]);
                }
                db.Append(Environment.NewLine);
            }

            string final = db.ToString();
            File.WriteAllText("output2.txt", final);

            string regx1 = @"###\W\W\W\W##\W\W\W\W##\W\W\W\W#";
            string regx2 = @"\W\W\W#\W\W#\W\W#\W\W#\W\W#\W\W#\W";
            string regx3 = @"\W#\W\W\W\W\W\W\W\W\W\W\W\W\W\W\W\W\W\W";

            MatchCollection mc = Regex.Matches(final, regx2);

            HashSet<int> indexCollection = new HashSet<int>();

            foreach (Match item in mc)
            {
                indexCollection.Add(item.Index);
            }

            mc = Regex.Matches(final, regx1);
            int counter = 0;
            foreach (Match item in mc)
            {
                if (indexCollection.Contains(item.Index+98))
                    counter++;
            }
            Console.WriteLine($"{counter} {final.Where(x=>x=='#').Count()}");

        }
        static void Main(string[] args)
        {
            string[] tiles = File.ReadAllLines("content.txt");

            PopulateTiles(tiles);
            PopulateTiles2(tiles);

            OrderBuilder();

            PrintCompleted();

            SanitizeBoard();

            flattenTiles();
            // CalculateOrder();
            // CalculateOrder2();
            // Guess();
            // BuildBoard();

            Console.WriteLine("Part 2 - ");
        }
    }
}

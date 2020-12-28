using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using SkiaSharp;

namespace _12
{
    class Program
    {
        public static List<(int, int)> _points = new List<(int, int)>();
        public static List<(int, int)> _points2 = new List<(int, int)>();
        public static (int, int) _currentLocation = (0, 0);
        public static (int, int) _currentWayPoint = (0, 0);
        public static double _hypo = 0;
        public static double _angle = 0;
        public static enumDir _currenDirection = enumDir.E;
        public enum enumDir
        {
            N,
            E,
            S,
            W
        }

        public static void drawBoard()
        {
            var info = new SKImageInfo(1000, 1000);
            using (var surface = SKSurface.Create(info))
            {
                // the the canvas and properties
                var canvas = surface.Canvas;

                // make sure the canvas is blank
                canvas.Clear(SKColors.White);

                // draw some text
                var paint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill
                };

                SKPoint coord;

                paint.Color = SKColors.Blue;
                SKPoint oldCoord = new SKPoint(info.Width / 2, info.Height / 2);

                foreach (var item in _points)
                {
                    coord = new SKPoint(info.Width / 2 + item.Item1, info.Height / 2 + item.Item2);
                    canvas.DrawLine(oldCoord, coord, paint);
                    oldCoord = coord;
                }

                paint = new SKPaint
                {
                    Color = SKColors.Black,
                    IsAntialias = true,
                    Style = SKPaintStyle.Fill,
                    TextAlign = SKTextAlign.Left,
                    TextSize = 24
                };

                // coord = new SKPoint(10, 20);
                // canvas.DrawText(displayText, coord, paint);

                // save the file
                using (var image = surface.Snapshot())
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite("output.png"))
                {
                    data.SaveTo(stream);
                }
            }
        }
        static void shift(enumDir direction, int units)
        {
            switch (direction)
            {
                case enumDir.N: _currentLocation.Item2 += units; break;
                case enumDir.S: _currentLocation.Item2 -= units; break;
                case enumDir.E: _currentLocation.Item1 += units; break;
                case enumDir.W: _currentLocation.Item1 -= units; break;
            }
        }
        static void move(int units)
        {
            switch (_currenDirection)
            {
                case enumDir.N: _currentLocation.Item2 += units; break;
                case enumDir.S: _currentLocation.Item2 -= units; break;
                case enumDir.E: _currentLocation.Item1 += units; break;
                case enumDir.W: _currentLocation.Item1 -= units; break;
            }
        }

        static void shiftWayPoint(enumDir direction, int units)
        {
            switch (direction)
            {
                case enumDir.N: _currentWayPoint.Item2 += units; break;
                case enumDir.S: _currentWayPoint.Item2 -= units; break;
                case enumDir.E: _currentWayPoint.Item1 += units; break;
                case enumDir.W: _currentWayPoint.Item1 -= units; break;
            }
            _hypo = Math.Sqrt( 
                Math.Pow(_currentWayPoint.Item1, 2) + 
                Math.Pow(_currentWayPoint.Item2, 2) );
            _angle = DegreeAngle(Math.Asin((double)(_currentWayPoint.Item1) / _hypo));

        }
        static void moveWayPoint(int units)
        {
            _currentLocation.Item1 += units * _currentWayPoint.Item1;
            _currentLocation.Item2 += units * _currentWayPoint.Item2;
        }

        public static double DegreeAngle(double rad)
        {
            double x = rad * 180/ Math.PI;
            return x;
        }

        public static double DegreeRad(double degrees)
        {
            double x = (Math.PI / 180) * degrees;
            return x;
        }

        static void turnWayPoint(string direction, int units)
        {
            _hypo = Math.Sqrt( 
                Math.Pow(_currentWayPoint.Item1, 2) + 
                Math.Pow(_currentWayPoint.Item2, 2) );

            
            Console.WriteLine($"{direction}{units} {_angle}");
            double newAngle = ((direction=="R"?1:-1) * (double)units) +_angle;
            Console.WriteLine($"{direction}{units} {_angle} -- {newAngle}");

            if (newAngle<0)
                newAngle+=360;

            if (newAngle>360)
                newAngle-=360;           

            double xx = Math.Sin(DegreeRad(newAngle))*_hypo;
            double yy = Math.Cos(DegreeRad(newAngle))*_hypo;

            double rads = DegreeRad((direction=="R"?-1:1) * (double)units);
            int newX = Convert.ToInt32((_currentWayPoint.Item1 * Math.Cos(rads) - _currentWayPoint.Item2 * (int)Math.Sin(rads)));
            int newY = Convert.ToInt32((_currentWayPoint.Item1 * Math.Sin(rads) + _currentWayPoint.Item2 * (int)Math.Cos(rads)));

            if (Convert.ToInt32(xx)!=newX || Convert.ToInt32(yy)!=newY)
            {
                Console.WriteLine();
            }

            _currentWayPoint = (newX, newY);
            //_currentWayPoint = (Convert.ToInt32(xx), Convert.ToInt32(yy));
            
            _angle = newAngle;

            //int calc = ((int)_currenDirection + ((direction=="R"?1:-1) * (units/90))) % 4;
            //int current = Math.Abs(calc>=0?calc:4+calc);
            //_currenDirection = (enumDir)current;
        }

        static void turn(string direction, int units)
        {

            // if (direction == "R")
            // {
            //     if (units == 90)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.S;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.W;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.E;
            //     }
            //     if (units == 180)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.W;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.E;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.S;
            //     }
            //     if (units == 270)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.S;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.E;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.W;
            //     }
            // }

            // if (direction == "L")
            // {
            //     if (units == 90)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.E;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.S;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.W;
            //     }
            //     if (units == 180)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.W;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.E;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.S;
            //     }
            //     if (units == 270)
            //     {
            //         if (_currenDirection == enumDir.E)
            //             _currenDirection = enumDir.S;
            //         else if (_currenDirection == enumDir.W)
            //             _currenDirection = enumDir.N;
            //         else if (_currenDirection == enumDir.S)
            //             _currenDirection = enumDir.W;
            //         else if (_currenDirection == enumDir.N)
            //             _currenDirection = enumDir.E;
            //     }
            // }

            int calc = ((int)_currenDirection + ((direction=="R"?1:-1) * (units/90))) % 4;
            int current = Math.Abs(calc>=0?calc:4+calc);
            _currenDirection = (enumDir)current;
        }
        static void Main(string[] args)
        {
            string[] collection = File.ReadAllLines("content.txt");
            //Part1(collection);
            Part2(collection);
        }

        public static void Part1(string[] collection) {
            foreach (var item in collection)
            {
                string action = item.Substring(0, 1);
                int units = int.Parse(item.Substring(1));

                switch (action)
                {
                    case "F":
                        move(units);
                        _points.Add(_currentLocation);
                        break;
                    case "L":
                    case "R":
                        turn(action, units);
                        break;
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        shift((enumDir)Enum.Parse(typeof(enumDir), action), units);
                        _points.Add(_currentLocation);
                        break;
                }
                //drawBoard();
            }
            
            Console.WriteLine($"Part 1 - {_currentLocation.Item1} {_currentLocation.Item2} = {Math.Abs(_currentLocation.Item1) + Math.Abs(_currentLocation.Item2)}");
        }

        public static void Part2(string[] collection) {
            //Reset
            _points.Clear();
            _points2.Clear();
            _currentLocation = (0,0);
            _currentWayPoint = (10, 1);
            _hypo = Math.Sqrt( 
                Math.Pow(_currentWayPoint.Item1, 2) + 
                Math.Pow(_currentWayPoint.Item2, 2) );
            _angle = 90-DegreeAngle(Math.Asin((double)(_currentWayPoint.Item2) / (double)(_currentWayPoint.Item1)));

            foreach (var item in collection)
            {
                string action = item.Substring(0, 1);
                int units = int.Parse(item.Substring(1));

                switch (action)
                {
                    case "F":
                        moveWayPoint(units);
                        _points.Add(_currentLocation);
                        break;
                    case "L":
                    case "R":
                        turnWayPoint(action, units);
                        break;
                    case "N":
                    case "S":
                    case "E":
                    case "W":
                        shiftWayPoint((enumDir)Enum.Parse(typeof(enumDir), action), units);
                        _points.Add(_currentLocation);
                        break;
                }
                //Console.WriteLine($"{item} L {_currentLocation.Item1}:{_currentLocation.Item2} W {_currentWayPoint.Item1}:{_currentWayPoint.Item2}");
                //drawBoard();
            }
            
            Console.WriteLine($"Part 2 - {_currentLocation.Item1} {_currentLocation.Item2} = {Math.Abs(_currentLocation.Item1) + Math.Abs(_currentLocation.Item2)}");
        }
    }
}

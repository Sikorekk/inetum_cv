using System;
using System.Drawing;
using System.Windows;
using System.Threading;

namespace TabliceCwiczenia
{
    class Program
    {
        static int EndNumber = 0;
        static void End()
        {
            Console.Clear();
            EndNumber = 1;
        }
        static void ChangeIndex(Point[] arr)
        {

            for (int i = arr.Length - 1; i > 0; i--)
            {
                arr[i].X = arr[i - 1].X;
                arr[i].Y = arr[i - 1].Y;
            }
        }
        static void Move(ref Point[] arr, Point[] arr2, int Vx, int Vy)
        {
            do
            {
                if (arr[0].X <= 0 || arr[0].Y <= 0 || EndNumber == 1)
                {
                    End();
                }
                else
                {
                    ChangeIndex(arr);
                    arr[0].X += Vx;
                    arr[0].Y += Vy;
                    Eat(ref arr, arr2);
                    Show('*', arr);
                    Delete(arr);
                    CheckHit(arr);
                    Thread.Sleep(300);
                }
            } while (Console.KeyAvailable == false);
        }
        static void Show(char c, Point[] arr)
        {
            Console.SetCursorPosition((int)arr[0].X, (int)arr[0].Y);
            Console.Write(c);
        }
        static void Delete(Point[] arr)
        {
            Console.SetCursorPosition((int)arr[arr.Length - 1].X, (int)arr[arr.Length - 1].Y);
            Console.Write(' ');
            Console.SetCursorPosition((int)arr[0].X, (int)arr[0].Y);
        }
        static void Set(int x, int y, Point[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i].X = x;
                arr[i].Y = y;
            }
        }
        static void EatWriter(ref Point[] arr, Point[] arr2)
        {

            for (int i = 0; i < arr.Length - 1; i++)
            {
                CheckFood(ref arr, arr2, i);
            }
            Show('#', arr2);
        }
        static void Eat(ref Point[] arr, Point[] arr2)
        {
            if (arr[0].X == arr2[0].X && arr[0].Y == arr2[0].Y)
            {
                Array.Resize(ref arr, arr.Length + 1);
                EatWriter(ref arr, arr2);
            }
        }
        static void CheckFood(ref Point[] arr, Point[] arr2, int i)
        {
            Random rand;
            do
            {
                rand = new Random();
                Set(rand.Next(1, 5), rand.Next(1, 5), arr2);
            } while (arr2[0].X == arr[i].X && arr2[0].Y == arr[i].Y);
        }
        static void CheckHit(Point[] arr)
        {
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[0].X == arr[i].X && arr[0].Y == arr[i].Y)
                {
                    End();
                }
            }
        }
        static void Main(string[] args)
        {
            ConsoleKeyInfo key;
            Point[] snake = new Point[5];
            Point[] food = new Point[1];
            Set(10, 10, snake);
            EatWriter(ref snake, food);
            do
            {
                key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow: Move(ref snake, food, 0, -1); break;
                    case ConsoleKey.DownArrow: Move(ref snake, food, 0, 1); break;
                    case ConsoleKey.LeftArrow: Move(ref snake, food, -1, 0); break;
                    case ConsoleKey.RightArrow: Move(ref snake, food, 1, 0); break;
                }
            } while (key.Key != ConsoleKey.Escape && EndNumber != 1);
        Console.ReadLine();
        }
    }
}

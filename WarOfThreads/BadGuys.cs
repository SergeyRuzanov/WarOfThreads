using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WarOfThreads
{
    class BadGuys
    {
        static public void CreateBadguys()
        {
            Random rnd = new Random();
            Program.startgame.WaitOne(15000, false);
            while (true)
            {
                if (rnd.Next(0, 101) < (Program.hit + Program.miss) / 25 + 20)
                    new Thread(new BadGuys(rnd.Next(0, 11)).MoveBadgays).Start();
                Thread.Sleep(1000);
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        private readonly int dir;
        private static char[] badchar = { '-', '\\', '|', '/' };
        public BadGuys(int y)
        {
            Y = y;
            X = y % 2 == 0 ? 0 : Console.BufferWidth - 1;
            dir = X == 0 ? 1 : -1;
        }
        public void MoveBadgays()
        {
            while ((dir == 1 && X != Console.BufferWidth) || (dir == -1 && X != 0))
            {
                bool hitme = false;
                AddingMethods.WriteTo(X, Y, badchar[X % 4]);
                for (int i = 0; i < 15; i++)
                {
                    Thread.Sleep(40);
                    if (AddingMethods.ReadTo(X, Y) == '*')
                    {
                        hitme = true;
                        break;
                    }
                }
                AddingMethods.WriteTo(X, Y, ' ');
                if (hitme)
                {
                    Console.Beep();
                    Interlocked.Increment(ref Program.hit);
                    AddingMethods.score();
                    Thread.CurrentThread.Abort();
                }
                X += dir;
            }
            Interlocked.Increment(ref Program.miss);
            AddingMethods.score();
        }
    }
}

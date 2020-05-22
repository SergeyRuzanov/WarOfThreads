using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace WarOfThreads
{
    class Bullet
    {
        static public void CreateBullet(object o)
        {
            int x = (int)o;
            if (AddingMethods.ReadTo(x, Console.BufferHeight-2) == '*') return;
            if (!Program.bulletsem.WaitOne(0)) return;
            new Thread(new Bullet(x).MoveBullet).Start();
        }
        public int X { get; set; }
        public int Y { get; set; }
        public Bullet(int x)
        {
            X = x;
            Y = Console.BufferHeight - 2;
        }
        public void MoveBullet()
        {
            for (; Y >= 0; Y--)
            {
                AddingMethods.WriteTo(X, Y, '*');
                Thread.Sleep(100);
                AddingMethods.WriteTo(X, Y, ' ');
            }
            Program.bulletsem.Release();
        }
    }
}

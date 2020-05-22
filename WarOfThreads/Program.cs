using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WarOfThreads
{
    class Program
    {
        public static Mutex screenlock = new Mutex();
        public static AutoResetEvent startgame = new AutoResetEvent(false);
        public static Semaphore bulletsem = new Semaphore(3, 3);
        public static Thread mainThread = Thread.CurrentThread;
        public static int miss = 0;
        public static int hit = 0;
        static void Main(string[] args)
        {
            Console.SetWindowSize(80, 25);
            Console.SetBufferSize(80, 25);
            Console.CursorVisible = false;
            AddingMethods.score();
            Gun gun = new Gun();
            new Thread(BadGuys.CreateBadguys).Start();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(false);
                switch (key.Key)
                {
                    case ConsoleKey.Spacebar:
                        new Thread(Bullet.CreateBullet).Start(gun.X);
                        Thread.Sleep(100);
                        break;
                    case ConsoleKey.LeftArrow:
                        startgame.Set();
                        gun.MoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        startgame.Set();
                        gun.MoveRight();
                        break;
                }
            }
        }
    }
}

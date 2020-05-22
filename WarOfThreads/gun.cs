using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarOfThreads
{
    class Gun
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Gun()
        {
            X = Console.BufferWidth / 2;
            Y = Console.BufferHeight - 1;
            AddingMethods.WriteTo(X, Y, '|');
        }

        public void MoveLeft()
        {
            AddingMethods.WriteTo(X, Y, ' ');
            X--;
            if (X == -1)
                X = Console.BufferWidth - 1;
            AddingMethods.WriteTo(X, Y, '|');
        }
        public void MoveRight()
        {
            AddingMethods.WriteTo(X, Y, ' ');
            X++;
            if (X > Console.BufferWidth - 1)
                X = 0;
            AddingMethods.WriteTo(X, Y, '|');
        }
    }
}

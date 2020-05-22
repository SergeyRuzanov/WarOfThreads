using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace WarOfThreads
{
    static class AddingMethods
    {
        [DllImport("Kernel32", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("Kernel32", SetLastError = true)]
        static extern bool ReadConsoleOutputCharacter(IntPtr hConsoleOutput, [Out] char[] lpCharacter, uint nLength, COORD dwReadCoord, out int lpNumberOfCharsRead);

        [StructLayout(LayoutKind.Sequential)]
        private struct COORD
        {
            public short X;
            public short Y;
        }
        public static void WriteTo(int x, int y, char ch)
        {
            Program.screenlock.WaitOne();
            Console.SetCursorPosition(x, y);
            Console.Write(ch);
            Program.screenlock.ReleaseMutex();
        }
        public static char ReadTo(int x, int y)
        {
            char[] ch = new char[1];
            int readCount;
            Program.screenlock.WaitOne();
            ReadConsoleOutputCharacter(GetStdHandle(-11), ch, 1, new COORD() { X = (short)x, Y = (short)y }, out readCount);
            Program.screenlock.ReleaseMutex();
            return ch[0];
        }

        static object gameover = new object();
        public static void score()
        {
            Console.Title = $"Война потоков - Попаданий: {Program.hit}, Промахов: {Program.miss}";
            if (Program.miss >= 4)
            {
                lock (gameover)
                {
                    Program.mainThread.Suspend();
                    MessageBox.Show($"Игра окончена!\nВаш счет: {Program.hit} попаданий.", "Thread War", MessageBoxButtons.OK);
                    Environment.Exit(0);          
                }
            }
        }
    }
}

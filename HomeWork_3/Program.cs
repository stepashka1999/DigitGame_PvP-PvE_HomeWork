using System;

namespace HomeWork_3
{   
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();

            PrintOnTheCenter("THE GAME\n");

            game.SetGameSettings();

            Console.Clear();

            PrintOnTheCenter("GAME IN PROGRESS\n");

            game.Start();
        }

        /// <summary>
        /// Выводит текст по центру окна консоли с учетом размера строки
        /// </summary>
        /// <param name="text"></param>
        public static void PrintOnTheCenter(string text)
        {
            Console.CursorLeft = Console.WindowWidth / 2 - text.Length / 2;
            Console.WriteLine(text);
        }
    };
};

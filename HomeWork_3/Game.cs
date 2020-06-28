using System;

namespace HomeWork_3
{
    /// <summary>
    /// Класс игры
    /// </summary>
    class Game
    {
        Random rnd = new Random();// рандомайзер

        int userTry;             // переменная для зранения хода пользователя

        int playersNumber;       // кол-во игроков
        int gameNumber;          // рандомное число для игры
        int startNumber = 12;    // минимальное значение для рандома
        int endNumber = 120;     // максимальное значение для рандома 
        string[] playersNames;   // массив игркоов
        int choice;              // индекс режима игры (0 - обычная | 1 - настраиваемая)

        /// <summary>
        /// Задает параметры игры
        /// </summary>
        public void SetGameSettings()
        {
            Console.Write("Введите количество игроков: ");

            var inputValue = int.Parse(Console.ReadLine());

            while(inputValue < 1)
            {
                Console.Clear();

                Console.Write("\nКоличество игроков не может быть отрицательным или равным нулю.\n" +
                                  "Введите количество игроков: ");

                inputValue = int.Parse(Console.ReadLine());
            }

            playersNumber = inputValue;

            SetPlayers(playersNumber);

            SetMode(out choice);

            if(choice == 1)
            {
                SetRandomValues(ref startNumber, ref endNumber);
            }

            gameNumber = rnd.Next(startNumber, endNumber);
        }

        /// <summary>
        /// Запуск игры
        /// </summary>
        public void Start()
        {
            if(playersNumber == 1)
            {
                StartSolo();
            }
            else
            {
                StartCoop();
            }
        }

        /// <summary>
        /// Режим игры с другими пользователями
        /// </summary>
        private void StartCoop()
        {
            int playerIndex = 0;

            while (true)
            {
                if (playerIndex >= playersNumber) playerIndex = 0;

                Console.WriteLine("Число: " + gameNumber);
                Console.Write($"Ход {playersNames[playerIndex]}: ");

                userTry = int.Parse(Console.ReadLine());
                
                if(userTry > 4 || userTry < 1)
                {
                    Console.WriteLine("Отнимать можно толкьо число, значение котрого от 1 до 4 включительно.\n" +
                                      "За попытку сжульничать вы пропускаете ход!");
                }
                else gameNumber -= userTry;

                Console.WriteLine();

                if (gameNumber <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Игрок {playersNames[playerIndex]} победил!!!");
                    break;
                }

                playerIndex++;
            }
        }

        /// <summary>
        /// Режим игры с ботом
        /// </summary>
        private void StartSolo()
        {           
            while (true)
            {
                
                Console.WriteLine("Число: " + gameNumber);
                Console.Write($"Ход {playersNames[0]}: ");

                userTry = int.Parse(Console.ReadLine());

                Console.WriteLine();
                if (userTry > 4 || userTry < 1)
                {
                    Console.WriteLine("Отнимать можно толкьо число, значение котрого от 1 до 4 включительно.\n" +
                                      "За попытку сжульничать вы пропускаете ход!\n");
                }
                else gameNumber -= userTry;
                
                if (gameNumber <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Игрок {playersNames[0]} победил!!!");
                    break;
                }


                Console.WriteLine("Число: " + gameNumber);
                Console.Write($"Ход BOT: ");

                var botTry = BotPlay(ref gameNumber);

                Console.WriteLine(botTry);
                Console.WriteLine();

                gameNumber -= botTry;

                if (gameNumber <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"BOT победил!!!");
                    break;
                }
            }
        }

        /// <summary>
        /// Ход бота
        /// </summary>
        /// <param name="gameNumber">Игровое число</param>
        /// <returns>Возвращает целое число - ход бота</returns>
        private int BotPlay(ref int gameNumber)
        {
            for(int i = 4; i > 0; i--)
            {
                if (gameNumber > 4)
                {
                    if (gameNumber - i > 4)
                    {
                        return i;
                    }
                }
            }

            return gameNumber;
        }

        /// <summary>
        /// Заполнение массива с игроками
        /// </summary>
        /// <param name="numberOfPlayers">количество игроков</param>
        private void SetPlayers( int numberOfPlayers)
        {
            playersNames = new string[numberOfPlayers];

            for (int i = 0; i < numberOfPlayers; i++)
            {
                Console.Clear();
                Console.WriteLine("Введите никнейм(-ы) игроков ");

                Console.Write(i + 1 + "й игрок: ");
                playersNames[i] = Console.ReadLine();
            }
        }

        /// <summary>
        /// Выбирает режим игры
        /// </summary>
        /// <param name="choice">Параметр выбора игры</param>
        private void SetMode(out int choice)
        {
            choice = 0;
            ConsoleKey keyDown = ConsoleKey.Zoom;
            string[] paramsGame = { "Обычная", "Настраиваемая" };

            //Выбо режима игры
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите режим игры");

                for (int i = 0; i < paramsGame.Length; i++)
                {
                    if (choice == i)
                        Console.WriteLine("> " + paramsGame[i]);
                    else
                        Console.WriteLine(paramsGame[i]);
                }

                keyDown = Console.ReadKey().Key;

                if (keyDown == ConsoleKey.DownArrow)
                {
                    if (choice == paramsGame.Length - 1) choice = 0;
                    else choice++;
                }

                if (keyDown == ConsoleKey.UpArrow)
                {
                    if (choice == 0) choice = paramsGame.Length - 1;
                    else choice--;
                }

                if (keyDown == ConsoleKey.Enter) break;

            };
        }

        /// <summary>
        /// Задает диапазон рандомных числе
        /// </summary>
        /// <param name="start">Минимальное значение</param>
        /// <param name="end">Максимальное значение</param>
        private void SetRandomValues(ref int start, ref int end)
        {
            Console.Write("Введите минимальное значение случайного числа: ");
            var inputValue = int.Parse(Console.ReadLine());
            
            while(inputValue < 0)
            {
                Console.Clear();

                Console.WriteLine("Минимальное значение не может быть отрицательным.");
                Console.Write("Введите минимальное значение случайного числа: ");
                inputValue = int.Parse(Console.ReadLine());
            }

            startNumber = inputValue; 

            Console.Write("Введите максимальное значение случайного числа: ");
            inputValue = int.Parse(Console.ReadLine());

            while (inputValue < startNumber)
            {
                Console.Clear();

                Console.WriteLine("Максимальное значение не может быть отрицательным и быть меньше минимального");
                Console.Write("Введите максинимальное значение случайного числа: ");
                inputValue = int.Parse(Console.ReadLine());
            }

            endNumber = inputValue;
        }

    }
}

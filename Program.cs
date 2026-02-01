using System;
using System.Threading;
using Tetris.Core;
using Tetris.UI;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            const int boardWidth = 10;
            const int boardHeight = 20;

            GameEngine engine = new GameEngine(boardWidth, boardHeight);
            ConsoleRenderer renderer = new ConsoleRenderer();

            renderer.Initialize();
            engine.Start();

            DateTime lastMoveTime = DateTime.Now;

            while (!engine.IsGameOver)
            {
                renderer.DrawGame(engine);

                // Автоматическое падение
                if ((DateTime.Now - lastMoveTime).TotalMilliseconds >= engine.GetSpeed())
                {
                    engine.MoveDown();
                    lastMoveTime = DateTime.Now;
                }

                // Обработка ввода
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    switch (key)
                    {
                        case ConsoleKey.LeftArrow:
                            engine.MoveLeft();
                            break;
                        case ConsoleKey.RightArrow:
                            engine.MoveRight();
                            break;
                        case ConsoleKey.UpArrow:
                            engine.Rotate();
                            break;
                        case ConsoleKey.DownArrow:
                            engine.MoveDown();
                            lastMoveTime = DateTime.Now;
                            break;
                        case ConsoleKey.Spacebar:
                            engine.Drop();
                            lastMoveTime = DateTime.Now;
                            break;
                        case ConsoleKey.Escape:
                            return;
                    }
                }

                Thread.Sleep(10);
            }

            // Финальная отрисовка
            renderer.DrawGame(engine);
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(2, 32);
            Console.WriteLine($"Игра окончена! Ваш счёт: {engine.Score}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}

using System;
using Tetris.Core;
using Tetris.Models;

namespace Tetris.UI
{
    // Класс для отрисовки игры в консоли (инкапсуляция логики отображения)
    public class ConsoleRenderer
    {
        private readonly int offsetX;
        private readonly int offsetY;

        public ConsoleRenderer(int offsetX = 2, int offsetY = 2)
        {
            this.offsetX = offsetX;
            this.offsetY = offsetY;
        }

        public void Initialize()
        {
            Console.CursorVisible = false;
            
            // Устанавливаем размер окна консоли
            try
            {
                Console.SetWindowSize(Math.Min(80, Console.LargestWindowWidth), 
                                     Math.Min(35, Console.LargestWindowHeight));
                Console.SetBufferSize(80, 35);
            }
            catch
            {
                // Если не удалось установить размер, продолжаем с текущим
            }
            
            Console.Clear();
        }

        public void DrawGame(GameEngine engine)
        {
            Console.SetCursorPosition(0, 0);
            
            DrawBoard(engine.Board, engine.CurrentShape);
            DrawInfo(engine);
            DrawControls();
        }

        private void DrawBoard(GameBoard board, Shape? currentShape)
        {
            // Верхняя граница
            Console.SetCursorPosition(offsetX, offsetY);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╔" + new string('═', board.Width * 2) + "╗");

            // Игровое поле
            for (int i = 0; i < board.Height; i++)
            {
                Console.SetCursorPosition(offsetX, offsetY + i + 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("║");

                for (int j = 0; j < board.Width; j++)
                {
                    bool isCurrentShape = false;
                    ConsoleColor color = ConsoleColor.Black;

                    // Проверяем текущую фигуру
                    if (currentShape != null)
                    {
                        int[,] matrix = currentShape.Matrix;
                        int relY = i - currentShape.Position.Y;
                        int relX = j - currentShape.Position.X;

                        if (relY >= 0 && relY < matrix.GetLength(0) &&
                            relX >= 0 && relX < matrix.GetLength(1) &&
                            matrix[relY, relX] == 1)
                        {
                            isCurrentShape = true;
                            color = currentShape.Color;
                        }
                    }

                    // Проверяем зафиксированные блоки
                    if (!isCurrentShape && board.GetCell(i, j) == 1)
                    {
                        color = board.GetColor(i, j);
                    }

                    if (isCurrentShape || board.GetCell(i, j) == 1)
                    {
                        Console.ForegroundColor = color;
                        Console.Write("██");
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("║");
            }

            // Нижняя граница
            Console.SetCursorPosition(offsetX, offsetY + board.Height + 1);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("╚" + new string('═', board.Width * 2) + "╝");
        }

        private void DrawInfo(GameEngine engine)
        {
            int infoX = offsetX + engine.Board.Width * 2 + 5;
            int infoY = offsetY;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(infoX, infoY);
            Console.Write("╔════════════════╗");
            
            Console.SetCursorPosition(infoX, infoY + 1);
            Console.Write("║   ТЕТРИС      ║");
            
            Console.SetCursorPosition(infoX, infoY + 2);
            Console.Write("╠════════════════╣");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(infoX, infoY + 3);
            Console.Write($"║ Счёт: {engine.Score,-8}║");

            Console.SetCursorPosition(infoX, infoY + 4);
            Console.Write($"║ Уровень: {engine.Level,-5}║");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(infoX, infoY + 5);
            Console.Write("╚════════════════╝");

            if (engine.IsGameOver)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(infoX, infoY + 7);
                Console.Write("  ИГРА ОКОНЧЕНА  ");
            }
        }

        private void DrawControls()
        {
            int controlsY = offsetY + 24;
            
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(offsetX, controlsY);
            Console.Write("Управление:");
            Console.SetCursorPosition(offsetX, controlsY + 1);
            Console.Write("← → - Движение");
            Console.SetCursorPosition(offsetX, controlsY + 2);
            Console.Write("↑   - Поворот");
            Console.SetCursorPosition(offsetX, controlsY + 3);
            Console.Write("↓   - Ускорение");
            Console.SetCursorPosition(offsetX, controlsY + 4);
            Console.Write("Пробел - Сброс");
            Console.SetCursorPosition(offsetX, controlsY + 5);
            Console.Write("ESC - Выход");
        }

        public void Clear()
        {
            Console.Clear();
        }
    }
}

using System;
using Tetris.Models;

namespace Tetris.Core
{
    // Класс игрового поля (инкапсуляция данных игры)
    public class GameBoard
    {
        private readonly int width;
        private readonly int height;
        private readonly int[,] board;
        private readonly ConsoleColor[,] colors;

        public int Width => width;
        public int Height => height;

        public GameBoard(int width, int height)
        {
            this.width = width;
            this.height = height;
            board = new int[height, width];
            colors = new ConsoleColor[height, width];
        }

        public bool IsValidPosition(Shape shape)
        {
            int[,] matrix = shape.Matrix;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        int boardX = shape.Position.X + j;
                        int boardY = shape.Position.Y + i;

                        if (boardX < 0 || boardX >= width || boardY >= height)
                            return false;

                        if (boardY >= 0 && board[boardY, boardX] == 1)
                            return false;
                    }
                }
            }

            return true;
        }

        public void PlaceShape(Shape shape)
        {
            int[,] matrix = shape.Matrix;
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] == 1)
                    {
                        int boardX = shape.Position.X + j;
                        int boardY = shape.Position.Y + i;

                        if (boardY >= 0 && boardY < height && boardX >= 0 && boardX < width)
                        {
                            board[boardY, boardX] = 1;
                            colors[boardY, boardX] = shape.Color;
                        }
                    }
                }
            }
        }

        public int ClearFullLines()
        {
            int linesCleared = 0;

            for (int i = height - 1; i >= 0; i--)
            {
                bool isFull = true;
                for (int j = 0; j < width; j++)
                {
                    if (board[i, j] == 0)
                    {
                        isFull = false;
                        break;
                    }
                }

                if (isFull)
                {
                    linesCleared++;
                    RemoveLine(i);
                    i++; // Проверяем эту же линию снова
                }
            }

            return linesCleared;
        }

        private void RemoveLine(int line)
        {
            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < width; j++)
                {
                    board[i, j] = board[i - 1, j];
                    colors[i, j] = colors[i - 1, j];
                }
            }

            for (int j = 0; j < width; j++)
            {
                board[0, j] = 0;
                colors[0, j] = ConsoleColor.Black;
            }
        }

        public int GetCell(int y, int x) => board[y, x];
        public ConsoleColor GetColor(int y, int x) => colors[y, x];
    }
}

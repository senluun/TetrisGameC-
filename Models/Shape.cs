using System;

namespace Tetris.Models
{
    // Абстрактный базовый класс для всех фигур (демонстрация наследования и полиморфизма)
    public abstract class Shape
    {
        protected Point position;
        protected int[,] matrix;
        protected ConsoleColor color;

        public Point Position => position;
        public int[,] Matrix => matrix;
        public ConsoleColor Color => color;

        protected Shape(int startX, int startY)
        {
            position = new Point(startX, startY);
        }

        // Виртуальный метод для поворота фигуры (полиморфизм)
        public virtual void Rotate()
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int[,] rotated = new int[cols, rows];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    rotated[j, rows - 1 - i] = matrix[i, j];
                }
            }

            matrix = rotated;
        }

        public void MoveDown() => position.Y++;
        public void MoveLeft() => position.X--;
        public void MoveRight() => position.X++;

        public abstract Shape Clone();
    }
}

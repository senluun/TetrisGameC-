using System;

namespace Tetris.Models
{
    // O-образная фигура (квадрат)
    public class OShape : Shape
    {
        public OShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 1, 1 },
                { 1, 1 }
            };
            color = ConsoleColor.Yellow;
        }

        // Переопределение метода - квадрат не вращается (полиморфизм)
        public override void Rotate()
        {
            // Квадрат не меняется при вращении
        }

        public override Shape Clone()
        {
            return new OShape(position.X, position.Y);
        }
    }
}

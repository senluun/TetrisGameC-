using System;

namespace Tetris.Models
{
    // Z-образная фигура
    public class ZShape : Shape
    {
        public ZShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 1, 1, 0 },
                { 0, 1, 1 }
            };
            color = ConsoleColor.Red;
        }

        public override Shape Clone()
        {
            return new ZShape(position.X, position.Y);
        }
    }
}

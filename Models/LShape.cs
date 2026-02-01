using System;

namespace Tetris.Models
{
    // L-образная фигура
    public class LShape : Shape
    {
        public LShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 1, 0 },
                { 1, 0 },
                { 1, 1 }
            };
            color = ConsoleColor.Blue;
        }

        public override Shape Clone()
        {
            return new LShape(position.X, position.Y);
        }
    }
}

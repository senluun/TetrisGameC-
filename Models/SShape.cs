using System;

namespace Tetris.Models
{
    // S-образная фигура
    public class SShape : Shape
    {
        public SShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 0, 1, 1 },
                { 1, 1, 0 }
            };
            color = ConsoleColor.Green;
        }

        public override Shape Clone()
        {
            return new SShape(position.X, position.Y);
        }
    }
}

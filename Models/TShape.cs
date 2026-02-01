using System;

namespace Tetris.Models
{
    // T-образная фигура
    public class TShape : Shape
    {
        public TShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 0, 1, 0 },
                { 1, 1, 1 }
            };
            color = ConsoleColor.Magenta;
        }

        public override Shape Clone()
        {
            return new TShape(position.X, position.Y);
        }
    }
}

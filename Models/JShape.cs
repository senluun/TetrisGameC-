using System;

namespace Tetris.Models
{
    // J-образная фигура
    public class JShape : Shape
    {
        public JShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 0, 1 },
                { 0, 1 },
                { 1, 1 }
            };
            color = ConsoleColor.DarkYellow;
        }

        public override Shape Clone()
        {
            return new JShape(position.X, position.Y);
        }
    }
}

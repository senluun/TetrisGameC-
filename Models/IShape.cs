using System;

namespace Tetris.Models
{
    // I-образная фигура (наследование от Shape)
    public class IShape : Shape
    {
        public IShape(int startX, int startY) : base(startX, startY)
        {
            matrix = new int[,]
            {
                { 1, 1, 1, 1 }
            };
            color = ConsoleColor.Cyan;
        }

        public override Shape Clone()
        {
            return new IShape(position.X, position.Y);
        }
    }
}

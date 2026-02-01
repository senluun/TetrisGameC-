using System;
using Tetris.Models;

namespace Tetris.Core
{
    // Фабрика для создания фигур (паттерн Factory)
    public class ShapeFactory
    {
        private readonly Random random;
        private readonly int startX;

        public ShapeFactory(int boardWidth)
        {
            random = new Random();
            startX = boardWidth / 2 - 2;
        }

        public Shape CreateRandomShape()
        {
            int shapeType = random.Next(7);

            return shapeType switch
            {
                0 => new IShape(startX, 0),
                1 => new OShape(startX, 0),
                2 => new TShape(startX, 0),
                3 => new LShape(startX, 0),
                4 => new JShape(startX, 0),
                5 => new SShape(startX, 0),
                6 => new ZShape(startX, 0),
                _ => new IShape(startX, 0)
            };
        }
    }
}

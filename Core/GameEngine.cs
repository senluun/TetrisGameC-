using System;
using Tetris.Models;

namespace Tetris.Core
{
    // Основной игровой движок (инкапсуляция игровой логики)
    public class GameEngine
    {
        private readonly GameBoard board;
        private readonly ShapeFactory shapeFactory;
        private Shape? currentShape;
        private int score;
        private int level;
        private bool isGameOver;

        public GameBoard Board => board;
        public Shape? CurrentShape => currentShape;
        public int Score => score;
        public int Level => level;
        public bool IsGameOver => isGameOver;

        public GameEngine(int width, int height)
        {
            board = new GameBoard(width, height);
            shapeFactory = new ShapeFactory(width);
            score = 0;
            level = 1;
            isGameOver = false;
        }

        public void Start()
        {
            SpawnNewShape();
        }

        private void SpawnNewShape()
        {
            currentShape = shapeFactory.CreateRandomShape();
            
            if (!board.IsValidPosition(currentShape))
            {
                isGameOver = true;
            }
        }

        public bool MoveDown()
        {
            if (currentShape == null || isGameOver) return false;

            currentShape.MoveDown();

            if (!board.IsValidPosition(currentShape))
            {
                currentShape.Position.Y--;
                board.PlaceShape(currentShape);
                
                int linesCleared = board.ClearFullLines();
                UpdateScore(linesCleared);
                
                SpawnNewShape();
                return false;
            }

            return true;
        }

        public void MoveLeft()
        {
            if (currentShape == null || isGameOver) return;

            currentShape.MoveLeft();
            if (!board.IsValidPosition(currentShape))
            {
                currentShape.MoveRight();
            }
        }

        public void MoveRight()
        {
            if (currentShape == null || isGameOver) return;

            currentShape.MoveRight();
            if (!board.IsValidPosition(currentShape))
            {
                currentShape.MoveLeft();
            }
        }

        public void Rotate()
        {
            if (currentShape == null || isGameOver) return;

            currentShape.Rotate();
            if (!board.IsValidPosition(currentShape))
            {
                // Попытка сдвинуть влево/вправо при вращении у стены
                currentShape.MoveLeft();
                if (!board.IsValidPosition(currentShape))
                {
                    currentShape.MoveRight();
                    currentShape.MoveRight();
                    if (!board.IsValidPosition(currentShape))
                    {
                        currentShape.MoveLeft();
                        // Отменяем вращение
                        for (int i = 0; i < 3; i++)
                            currentShape.Rotate();
                    }
                }
            }
        }

        public void Drop()
        {
            if (currentShape == null || isGameOver) return;

            while (MoveDown()) { }
        }

        private void UpdateScore(int linesCleared)
        {
            int points = linesCleared switch
            {
                1 => 100,
                2 => 300,
                3 => 500,
                4 => 800,
                _ => 0
            };

            score += points * level;
            level = 1 + score / 1000;
        }

        public int GetSpeed()
        {
            return Math.Max(100, 500 - (level - 1) * 50);
        }
    }
}

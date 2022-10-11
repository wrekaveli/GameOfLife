﻿namespace GameOfLife
{
    public class Cell
    {
        public int X { get; }

        public int Y { get; }

        public bool IsAlive { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Cell(Cell cell)
        {
            X = cell.X;
            Y = cell.Y;
            IsAlive = cell.IsAlive;
        }
    }
}

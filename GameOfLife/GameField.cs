namespace GameOfLife
{
    public class GameField
    {
        public int Size { get; set; } = 50;
        public Cell[,] Generation { get; set; }

        private readonly Cell[,] _copy;

        public GameField()
        {
            _copy = new Cell[Size, Size];

            Generation = new Cell[Size, Size];

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    Generation[y, x] = new Cell(x, y);
                }
            }
        }

        public void Randomize()
        {
            var random = new Random();
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    if (random.Next(2) % 2 == 0)
                    {
                        SetCell(x, y);
                    }
                }
            }
        }

        public void SetCell(int x, int y)
        {
            Generation[x, y].IsAlive = !Generation[x, y].IsAlive;
        }

        public void NextGeneration()
        {
            CopyGeneration();

            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    CheckIsAlive(_copy[y, x]);
                }
            }
        }

        private void CopyGeneration()
        {
            for (int x = 0; x < Size; x++)
            {
                for (int y = 0; y < Size; y++)
                {
                    _copy[y, x] = new Cell(Generation[y, x]);
                }
            }
        }

        private void CheckIsAlive(Cell cell)
        {
            int neighbours = 0;
            for (int x = -1; x < 2; x++)
            {
                if (cell.X + x < 0 || cell.X + x >= Size) continue;
                for (int y = -1; y < 2; y++)
                {
                    if (cell.Y + y < 0 || cell.Y + y >= Size) continue;
                    if (y == 0 && x == 0) continue;
                    if (_copy[cell.Y + y, cell.X + x].IsAlive)
                    {
                        neighbours++;
                    }
                }
            }

            Generation[cell.Y, cell.X].IsAlive = cell.IsAlive
                ? (neighbours == 2 || neighbours == 3)
                : neighbours == 3;
        }
    }
}

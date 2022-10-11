using Timer = System.Windows.Forms.Timer;

namespace GameOfLife
{
    public partial class Form1 : Form
    {
        private const int CellSize = 15;
        private const int OffSet = 5;
        private const int TimerInterval = 200;

        private GameField _gameField;
        private PictureBox _pictureBox;
        private bool _isAutoPlay;
        private Timer _timer;

        public Form1()
        {
            InitializeComponent();

            _timer = new Timer();
            _timer.Interval = TimerInterval;
            _timer.Tick += timer_Tick;
            _timer.Start();

            KeyDown += Form1_KeyDown;

            _gameField = new GameField();
            _pictureBox = new PictureBox
            {
                Location = new Point(OffSet, OffSet),
                Size = new Size(_gameField.Size * CellSize, _gameField.Size * CellSize),
                BackColor = Color.Red,
                Cursor = Cursors.Hand
            };

            _pictureBox.MouseDown += pictureBox_MouseDown;

            Controls.Add(_pictureBox);

            MinimumSize = new Size(_gameField.Size * CellSize + OffSet * 2, _gameField.Size * CellSize + OffSet * 2);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            Draw();
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if (_isAutoPlay)
            {
                _gameField.NextGeneration();
                Draw();
            }
        }

        private void Form1_KeyDown(object? sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    _isAutoPlay = !_isAutoPlay;
                    Text = "GameOfLife - Autoplay: " + (_isAutoPlay ? "ON" : "OFF");
                    break;
                case Keys.R:
                    _gameField.Randomize();
                    break;
                case Keys.Back:
                    _gameField = new GameField();
                    break;
            }
        }

        private void pictureBox_MouseDown(object? sender, MouseEventArgs e)
        {
            var x = e.X / CellSize;
            var y = e.Y / CellSize;

            _gameField.SetCell(x, y);

            Draw();
        }

        private void Draw()
        {
            using (var bitmap = new Bitmap(_gameField.Size * CellSize, _gameField.Size * CellSize))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < _gameField.Size; x++)
                {
                    for (int y = 0; y < _gameField.Size; y++)
                    {
                        if (_gameField.Generation[y, x].IsAlive)
                        {
                            graphics.FillRectangle(Brushes.Black, new Rectangle(y * CellSize, x * CellSize, CellSize, CellSize));
                        }
                        else
                        {
                            graphics.DrawRectangle(Pens.Black, new Rectangle(y * CellSize, x * CellSize, CellSize, CellSize));
                        }
                    }
                }

                _pictureBox.Image?.Dispose();
                _pictureBox.Image = (Bitmap)bitmap.Clone();
            }
        }
    }
}
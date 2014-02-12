using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GameOfLifeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool[,] _seed;
        private bool _isRunning = true;
        private bool _isRecording = false;

        public MainWindow()
        {
            InitializeComponent();
            //Board.SetCell(10,10,isAlive:true);

            DoWork();
        }

        private void Record_Click(object sender, RoutedEventArgs e)
        {
            var seed = new BoardArray(30);
            seed[10][10] = true;
            seed[20][20] = true;
            var storage = new Storage();
            storage.Store(seed,"board1.gb");
            var board = storage.Load<Dictionary<int, Dictionary<int, bool>>>("board1.gb");
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Play.Content = _isRunning ? "Stop" : "Play";
            _isRunning = !_isRunning;
        }

        public async Task DoWork()
        {
            await Task.Run(await DoWorkAsync());
        }

        private async Task<Func<Task>> DoWorkAsync()
        {
            return async () =>
                {
                    var random = new Random();

                    while (_isRunning)
                    {
                        SpinWait.SpinUntil(() => false, 100);

                        await Dispatcher.InvokeAsync(MakeRandomCellAlive(random),DispatcherPriority.Normal);
                    }
                };
        }

        private Action MakeRandomCellAlive(Random random)
        {
            return () =>
                {
                    var cell = GetRandomCell(random);
                    Board.SetCell(cell.X, cell.Y, isAlive: true);
                };
        }

        public Point GetRandomCell(Random random)
        {
            var x = random.Next(Board.BoardSize);
            var y = random.Next(Board.BoardSize);
            return new Point(x,y);
        }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

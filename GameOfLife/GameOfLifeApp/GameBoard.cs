using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace GameOfLifeApp
{
    public class GameBoard : Grid, INotifyPropertyChanged
    {
        public Dictionary<int, Dictionary<int, Control>> Cells = new Dictionary<int, Dictionary<int, Control>>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region BoardSize
        public static readonly DependencyProperty BoardSizeProperty = DependencyProperty.Register("BoardSize",
            typeof(int),
            typeof(GameBoard),
            new PropertyMetadata(0, OnBoardSizeChanged));

        public int BoardSize
        {
            get
            {
                return (int)this.GetValue(BoardSizeProperty);
            }
            set
            {
                this.SetValue(BoardSizeProperty, value);
            }
        }

        private static void OnBoardSizeChanged(DependencyObject dependencyObject,
               DependencyPropertyChangedEventArgs e)
        {
            var gameBoard = dependencyObject as GameBoard;
            gameBoard.OnPropertyChanged("BoardSize");
            GenerateBoardCells(gameBoard);
        }
        #endregion

        #region Alive Cell Color
        public static readonly DependencyProperty AliveCellColorProperty = DependencyProperty.Register("AliveCellColor",
            typeof(Brush),
            typeof(GameBoard),
            new PropertyMetadata(new SolidColorBrush(Colors.YellowGreen), OnAliveCellColorChanged));

        public Brush AliveCellColor
        {
            get
            {
                return (Brush)this.GetValue(AliveCellColorProperty);
            }
            set
            {
                this.SetValue(AliveCellColorProperty, value);
            }
        }

        private static void OnAliveCellColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var gameBoard = dependencyObject as GameBoard;
            gameBoard.RepaintBoard();
            gameBoard.OnPropertyChanged("AliveCellColor");
        }
        #endregion

        #region Dead Cell Color
        public static readonly DependencyProperty DeadCellColorProperty = DependencyProperty.Register("DeadCellColor",
            typeof(Brush),
            typeof(GameBoard),
            new PropertyMetadata(new SolidColorBrush(Colors.Black), OnDeadCellColorChanged));

        public Brush DeadCellColor
        {
            get
            {
                return (Brush)this.GetValue(DeadCellColorProperty);
            }
            set
            {
                this.SetValue(DeadCellColorProperty, value);
            }
        }

        private static void OnDeadCellColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var gameBoard = dependencyObject as GameBoard;
            gameBoard.RepaintBoard();
            gameBoard.OnPropertyChanged("DeadCellColor");
        }
        #endregion

        #region Cell Border Color
        public static readonly DependencyProperty CellBorderColorProperty = DependencyProperty.Register("CellBorderColor",
            typeof(Brush),
            typeof(GameBoard),
            new PropertyMetadata(new SolidColorBrush(Colors.Gray), OnCellBorderColorChanged));

        public Brush CellBorderColor
        {
            get
            {
                return (Brush)this.GetValue(CellBorderColorProperty);
            }
            set
            {
                this.SetValue(CellBorderColorProperty, value);
            }
        }

        private static void OnCellBorderColorChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var gameBoard = dependencyObject as GameBoard;
            gameBoard.RepaintBoard();
            gameBoard.OnPropertyChanged("CellBorderColor");
        }
        #endregion

        private static void CreateGridColumns(GameBoard gameBoard, int numberOfColumns)
        {
            for (int i = 0; i < gameBoard.BoardSize; i++)
            {
                var colDefinition = new ColumnDefinition();
                gameBoard.ColumnDefinitions.Add(colDefinition);
                colDefinition.SharedSizeGroup = "A";
                gameBoard.Cells.Add(i, new Dictionary<int, Control>());
            }
        }

        private static void CreateGridRows(GameBoard gameBoard, int numberOfRows)
        {
            for (int i = 0; i < gameBoard.BoardSize; i++)
            {
                var rowDefinition = new RowDefinition();
                gameBoard.RowDefinitions.Add(rowDefinition);
                rowDefinition.SharedSizeGroup = "A";
            }
        }

        public static void GenerateBoardCells(GameBoard gameBoard)
        {
            CreateGridColumns(gameBoard, gameBoard.BoardSize);
            CreateGridRows(gameBoard, gameBoard.BoardSize);
          

            for (int i = 0; i < gameBoard.BoardSize; i++)
            {
                for (int j = 0; j < gameBoard.BoardSize; j++)
                {
                    var cell = CreateCell(gameBoard, row: i, column: j);
                    gameBoard.Children.Add(cell);

                    gameBoard.Cells[i][j] = cell;
                }
            }
        }

        private static Button CreateCell(GameBoard gameBoard, int row, int column)
        {
            var button = new Button();
            button.Tag = "dead";
            button.Background = gameBoard.DeadCellColor;
            button.BorderBrush = gameBoard.CellBorderColor;
            button.BorderThickness = new Thickness(1);
            Grid.SetColumn(button, row);
            Grid.SetRow(button, column);
            button.Click += Cell_Click;
            return button;
        }

        private static void Cell_Click(object sender, RoutedEventArgs e)
        {
            var cell = sender as Button;

            int row = (int)cell.GetValue(Grid.RowProperty);
            int column = (int)cell.GetValue(Grid.ColumnProperty);
            MessageBox.Show(string.Format("Button clicked at column {0}, row {1}", column, row));
        }

        public void RepaintBoard()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[i].Count; j++)
                {
                    SetCell(i, j, Cells[i][j].Tag == "alive");
                }
            }
        }

        public void SetCell(int row, int column, bool isAlive)
        {
            if (Cells.Count < row)
            {
                throw new ArgumentOutOfRangeException("invalid row number");
            }

            if (Cells[row].Count < column)
            {
                throw new ArgumentOutOfRangeException("invalid column number");
            }

            var cell = Cells[row][column];
            cell.Tag = isAlive ? "alive" : "dead";
            cell.Background = isAlive ? AliveCellColor : DeadCellColor;

        }

        public void ClearBoard()
        {
            for (int i = 0; i < Cells.Count; i++)
            {
                for (int j = 0; j < Cells[i].Count; j++)
                {
                    SetCell(i, j, isAlive: false);
                    Cells[i][j].Tag = "dead";
                }
            }
        }
    }
}

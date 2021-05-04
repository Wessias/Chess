using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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

namespace Chess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ObservableCollection<ChessPiece> Pieces { get; set; }
        
        public MainWindow()
        {
            Pieces = new ObservableCollection<ChessPiece>();
            InitializeComponent();
            DataContext = Pieces;
            CreateBoard();
            NewGame();
            Pieces.Add(new Pawn() { Row = 2, Column = 5, IsBlack = false });
            //Pieces.Add(new ChessPiece() { Row = 4, Column = 4, Type = ChessPieceTypes.Tower, IsBlack = true });
            //Pieces.Where((x => x.Row == 2)); TESTIGN
        }



        private void OnClick(object sender, EventArgs e)
        {
            Pieces.Add(new Pawn() { Row = 3, Column = 4, IsBlack = false });
        }

        private void NewGame()
        {
            Pieces.Clear();
            Pieces.Add(new Rook() { Row = 0, Column = 0, IsBlack = true });
            Pieces.Add(new Knight() { Row = 0, Column = 1, IsBlack = true });
            Pieces.Add(new Bishop() { Row = 0, Column = 2, IsBlack = true });
            Pieces.Add(new Queen() { Row = 0, Column = 3, IsBlack = true });
            Pieces.Add(new King() { Row = 0, Column = 4, IsBlack = true });
            Pieces.Add(new Bishop() { Row = 0, Column = 5, IsBlack = true });
            Pieces.Add(new Knight() { Row = 0, Column = 6, IsBlack = true });
            Pieces.Add(new Rook() { Row = 0, Column = 7, IsBlack = true });

            for (var i = 0; i < 8; i++)
            {
                Pieces.Add(new Pawn() { Row = 1, Column = i, IsBlack = true });
            }


            Pieces.Add(new Rook() { Row = 7, Column = 0, IsBlack = false });
            Pieces.Add(new Knight() { Row = 7, Column = 1, IsBlack = false });
            Pieces.Add(new Bishop() { Row = 7, Column = 2, IsBlack = false });
            Pieces.Add(new Queen() { Row = 7, Column = 3, IsBlack = false });
            Pieces.Add(new King() { Row = 7, Column = 4, IsBlack = false });
            Pieces.Add(new Bishop() { Row = 7, Column = 5, IsBlack = false });
            Pieces.Add(new Knight() { Row = 7, Column = 6, IsBlack = false });
            Pieces.Add(new Rook() { Row = 7, Column = 7, IsBlack = false });

            for (var i = 0; i < 8; i++)
            {
                Pieces.Add(new Pawn() { Row = 6, Column = i, IsBlack = false });
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                var row = Grid.GetRow(sender as Button);
                var col = Grid.GetColumn(sender as Button);
            }

        }

        //private void CreateBoard()
        // {
        //   for (var row = 0; row < 8; row++)
        //    {
        //   var isBlack = row % 2 == 1;
        //  for (int col = 0; col < 8; col++)
        //  {
        //    var button = new Button();
        //    button.Click += OnClick;
        //    var square = new Rectangle { Fill = isBlack ? Brushes.Black : Brushes.White };
        //    button.Content = square;
        //    SquareGrid.Children.Add(button);
        //     isBlack = !isBlack;

        //}
        //}

        //}

        //https://stackoverflow.com/questions/6808739/how-to-convert-color-code-into-media-brush
        private void CreateBoard()
        {
            var converter = new BrushConverter();
            var blackBrush = (Brush)converter.ConvertFromString("#b48762");
            var whiteBrush = (Brush)converter.ConvertFromString("#f0d8b5");
            for (var row = 0; row < 8; row++)
            {
                var isBlack = row % 2 == 1;
                for (int col = 0; col < 8; col++)
                {
                    Button button = new Button { BorderThickness = new Thickness(0, 0, 0, 0), Background = isBlack ? blackBrush : whiteBrush };
                    button.Click += Button_Click;
                    ButtonGrid.Children.Add(button);
                    isBlack = !isBlack;
                }
            }

        }



        public int FindPiece(int row, int col)
        {
            for (int i = 0; i < Pieces.Count; i++)
            {
                if (row == Pieces.ElementAt(i).Row && col == Pieces.ElementAt(i).Column)
                {
                    return i;
                }
            }
            return -1;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Image)
            {
                var row = Grid.GetRow(sender as Image);
                var col = Grid.GetColumn(sender as Image);
            }
        }
    }
}

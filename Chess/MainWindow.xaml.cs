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
        private int _row1;
        private int _row2;
        private int _col1;
        private int _col2;
        private bool pieceClicked = false;
        

        
        public MainWindow()
        {
            Pieces = new ObservableCollection<ChessPiece>();
            InitializeComponent();
            DataContext = Pieces;
            CreateBoard();
            NewGame();
            
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
                    button.Name += "A_" + row.ToString() + "_" + col.ToString();
                    ButtonGrid.Children.Add(button);
                    isBlack = !isBlack;
                }
            }

        }



        private ChessPiece FindPiece(int row, int col)
        {
            for (int i = 0; i < Pieces.Count; i++)
            {
                if (row == Pieces.ElementAt(i).Row && col == Pieces.ElementAt(i).Column)
                {
                    return Pieces.ElementAt(i);
                }
            }
            return null;
        }


        //https://social.msdn.microsoft.com/Forums/en-US/1e550182-5b7e-4fc1-b8bb-d4de132d3625/how-to-get-the-row-and-column-of-button-clicked-in-the-grid-event-handler?forum=csharpgeneral
        //https://stackoverflow.com/questions/10041238/how-to-get-row-index-and-column-of-grid-on-button-click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (pieceClicked)
            {
                if (e.Source is Button btn)
                {
                    string btnName = btn.Name;
                    var rowAndColArray = btnName.Split("_");
                    _row2 = Convert.ToInt32(rowAndColArray[1]);
                    _col2 = Convert.ToInt32(rowAndColArray[2]);
                    FindPiece(_row1, _col1).Move(_row2, _col2);
                    pieceClicked = false;
                }
            }

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!pieceClicked)
            {
                if (e.Source is Image img)
                {
                    ChessPiece clickedPiece = (ChessPiece)img.DataContext;
                    _row1 = clickedPiece.Row;
                    _col1 = clickedPiece.Column;
                    pieceClicked = true;

                }
            }
            else pieceClicked = false;
        }
    }
}

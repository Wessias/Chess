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
        private bool _pieceClicked = false;
        private List<Tuple<int, int, string>> _allowedMoves;
        private bool _IsBlackTurn = false;
        GameLogic _gameLogic = new GameLogic();
        

        
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

            //for (var i = 0; i < 8; i++)
            //{
             //   Pieces.Add(new Pawn() { Row = 1, Column = i, IsBlack = true });
            //}


            Pieces.Add(new Rook() { Row = 7, Column = 0, IsBlack = false });
            Pieces.Add(new Knight() { Row = 7, Column = 1, IsBlack = false });
            Pieces.Add(new Bishop() { Row = 7, Column = 2, IsBlack = false });
            Pieces.Add(new Queen() { Row = 7, Column = 3, IsBlack = false });
            Pieces.Add(new King() { Row = 7, Column = 4, IsBlack = false });
            Pieces.Add(new Bishop() { Row = 7, Column = 5, IsBlack = false });
            Pieces.Add(new Knight() { Row = 7, Column = 6, IsBlack = false });
            Pieces.Add(new Rook() { Row = 7, Column = 7, IsBlack = false });

            //for (var i = 0; i < 8; i++)
            //{
            //    Pieces.Add(new Pawn() { Row = 6, Column = i, IsBlack = false });
            //}

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
            NameScope.SetNameScope(ButtonGrid, new NameScope());
            for (var row = 0; row < 8; row++)
            {
                var isBlack = row % 2 == 1;
                for (int col = 0; col < 8; col++)
                {
                    Button button = new Button { BorderThickness = new Thickness(0, 0, 0, 0), Background = isBlack ? blackBrush : whiteBrush };
                    button.Click += Button_Click;
                    button.Name += "A_" + row.ToString() + "_" + col.ToString();
                    ButtonGrid.RegisterName(button.Name, button);
                    ButtonGrid.Children.Add(button);
                    isBlack = !isBlack;
                }
            }
        }





        //https://social.msdn.microsoft.com/Forums/en-US/1e550182-5b7e-4fc1-b8bb-d4de132d3625/how-to-get-the-row-and-column-of-button-clicked-in-the-grid-event-handler?forum=csharpgeneral
        //https://stackoverflow.com/questions/10041238/how-to-get-row-index-and-column-of-grid-on-button-click
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (_pieceClicked)
            {
                if (e.Source is Button btn)
                {
                    string btnName = btn.Name;
                    var rowAndColArray = btnName.Split("_");
                    int desRow = Convert.ToInt32(rowAndColArray[1]);
                    int desCol = Convert.ToInt32(rowAndColArray[2]);
                    _gameLogic._desiredPos = Tuple.Create(desRow, desCol);
                    _pieceClicked = false;
                    if (true) {
                        _gameLogic._currentPiece.Move(desRow, desCol);
                            }
                }
            }

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_pieceClicked)
            {
                if (e.Source is Image img)
                {
                    ChessPiece clickedPiece = (ChessPiece)img.DataContext;
                    if (_IsBlackTurn && clickedPiece.IsBlack)
                    {
                        _gameLogic._currentPiece = clickedPiece;
                        _allowedMoves = _gameLogic.GenerateAllowedMoves(clickedPiece, Pieces);
                        _pieceClicked = true;
                        _IsBlackTurn = false;
                        ColorAllowedMoves(_allowedMoves);
                        
                    }
                    else if (!_IsBlackTurn && !(clickedPiece.IsBlack)) //Reduant else if? Try removing it
                    {
                        _gameLogic._currentPiece = clickedPiece;
                        _allowedMoves = _gameLogic.GenerateAllowedMoves(clickedPiece, Pieces);
                        _pieceClicked = true;
                        _IsBlackTurn = true;
                        ColorAllowedMoves(_allowedMoves);
                    }
                }
            }
            else _pieceClicked = false;
        }



        private void ColorAllowedMoves(List<Tuple<int, int, string>> allowedMoves)
        {
            if (allowedMoves.Count > 0)
            {
                for (int i = 0; i < allowedMoves.Count(); i++)
                {

                    Button btn = (Button)ButtonGrid.FindName("A_" + allowedMoves[i].Item1.ToString() + "_" + allowedMoves[i].Item2.ToString());
                    btn.Background = Brushes.Red;
                }
            }
        }
    }
}

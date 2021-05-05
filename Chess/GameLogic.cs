using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Chess
{
    class GameLogic
    {
        public ChessPiece _currentPiece;
        public Tuple<int, int> _desiredPos;
        private int _maxRow = 7;
        private int _maxCol = 7;
        private int _minRow = 0;
        private int _minCol = 0;



        public ChessPiece FindPiece(int row, int col, ObservableCollection<ChessPiece> pieces)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (row == pieces.ElementAt(i).Row && col == pieces.ElementAt(i).Column)
                {
                    return pieces.ElementAt(i);
                }
            }
            return null;
        }

        private bool IsChessPieceHere(int row, int col, ObservableCollection<ChessPiece> pieces)
        {
            for (int i = 0; i < pieces.Count; i++)
            {
                if (row == pieces.ElementAt(i).Row && col == pieces.ElementAt(i).Column)
                {
                    return true;
                }
            }
            return false;

        }



        public List<Tuple<int, int, string>> GenerateAllowedMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            switch (_currentPiece.GetType().Name)
            {
                case "Pawn":
                    return GeneratePawnMoves(piece);

                case "Knight":
                    return GenerateKnightMoves(piece);

                case "Bishop":
                    return GenerateBishopMoves(piece);

                case "Rook":
                    return GenerateRookMoves(piece, pieces);

                case "Queen":
                    return GenerateQueenMoves(piece);

                case "King":
                    return GenerateKingMoves(piece);

                default:
                    _currentPiece.Move(7, 7);
                    break;
            }

            return null;
        }


        private List<Tuple<int, int, string>> GenerateRookMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int, string>> rookMoves = new List<Tuple<int, int, string>>();

            for (int i = 0; i <= 4; i++)
            {
                switch (i)
                {
                    //SOUTH
                    case 0:
                        for (int j = piece.Row++; j < _maxRow; j++)
                        {
                            if (!IsChessPieceHere(j, piece.Column, pieces))
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column, "notCapture"));
                            }
                            else if (FindPiece(j, piece.Column, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column, "Capture"));
                            }
                        }
                        break;

                    //NORTH
                    case 1:

                        break;

                    //WEST
                    case 2:

                        break;

                    //EAST
                    case 3:

                        break;

                    case 4:
                        return rookMoves;

                    default:
                        return null;
                }
            }
            return rookMoves;
        }

        private List<Tuple<int, int, string>> GenerateKnightMoves(ChessPiece piece)
        {
            return null;
        }
        private List<Tuple<int, int, string>> GenerateBishopMoves(ChessPiece piece)
        {
            return null;
        }
        private List<Tuple<int, int, string>> GenerateKingMoves(ChessPiece piece)
        {
            return null;
        }
        private List<Tuple<int, int, string>> GenerateQueenMoves(ChessPiece piece)
        {
            return null;
        }
        private List<Tuple<int, int, string>> GeneratePawnMoves(ChessPiece piece)
        {
            return null;

        }









    }
}

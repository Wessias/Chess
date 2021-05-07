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



        public List<Tuple<int, int>> GenerateAllowedMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            switch (_currentPiece.GetType().Name)
            {
                case "Pawn":
                    return GeneratePawnMoves(piece, pieces);

                case "Knight":
                    return GenerateKnightMoves(piece, pieces);

                case "Bishop":
                    return GenerateBishopMoves(piece, pieces);

                case "Rook":
                    return GenerateRookMoves(piece, pieces);

                case "Queen":
                    return GenerateQueenMoves(piece, pieces);

                case "King":
                    return GenerateKingMoves(piece, pieces);

                default:
                    _currentPiece.Move(7, 7);
                    break;
            }

            return null;
        }


        public bool CheckIfDesiredPosIsInAllowedMoves(int desRow, int desCol, List<Tuple<int, int>> allowedMoves)
        {

            for (int i = 0; i < allowedMoves.Count(); i++)
            {
                if (desRow == allowedMoves[i].Item1 && desCol == allowedMoves[i].Item2)
                {
                    return true;
                }
            }
            return false;
        }


        //USELESS
        public string CheckCaptureOrNotCapture(int desRow, int desCol, List<Tuple<int, int, string>> allowedMoves) {

            for (int i = 0; i < allowedMoves.Count(); i++)
            {
                if (desRow == allowedMoves[i].Item1 && desCol == allowedMoves[i].Item2)
                {
                    return allowedMoves[i].Item3;
                }
            }
            return null;
        }


        private List<Tuple<int, int>> GenerateRookMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int>> rookMoves = new List<Tuple<int, int>>();

            for (int i = 0; i <= 4; i++)
            {
                switch (i)
                {
                    //SOUTH
                    case 0:
                        for (int j = piece.Row + 1; j <= _maxRow; j++)
                        {
                            if (!IsChessPieceHere(j, piece.Column, pieces))
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column));
                            }
                            else if (FindPiece(j, piece.Column, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;

                    //NORTH
                    case 1:
                        for (int j = piece.Row - 1; j >= _minRow; j--)
                        {
                            if (!IsChessPieceHere(j, piece.Column, pieces))
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column));
                            }
                            else if (FindPiece(j, piece.Column, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;

                    //WEST
                    case 2:
                        for (int j = piece.Column - 1; j >= _minCol; j--)
                        {
                            if (!IsChessPieceHere(piece.Row, j, pieces))
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j));
                            }
                            else if (FindPiece(piece.Row, j, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }

                        break;

                    //EAST
                    case 3:
                        for (int j = piece.Column + 1; j <= _maxCol; j++)
                        {
                            if (!IsChessPieceHere(piece.Row, j, pieces))
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j));
                            }
                            else if (FindPiece(piece.Row, j, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j));
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }

                        break;

                    case 4:
                        return rookMoves;

                    default:
                        return null;
                }
            }
            return rookMoves;
        }

        private List<Tuple<int, int>> GenerateKnightMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int>> knightMoves = new List<Tuple<int, int>>();

            for (int k = 0; k <= 4; k++)
            {
                switch (k)
                {
                    //NORTHWEST
                    case 0:
                        
                        if (piece.Row - 2 >= _minRow && piece.Column - 1 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 2, piece.Column - 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column - 1));
                                
                            }
                            else if (FindPiece(piece.Row - 2, piece.Column - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column - 1));
                                
                            }
                        }
                        

                        if (piece.Row - 1 >= _minRow && piece.Column - 2 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 1, piece.Column - 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - 2));
                            }
                            else if (FindPiece(piece.Row - 1, piece.Column - 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - 2));
                            }

                        }
                        break;


                    //NORTHEAST
                    case 1:

                        if (piece.Row - 2 >= _minRow && piece.Column + 1 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 2, piece.Column + 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column + 1));

                            }
                            else if (FindPiece(piece.Row - 2, piece.Column + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column + 1));

                            }
                        }


                        if (piece.Row - 1 >= _minRow && piece.Column + 2 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 1, piece.Column + 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column + 2));
                            }
                            else if (FindPiece(piece.Row - 1, piece.Column + 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column + 2));
                            }

                        }
                        break;

                    //SOUTHWEST
                    case 2:

                        if (piece.Row + 2 <= _maxRow && piece.Column - 1 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 2, piece.Column - 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column - 1));

                            }
                            else if (FindPiece(piece.Row + 2, piece.Column - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column - 1));

                            }
                        }


                        if (piece.Row + 1 <= _maxRow && piece.Column - 2 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 1, piece.Column - 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - 2));
                            }
                            else if (FindPiece(piece.Row + 1, piece.Column - 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - 2));
                            }

                        }
                        break;

                    //SOUTHEAST
                    case 3:

                        if (piece.Row + 2 <= _maxRow && piece.Column + 1 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 2, piece.Column + 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column + 1));

                            }
                            else if (FindPiece(piece.Row + 2, piece.Column + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column + 1));

                            }
                        }


                        if (piece.Row + 1 <= _maxRow && piece.Column + 2 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 1, piece.Column + 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column + 2));
                            }
                            else if (FindPiece(piece.Row + 1, piece.Column + 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column + 2));
                            }

                        }
                        break;


                    case 4:
                        return knightMoves;

                    default:
                        return knightMoves;
                }
            }
            return knightMoves;
        }




        private List<Tuple<int, int>> GenerateBishopMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            return null;
        }
        private List<Tuple<int, int>> GenerateKingMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            return null;
        }
        private List<Tuple<int, int>> GenerateQueenMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            return null;
        }
        private List<Tuple<int, int>> GeneratePawnMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            return null;

        }
    }  
}

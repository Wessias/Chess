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
                    return GeneratePawnMoves((Pawn)piece, pieces);

                case "Knight":
                    return GenerateKnightMoves(piece, pieces);

                case "Bishop":
                    return GenerateBishopMoves(piece, pieces);

                case "Rook":
                    return GenerateRookMoves(piece, pieces);

                case "Queen":
                    return GenerateQueenMoves(piece, pieces);

                case "King":

                    return GenerateKingMoves((King)piece, pieces);

                default:
                    _currentPiece.Move(7, 7);
                    break;
            }


            //Not in use, was gonna use it to check that the king cant go into attacked squares and if he is in check.
            var attackedPositions = new List<Tuple<int, int, string>>();

            foreach (var enemyPiece in pieces)
            {
                if (enemyPiece.IsBlack != piece.IsBlack)
                {
                    var attackedPositionsByEnemyPiece = GenerateAllowedMoves(enemyPiece, pieces);

                    foreach (var move in attackedPositionsByEnemyPiece)
                    {
                        attackedPositions.Add(move);
                    }
                }
            }

            return null;
        }


        public bool CheckIfDesiredPosIsInAllowedMoves(int desRow, int desCol, List<Tuple<int, int, string>> allowedMoves)
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


        //USELESS lel
        /*
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
        */


        private List<Tuple<int, int, string>> GenerateRookMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int, string>> rookMoves = new List<Tuple<int, int, string>>();

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
                                rookMoves.Add(Tuple.Create(j, piece.Column, "normal"));
                            }
                            else if (FindPiece(j, piece.Column, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column, "normal"));
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
                                rookMoves.Add(Tuple.Create(j, piece.Column, "normal"));
                            }
                            else if (FindPiece(j, piece.Column, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(j, piece.Column, "normal"));
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
                                rookMoves.Add(Tuple.Create(piece.Row, j, "normal"));
                            }
                            else if (FindPiece(piece.Row, j, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j, "normal"));
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
                                rookMoves.Add(Tuple.Create(piece.Row, j, "normal"));
                            }
                            else if (FindPiece(piece.Row, j, pieces).IsBlack == !piece.IsBlack)
                            {
                                rookMoves.Add(Tuple.Create(piece.Row, j, "normal"));
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

        private List<Tuple<int, int, string>> GenerateKnightMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int, string>> knightMoves = new List<Tuple<int, int, string>>();

            for (int k = 0; k <= 4; k++)
            {
                switch (k)
                {
                    //NORTH WEST
                    case 0:
                        
                        if (piece.Row - 2 >= _minRow && piece.Column - 1 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 2, piece.Column - 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column - 1, "normal"));
                                
                            }
                            else if (FindPiece(piece.Row - 2, piece.Column - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column - 1, "normal"));
                                
                            }
                        }
                        

                        if (piece.Row - 1 >= _minRow && piece.Column - 2 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 1, piece.Column - 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - 2, "normal"));
                            }
                            else if (FindPiece(piece.Row - 1, piece.Column - 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - 2, "normal"));
                            }

                        }
                        break;


                    //NORTH EAST
                    case 1:

                        if (piece.Row - 2 >= _minRow && piece.Column + 1 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 2, piece.Column + 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column + 1, "normal"));

                            }
                            else if (FindPiece(piece.Row - 2, piece.Column + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 2, piece.Column + 1, "normal"));

                            }
                        }


                        if (piece.Row - 1 >= _minRow && piece.Column + 2 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row - 1, piece.Column + 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column + 2, "normal"));
                            }
                            else if (FindPiece(piece.Row - 1, piece.Column + 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row - 1, piece.Column + 2, "normal"));
                            }

                        }
                        break;

                    //SOUTH WEST
                    case 2:

                        if (piece.Row + 2 <= _maxRow && piece.Column - 1 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 2, piece.Column - 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column - 1, "normal"));

                            }
                            else if (FindPiece(piece.Row + 2, piece.Column - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column - 1, "normal"));

                            }
                        }


                        if (piece.Row + 1 <= _maxRow && piece.Column - 2 >= _minCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 1, piece.Column - 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - 2, "normal"));
                            }
                            else if (FindPiece(piece.Row + 1, piece.Column - 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - 2, "normal"));
                            }

                        }
                        break;

                    //SOUTH EAST
                    case 3:

                        if (piece.Row + 2 <= _maxRow && piece.Column + 1 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 2, piece.Column + 1, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column + 1, "normal"));

                            }
                            else if (FindPiece(piece.Row + 2, piece.Column + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 2, piece.Column + 1, "normal"));

                            }
                        }


                        if (piece.Row + 1 <= _maxRow && piece.Column + 2 <= _maxCol)
                        {
                            if (!IsChessPieceHere(piece.Row + 1, piece.Column + 2, pieces))
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column + 2, "normal"));
                            }
                            else if (FindPiece(piece.Row + 1, piece.Column + 2, pieces).IsBlack != piece.IsBlack)
                            {
                                knightMoves.Add(Tuple.Create(piece.Row + 1, piece.Column + 2, "normal"));
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




        private List<Tuple<int, int, string>> GenerateBishopMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int, string>> bishopMoves = new List<Tuple<int, int, string>>();

            for (int i = 0; i <= 4; i++)
            {
                switch (i)
                {
                    //NORTH WEST
                    case 0:
                        int k = piece.Column - 1;
                        for (int j = piece.Row - 1; j >= _minRow; j--)
                        { 
                            if (k >= _minCol)
                            {
                                if (!IsChessPieceHere(j, k, pieces))
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                }
                                else if (FindPiece(j, k, pieces).IsBlack != piece.IsBlack)
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            k--; ;
                        }

                        break;

                    //NORTH EAST
                    case 1:
                        k = piece.Column + 1;
                        for (int j = piece.Row - 1; j >= _minRow; j--)
                        {
                            if (k <= _maxCol)
                            {
                                if (!IsChessPieceHere(j, k, pieces))
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                }
                                else if (FindPiece(j, k, pieces).IsBlack != piece.IsBlack)
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            k++; ;
                        }

                        break;

                    //SOUTH WEST
                    case 2:

                        k = piece.Column - 1;
                        for (int j = piece.Row + 1; j <= _maxRow; j++)
                        {
                            if (k >= _minCol)
                            {
                                if (!IsChessPieceHere(j, k, pieces))
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                }
                                else if (FindPiece(j, k, pieces).IsBlack != piece.IsBlack)
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            k--; ;
                        }

                        break;

                    //SOUTH EAST
                    case 3:
                        k = piece.Column + 1;
                        for (int j = piece.Row + 1; j <= _maxRow; j++)
                        {
                            if (k <= _maxCol)
                            {
                                if (!IsChessPieceHere(j, k, pieces))
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                }
                                else if (FindPiece(j, k, pieces).IsBlack != piece.IsBlack)
                                {
                                    bishopMoves.Add(Tuple.Create(j, k, "normal"));
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                            k++; ;
                        }

                        break;
                    case 4:
                        return bishopMoves;

                    default: 
                        return bishopMoves;

                }
            }
            return null;
        }
        private List<Tuple<int, int, string>> GenerateKingMoves(King piece, ObservableCollection<ChessPiece> pieces)
        {
            var kingMoves = new List<Tuple<int, int, string>>();

            //North and south moves
            for (int i = -1; i <=1; i++)
            {
                //North
                if (piece.Row - 1 >= _minRow && piece.Column - i >= _minCol && piece.Column - i <= _maxCol)
                {
                    if(!IsChessPieceHere(piece.Row - 1, piece.Column - i, pieces))
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - i, "normal"));
                    }
                    else if (FindPiece(piece.Row - 1, piece.Column - i, pieces).IsBlack != piece.IsBlack)
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - 1, piece.Column - i, "normal"));
                    }
                }

                //South
                if (piece.Row + 1 <= _maxRow && piece.Column - i >= _minCol && piece.Column - i <= _maxCol)
                {
                    if (!IsChessPieceHere(piece.Row + 1, piece.Column - i, pieces))
                    {
                        kingMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - i, "normal"));
                    }
                    else if (FindPiece(piece.Row + 1, piece.Column - i, pieces).IsBlack != piece.IsBlack)
                    {
                        kingMoves.Add(Tuple.Create(piece.Row + 1, piece.Column - i, "normal"));
                    }
                }


            }
            //WEST and EAST
            for (int i = 0; i < 1; i++)
            {

                //west
                if (piece.Row - i >= _minRow && piece.Column - 1 >= _minCol)
                {
                    if (!IsChessPieceHere(piece.Row - i, piece.Column - 1, pieces))
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - i, piece.Column - 1, "normal"));
                    }
                    else if (FindPiece(piece.Row - i, piece.Column - 1, pieces).IsBlack != piece.IsBlack)
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - i, piece.Column - 1, "normal"));
                    }
                }

                //east
                if (piece.Row - i <= _maxRow && piece.Column + 1 <= _maxCol)
                {
                    if (!IsChessPieceHere(piece.Row - i, piece.Column + 1, pieces))
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - i, piece.Column + 1, "normal"));
                    }
                    else if (FindPiece(piece.Row - i, piece.Column + 1, pieces).IsBlack != piece.IsBlack)
                    {
                        kingMoves.Add(Tuple.Create(piece.Row - i, piece.Column + 1, "normal"));
                    }
                }

            }

            if(piece._movesDone == 0)
            {

                    //Castle to da left
                    for (int i = 1; i < 5; i++)
                    {
                        if (i < 4 && IsChessPieceHere(piece.Row, piece.Column - i, pieces))
                        {
                            break;
                        }
                        else if (i == 4 && IsChessPieceHere(piece.Row, piece.Column - i, pieces))
                        {
                            if(FindPiece(piece.Row, piece.Column - i, pieces).GetType().Name == "Rook")
                            {
                                Rook rookKingWantsToHug = (Rook)FindPiece(piece.Row, piece.Column - i, pieces);

                                if (rookKingWantsToHug._movesDone == 0)
                                {
                                    kingMoves.Add(Tuple.Create(piece.Row, piece.Column - 2, "castleQueen"));
                                }
                            }
                        }
                    }

                    for (int i = 1; i < 4; i++)
                {
                    //Castle to da right
                    if (i < 3 && IsChessPieceHere(piece.Row, piece.Column + i, pieces))
                    {
                        break;
                    }
                    else if (i == 3 && IsChessPieceHere(piece.Row, piece.Column + i, pieces))
                    {
                        if (FindPiece(piece.Row, piece.Column + i, pieces).GetType().Name == "Rook")
                        {
                            Rook kingWantsToHuggies = (Rook)FindPiece(piece.Row, piece.Column + i, pieces);
                            if (kingWantsToHuggies._movesDone == 0)
                            {
                                kingMoves.Add(Tuple.Create(piece.Row, piece.Column + 2, "castleKing"));
                            }

                        }
                    }
                }


            }






            return kingMoves;
        }
        private List<Tuple<int, int, string>> GenerateQueenMoves(ChessPiece piece, ObservableCollection<ChessPiece> pieces)
        {

            var diagonal = GenerateBishopMoves(piece, pieces);
            var verticalAndHorizontal = GenerateRookMoves(piece, pieces);
            var queenMoves = verticalAndHorizontal;


            foreach (var move in diagonal)
            {
                queenMoves.Add(move);
            }

            return queenMoves;
        }
        private List<Tuple<int, int, string>> GeneratePawnMoves(Pawn piece, ObservableCollection<ChessPiece> pieces)
        {
            List<Tuple<int, int, string>> pawnMoves = new List<Tuple<int, int, string>>();

            var currentRow = piece.Row;
            var currentCol = piece.Column;
            //Checks if this bad boy is black or white
            switch (piece.IsBlack.ToString())
            {
                //Bleck
                case "True":
                    

                    //CHECK F;rWARD
                    if (!IsChessPieceHere(currentRow + 1, currentCol, pieces))
                    {
                        //PROOOOOMOTION
                        if(currentRow + 1 == _maxRow)
                        {
                            pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol, "promotion"));
                        }
                        else
                        {
                            pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol, "normal"));
                        }
                    }

                    //CHECK CAPTURE

                    //RIGHT
                    if(currentCol - 1 >= _minCol)
                    {
                        if (IsChessPieceHere(currentRow + 1, currentCol - 1, pieces) && FindPiece(currentRow + 1, currentCol - 1, pieces).IsBlack != piece.IsBlack)
                        {
                            //promotion
                            if (currentRow + 1 == _maxRow)
                            {
                                pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol - 1, "promotion"));
                            }
                            else
                            {
                                pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol - 1, "normal"));
                            }
                        }
                    }

                    //LEFT
                    if (currentCol + 1 <= _maxCol)
                    {
                        if (IsChessPieceHere(currentRow + 1, currentCol + 1, pieces) && FindPiece(currentRow + 1, currentCol + 1, pieces).IsBlack != piece.IsBlack)
                        {
                            //PROOOOMOTION
                            if (currentRow + 1 == _maxRow)
                            {
                                pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol + 1, "promotion"));
                            }
                            else
                            {
                                pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol + 1, "normal"));
                            }
                        }
                    }


                    //EN PASSANT
                    //GOES HERE
                    //EN PASSANT RIGHT (From black pawns POV)
                    if (currentCol - 1 >= _minCol)
                    {
                        if (IsChessPieceHere(currentRow, currentCol - 1, pieces))
                        {
                            if (FindPiece(currentRow, currentCol - 1, pieces).GetType().Name == piece.GetType().Name && FindPiece(currentRow, currentCol - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                Pawn pawn = (Pawn)FindPiece(currentRow, currentCol - 1, pieces);
                                if (pawn._movesDone == 1)
                                {
                                    pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol - 1, "en passant"));
                                }
                            }
                        }
                    }

                    //En passant left
                    if (currentCol + 1 <= _maxCol)
                    {
                        if (IsChessPieceHere(currentRow, currentCol + 1, pieces))
                        {
                            if (FindPiece(currentRow, currentCol + 1, pieces).GetType().Name == piece.GetType().Name && FindPiece(currentRow, currentCol + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                Pawn pawn = (Pawn)FindPiece(currentRow, currentCol + 1, pieces);
                                if (pawn._movesDone == 1)
                                {
                                    pawnMoves.Add(Tuple.Create(currentRow + 1, currentCol + 1, "en passant"));
                                }
                            }
                        }
                    }



                    //Check if BIG JUMP is enabled
                    if (piece._movesDone == 0)
                    {
                        if(!IsChessPieceHere(currentRow + 2, currentCol, pieces)){
                            pawnMoves.Add(Tuple.Create(currentRow + 2, currentCol, "normal"));
                        }
                    }
                    break;



                //TITANIUM WHITE
                case "False":

                    //CHECK F;rWARD
                    if (!IsChessPieceHere(currentRow - 1, currentCol, pieces))
                    {
                        //PROOOOOMOTION
                        if (currentRow - 1 == _minRow)
                        {
                            pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol, "promotion"));
                        }
                        else
                        {
                            pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol, "normal"));
                        }
                    }

                    //CHECK CAPTURE

                    //LEFT
                    if (currentCol - 1 >= _minCol)
                    {
                        if (IsChessPieceHere(currentRow - 1, currentCol - 1, pieces) && FindPiece(currentRow - 1, currentCol - 1, pieces).IsBlack != piece.IsBlack)
                        {
                            //PROMOTION
                            if (currentRow - 1 == _minRow)
                            {
                                pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol - 1, "promotion"));
                            }
                            else
                            {
                                pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol - 1, "normal"));
                            }
                        }
                    }

                    //RIGHT
                    if (currentCol + 1 <= _maxCol)
                    {
                        if (IsChessPieceHere(currentRow - 1, currentCol + 1, pieces) && FindPiece(currentRow - 1, currentCol + 1, pieces).IsBlack != piece.IsBlack)
                        {
                            //PPPPPPROOOOOOMOTION. I dont have implement friend
                            if (currentRow - 1 == _minRow)
                            {
                                pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol + 1, "promotion"));
                            }
                            else
                            {
                                pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol + 1, "normal"));
                            }
                        }
                    }


                    //EN PASSANT
                    //My en passant shit isn't correct, en passant is only possible right after the pawn in question has moved up 2 spaces next to ur pawn, 
                    //currently you can do it even if its not directly after the pawn jumps 2 positions. 
                    //Simple fix is tracking the moves that are made in a list and checking if the most recent item added to the list has matching row and col to the pawn ur trying to en passant.
                    //EN PASSANT left (From white pawns POV)
                    if (currentCol - 1 >= _minCol)
                    {
                        if (IsChessPieceHere(currentRow, currentCol - 1, pieces))
                        {
                            if (FindPiece(currentRow, currentCol - 1, pieces).GetType().Name == piece.GetType().Name && FindPiece(currentRow, currentCol - 1, pieces).IsBlack != piece.IsBlack)
                            {
                                Pawn pawn = (Pawn)FindPiece(currentRow, currentCol - 1, pieces);
                                if (pawn._movesDone == 1)
                                {
                                    pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol - 1, "en passant"));
                                }
                            }
                        }
                    }

                    //En passant right
                    if (currentCol + 1 <= _maxCol)
                    {
                        if (IsChessPieceHere(currentRow, currentCol + 1, pieces))
                        {
                            if (FindPiece(currentRow, currentCol + 1, pieces).GetType().Name == piece.GetType().Name && FindPiece(currentRow, currentCol + 1, pieces).IsBlack != piece.IsBlack)
                            {
                                Pawn pawn = (Pawn)FindPiece(currentRow, currentCol + 1, pieces);
                                if (pawn._movesDone == 1)
                                {
                                    pawnMoves.Add(Tuple.Create(currentRow - 1, currentCol + 1, "en passant"));
                                }
                            }
                        }
                    }



                    //Check if BIG JUMP is enabled
                    if (piece._movesDone == 0)
                    {
                        if (!IsChessPieceHere(currentRow - 2, currentCol, pieces))
                        {
                            pawnMoves.Add(Tuple.Create(currentRow - 2, currentCol, "normal"));
                        }
                    }
                    break;

                default: return pawnMoves;
            }
            
            return pawnMoves;

        }

        public int FindIndexOfMoveInAllowedMoves(int desRow, int desCol, List<Tuple<int, int, string>> allowedMoves)
        {
            for (int i = 0; i < allowedMoves.Count(); i++)
            {
                if (desRow == allowedMoves[i].Item1 && desCol == allowedMoves[i].Item2)
                {
                    return i;
                }
            }
            return 0;
        }
    }  
}

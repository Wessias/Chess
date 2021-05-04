using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    class Rook : ChessPiece
    {
        public int _movesDone = 0;

        public override void Move(int row, int col)
        {
            base.Move(row, col);
            _movesDone++;
        }
    }
}

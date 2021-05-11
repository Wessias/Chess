using System;
using System.Collections.Generic;
using System.Text;

namespace Chess
{
    //Blessed brother, WELCOME! This is the kings domain, to leave swish 500kr to 0763055779, may Allah bless you, inshallah.
    class King : ChessPiece
    {
        public int _movesDone = 0;

        public override void Move(int row, int col)
        {
            base.Move(row, col);
            _movesDone++;
        }
    }
}

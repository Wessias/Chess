using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Chess
{
    //My friend this is my viewmodel.
    class ChessPiece : INotifyPropertyChanged {

        // This property change stuff my blessed brother is so that my view can get notification when property in here is update.
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsBlack { get; set; }

    public virtual void Move(int row, int col)
        {
            Row = row;
            Column = col;
        }

    private int _row;
    public int Row
    {
        get { return _row; }
        set
        {
            _row = value;
            OnPropertyChanged("Row");
        }
    }

    //Yes,yes brother this is a variabel of the datatype integer very useful, inshallah.
    private int _column;
    public int Column
    {
        get { return _column; }
        set
        {
            _column = value;
            OnPropertyChanged("Column");
        }
    }

        //My friend you are blessed by Allah, mashallah.
    

    public string ImageSource
    {
        get { return "../ChessPieceImages/" + (IsBlack ? "Black" : "White") + GetType().Name + ".png"; }
    }
}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Chess
{
    class ChessPiece : INotifyPropertyChanged { 
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

    public event PropertyChangedEventHandler PropertyChanged;

    public virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }

    public string ImageSource
    {
        get { return "../ChessPieceImages/" + (IsBlack ? "Black" : "White") + GetType().Name + ".png"; }
    }
}
}

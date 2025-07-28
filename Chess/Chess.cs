using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Field
    {
        ChessPiece? content;

        public bool IsEmpty => content == null;
    }
    public class ChessBoard : IEnumerable
    {
        private ChessBoard[] _chessBoard;

        private void PlaceWhitePieces()
        {
            // ...
        }
        private void PlaceBlackPieces()
        {
            // ...
        }
        public void InitBoard()
        {
            // ...
        }

        // ---------------------------------------------

        public ChessBoard(ChessBoard[] pArray)
        {
            _chessBoard = new ChessBoard[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _chessBoard[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ChessBoardEnum GetEnumerator()
        {
            return new ChessBoardEnum(_chessBoard);
        }

    }
    public class ChessBoardEnum : IEnumerator
    {
        private ChessBoard[] _chessBoard;
        int position = -1;

        public ChessBoardEnum(ChessBoard[] chessBoard)
        {
            this._chessBoard = chessBoard;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _chessBoard.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public ChessBoard Current
        {
            get
            {
                try
                {
                    return _chessBoard[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public class Row : IEnumerable
    {
        private Row[] _row;
        public Row(Row[] pArray)
        {
            _row = new Row[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _row[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public RowEnum GetEnumerator()
        {
            return new RowEnum(_row);
        }
    }

    public class RowEnum : IEnumerator
    {
        private Row[] _row;
        int position = -1;

        public RowEnum(Row[] row)
        {
            this._row = row;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _row.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public Row Current
        {
            get
            {
                try
                {
                    return _row[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
    public abstract class ChessPiece
    {
        private string Color { get; set; }
        protected int[] Position { get; set; }
        protected int CountOfMoves { get; set; }
        public abstract bool CanMove(int endRow, int endColumn);
        public void Move(int endRow, int endColumn) => this.Position = [endRow, endColumn];
        public string GetCurrentPosition() => $"{Config.ALPHABET[Position[0]]}{Position[1] + 1}";
        protected ChessPiece(int[] position, string color)
        {
            this.Position = 
                position[0] <= Config.SIZE && position[1] <= Config.SIZE ? 
                position : throw new Exception($"ChessPiece position {Config.ALPHABET[position[0]] + " " + position[1]} out of bounds.");
            this.Color = 
                Config.COLORS.Contains(color.ToLower()) ?
                color : throw new Exception($"ChessPiece color {color} is unavailable.");
            this.CountOfMoves = 0;
        }

    }

    public class Pawn : ChessPiece
    {
        public Pawn(int[] position, string color) : base(position, color)
        {
        }

        public override bool CanMove(int endRow, int endColumn)
        {
            return true;
        }
    }

    public class King : ChessPiece
    {
        public King(int[] position, string color) : base(position, color)
        {
        }

        public override bool CanMove(int endRow, int endColumn)
        {
            return true;
        }
    }

    public class Queen : ChessPiece
    {
        public Queen(int[] position, string color) : base(position, color)
        {
        }

        public override bool CanMove(int endRow, int endColumn)
        {
            return true;
        }

        public bool CanCastle()
        {
            return this.CountOfMoves == 0;
        }
    }

    public class Bishop : ChessPiece
    {
        public Bishop(int[] position, string color) : base(position, color)
        {
        }

        public override bool CanMove(int endRow, int endColumn)
        {
            return true;
        }
    }

    public class Rook : ChessPiece
    {
        public Rook(int[] position, string color) : base(position, color)
        {
        }

        public override bool CanMove(int endRow, int endColumn)
        {
            return true;
        }

        public bool CanCastle()
        {
            return this.CountOfMoves == 0;
        }
    }
}

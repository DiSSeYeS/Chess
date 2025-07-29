using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    public class Field
    {
        private ChessPiece? _content { get; set; }

        public bool IsEmpty => _content == null;

        public void PlacePiece(ChessPiece piece) => this._content = piece;
        public void RemovePiece() => this._content = null;
        public ChessPiece? GetPiece() => this._content;

        public override string ToString()
        {
            if (this._content == null) return ".";
            return Config.ICONS[this._content.GetType()];
        }
    }
    public class ChessBoard
    {
        private Field[,] _chessBoard { get; }
        
        private void PlaceWhitePieces()
        {
            string COLOR = "white";

            this._chessBoard[0, 0].PlacePiece(new Rook([0, 0], COLOR));
            this._chessBoard[0, Config.SIZE - 1].PlacePiece(new Rook([0, Config.SIZE - 1], COLOR));

            this._chessBoard[0, 1].PlacePiece(new Knight([0, 1], COLOR));
            this._chessBoard[0, Config.SIZE - 2].PlacePiece(new Knight([0, Config.SIZE - 2], COLOR));

            this._chessBoard[0, 2].PlacePiece(new Bishop([0, 2], COLOR));
            this._chessBoard[0, Config.SIZE - 3].PlacePiece(new Bishop([0, Config.SIZE - 3], COLOR));

            this._chessBoard[0, 3].PlacePiece(new King([0, 3], COLOR));
            this._chessBoard[0, Config.SIZE - 4].PlacePiece(new Queen([0, Config.SIZE - 4], COLOR));

            for (int i = 0; i < Config.SIZE; i++)
            {
                this._chessBoard[1, i].PlacePiece(new Pawn([1, i], COLOR));
            }
        }
        private void PlaceBlackPieces()
        {
            string COLOR = "black";

            this._chessBoard[Config.SIZE - 1, 0].PlacePiece(new Rook([Config.SIZE - 1, 0], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 1].PlacePiece(new Rook([Config.SIZE - 1, Config.SIZE - 1], COLOR));

            this._chessBoard[Config.SIZE - 1, 1].PlacePiece(new Knight([0, 1], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 2].PlacePiece(new Knight([Config.SIZE - 1, Config.SIZE - 2], COLOR));

            this._chessBoard[Config.SIZE - 1, 2].PlacePiece(new Bishop([Config.SIZE - 1, 2], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 3].PlacePiece(new Bishop([Config.SIZE - 1, Config.SIZE - 3], COLOR));

            this._chessBoard[Config.SIZE - 1, 3].PlacePiece(new King([0, 3], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 4].PlacePiece(new Queen([Config.SIZE - 1, Config.SIZE - 4], COLOR));

            for (int i = 0; i < Config.SIZE; i++)
            {
                this._chessBoard[Config.SIZE - 2, i].PlacePiece(new Pawn([Config.SIZE - 2, i], COLOR));
            }
        }
        public void InitBoard()
        {
            for (int i = 0; i < Config.SIZE; i++)
            {
                for (int j = 0; j < Config.SIZE; j++)
                {
                    this._chessBoard[i, j] = new Field();
                }
            }

            PlaceWhitePieces();
            PlaceBlackPieces();
        }
        public Field[,] GetFields() => this._chessBoard;
        public Field[,] Fields => this._chessBoard;
        public ChessBoard()
        {
            this._chessBoard = new Field[Config.SIZE, Config.SIZE];
            InitBoard();
        }

    }
    public abstract class ChessPiece
    {
        private string Color { get; }
        protected int[] Position { get; set; }
        protected int CountOfMoves { get; set; }
        protected IEnumerable<int[]> Moves { get; set; }
        public bool CanMove() => this.Moves.Contains(Position);
        public void Move(int[] coordinates) => this.Position = [coordinates[0], coordinates[1]];
        public void Take(int[] coordinates) => this.Position = Position; // ...
        public abstract void CalculateMoves();
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

        public override void CalculateMoves()
        {
            if (this.CountOfMoves > 0)
            {
                

                return;
            }
            if (Config.CHESSBOARD.Fields[this.Position[0], this.Position[1] + 2].IsEmpty)
            {
                Moves.Append([this.Position[0], this.Position[1] + 2]);
            }
        }

    }

    public class King : ChessPiece
    {
        public King(int[] position, string color) : base(position, color)
        {
        }

        public override void CalculateMoves()
        {

        }

    }

    public class Queen : ChessPiece
    {
        public Queen(int[] position, string color) : base(position, color)
        {
        }

        public override void CalculateMoves()
        {

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

        public override void CalculateMoves()
        {

        }

    }

    public class Rook : ChessPiece
    {
        public Rook(int[] position, string color) : base(position, color)
        {
        }

        public override void CalculateMoves()
        {

        }

        public bool CanCastle()
        {
            return this.CountOfMoves == 0;
        }
    }

    public class Knight : ChessPiece
    {
        public Knight(int[] position, string color) : base(position, color)
        {
        }

        public override void CalculateMoves()
        {

        }
    }
}

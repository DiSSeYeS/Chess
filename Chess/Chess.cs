using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Globalization;
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

            this._chessBoard[Config.SIZE - 1, 1].PlacePiece(new Knight([Config.SIZE - 1, 1], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 2].PlacePiece(new Knight([Config.SIZE - 1, Config.SIZE - 2], COLOR));

            this._chessBoard[Config.SIZE - 1, 2].PlacePiece(new Bishop([Config.SIZE - 1, 2], COLOR));
            this._chessBoard[Config.SIZE - 1, Config.SIZE - 3].PlacePiece(new Bishop([Config.SIZE - 1, Config.SIZE - 3], COLOR));

            this._chessBoard[Config.SIZE - 1, 3].PlacePiece(new King([Config.SIZE - 1, 3], COLOR));
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
        public string Color { get; }
        protected int[] Position { get; set; } // [цифра, буква] 
        protected int CountOfMoves { get; set; }
        public List<int[]> Moves = [];
        public bool CanMove() => this.Moves.Contains(Position);
        public void Move(int[] coordinates) 
        {
            if (!CanMove()) return;
            this.Position = [coordinates[0], coordinates[1]];
        }
        public void Take(int[] coordinates)                             // ............................................................... //
        {                                                              // .......УБРАТЬ КООРДИНАТЫ У ФИГУР ОСТАВИТЬ ТОЛЬКО У ПОЛЕЙ....... //
            if (!CanMove()) return;                                   // ............................................................... //
            this.Position = [coordinates[0], coordinates[1]];
        }
        public abstract void CalculateMoves();
        public string GetCurrentPosition() => $"{Config.ALPHABET[Position[1]]}{Position[0] + 1}";
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
            int delta = this.Color == "white" ? 1 : -1;

            if (this.CountOfMoves == 0)
            {
                if (Config.CHESSBOARD.Fields[this.Position[0] + (2 * delta), this.Position[1]].IsEmpty &&
                    Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1]].IsEmpty)
                {
                    Moves.Add([this.Position[0] + (2 * delta), this.Position[1]]);
                }
            }
            if (Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1]].IsEmpty)
            {
                this.Moves.Add([this.Position[0] + delta, this.Position[1]]);
            }

            switch (this.Position[1])
            {
                case 0:
                    if (!Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1] + 1].IsEmpty)
                    {
                        this.Moves.Add([this.Position[0] + delta, this.Position[1] + 1]);
                    }
                    break;
                case 7:
                    if (!Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1] - 1].IsEmpty)
                    {
                        this.Moves.Add([this.Position[0] + delta, this.Position[1] - 1]);
                    }
                    break;
                default:


                    if (!Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1] - 1].IsEmpty)
                    {
                        this.Moves.Add([this.Position[0] + delta, this.Position[1] - 1]);
                    }
                    if (!Config.CHESSBOARD.Fields[this.Position[0] + delta, this.Position[1] + 1].IsEmpty)
                    {
                        this.Moves.Add([this.Position[0] + delta, this.Position[1] + 1]);
                    }
                    break;
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
            this.Moves.Clear();

            int[,] knightMoves = new int[8, 2] {
                { 1, 2 }, { 2, 1 }, { 2, -1 }, { 1, -2 },
                { -1, -2 }, { -2, -1 }, { -2, 1 }, { -1, 2 }
            };

            for (int i = 0; i < 8; i++)
            {
                int newRow = this.Position[0] + knightMoves[i, 0];
                int newCol = this.Position[1] + knightMoves[i, 1];

                if (newRow >= 0 && newRow < Config.SIZE &&
                    newCol >= 0 && newCol < Config.SIZE)
                {
                    Field targetField = Config.CHESSBOARD.Fields[newRow, newCol];

                    if (targetField.IsEmpty ||
                        (!targetField.IsEmpty && targetField.GetPiece().Color != this.Color))
                    {
                        this.Moves.Add([newRow, newCol]);
                    }
                }
            }
        }
    }
}

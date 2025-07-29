// ...


namespace Chess
{
    class Program
    {
        public static void Main()
        {
            InitConfig();
            ChessBoard board = Config.CHESSBOARD;

            for (int i = 0; i < Config.SIZE; i++)
            {
                for (int j = 0; j < Config.SIZE; j++)
                {
                    Console.Write(board.Fields[i, j].ToString() + " ");
                }
                Console.WriteLine();
            }
        }

        public static void InitConfig()
        {
            Config.COLORS = ["white", "black"];
            Config.SIZE = 8;
            Config.CHESSBOARD = new ChessBoard();

            for (int i = 0; i < 26; i++)
            {
                Config.ALPHABET[i] = (char)('a' + i);
            }

            Config.ICONS = new Dictionary<Type, string>
            {
                { typeof(Pawn),   "P"},
                { typeof(Bishop), "B"},
                { typeof(Rook),   "R"},
                { typeof(Knight), "N"},
                { typeof(Queen),  "Q"},
                { typeof(King),   "K"}

            };
        }
    }  
}
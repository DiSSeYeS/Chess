// ...


namespace Chess
{
    class Program
    {
        public static void Main()
        {
            InitConfig();

            Pawn pawn = new Pawn([0,0], "white");

            Console.WriteLine(pawn.GetCurrentPosition());
        }

        public static void InitConfig()
        {
            Config.COLORS = ["white", "black"];
            Config.SIZE = 8;

            for (int i = 0; i < 26; i++)
            {
                Config.ALPHABET[i] = (char)('a' + i);
            }
        }
    }
    
}
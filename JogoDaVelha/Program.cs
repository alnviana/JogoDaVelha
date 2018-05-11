using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoDaVelha {
    class Program {
        readonly static string[] xSymbolStringArray = new string[] {    @"  \  /  ",
                                                                        @"   \/   ",
                                                                        @"   /\   ",
                                                                        @"  /  \  " };
        readonly static string[] oSymbolStringArray = new string[] {    @" ┌────┐ ",
                                                                        @" │    │ ",
                                                                        @" │    │ ",
                                                                        @" └────┘ " };
        readonly static string[] emptyStringArray = new string[] {  @"        ",
                                                                    @"        ",
                                                                    @"        ",
                                                                    @"        " };
        readonly static string verticalLineString = @"│" ;
        readonly static string horizontalLineString = @"────────┼────────┼────────";

        static void Main(string[] args) {
            //Console.WriteLine(@"  \  /  │ ┌────┐ │");
            //Console.WriteLine(@"   \/   │ │    │ │");
            //Console.WriteLine(@"   /\   │ │    │ │");
            //Console.WriteLine(@"  /  \  │ └────┘ │");
            //Console.WriteLine(@"────────┼────────┼──────");
            //Console.WriteLine(@"  \  /  │ ┌────┐ │");
            //Console.WriteLine(@"   \/   │ │    │ │");
            //Console.WriteLine(@"   /\   │ │    │ │");
            //Console.WriteLine(@"  /  \  │ └────┘ │");
            //Console.WriteLine(@"────────┼────────┼──────");
            //Console.WriteLine(@"  \  /  │ ┌────┐ │");
            //Console.WriteLine(@"   \/   │ │    │ │");
            //Console.WriteLine(@"   /\   │ │    │ │");
            //Console.WriteLine(@"  /  \  │ └────┘ │");
            //Console.ReadLine();

            int[,] boardArray = new int[,] { { 0, 2, 2 }, { 1, 2, 1 }, { 0, 0, 2 } };
            PrintBoard(boardArray);

            Console.ReadLine();
        }

        static void PrintBoard(int[,] boardArray) {
            if (boardArray.Length != 9) {
                Console.WriteLine("Número de posições do tabuleiro incorreto.");
                return;
            }

            //Each row of board
            for (int i = 0; i < 3; i++) {
                int position1 = boardArray[i, 0];
                int position2 = boardArray[i, 1];
                int position3 = boardArray[i, 2];

                //Each text line of row
                for (int j = 0; j < 4; j++) {
                    Console.Write(GetSymbolString(position1, j));
                    Console.Write(verticalLineString);
                    Console.Write(GetSymbolString(position2, j));
                    Console.Write(verticalLineString);
                    Console.Write(GetSymbolString(position3, j));
                    Console.Write("\n");
                }

                if (i != 2) {
                    Console.WriteLine(horizontalLineString);
                }                
            }
        }

        static string GetSymbolString(int id, int row) {
            switch (id) {
                case 1:
                    return xSymbolStringArray[row];
                case 2:
                    return oSymbolStringArray[row];
                default:
                    return emptyStringArray[row];
            }
        }
    }
}

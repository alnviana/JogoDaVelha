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
        readonly static string verticalLineString = @"│";
        readonly static string horizontalLineString = @"────────┼────────┼────────";

        static void Main(string[] args) {
            //Pra fazer:
            //- Colocar "Régua" para mostrar X e Y
            //- Escolha aleatória do programa
            //- Permitir escolha dos jogadores
            //- Validar vitória

            Console.WriteLine();

            int[,] boardArray = new int[,] { { 1, 2, 2 }, { 1, 2, 1 }, { 0, 0, 2 } };
            PrintBoard(boardArray, 10);

            Console.ReadLine();
        }

        static void PrintBoard(int[,] boardArray, uint horizontalSpaces = 0) {
            //O tabuleiro deve ser 3x3
            if (boardArray.Length != 9) {
                Console.WriteLine("Número incorreto de posições do tabuleiro.");
                return;
            }

            //Coloca na variável os espaços que serão colocados ANTES de cada linha
            string preSpaces = "";
            for (int i = 0; i < horizontalSpaces; i++) {
                preSpaces += " ";
            }

            //Cada coluna do tabuleiro
            for (int i = 0; i < 3; i++) {
                //Facilita para obter o ID de cada posição
                int[] position = { boardArray[i, 0], boardArray[i, 1], boardArray[i, 2] };

                //Cada linha de cada "linha do tabuleiro"
                for (int j = 0; j < 4; j++) {
                    //Escreve os espaços antes de cada linha de texto
                    Console.Write(preSpaces);

                    for (int k = 0; k < 3; k++) {
                        //Obtem a cor da peça
                        ConsoleColor foregroundColor;
                        switch (position[k]) {
                            case 1:
                                foregroundColor = ConsoleColor.DarkRed;
                                break;
                            case 2:
                                foregroundColor = ConsoleColor.Green;
                                break;
                            default:
                                foregroundColor = ConsoleColor.White;
                                break;
                        }

                        ConsoleWriteText(GetSymbolString(position[k], j), foregroundColor, ConsoleColor.DarkGray);

                        if (k < 2) {
                            ConsoleWriteText(verticalLineString, ConsoleColor.White, ConsoleColor.DarkGray);
                        }
                    }

                    Console.WriteLine();
                }

                if (i < 2) {
                    Console.Write(preSpaces);
                    ConsoleWriteText(horizontalLineString, ConsoleColor.White, ConsoleColor.DarkGray);
                }

                Console.WriteLine();
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

        static void ConsoleWriteText(string text, ConsoleColor foregroundColor = ConsoleColor.White, ConsoleColor backgroundColor = ConsoleColor.Black) {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

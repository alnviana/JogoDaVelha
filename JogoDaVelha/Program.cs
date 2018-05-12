using System;

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
        readonly static string[] emptyStringArray = new string[] {      @"        ",
                                                                        @"        ",
                                                                        @"        ",
                                                                        @"        " };
        readonly static string verticalLineString = @"│";
        readonly static string horizontalLineString = @"────────┼────────┼────────";

        static void Main(string[] args) {
            Menu();
        }

        static void Menu() {
            string[] options = { "Jogadores reais", "Ações aleatórias", "Sair" };
            bool exit = false;
            do {
                Console.Clear();

                for (int i = 0; i < options.Length; i++) {
                    Console.WriteLine("{0} - {1}", i + 1, options[i]);
                }
                Console.Write("Escolha o modo de jogo: ");
                string choiceString = Console.ReadKey().KeyChar.ToString();
                Console.WriteLine();

                int.TryParse(choiceString, out int choiceNumber);
                switch (choiceNumber) {
                    case 1:
                        Game();
                        break;
                    case 2:
                        RandomGame();
                        break;
                    case 3:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        Console.ReadKey();
                        break;
                }
            } while (!exit);

                   
        }

        static void Game() {
            int[,] boardArray = new int[,] {    { 0, 0, 0 },
                                                { 0, 0, 0 },
                                                { 0, 0, 0 } };
            int currentPlayer = 1;
            do {
                Console.Clear();
                Console.WriteLine();
                PrintBoard(boardArray, 20);

                if (CheckStuck(boardArray)) {
                    Console.Write("Deu velha! Voltando ao menu... ");
                    Console.ReadLine();
                    return;
                }

                int horizontalChoice = 0;
                do {
                    if (currentPlayer == 1) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    } else {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write("Jogador {0} (X)", currentPlayer);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" - Digite a posição horizontal desejada (1-3): ");
                    string choiceString = Console.ReadLine();

                    int.TryParse(choiceString, out horizontalChoice);
                    if (horizontalChoice < 1 || horizontalChoice > 3) {
                        Console.WriteLine("Favor digitar um número entre 1 e 3. ");
                    }
                } while (horizontalChoice < 1 || horizontalChoice > 3);

                int verticalChoice = 0;
                do {
                    if (currentPlayer == 1) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    } else {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }

                    Console.Write("Jogador {0} (X)", currentPlayer);
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(" - Digite a posição vertical desejada (1-3): ");
                    string choiceString = Console.ReadLine();

                    int.TryParse(choiceString, out verticalChoice);
                    if (verticalChoice < 1 || verticalChoice > 3) {
                        Console.WriteLine("Favor digitar um número entre 1 e 3. ");
                    }
                } while (verticalChoice < 1 || verticalChoice > 3);

                if (boardArray[horizontalChoice - 1, verticalChoice - 1] != 0) {
                    Console.Write("Você não pode escolher uma posição que já possui uma peça! ");
                    Console.ReadLine();
                } else {
                    boardArray[horizontalChoice - 1, verticalChoice - 1] = currentPlayer;                    

                    if (currentPlayer == 1) {
                        currentPlayer++;
                    } else {
                        currentPlayer--;
                    }
                }
            } while (!CheckVictory(boardArray));

            Console.Clear();
            Console.WriteLine();
            PrintBoard(boardArray, 20);

            if (currentPlayer == 1) {
                currentPlayer++;
                Console.ForegroundColor = ConsoleColor.Green;
            } else {
                currentPlayer--;
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine("O jogador {0} foi o vencedor!", currentPlayer);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadLine();
        }

        static void RandomGame() {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            int[,] boardArray = new int[,] {    { 0, 0, 0 },
                                                { 0, 0, 0 },
                                                { 0, 0, 0 } };
            int currentPlayer = 1;
            do {
                Console.Clear();
                Console.WriteLine();
                PrintBoard(boardArray, 20);

                if (CheckStuck(boardArray)) {
                    Console.Write("Deu velha! Voltando ao menu... ");
                    Console.ReadLine();
                    return;
                }
                
                int horizontalChoice = random.Next(1, 4);
                int verticalChoice = random.Next(1, 4);

                if (boardArray[horizontalChoice - 1, verticalChoice - 1] == 0) {
                    boardArray[horizontalChoice - 1, verticalChoice - 1] = currentPlayer;

                    if (currentPlayer == 1) {
                        currentPlayer++;
                    } else {
                        currentPlayer--;
                    }
                }
            } while (!CheckVictory(boardArray));

            Console.Clear();
            Console.WriteLine();
            PrintBoard(boardArray, 20);

            if (currentPlayer == 1) {
                currentPlayer++;
                Console.ForegroundColor = ConsoleColor.Green;
            } else {
                currentPlayer--;
                Console.ForegroundColor = ConsoleColor.DarkRed;
            }
            Console.WriteLine("O jogador {0} foi o vencedor!", currentPlayer);
            Console.ForegroundColor = ConsoleColor.White;
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

        static bool CheckVictory(int[,] boardArray) {
            for (int i = 0; i < 3; i++) {
                if (boardArray[i, 0] != 0 && boardArray[i, 0] == boardArray[i, 1] && boardArray[i, 0] == boardArray[i, 2]) {
                    //Verifica se todos da linha "i" são iguais.
                    return true;
                } else if (boardArray[0, i] != 0 && boardArray[0, i] == boardArray[1, i] && boardArray[0, i] == boardArray[2, i]) {
                    //Verifica se todos da coluna "i" são iguais.
                    return true;
                }
            }

            if (boardArray[0, 0] != 0 && boardArray[0, 0] == boardArray[1, 1] && boardArray[0, 0] == boardArray[2, 2]) {
                //Verifica diagonal superior esquerda->inferior direita
                return true;
            } else if (boardArray[2, 0] != 0 && boardArray[2, 0] == boardArray[1, 1] && boardArray[2, 0] == boardArray[0, 2]) {
                //Verifica diagonal inferior esquerda->superior direita
                return true;
            }

            return false;
        }

        static bool CheckStuck(int[,] boardArray) {
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (boardArray[i, j] == 0) {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

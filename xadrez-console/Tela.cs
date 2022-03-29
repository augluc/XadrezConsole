
using System;
using System.Collections.Generic;
using xadrez_console.tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console
{
    internal class Tela
    {
        public static void ImprimirPartida(PartidaXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);

            Console.WriteLine();

            ImprimirPecasCapturadas(partida);

            Console.WriteLine("\n");

            Console.WriteLine($"Turno: {partida.Turno}\n" +
                $"Aguardando jogada das peças: {partida.JogadorAtual}");

            if (partida.Xeque)
                Console.WriteLine("\nXEQUE!");
        }

        public static void ImprimirPecasCapturadas(PartidaXadrez partida)
        {
            Console.WriteLine("Peças capturadas:");
            Console.Write("Brancas: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.Write("Pretas: ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach (Peca peca in conjunto)
            {
                Console.Write($"{peca} ");
            }
            Console.Write("]");
        }
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    ImprimirPeca(tab.Peca(i, j));
                }

                Console.WriteLine();
            }

            Console.WriteLine("  A B C D E F G H ");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] possiveisPosicoes)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.Yellow;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (possiveisPosicoes[i, j])
                        Console.BackgroundColor = fundoAlterado;
                    else
                        Console.BackgroundColor = fundoOriginal;
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }

                Console.WriteLine();
            }
            Console.BackgroundColor = fundoOriginal;

            Console.WriteLine("  A B C D E F G H ");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();

            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);
        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
                Console.Write("- ");
            else
            {
                if (peca.Cor == Cor.Branca)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(' ');
            }


        }
    }
}

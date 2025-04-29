using JogoXadrez3000.Xadrez;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.tabuleiro
{
    public class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);

            Console.WriteLine($"Turno: {partida.turno}");
            Console.WriteLine($"Aguardando jogador da peça {partida.jogadorAtual}");

            Console.WriteLine();
        }
        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças catpuradas:");
            Console.Write("Brancas:");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.Write("Pretas:");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
            
            Console.ForegroundColor= aux;
            Console.WriteLine();
        }
        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca p in conjunto) { Console.Write($"{p} "); }
            Console.Write("]\n");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    ImprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posiPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;


            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write($"{8 - i} ");
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (posiPossiveis[i, j]) 
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else 
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
            Console.BackgroundColor = fundoOriginal;
        }

        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");
           
            return new PosicaoXadrez(coluna, linha);

        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.cor == Cor.Branca)
                {
                    Console.Write(peca);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }

                Console.Write(" ");
            }
        }
    }
}

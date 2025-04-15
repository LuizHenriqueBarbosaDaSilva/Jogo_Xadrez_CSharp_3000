using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.tabuleiro
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i, j) != null)
                    {
                        Console.Write(tab.peca(i,j) +" ");
                    }
                    else
                    {
                        Console.Write("- ");
                    }
                    
                }
                Console.WriteLine();
            }
        }
    }
}

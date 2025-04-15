using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.tabuleiro
{
    public class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }

        private Peca[,] pecas;

        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }

        public Peca peca(int linhas, int colunas)
        {
            return pecas[linhas, colunas];
        }

        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }

        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos);
            return peca(pos) != null;
        }

        public void ColocarPeca(Peca pe, Posicao posi)
        {
            if (existePeca(posi))
            {
                throw new TabuleiroException("Ja existe uma peça de tabuleiro nessa posição!");
            }
            pecas[posi.linha, posi.coluna] = pe;
            pe.posicao = posi;
        }

        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.coluna > linhas || pos.coluna < 0 || pos.coluna > colunas) { return false; }
            return true;// Se a posição for verdadeira ela ira cortar o metodo de verificação assim não tendo a necessidade de else!  
        }

        public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição invalida!");
            }
        }
    }
}

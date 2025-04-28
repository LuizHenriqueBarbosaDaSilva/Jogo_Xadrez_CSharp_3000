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

        public void colocarPeca(Peca pe, Posicao posi)
        {
            if (existePeca(posi))
            {
                throw new TabuleiroException("Ja existe uma peça de tabuleiro nessa posição!");
            }
            pecas[posi.linha, posi.coluna] = pe;
            pe.posicao = posi;
        }
        public Peca retirarPeca(Posicao posi)
        {
            if (!existePeca(posi)) { return null; }
            Peca aux = peca(posi);

            aux.posicao = null;
            pecas[posi.linha,posi.coluna] = null;
            
            return aux;
        }

        public bool posicaoValida(Posicao pos)
        {
            return pos.linha >= 0 && pos.linha < linhas && pos.coluna >= 0 && pos.coluna < colunas;
        
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

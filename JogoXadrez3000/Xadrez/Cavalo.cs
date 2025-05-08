using JogoXadrez3000.tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.Xadrez
{
    class Cavalo : Peca
    {
        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor) { }
        public override string ToString()
        {
            return "C";
        }

        private bool podeMover(Posicao posi)
        {
            Peca pe = tab.peca(posi);
            return pe == null || pe.cor != cor;
        }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);


            // Duas linhas acima e uma coluna para a esquerda para o cavalo
            pos.definirValores(posicao.linha - 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Uma linhas acima e duas coluna para a esquerda para o cavalo
            pos.definirValores(posicao.linha - 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Uma linhas abaixo e duas coluna para a esquerda para o cavalo
            pos.definirValores(posicao.linha - 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Duas linhas abaixo e uma coluna para a esquerda para o cavalo
            pos.definirValores(posicao.linha - 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Duas linhas abaixo e uma coluna para a direita para o cavalo
            pos.definirValores(posicao.linha + 1, posicao.coluna + 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Uma linhas abaixo e uma coluna para a direita para o cavalo
            pos.definirValores(posicao.linha + 2, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Uma linhas acima e duas coluna para a direita para o cavalo
            pos.definirValores(posicao.linha + 2, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Duas linhas acima e uma coluna para a direita para o cavalo
            pos.definirValores(posicao.linha + 1, posicao.coluna - 2);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            return mat;
        }
    }
}


using JogoXadrez3000.tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.Xadrez
{
    class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }
        public override string ToString()
        {
            return "R";
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

            // Verificação a acima do rei!

            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a nordeste do rei!
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a direita do rei!
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a sudeste do rei!
            pos.definirValores(posicao.linha + 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a abaixo do rei!
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a sudoEste do rei!
            pos.definirValores(posicao.linha + 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a esquerda do rei!
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            // Verificação a noroeste do rei!
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }

            return mat;
        }
    }
}
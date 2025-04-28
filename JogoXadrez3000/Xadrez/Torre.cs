using JogoXadrez3000.tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.Xadrez
{
    class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }
        public override string ToString()
        {
            return "T";
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

            // Verificação a acima do Torre!
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }
            while(tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { break; }

                pos.linha = pos.linha - 1;
            }

            // Verificação a abaixo do Torre!
            pos.definirValores(posicao.linha + 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { break; }

                pos.linha = pos.linha + 1;
            }
            // Verificação a direita do Torre!
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { break; }

                pos.coluna = pos.coluna + 1;
            }
            // Verificação a esquerda do Torre!
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos)) { mat[pos.linha, pos.coluna] = true; }
            while (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
                if (tab.peca(pos) != null && tab.peca(pos).cor != cor) { break; }

                pos.coluna = pos.coluna - 1;
            }

            return mat;
        }
    }
}

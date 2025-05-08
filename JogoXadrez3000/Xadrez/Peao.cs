using JogoXadrez3000.tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.Xadrez
{
    class Peao : Peca
    {
        public Peao(Tabuleiro tab, Cor cor) : base(tab, cor) { }
        public override string ToString() { return "P"; }

        private bool podeMover(Posicao posi)
        {
            Peca pe = tab.peca(posi);
            return pe == null || pe.cor != cor;
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p.cor != cor;
        }

        private bool livre(Posicao pos) { return tab.peca(pos) == null; }

        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas, tab.colunas];

            Posicao pos = new Posicao(0, 0);

            if (cor == Cor.Branca) 
            {
                pos.definirValores(posicao.linha - 1, posicao.coluna); // Uma casa acima se for livre!
                if (tab.posicaoValida(pos) && livre(pos))
                {
                    mat[pos.linha,pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 2, posicao.coluna); // Duas casas acima! se estiver livre e a quanitdade de movimentos do peao for zero!
                if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna - 1); // Pode andar na diagonal se exister um inimigo! 
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                pos.definirValores(posicao.linha - 1, posicao.coluna + 1);  // Pode andar na diagonal se exister um inimigo! 
                if (tab.posicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.linha, pos.coluna] = true;
                }
                else
                {
                    pos.definirValores(posicao.linha + 1, posicao.coluna); // Uma casa abaixo se for livre!
                    if (tab.posicaoValida(pos) && livre(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }
                    pos.definirValores(posicao.linha + 2, posicao.coluna); // Duas casas abaixo! se estiver livre e a quanitdade de movimentos do peao for zero!
                    if (tab.posicaoValida(pos) && livre(pos) && qteMovimentos == 0)
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }
                    pos.definirValores(posicao.linha + 1, posicao.coluna - 1); // Pode andar na diagonal se exister um inimigo! 
                    if (tab.posicaoValida(pos) && existeInimigo(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }
                    pos.definirValores(posicao.linha + 1, posicao.coluna + 1);  // Pode andar na diagonal se exister um inimigo! 
                    if (tab.posicaoValida(pos) && existeInimigo(pos))
                    {
                        mat[pos.linha, pos.coluna] = true;
                    }
                }
            }

            return mat;
        }
    }
}

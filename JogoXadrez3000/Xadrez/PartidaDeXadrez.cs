using JogoXadrez3000.tabuleiro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();            
            
            colocarPeca();
        }

        public void executarMovimento(Posicao origem, Posicao destino)
        {
            Peca pe = tab.retirarPeca(origem);
            pe.incrementarQteMovimentos();

            Peca pecaCaputrada = tab.retirarPeca(destino);
            tab.colocarPeca(pe, destino);

            if (pecaCaputrada != null) { capturadas.Add(pecaCaputrada); }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            executarMovimento(origem,destino);
            turno++;
            MudaJogador();
        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null) { throw new TabuleiroException("Não existe peça na posição de origem escolhida!"); }
            if (tab.peca(pos).cor != jogadorAtual) { throw new TabuleiroException("A peça de origem foi escolhida não e sua!"); }
            if (!tab.peca(pos).ExisteMovimentosPossiveis()) { throw new TabuleiroException("Não há movimentos possíveis para a peça de origiem escolhida!"); }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino)) { throw new TabuleiroException("Posição de destino invalida!"); }
        }

        private void MudaJogador()
        {
            if (jogadorAtual == Cor.Branca) { jogadorAtual = Cor.Preta; }
            else { jogadorAtual = Cor.Branca;  }
        }
        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca x in capturadas) {
                if (x.cor == cor) { aux.Add(x); }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor) { aux.Add(x); }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;     
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).convertPosicao());
            pecas.Add(peca);
        }

        private void colocarPeca()
        {
            // PEÇAS BRANCAS EM NOVAS POSIÇÕES!
            colocarNovaPeca('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cor.Branca));

            // PEÇAS PRETAS EM NOVAS POSIÇÕES!

            colocarNovaPeca('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('d', 8, new Rei(tab, Cor.Preta));

        }

    }
}

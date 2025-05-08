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
        public bool xeque {  get; private set; }
        public Peca vulneralEnPassant { get; private set; } 

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;  
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();            
            
            colocarPeca();
        }

        public Peca executarMovimento(Posicao origem, Posicao destino)
        {
            Peca pe = tab.retirarPeca(origem);
            pe.incrementarQteMovimentos();

            Peca pecaCaputrada = tab.retirarPeca(destino);
            tab.colocarPeca(pe, destino);

            if (pecaCaputrada != null) { capturadas.Add(pecaCaputrada); }

            // #jogada especial roque pequeno!
            if (pe is Rei && destino.coluna == origem.coluna + 2) 
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(origemT);

                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT); 
            }

            // #jogada especial roque grande!
            if (pe is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(origemT);

                T.incrementarQteMovimentos();
                tab.colocarPeca(T, destinoT);
            }

            // #Jogada especial en passaant!
            if (pe is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCaputrada == null)
                {
                    Posicao posP;
                    if (pe.cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.linha +1,destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha -1,destino.coluna);
                    }
                    pecaCaputrada = tab.retirarPeca(posP);
                    capturadas.Add(pecaCaputrada);  
                }
            }

            return pecaCaputrada;
        }
        public void desfazMovimento(Posicao origem,Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimentos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada,destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p,origem);

            // #jogada especial roque pequeno!
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.retirarPeca(destinoT);

                T.decrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }

            // #jogada especial roque grande!
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.retirarPeca(destinoT);

                T.incrementarQteMovimentos();
                tab.colocarPeca(T, origemT);
            }
            // #Jogada especial en passaant!
            if (p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == vulneralEnPassant)
                {
                    Peca peao = tab.retirarPeca(destino);
                    Posicao posP;
                    if (p.cor == Cor.Branca) 
                    {
                        posP = new Posicao(3, destino.coluna);
                    }
                    else 
                    {
                        posP = new Posicao(4,destino.coluna);
                    }
                    tab.colocarPeca(peao, posP);

                    
                }
            }
        }
        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = executarMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))
            {
                desfazMovimento(origem,destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }
            Peca p = tab.peca(destino);
            // #Jogada especial promoção
            if (p is Peao)
            {
                if (p.cor == Cor.Branca && destino.linha == 0 || p.cor == Cor.Preta && destino.linha == 7)
                {
                    p = tab.retirarPeca(destino);
                    pecas.Remove(p);
                    Peca dama = new Dama(tab, p.cor);
                    tab.colocarPeca(dama, destino);
                    pecas.Add(dama);
                }
            }

            if (estaEmXeque(adversaria(jogadorAtual))) { xeque = true; } else { xeque=false; }
            if (testeXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                MudaJogador();
            }

            
            
            // #Jogada Especial En Passant!
            if (p is Peao && (destino.linha == origem.linha -2 || destino.linha == origem.linha + 2))
            {
                vulneralEnPassant = p;
            }
            else
            {
                vulneralEnPassant = null;
            }
                

        }
        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null) { throw new TabuleiroException("Não existe peça na posição de origem escolhida!"); }
            if (tab.peca(pos).cor != jogadorAtual) { throw new TabuleiroException("A peça de origem foi escolhida não e sua!"); }
            if (!tab.peca(pos).ExisteMovimentosPossiveis()) { throw new TabuleiroException("Não há movimentos possíveis para a peça de origiem escolhida!"); }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).movimentosPossivel(destino)) { throw new TabuleiroException("Posição de destino invalida!"); }
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
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }
        private Peca Rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }
        public bool estaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);  
            if (R == null)
            {
                throw new TabuleiroException($"Não tem bispo da cor {cor} no tabuleiro!"); // Esse erro nao deveria acontecer mas por questões de segurança foi impletado o Exception para esse caso!
            }

            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }
        public bool testeXequeMate(Cor cor)
        {
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach(Peca x in pecasEmJogo(cor)) {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for(int j = 0;j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executarMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna, linha).convertPosicao());
            pecas.Add(peca);
        }
        private void colocarPeca()
        {
            // Torres
            colocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            // Cavalos
            colocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            // Bispos
            colocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            // Dama e Rei
            colocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            // Peões pretos (linha 7)
            for (char col = 'a'; col <= 'h'; col++) // Funciona por que o characther e tratado como unicode!
            {
                colocarNovaPeca(col, 7, new Peao(tab, Cor.Preta, this));
            }

            // PEÇAS PRETAS EM NOVAS POSIÇÕES!
            // Torres
            colocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            // Cavalos
            colocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            // Bispos
            colocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            // Dama e Rei
            colocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            // Peões brancos (linha 2)
            for (char col = 'a'; col <= 'h'; col++)
            {
                colocarNovaPeca(col, 2, new Peao(tab, Cor.Branca, this));
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JogoXadrez3000.tabuleiro
{
    public class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public int qteMovimentos { get; protected set; }

        public Peca(Posicao posicao, Cor cor, Tabuleiro tab, int qteMovimentos)
        {
            this.posicao = posicao;
            this.cor = cor;
            this.tab = tab;
            this.qteMovimentos = 0;
        }

        public override string ToString()
        {
            return "Isso e um teste SSSSSSSSSSSSSSSSSSSS";
        }
    }
}
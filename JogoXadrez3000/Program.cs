/* Feito por Luiz Henrique Barbosa da Silva no curso de CSharp por Nelio Alves UDEMY */
using JogoXadrez3000.tabuleiro;
using JogoXadrez3000.Xadrez;

//Posição do tabuleiro!
Tabuleiro tab = new Tabuleiro(8, 8);
Tela.imprimirTabuleiro(tab);

tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));



Console.WriteLine("Impressão da tela!");
Tela.imprimirTabuleiro(tab);
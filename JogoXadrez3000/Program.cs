/* Feito por Luiz Henrique Barbosa da Silva no curso de CSharp por Nelio Alves UDEMY */
using JogoXadrez3000.tabuleiro;
using JogoXadrez3000.Xadrez;

//Posição do tabuleiro!
try {
    PartidaDeXadrez partida = new PartidaDeXadrez();
    while (!partida.terminada)
    {
        Console.Clear();
        Tela.imprimirTabuleiro(partida.tab);

        Console.Write("Origem: ");
        Posicao origem = Tela.lerPosicaoXadrez().convertPosicao();

        bool[,] posicoesPossives = partida.tab.peca(origem).movimentosPossiveis();

        Console.Clear();
        Tela.imprimirTabuleiro(partida.tab, posicoesPossives);

        Console.WriteLine();
        Console.Write("Destino: ");
        Posicao destino = Tela.lerPosicaoXadrez().convertPosicao();

        partida.executarMovimento(origem, destino);
    }
}
catch (TabuleiroException e)
{
    Console.WriteLine(e.Message);
}
Console.ReadLine();
using xadrez_console.tabuleiro;
using xadrez_console.xadrez;

namespace xadrez_console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Tabuleiro tab = new Tabuleiro(8, 8);

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(2, 4));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(6, 4));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(7, 0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(7, 5));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(7, 7));

            Tela.ImprimirTabuleiro(tab);
        }
    }
}

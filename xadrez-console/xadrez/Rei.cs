using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        public override string ToString()
        {
            return "R";
        }
    }
}

using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Rei : Peca
    {
        public Rei(Tabuleiro tab, Cor cor) : base(cor, tab)
        {

        }

        private bool MovimentoPossivel(Posicao pos)
        {
            Peca peca = Tabuleiro.Peca(pos);

            return peca == null || peca.Cor != Cor;
        }
        public override bool[,] MovimentosPossiveis()
        {
            bool[,] ret = new bool[Tabuleiro.Linhas, Tabuleiro.Colunas];

            Posicao pos = new Posicao(0, 0);

            //Mov N
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov NE
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov L
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov SE
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov S
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov SO
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov O
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }
            //Mov NO
            pos.DefinirPosicao(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;
            }

            return ret;
        }

        public override string ToString()
        {
            return "R";
        }
    }
}

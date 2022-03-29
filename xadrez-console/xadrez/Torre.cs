using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor) : base(cor, tab)
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
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
                    break;

                pos.Linha -= 1;
            }
            //Mov S
            pos.DefinirPosicao(Posicao.Linha + 1, Posicao.Coluna);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
                    break;

                pos.Linha += 1;
            }
            //Mov L
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna + 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
                    break;

                pos.Coluna += 1;
            }
            //Mov O
            pos.DefinirPosicao(Posicao.Linha, Posicao.Coluna - 1);
            while (Tabuleiro.PosicaoValida(pos) && MovimentoPossivel(pos))
            {
                ret[pos.Linha, pos.Coluna] = true;

                if (Tabuleiro.Peca(pos) != null && Tabuleiro.Peca(pos).Cor != Cor)
                    break;

                pos.Coluna -= 1;
            }

            return ret;
        }
        public override string ToString()
        {
            return "T";
        }
    }
}

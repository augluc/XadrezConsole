using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xadrez_console.tabuleiro;

namespace xadrez_console.xadrez
{
    internal class PartidaXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;


        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RemoverPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
                pecasCapturadas.Add(pecaCapturada);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            Turno++;
            ProximoJogador();

        }

        public void ValidarOrigem(Posicao origem)
        {
            if (Tab.Peca(origem) == null)
                throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
            if (JogadorAtual != Tab.Peca(origem).Cor)
                throw new TabuleiroException("A peça da posição de origem não te pertence.");
            if (!Tab.Peca(origem).ExisteMovimentosPossiveis())
                throw new TabuleiroException("Não há movimentos possíveis para essa peça.");
        }

        public void ValidarDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).PodeMover(destino))
                throw new TabuleiroException("Posição inválida!");
        }

        private void ProximoJogador()
        {
            if (JogadorAtual == Cor.Branca)
                JogadorAtual = Cor.Preta;
            else
                JogadorAtual = Cor.Branca;
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> ret = new HashSet<Peca>();
            foreach (Peca peca in pecasCapturadas)
            {
                if (peca.Cor == cor)
                    ret.Add(peca);
            }

            return ret;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> ret = new HashSet<Peca>();

            foreach (Peca peca in pecas)
            {
                if (peca.Cor == cor)
                    ret.Add(peca);
            }

            ret.ExceptWith(PecasCapturadas(cor));

            return ret;
        }
        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 4, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rei(Tab, Cor.Branca));

            ColocarNovaPeca('c', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 5, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rei(Tab, Cor.Preta));
        }
    }
}

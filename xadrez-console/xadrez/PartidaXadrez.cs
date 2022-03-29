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
        public bool Xeque { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> pecasCapturadas;


        public PartidaXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Xeque = false;
            Terminada = false;
            pecas = new HashSet<Peca>();
            pecasCapturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca peca = Tab.RemoverPeca(origem);
            peca.IncrementarQuantidadeMovimentos();
            Peca pecaCapturada = Tab.RemoverPeca(destino);
            Tab.ColocarPeca(peca, destino);

            if (pecaCapturada != null)
                pecasCapturadas.Add(pecaCapturada);

            return pecaCapturada;
        }

        public void DesfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca peca = Tab.RemoverPeca(destino);
            peca.DecrementarQuantidadeMovimentos();

            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                pecasCapturadas.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(peca, origem);
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazerMovimento(origem, destino, pecaCapturada);

                throw new TabuleiroException("Não pode!\nNão pode se colocar em Xeque!");
            }

            if (EstaEmXeque(Adversario(JogadorAtual)))
                Xeque = true;
            else
                Xeque = false;

            if (TesteXequemate(Adversario(JogadorAtual)))
                Terminada = true;
            else
            {
                Turno++;
                ProximoJogador();
            }
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

        private Cor Adversario(Cor cor)
        {
            if (cor == Cor.Branca)
                return Cor.Preta;
            else
                return Cor.Branca;
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca peca in PecasEmJogo(cor))
            {
                if (peca is Rei)
                    return peca;
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca rei = Rei(cor);

            if (rei == null)
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro.");

            foreach (Peca peca in PecasEmJogo(Adversario(cor)))
            {
                bool[,] ret = peca.MovimentosPossiveis();

                if (ret[rei.Posicao.Linha, rei.Posicao.Coluna])
                    return true;
            }

            return false;
        }

        public bool TesteXequemate(Cor cor)
        {
            if (!EstaEmXeque(cor))
                return false;
            foreach (Peca peca in PecasEmJogo(cor))
            {
                bool[,] mat = peca.MovimentosPossiveis();

                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = peca.Posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazerMovimento(origem, destino, pecaCapturada);

                            if (!testeXeque)
                                return false;
                        }
                    }
                }

            }

            return true;
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

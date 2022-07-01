using JogoDaVelha.Enumeradores;

namespace JogoDaVelha.Models
{
    public class Tabuleiro
    {
        #region Constantes
        public const int DimensaoTabuleiro = 3;
        public const int UltimoIndiceDimensaoTabuleiro = 2;

        private const string Espaco = " ";
        private const string SeparadorColunaTabuleiro = "|";
        private const string SeparadorLinhaTabuleiro = "-----------";
        #endregion

        public string[,] Matriz { get; private set; }

        public Tabuleiro()
        {
            Matriz = new string[DimensaoTabuleiro, DimensaoTabuleiro];
            PopularTabuleiroComCargaInicial();
        }

        public void IncluirJogada(int linha, int coluna, SimboloJogada simboloJogada)
        {
            Matriz[linha, coluna] = Enum.GetName(typeof(SimboloJogada), simboloJogada);
        }

        public string ObterValorPosicao(int indiceLinha, int indiceColuna)
        {
            return Matriz[indiceLinha, indiceColuna];
        }

        public string FormatarTabuleiroParaImpressao()
        {
            string impressao = string.Empty;

            for (int indiceLinha = 0; indiceLinha <= UltimoIndiceDimensaoTabuleiro; indiceLinha++)
            {
                for (int indiceColuna = 0; indiceColuna <= UltimoIndiceDimensaoTabuleiro; indiceColuna++)
                {
                    impressao += Espaco + this.Matriz[indiceLinha, indiceColuna];

                    impressao += indiceColuna < UltimoIndiceDimensaoTabuleiro
                        ? Espaco + SeparadorColunaTabuleiro
                        : Environment.NewLine;
                }

                if (indiceLinha < UltimoIndiceDimensaoTabuleiro)
                {
                    impressao += SeparadorLinhaTabuleiro;
                    impressao += Environment.NewLine;
                }
            }

            return impressao;
        }

        public void PopularTabuleiroComCargaInicial()
        {
            for (int indiceLinha = 0; indiceLinha <= UltimoIndiceDimensaoTabuleiro; indiceLinha++)
            {
                for (int indiceColuna = 0; indiceColuna <= UltimoIndiceDimensaoTabuleiro; indiceColuna++)
                {
                    Matriz[indiceLinha, indiceColuna] = Espaco;
                }
            }
        }
    }
}
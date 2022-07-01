using JogoDaVelha.Enumeradores;
using JogoDaVelha.Models;

namespace JogoDaVelha.Servicos
{
    public class ServicoDeJogo
    {
        #region Constantes
        public const SimboloJogada SimboloJogadaJogador1 = SimboloJogada.X;
        public const SimboloJogada SimboloJogadaJogador2 = SimboloJogada.O;
        private const int QuantidadeMinimaDeJogadasParaOcorrerVitoria = 5;
        #endregion

        #region Atributos de controle
        private SimboloJogada? _simboloUltimaJogada { get; set; }
        private int _quantidadesJogadasPartidaAtual { get; set; }
        #endregion

        public Tabuleiro Tabuleiro { get; private set; }
        public ICollection<Jogador> Jogadores { get; private set; }

        public ServicoDeJogo(string nomeJogador1, string nomeJogador2)
        {
            Tabuleiro = new Tabuleiro();
            Jogadores = new List<Jogador>()
            {
                new Jogador(nomeJogador1, SimboloJogadaJogador1),
                new Jogador(nomeJogador2, SimboloJogadaJogador2)
            };

            _simboloUltimaJogada = null;
            _quantidadesJogadasPartidaAtual = 0;
        }

        public string ObterTabuleiroParaImpressao()
        {
            return Tabuleiro.FormatarTabuleiroParaImpressao();
        }

        public Jogador ObterJogadorVencedorPorSituacaoJogo(SituacaoJogo situacao)
        {
            if (situacao == SituacaoJogo.EmAndamento || situacao == SituacaoJogo.Empate)
            {
                return null;
            }

            var simboloJogadaJogadorVencedor = situacao == SituacaoJogo.VitoriaJogador1
                ? SimboloJogadaJogador1
                : SimboloJogadaJogador2;

            return ObterJogadorPorSimboloJogada(simboloJogadaJogadorVencedor);
        }

        public string ObterNomeJogadorPorSimboloJogada(SimboloJogada simboloJogada)
        {
            return ObterJogadorPorSimboloJogada(simboloJogada)?.Nome ?? string.Empty;
        }

        public Jogador ObterJogadorPorSimboloJogada(SimboloJogada simboloJogada)
        {
            return Jogadores.FirstOrDefault(j => j.SimboloJogada == simboloJogada);
        }

        public Jogador ObterJogadorPorSimboloJogadaString(string simboloJogada)
        {
            return Jogadores.FirstOrDefault(j => j.ObterSimboloJogada().Equals(simboloJogada));
        }

        public void IncluirJogada(int indiceLinha, int indiceColuna, SimboloJogada simboloJogada)
        {
            ValidarParametros(indiceLinha, indiceColuna, simboloJogada);

            Tabuleiro.IncluirJogada(indiceLinha, indiceColuna, simboloJogada);

            _quantidadesJogadasPartidaAtual++;
            _simboloUltimaJogada = simboloJogada;
        }

        public SituacaoJogo ObterSituacaoJogo()
        {
            var situacaoJogo = SituacaoJogo.EmAndamento;

            if (_quantidadesJogadasPartidaAtual >= QuantidadeMinimaDeJogadasParaOcorrerVitoria)
            {
                var jogadorVencedor = VerificarVencedor();
                
                if (jogadorVencedor != null)
                {
                    situacaoJogo = jogadorVencedor.SimboloJogada == SimboloJogadaJogador1
                        ? SituacaoJogo.VitoriaJogador1
                        : SituacaoJogo.VitoriaJogador2;
                }
                else
                {
                    var todasPosicoesPreenchidas = VerificarSeTodasPosicoesTabuleiroForamPreenchidas();
                    if (todasPosicoesPreenchidas)
                    {
                        situacaoJogo = SituacaoJogo.Empate;
                    }
                }
            }

            AtualizarPlacarJogadores(situacaoJogo);

            return situacaoJogo;
        }

        public void ReiniarJogo()
        {
            _simboloUltimaJogada = null;
            _quantidadesJogadasPartidaAtual = 0;

            Tabuleiro.PopularTabuleiroComCargaInicial();
        }

        private Jogador VerificarVencedor()
        {
            var linhaAnalisada = new string[Tabuleiro.DimensaoTabuleiro];
            var colunaAnalisada = new string[Tabuleiro.DimensaoTabuleiro];
            var diagonalPrincipal = new string[Tabuleiro.DimensaoTabuleiro];
            var diagonalSecundaria = new string[Tabuleiro.DimensaoTabuleiro];

            string valorPosicaoIndices;
            string valorPosicaoIndicesInvertidos;
            string simboloJogadaVencedor = null;

            for (int indiceLinha = 0; indiceLinha < Tabuleiro.DimensaoTabuleiro; indiceLinha++)
            {
                for (int indiceColuna = 0; indiceColuna < Tabuleiro.DimensaoTabuleiro; indiceColuna++)
                {
                    valorPosicaoIndices = Tabuleiro.ObterValorPosicao(indiceLinha, indiceColuna);
                    valorPosicaoIndicesInvertidos = Tabuleiro.ObterValorPosicao(indiceColuna, indiceLinha);

                    linhaAnalisada[indiceColuna] = valorPosicaoIndices;
                    colunaAnalisada[indiceColuna] = valorPosicaoIndicesInvertidos;

                    if (indiceLinha == indiceColuna)
                    {
                        diagonalPrincipal[indiceLinha] = valorPosicaoIndices;
                    }

                    if (indiceLinha + indiceColuna == Tabuleiro.UltimoIndiceDimensaoTabuleiro)
                    {
                        diagonalSecundaria[indiceLinha] = valorPosicaoIndices;
                    }
                }

                simboloJogadaVencedor = VerificaSeTodosValoresIguaisPorArray(linhaAnalisada, colunaAnalisada);
                if (simboloJogadaVencedor != null)
                {
                    break;
                }
            }

            if (simboloJogadaVencedor == null)
            {
                simboloJogadaVencedor = VerificaSeTodosValoresIguaisPorArray(diagonalPrincipal, diagonalSecundaria);
            }

            var jogadorVencedor = simboloJogadaVencedor != null
                ? ObterJogadorPorSimboloJogadaString(simboloJogadaVencedor)
                : null;

            return jogadorVencedor;
        }

        private bool VerificarSeTodasPosicoesTabuleiroForamPreenchidas()
        {
            var todasPosicoesPreenchidas = true;

            for (int indiceLinha = 0; indiceLinha < Tabuleiro.DimensaoTabuleiro; indiceLinha++)
            {
                for (int indiceColuna = 0; indiceColuna < Tabuleiro.DimensaoTabuleiro; indiceColuna++)
                {
                    if (string.IsNullOrWhiteSpace(Tabuleiro.ObterValorPosicao(indiceLinha, indiceColuna)))
                    {
                        todasPosicoesPreenchidas = false;
                    }
                }
            }

            return todasPosicoesPreenchidas;
        }

        private void AtualizarPlacarJogadores(SituacaoJogo situacao)
        {
            if (situacao == SituacaoJogo.EmAndamento)
            {
                return;
            }

            if (situacao == SituacaoJogo.Empate)
            {
                foreach (var jogador in Jogadores)
                {
                    jogador.IncrementarEmpates();
                }
            }
            else
            {
                var simboloJogadaJogadorVencedor = situacao == SituacaoJogo.VitoriaJogador1
                    ? SimboloJogadaJogador1
                    : SimboloJogadaJogador2;

                foreach (var jogador in Jogadores)
                {
                    if (jogador.SimboloJogada == simboloJogadaJogadorVencedor)
                    {
                        jogador.IncrementarVitorias();
                    }
                    else
                    {
                        jogador.IncrementarDerrotas();
                    }
                }
            }
        }

        private void ValidarParametros(int indiceLinha, int indiceColuna, SimboloJogada simboloJogada)
        {
            var ultimoIndiceDimensaoTabuleiro = Tabuleiro.UltimoIndiceDimensaoTabuleiro;

            if (indiceLinha < 0 || indiceLinha > ultimoIndiceDimensaoTabuleiro)
            {
                var mensagem = ServicoDeMensagem.ObterMensagemSubstituindoMacro(ServicoDeMensagem.MensagemIndiceLinhaInvalido, ultimoIndiceDimensaoTabuleiro.ToString());
                throw new ArgumentOutOfRangeException(mensagem);
            }

            if (indiceColuna < 0 || indiceColuna > ultimoIndiceDimensaoTabuleiro)
            {
                var mensagem = ServicoDeMensagem.ObterMensagemSubstituindoMacro(ServicoDeMensagem.MensagemIndiceColunaInvalido, ultimoIndiceDimensaoTabuleiro.ToString());
                throw new ArgumentOutOfRangeException(mensagem);
            }

            if (!Enum.IsDefined(typeof(SimboloJogada), simboloJogada))
            {
                throw new ArgumentException(ServicoDeMensagem.MensagemSimboloJogadaInvalido);
            }

            if (simboloJogada == _simboloUltimaJogada)
            {
                throw new ArgumentException(ServicoDeMensagem.MensagemJogadaConsecutivaInvalida);
            }

            if (!string.IsNullOrWhiteSpace(Tabuleiro.ObterValorPosicao(indiceLinha, indiceColuna)))
            {
                throw new ArgumentException(ServicoDeMensagem.MensagemPosicaoTabuleiroJaOcupada);
            }
        }

        private string VerificaSeTodosValoresIguaisPorArray(params string[][] arrays)
        {
            if (VerificaSeTodosArraysPossuemMesmoTamanho(arrays) == false)
            {
                return null;
            }

            var tamanhoDosArrays = arrays[0].Length;
            var quantidadesComparacoesSucessoPorArray = new int[arrays.Length];
            var quantidadeMinimaComparacoesSucessoPorArray = tamanhoDosArrays - 1;

            for (int posArrayAnalisado = 0; posArrayAnalisado < arrays.Length; posArrayAnalisado++)
            {
                for (int posAnalisada = tamanhoDosArrays - 1; posAnalisada > 0; posAnalisada--)
                {
                    if (arrays[posArrayAnalisado][posAnalisada] == arrays[posArrayAnalisado][posAnalisada - 1] 
                        && !string.IsNullOrWhiteSpace(arrays[posArrayAnalisado][posAnalisada]))
                    {
                        quantidadesComparacoesSucessoPorArray[posArrayAnalisado]++;
                    }
                }
            }

            string elementoDoArrayComTodosValoresIguais = null;
            for (int posArrayAnalisado = 0; posArrayAnalisado < arrays.Length; posArrayAnalisado++)
            {
                if (quantidadesComparacoesSucessoPorArray[posArrayAnalisado] == quantidadeMinimaComparacoesSucessoPorArray)
                {
                    elementoDoArrayComTodosValoresIguais = arrays[posArrayAnalisado][0];
                    break;
                }
            }

            return elementoDoArrayComTodosValoresIguais;
        }

        private bool VerificaSeTodosArraysPossuemMesmoTamanho(params string[][] arrays)
        {
            var todosArraysComMesmoTamanho = true;

            for (int i = arrays.Length - 1; i > 0; i--)
            {
                if (arrays[i].Length != arrays[i - 1].Length)
                {
                    todosArraysComMesmoTamanho = false;
                    break;
                }
            }

            return todosArraysComMesmoTamanho;
        }
    }
}
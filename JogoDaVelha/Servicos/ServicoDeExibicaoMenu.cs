using JogoDaVelha.Enumeradores;
using JogoDaVelha.Models;

namespace JogoDaVelha.Servicos
{
    public class ServicoDeExibicaoMenu
    {
        #region Constantes
        private const int CodigoErroGeral = -1;
        private const int DescontoUnidadeMedida = 1;
        private const int IdentificadorJogador1 = 1;
        private const int IdentificadorJogador2 = 2;
        private const int MargemPadrao = 50;
        private const int OpcaoJogarNovamente = 1;
        private const int TempoEsperaPadraoMenu = 5000;
        private const int TempoEsperaPadraoErro = 3500;
        private const int TamanhoMinimoNomeJogador = 3;
        private const int ValorJogadaLinhaColunaMinimo = 1;
        private const int ValorMinimoOpcaoMenu = 1;
        private const int ValorMaximoOpcaoMenu = 2;
        private const bool PermiteNomeJogadorComNumeros = false;
        #endregion

        public void CarregarAplicativo()
        {
            try
            {
                ExibirTelaInicializacao();

                ExibirTelaMenuPrincipal();

                ExibirTelaEncerramento();
            }
            catch (Exception)
            {
                ExibirTelaErro();
            }
        }

        private static void ExibirTelaInicializacao()
        {
            var tituloInicializacao = ServicoDeMensagem.TituloTelaInicializacao.PadLeft(MargemPadrao);
            Console.WriteLine(tituloInicializacao);

            PularLinha();
            Console.Write(ServicoDeMensagem.MensagemCarregando);

            RealizarEsperaImplicita(TempoEsperaPadraoMenu);
        }

        #region Telas

        private static void ExibirTelaMenuPrincipal()
        {
            var permanecerNaTelaMenuPrincipal = true;
            while(permanecerNaTelaMenuPrincipal)
            {
                LimparTela();

                var tituloMenuPrincipal = ServicoDeMensagem.TituloMenuPrincipal.PadLeft(MargemPadrao);
                Console.WriteLine(tituloMenuPrincipal);

                PularLinha();

                var mensagemOpcoes = ServicoDeMensagem.MensagemOpcoesMenuPrincipal;
                Console.Write(mensagemOpcoes);

                var opcaoSelecionada = ServicoDeValidacaoEntradaUsuario.ObterEntradaValorInteiro(
                    ValorMinimoOpcaoMenu, 
                    ValorMaximoOpcaoMenu, 
                    ServicoDeMensagem.MensageMenuPrincipalEntradaInvalida);

                switch (opcaoSelecionada)
                {
                    case 1:
                        ExibirTelaJogo();
                        break;
                    case 2:
                        permanecerNaTelaMenuPrincipal = false;
                        break;
                    case 3:
                        throw new ApplicationException();
                }
            }
        }

        private static void ExibirTelaJogo()
        {
            LimparTela();
            ImprimirTituloTelaJogo();

            PularLinha();

            string nomeJogador1 = SolicitarNomeJogador(IdentificadorJogador1);
            string nomeJogador2 = SolicitarNomeJogador(IdentificadorJogador2);

            ServicoDeJogo servicoDeJogo = new ServicoDeJogo(nomeJogador1, nomeJogador2);

            int totalJogadas = 0;
            bool jogoEmAndamento = true;
            SimboloJogada simboloJogadaJogadorAtual;
            while (jogoEmAndamento)
            {
                ImprimirTituloTelaJogoComTabuleiro(servicoDeJogo.ObterTabuleiroParaImpressao());

                simboloJogadaJogadorAtual = ObterIdentificadorJogadorAtual(totalJogadas) == IdentificadorJogador1
                    ? SimboloJogada.X
                    : SimboloJogada.O;

                var mensagemIndicativaVezJogador = ServicoDeMensagem.ObterMensagemSubstituindoMacro(
                    ServicoDeMensagem.MensagemIndicativaVezJogador,
                    servicoDeJogo.ObterNomeJogadorPorSimboloJogada(simboloJogadaJogadorAtual));

                Console.WriteLine(mensagemIndicativaVezJogador);
                int indiceLinhaJogada = SolicitarJogada(ServicoDeMensagem.MensagemSolicitacaoLinhaJogada, true);
                int indiceColunaJogada = SolicitarJogada(ServicoDeMensagem.MensagemSolicitacaoColunaJogada, true);

                try
                {
                    servicoDeJogo.IncluirJogada(indiceLinhaJogada, indiceColunaJogada, simboloJogadaJogadorAtual);
                    totalJogadas++;
                }
                catch(Exception ex)
                {
                    var mensagemErro = ServicoDeMensagem.ObterMensagemSubstituindoMacro(
                        ServicoDeMensagem.MensagemDeExceptionInterna, 
                        ex.Message);

                    Console.WriteLine(mensagemErro);

                    RealizarEsperaImplicita(TempoEsperaPadraoErro);
                }

                var situacaoJogo = servicoDeJogo.ObterSituacaoJogo();

                if (situacaoJogo != SituacaoJogo.EmAndamento)
                {
                    jogoEmAndamento = false;
                    ImprimirTituloTelaJogoComTabuleiro(servicoDeJogo.ObterTabuleiroParaImpressao());

                    if (situacaoJogo == SituacaoJogo.VitoriaJogador1
                        || situacaoJogo == SituacaoJogo.VitoriaJogador2)
                    {
                        var jogadorVencedor = servicoDeJogo.ObterJogadorVencedorPorSituacaoJogo(situacaoJogo);

                        var informacoesJogadorVencedor = jogadorVencedor.Informacoes();

                        var mensagemVitoria = ServicoDeMensagem.ObterMensagemSubstituindoMacro(
                            ServicoDeMensagem.MensagemJogoVitoria,
                            jogadorVencedor.Nome);

                        Console.WriteLine(mensagemVitoria);
                        PularLinha();
                        Console.WriteLine(informacoesJogadorVencedor);
                    }
                    else if (situacaoJogo == SituacaoJogo.Empate)
                    {
                        Console.WriteLine(ServicoDeMensagem.MensagemJogoEmpate);
                    }

                    RealizarEsperaImplicita(TempoEsperaPadraoMenu);
                }

                if (!jogoEmAndamento)
                {
                    PularLinha();

                    Console.Write(ServicoDeMensagem.MensagemOpcaoMenuJogo);

                    var opcaoSelecionada = ServicoDeValidacaoEntradaUsuario.ObterEntradaValorInteiro(
                        ValorMinimoOpcaoMenu, 
                        ValorMaximoOpcaoMenu, 
                        null);

                    if (opcaoSelecionada == OpcaoJogarNovamente)
                    {
                        totalJogadas = 0;
                        jogoEmAndamento = true;
                        servicoDeJogo.ReiniarJogo();
                    }
                }
            }
        }

        private static void ExibirTelaEncerramento()
        {
            LimparTela();

            var mensagem = ServicoDeMensagem.MensagemEncerramentoSistema;
            Console.Write(mensagem);

            Console.ReadKey();
        }

        private static void ExibirTelaErro()
        {
            LimparTela();

            var mensagem = ServicoDeMensagem.MensagemErroGeral;
            Console.WriteLine(mensagem);

            RealizarEsperaImplicita(TempoEsperaPadraoMenu);
            Environment.Exit(CodigoErroGeral);
        }

        #endregion

        #region Métodos auxiliares

        private static void ImprimirTituloTelaJogo()
        {
            var tituloTelaJogo = ServicoDeMensagem.TituloTelaJogo.PadLeft(MargemPadrao);
            Console.WriteLine(tituloTelaJogo);
        }

        private static void ImprimirTituloTelaJogoComTabuleiro(string tabuleiro)
        {
            LimparTela();
            ImprimirTituloTelaJogo();

            PularLinha();
            Console.WriteLine(tabuleiro);
        }

        private static string SolicitarNomeJogador(int identificadorJogador)
        {
            var mensagemSolicitacaoNomeJogador = ServicoDeMensagem.ObterMensagemSubstituindoMacro(
                ServicoDeMensagem.MensagemSolicitacaoNomeJogador, 
                identificadorJogador);

            var mensagemErroTamanhoMinimoNomeJogador = ServicoDeMensagem.ObterMensagemSubstituindoMacro(
                ServicoDeMensagem.MensagemTamanhoMinimoNomeJogador,
                TamanhoMinimoNomeJogador);
            
            Console.Write(mensagemSolicitacaoNomeJogador);

            string nomeJogador = ServicoDeValidacaoEntradaUsuario.ObterEntradaValorString(
                TamanhoMinimoNomeJogador,
                PermiteNomeJogadorComNumeros,
                mensagemErroTamanhoMinimoNomeJogador);

            return nomeJogador;
        }

        private static int SolicitarJogada(string mensagemSolicitacao, bool substituirMacroMensagemPorValoresMinimoMaximo)
        {
            mensagemSolicitacao = ServicoDeMensagem.ObterMensagemSubstituindoMacro(mensagemSolicitacao, ValorJogadaLinhaColunaMinimo);
            mensagemSolicitacao = ServicoDeMensagem.ObterMensagemSubstituindoMacro(mensagemSolicitacao, Tabuleiro.DimensaoTabuleiro);

            Console.Write(mensagemSolicitacao);

            int valorJogada = ServicoDeValidacaoEntradaUsuario.ObterEntradaValorInteiro(
                ValorJogadaLinhaColunaMinimo,
                Tabuleiro.DimensaoTabuleiro,
                null);

            int indiceJogada = valorJogada - DescontoUnidadeMedida;
            return indiceJogada;
        }

        private static int ObterIdentificadorJogadorAtual(int quantidadeJogadas)
        {
            return quantidadeJogadas % 2 == 0
                ? IdentificadorJogador1
                : IdentificadorJogador2;
        }

        private static void LimparTela()
        {
            Console.Clear();
        }

        private static void PularLinha()
        {
            Console.WriteLine();
        }

        private static void RealizarEsperaImplicita(int tempoEmMilisegundos)
        {
            Thread.Sleep(tempoEmMilisegundos);
        }

        #endregion
    }
}

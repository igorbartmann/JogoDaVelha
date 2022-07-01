using System;

namespace JogoDaVelha.Servicos
{
    public static class ServicoDeMensagem
    {
        #region Mensagens de validações de regras internas
        public const string MensagemIndiceLinhaInvalido = "O índice da linha é inválido. Selecione um valor entre 0 e {UltimoIndiceDimensaoTabuleiro}.";
        public const string MensagemIndiceColunaInvalido = "O índice da coluna é inválido. Selecione um valor entre 0 e {UltimoIndiceDimensaoTabuleiro}.";
        public const string MensagemSimboloJogadaInvalido = "O símbolo da jogada é inválido.";
        public const string MensagemJogadaConsecutivaInvalida = "Não é possível adicionar duas jogadas consecutivas para um mesmo jogador.";
        public const string MensagemPosicaoTabuleiroJaOcupada = "Já existe uma jogada na posição selecionada.";
        #endregion

        #region Mensagens das telas do sistema
        public const string TituloTelaInicializacao = "SEJA BEM-VINDO(A) AO JOGO DA VELHA DIGITAL!";
        public const string MensagemCarregando = "Carregando...";

        public const string TituloMenuPrincipal = "MENU PRINCIPAL";
        public const string MensagemOpcoesMenuPrincipal = "[1] - Jogar.\n[2] - Sair.\nDigite o valor correspondente a opção desejada: ";
        public const string MensagemOpcaoMenuJogo = "[1] - Sim.\n[2] - Não.\nDeseja jogar novamente?";

        public const string TituloTelaJogo = "JOGO DA VELHA";
        public const string MensagemSolicitacaoNomeJogador = "Digite o nome do jogador nº {NumeroJogador}: ";
        public const string MensagemIndicativaVezJogador = "Jogador {Jogador}, sua vez!";
        public const string MensagemSolicitacaoLinhaJogada = "Digite o número da linha da sua jogada (de {IndiceMinimo} a {IndiceMaximo}): ";
        public const string MensagemSolicitacaoColunaJogada = "Digite o número da coluna da sua jogada (de {IndiceMinimo} a {IndiceMaximo}): ";
        public const string MensagemJogoVitoria = "O jogador {NomeJogador} venceu!";
        public const string MensagemJogoEmpate = "Houve um empate!";

        public const string MensagemErroGeral = "Um erro geral ocorreu. Contate o administrador do sistema!";
        public const string MensagemDeExceptionInterna = "Erro! {ExceptionMessage} - Jogue novamente!";
        public const string MensagemPadraoEntradaInvalida = "O valor informado é inválido. Tente novamente: ";
        public const string MensagemTamanhoMinimoNomeJogador = "Nome inválido! O nome do jogador deve ter no mínimo {TamanhoNomeJogador} caractéres e, dependendo da configuração, pode não aceitar números.\nDigite novamente: ";
        public const string MensageMenuPrincipalEntradaInvalida = "O valor informado não corresponde a nenhuma opção disponível.\nDigite 1 para jogar ou 2 para sair: ";

        public const string MensagemEncerramentoSistema = "Obrigado por jogar no nosso sistema.\nPara sair, clique em qualquer tecla.";
        #endregion

        public static string ObterMensagemSubstituindoMacro(string mensagem, int valor)
        {
            return ObterMensagemSubstituindoMacro(mensagem, valor.ToString());
        }

        public static string ObterMensagemSubstituindoMacro(string mensagem, string valor)
        {
            var indiceInicioMacro = mensagem.IndexOf("{");
            var indiceFimMacro = mensagem.IndexOf("}");
            var comprimentoMacro = (indiceFimMacro - indiceInicioMacro) + 1;

            var substringMacro = mensagem.Substring(indiceInicioMacro, comprimentoMacro);
            var mensagemFormatada = mensagem.Replace(substringMacro, valor);

            return mensagemFormatada;
        }
    }
}
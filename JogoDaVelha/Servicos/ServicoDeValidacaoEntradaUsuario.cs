using System;

namespace JogoDaVelha.Servicos
{
    public static class ServicoDeValidacaoEntradaUsuario
    {
        public static int ObterEntradaValorInteiro(int valorMinimo, int valorMaximo, string mensagemRevalidacao)
        {
            int valorEntrada = valorMinimo - 1;
            bool entradaComSucesso = false;

            while (!entradaComSucesso)
            {
                if (int.TryParse(Console.ReadLine(), out valorEntrada)
                    && valorEntrada >= valorMinimo 
                    && valorEntrada <= valorMaximo)
                {
                    entradaComSucesso = true;
                }
                else
                {
                    entradaComSucesso = false;

                    var mensagem = mensagemRevalidacao ?? ServicoDeMensagem.MensagemPadraoEntradaInvalida;
                    Console.Write(mensagem);
                }
            }

            return valorEntrada;
        }

        public static string ObterEntradaValorString(int comprimentoMinimo, bool permiteNumeros, string mensagemRevalidacao)
        {
            string valorEntrada = string.Empty;
            bool entradaComSucesso = false;

            while(!entradaComSucesso)
            {
                valorEntrada = Console.ReadLine();

                if (valorEntrada != null 
                    && valorEntrada.Length >= comprimentoMinimo
                    && (permiteNumeros || valorEntrada.All(c => !char.IsDigit(c))))
                {
                    entradaComSucesso = true;
                }
                else
                {
                    entradaComSucesso = false;

                    var mensagem = mensagemRevalidacao ?? ServicoDeMensagem.MensagemPadraoEntradaInvalida;
                    Console.Write(mensagem);
                }
            }

            return valorEntrada;
        }
    }
}

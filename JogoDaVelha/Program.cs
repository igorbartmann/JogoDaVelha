using JogoDaVelha.Servicos;

namespace JogoDaVelha
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServicoDeExibicaoMenu servicoDeExibicaoMenu = new ServicoDeExibicaoMenu();

            servicoDeExibicaoMenu.CarregarAplicativo();
        }
    }
}
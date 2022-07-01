using JogoDaVelha.Enumeradores;

namespace JogoDaVelha.Models
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public SimboloJogada SimboloJogada { get; private set; }
        public int QuantidadeVitorias { get; private set; }
        public int QuantidadeEmpates { get; private set; }
        public int QuantidadeDerrotas { get; private set; }

        public Jogador(string nome, SimboloJogada simboloJogada)
        {
            Nome = nome;
            SimboloJogada = simboloJogada;
        }

        public void AlterarNome(string novoNome)
        {
            Nome = novoNome;
        }

        public string ObterSimboloJogada()
        {
            return Enum.GetName(typeof(SimboloJogada), SimboloJogada);
        }

        public void AlterarSimboloJogada(SimboloJogada novoSimboloJogada)
        {
            SimboloJogada = novoSimboloJogada;
        }

        public void IncrementarVitorias() => this.QuantidadeVitorias++;

        public void IncrementarEmpates() => this.QuantidadeEmpates++;

        public void IncrementarDerrotas() => this.QuantidadeDerrotas++;

        public string Informacoes()
        {
            return $"Nome: {this.Nome}.{Environment.NewLine}" +
                $"Quantidade de vítórias: {this.QuantidadeVitorias}.{Environment.NewLine}" +
                $"Quantidade de empates: {this.QuantidadeEmpates}.{Environment.NewLine}" +
                $"Quantidade de derrotas: {this.QuantidadeDerrotas}.";
        }
    }
}
using Points.Domain.Core.Commands;

namespace Points.Domain.Saldo.Commands
{
    public class AtualizarSaldoCommand : Command
    {
        public string Email { get; private set; }
        public int Pontos { get; private set; }

        public AtualizarSaldoCommand(string email, int pontos)
        {
            Email = email;
            Pontos = pontos;
        }
    }
}

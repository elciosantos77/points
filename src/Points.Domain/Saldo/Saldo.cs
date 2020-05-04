using Points.Domain.Core.Models;

namespace Points.Domain.Saldo
{
    public class Saldo : Entity<Saldo>
    {
        private Saldo() { }
        public Saldo(string email, int pontos)
        {
            Email = email;
            Pontos = pontos;
        }

        public string Email { get; set; }
        public int Pontos { get; set; }
    }
}

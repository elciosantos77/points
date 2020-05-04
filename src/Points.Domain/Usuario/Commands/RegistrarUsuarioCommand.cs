using Points.Domain.Core.Commands;

namespace Points.Domain.Usuario.Commands
{
    public class RegistrarUsuarioCommand : Command
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }

        public RegistrarUsuarioCommand(string nome, string email, string senha)
        {
            Nome = nome;
            Email = email;
            Senha = senha;
        }
    }
}

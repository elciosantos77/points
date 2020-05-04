using Points.Domain.Core.Commands;

namespace Points.Domain.Endereco.Commands
{
    public class RegistrarEnderecoCommand : Command
    {
        public string Email { get; set; }
        public string CEP { get; private set; }
        public string Rua { get; private set; }
        public int Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public RegistrarEnderecoCommand(string emailUsuario, string cep, string rua, int numero, string complemento, string bairro, string cidade, string estado)
        {
            Email = emailUsuario;
            CEP = cep;
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
    }
}

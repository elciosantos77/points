using Points.Domain.Core.Models;
using System;

namespace Points.Domain.Endereco
{
    public class Endereco : Entity<Endereco>
    {
        private Endereco() { }

        public Endereco(string email, string cep, string rua, int numero, string complemento, string bairro, string cidade, string estado)
        {
            Email = email;
            CEP = cep;
            Rua = rua;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
        }
        public string Email { get; set; }
        public string CEP { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}

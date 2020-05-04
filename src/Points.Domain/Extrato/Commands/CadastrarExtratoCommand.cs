using Points.Domain.Core.Commands;
using System;

namespace Points.Domain.Extrato.Commands
{
    public class CadastrarExtratoCommand : Command
    {
        public int Pontos { get; private set; }
        public string Email { get; private set; }
        public string Estabelecimento { get; private set; }
        public DateTime DataCompra { get; private set; }

        public CadastrarExtratoCommand(int pontos, string email, string estabelecimento)
        {
            Pontos = pontos;
            Email = email;
            DataCompra = DateTime.Now;
            Estabelecimento = estabelecimento;
        }
    }
}

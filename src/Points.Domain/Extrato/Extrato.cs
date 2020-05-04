using Points.Domain.Core.Models;
using System;

namespace Points.Domain.Extrato
{
    public class Extrato : Entity<Extrato>
    {
        private Extrato() { }

        public Extrato(int pontos, string email, string estabelecimento, DateTime dataCompra)
        {
            Pontos = pontos;
            Email = email;
            DataCompra = dataCompra;
            Estabelecimento = estabelecimento;
        }
        public int Pontos { get; set; }
        public string Email { get; set; }
        public string Estabelecimento { get; set; }
        public DateTime DataCompra { get; set; }
    }
}

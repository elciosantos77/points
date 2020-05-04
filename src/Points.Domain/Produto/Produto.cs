using Points.Domain.Core.Models;
using System;

namespace Points.Domain.Produto
{
    public class Produto : Entity<Produto>
    {
        private Produto() { }

        public Produto(string nome, int pontuacao, string categoria)
        {
            Nome = nome;
            Pontuacao = pontuacao;
            Categoria = categoria;
        }
        public Produto(Guid id)
        {
            Id = id;
        }
        public string Nome { get; set; }
        public int Pontuacao { get; set; }
        public string Categoria { get; set; }
    }
}

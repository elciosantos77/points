using Points.Domain.Core.Models;
using Points.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Points.Domain.Pedido
{
    public class Pedido : Entity<Pedido>
    {
        private Pedido() { }
        public Pedido(string email) { Email = email; }

        public Pedido(string email, StatusEntrega status, DateTime data, List<PedidoItem> items, Endereco.Endereco endereco)
        {
            Email = email;
            StatusEntrega = status;
            Data = data;
            Itens = items;
            Endereco = endereco;
        }

        public string Email { get; set; }
        public List<PedidoItem> Itens { get; set; }
        public DateTime Data { get; set; }
        public Endereco.Endereco Endereco { get; set; }
        public StatusEntrega StatusEntrega { get; set; }

        public int CalcularPontuacaoTotal()
        {
           return Itens.Sum(x => x.Pontos);
        }
    }
}

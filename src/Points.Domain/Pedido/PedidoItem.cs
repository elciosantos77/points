using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Points.Domain.Pedido
{
    public class PedidoItem
    {
        public PedidoItem(Guid produtoId, int quantidade)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
        }
        public int Quantidade { get; set; }
        public Produto.Produto Produto { get; set; }
        public int Pontos { get; set; }

        [BsonIgnore]
        public Guid ProdutoId { get; set; }


        public void CalcularPontuacao(int pontuacaoProduto)
        {
            this.Pontos = pontuacaoProduto * Quantidade;
        }
    }
}

using Points.Domain.Pedido;
using Points.Domain.Pedido.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Writer
{
    public class PedidoCommandRepository : BaseRepository<Pedido>, IPedidoCommandRepository
    {
        private const string COLLECTION_NAME = "pedidos";
        public PedidoCommandRepository()
            : base(COLLECTION_NAME) { }

        public void Adicionar(Pedido pedido)
        {
            collection.InsertOne(pedido);
        }
    }
}

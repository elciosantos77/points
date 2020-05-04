using MongoDB.Driver;
using Points.Domain.Pedido;
using Points.Domain.Pedido.Repository;
using Points.Infra.Data.NoSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Points.Infra.Data.Reader
{
    public class PedidoQueryRepository : BaseRepository<Pedido>, IPedidoQueryRepository
    {
        private const string COLLECTION_NAME = "pedidos";
        public PedidoQueryRepository()
            : base(COLLECTION_NAME) { }

        public Task<List<Pedido>> ObterPorEmail(string email)
        {
            var filter = Builders<Pedido>.Filter.Eq("Email", email);
            return collection.Find(filter).ToListAsync();
        }

        public Pedido ObterUltimoPedido(string email)
        {
            var filter = Builders<Pedido>.Filter.Eq("Email", email);
            return collection.AsQueryable().OrderByDescending(l => l.Data).FirstOrDefault();
        }

        Pedido IPedidoQueryRepository.ObterPorId(Guid id)
        {
            var filter = Builders<Pedido>.Filter.Eq("_Id", id);
            return collection.Find(filter).SingleOrDefault();
        }
    }
}

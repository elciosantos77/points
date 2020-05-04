using MongoDB.Driver;
using Points.Domain.Produto;
using Points.Domain.Produto.Repository;
using Points.Infra.Data.NoSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Points.Infra.Data.Reader
{
    public class ProdutoQueryRepository : BaseRepository<Produto>, IProdutoQueryRepository
    {
        private const string COLLECTION_NAME = "produtos";
        public ProdutoQueryRepository()
            : base(COLLECTION_NAME) { }
        public Produto ObterPorId(Guid id)
        {
            var filter = Builders<Produto>.Filter.Eq("_id", id);
            return collection.Find(filter).SingleOrDefault();
        }

        public Produto ObterPorNome(string nome)
        {
            var filter = Builders<Produto>.Filter.Eq("Nome", nome);
            return collection.Find(filter).SingleOrDefault();
        }

        public async Task<List<Produto>> ObterTodos()
        {
            return await collection.Find(_ => true).ToListAsync();
        }
    }
}

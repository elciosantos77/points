using MongoDB.Driver;
using Points.Domain.Extrato;
using Points.Domain.Extrato.Repository;
using Points.Infra.Data.NoSQL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Points.Infra.Data.Reader
{
    public class ExtratoQueryRepository : BaseRepository<Extrato>, IExtratoQueryRepository
    {
        private const string COLLECTION_NAME = "extrato";

        public ExtratoQueryRepository()
            : base(COLLECTION_NAME) { }

        public async Task<List<Extrato>> ObterPorEmail(string email)
        {
            var filter = Builders<Extrato>.Filter.Eq("Email", email);
            return await collection.Find(filter).ToListAsync();
        }

        public Extrato ObterPorId(Guid id)
        {
            var filter = Builders<Extrato>.Filter.Eq("_Id", id);
            return collection.Find(filter).SingleOrDefault();
        }

        public int ObterSaldoPorEmail(string email)
        {
            var filter = Builders<Extrato>.Filter.Eq("Email", email);
            return collection.Find(filter).ToList().Sum(x => x.Pontos);
        }

        public Extrato ObterUltimaCompra(string email)
        {
            var filter = Builders<Extrato>.Filter.Eq("Email", email);
            return collection.AsQueryable().OrderByDescending(l => l.DataCompra).FirstOrDefault();
        }
    }
}

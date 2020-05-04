using MongoDB.Driver;
using Points.Domain.Saldo;
using Points.Domain.Saldo.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Writer
{
    public class SaldoCommandRepository : BaseRepository<Saldo>, ISaldoCommandRepository
    {
        private const string COLLECTION_NAME = "saldo";
        public SaldoCommandRepository()
            : base(COLLECTION_NAME) { }
        public void Cadastrar(Saldo saldo)
        {
            collection.InsertOne(saldo);
        }

        public void Atualizar(Saldo saldo)
        {
            var filter = Builders<Saldo>.Filter.Eq("_id", saldo.Id);
            collection.ReplaceOne(filter, saldo);
        }
    }
}

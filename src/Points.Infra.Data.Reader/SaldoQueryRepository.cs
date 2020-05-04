using MongoDB.Driver;
using Points.Domain.Saldo;
using Points.Domain.Saldo.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Reader
{
    public class SaldoQueryRepository : BaseRepository<Saldo>, ISaldoQueryRepository
    {
        private const string COLLECTION_NAME = "saldo";
        public SaldoQueryRepository()
            : base(COLLECTION_NAME) { }

        public Saldo ObterPorEmail(string email)
        {
            var filter = Builders<Saldo>.Filter.Eq("Email", email);
            return collection.Find(filter).FirstOrDefault();
        }

        public bool SaldoInsuficiente(int pontosPedido, string email)
        {
            var filter = Builders<Saldo>.Filter.Eq("Email", email);
            return collection.Find(filter).FirstOrDefault().Pontos < pontosPedido;
        }
    }
}

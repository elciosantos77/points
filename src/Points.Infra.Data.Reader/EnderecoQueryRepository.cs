using MongoDB.Driver;
using Points.Domain.Endereco;
using Points.Domain.Endereco.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Reader
{
    public class EnderecoQueryRepository : BaseRepository<Endereco>, IEnderecoQueryRepository
    {
        private const string COLLECTION_NAME = "enderecos";
        public EnderecoQueryRepository()
           : base(COLLECTION_NAME) { }

        public Endereco ObterPorEmail(string email)
        {
            var filter = Builders<Endereco>.Filter.Eq("Email", email);
            return collection.Find(filter).SingleOrDefault();
        }
    }
}

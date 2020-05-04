using Points.Domain.Endereco;
using Points.Domain.Endereco.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Writer
{
    public class EnderecoCommandRepository : BaseRepository<Endereco>, IEnderecoCommandRepository
    {
        private const string COLLECTION_NAME = "enderecos";
        public EnderecoCommandRepository()
           : base(COLLECTION_NAME) { }
        public void Adicionar(Endereco endereco)
        {
            collection.InsertOne(endereco);
        }
    }
}

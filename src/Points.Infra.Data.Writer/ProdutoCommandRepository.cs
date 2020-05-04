using Points.Domain.Produto;
using Points.Domain.Produto.Repository;
using Points.Infra.Data.NoSQL.Repositories;

namespace Points.Infra.Data.Writer
{
    public class ProdutoCommandRepository : BaseRepository<Produto>, IProdutoCommandRepository
    {
        private const string COLLECTION_NAME = "produtos";
        public ProdutoCommandRepository()
            : base(COLLECTION_NAME) { }

        public void Adicionar(Produto produto)
        {
            collection.InsertOne(produto);
        }
    }
}

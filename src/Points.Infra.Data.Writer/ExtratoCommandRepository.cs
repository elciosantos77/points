using Points.Domain.Extrato;
using Points.Domain.Extrato.Repository;
using Points.Infra.Data.NoSQL.Repositories;
using System;

namespace Points.Infra.Data.Writer
{
    public class ExtratoCommandRepository : BaseRepository<Extrato>, IExtratoCommandRepository
    {
        private const string COLLECTION_NAME = "extrato";
        public ExtratoCommandRepository()
            : base(COLLECTION_NAME) { }
        public void Adicionar(Extrato extrato)
        {
            collection.InsertOne(extrato);
        }
    }
}

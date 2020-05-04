using MongoDB.Driver;
using Points.Domain.Usuario.Repository;
using Points.Infra.CrossCutting.Identity;
using Points.Infra.Data.NoSQL.Repositories;
using System.Threading.Tasks;

namespace Points.Infra.Data.Reader
{
    public class UsuarioQueryRepository : BaseRepository<ApplicationUser>, IUsuarioQueryRepository
    {
        private const string COLLECTION_NAME = "applicationUsers";
        public UsuarioQueryRepository()
            : base(COLLECTION_NAME) { }
        public Task<ApplicationUser> ObterPorEmail(string email)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq("Email", email);
            return collection.Find(filter).FirstOrDefaultAsync();
        }

        public bool UsuarioExistente(string email)
        {
            var filter = Builders<ApplicationUser>.Filter.Eq("Email", email);
            return collection.Find(filter).Any();
        }
    }
}

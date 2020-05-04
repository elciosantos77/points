using Points.Infra.CrossCutting.Identity;
using System.Threading.Tasks;

namespace Points.Domain.Usuario.Repository
{
    public interface IUsuarioQueryRepository
    {
        Task<ApplicationUser> ObterPorEmail(string email);
        bool UsuarioExistente(string email);
    }
}

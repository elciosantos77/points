using Points.Infra.CrossCutting.Identity;
using System.Threading.Tasks;

namespace Points.Domain.Interfaces.Services
{
    public interface IUsuarioService
    {
        Task<string> Autenticar(ApplicationUser usuario);
        Task<bool> UsuarioExistente(string email);
    }
}

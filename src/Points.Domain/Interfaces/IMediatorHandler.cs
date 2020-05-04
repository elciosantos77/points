using Points.Domain.Core.Commands;
using Points.Domain.Core.Models;
using System.Threading.Tasks;

namespace Points.Domain.Interfaces
{
    public interface IMediatorHandler
    {
        Task PublicarEvento<T>(T evento) where T : Event;
        Task EnviarComando<T>(T comando) where T : Command;
    }
}

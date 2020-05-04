using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Points.Domain.Produto.Repository
{
    public interface IProdutoQueryRepository
    {
        Produto ObterPorId(Guid id);
        Produto ObterPorNome(string nome);
        Task<List<Produto>> ObterTodos();
    }
}

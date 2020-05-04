using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Points.Domain.Extrato.Repository
{
    public interface IExtratoQueryRepository
    {
        Extrato ObterPorId(Guid id);
        Task<List<Extrato>> ObterPorEmail(string email);
        Extrato ObterUltimaCompra(string email);
        int ObterSaldoPorEmail(string email);
    }
}

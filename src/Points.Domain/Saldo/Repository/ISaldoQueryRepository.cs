using System;

namespace Points.Domain.Saldo.Repository
{
    public interface ISaldoQueryRepository
    {
        Saldo ObterPorEmail(string email);
        bool SaldoInsuficiente(int pontosPedido, string email);
    }
}

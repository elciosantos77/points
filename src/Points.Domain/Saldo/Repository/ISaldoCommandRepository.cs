namespace Points.Domain.Saldo.Repository
{
    public interface ISaldoCommandRepository
    {
        void Atualizar(Saldo saldo);
        void Cadastrar(Saldo saldo);
    }
}

namespace Points.Domain.Endereco.Repository
{
    public interface IEnderecoQueryRepository
    {
        Endereco ObterPorEmail(string email);
    }
}

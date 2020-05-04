using Points.Domain.Core.Commands;

namespace Points.Domain.Produto.Commands
{
    public class CadastrarProdutoCommand : Command
    {
        public string Nome { get; set; }
        public int Pontuacao { get; private set; }
        public string Categoria { get; private set; }

        public CadastrarProdutoCommand(string nome, int pontuacao, string categoria)
        {
            Nome = nome;
            Pontuacao = pontuacao;
            Categoria = categoria;
        }
    }
}

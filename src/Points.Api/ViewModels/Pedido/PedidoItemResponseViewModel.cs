namespace Points.Api.ViewModels.Pedido
{
    public class PedidoItemResponseViewModel
    {
        public int Quantidade { get; set; }
        public Produto.ProdutoViewModel Produto { get; set; }
        public int Pontos { get; set; }
    }
}

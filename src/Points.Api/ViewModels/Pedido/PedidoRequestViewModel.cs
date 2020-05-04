using System.Collections.Generic;

namespace Points.Api.ViewModels.Pedido
{
    public class PedidoRequestViewModel
    {
        public string Email { get; set; }
        public List<PedidoItemViewModel> Itens { get; set; }
    }
}

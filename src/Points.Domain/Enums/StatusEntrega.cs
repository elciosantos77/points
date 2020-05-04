using System.ComponentModel.DataAnnotations;

namespace Points.Domain.Enums
{
    public enum StatusEntrega
    {
        [Display(Name = "AGUARDANDO ENVIO")]
        AGUARDANDOENVIO,
        [Display(Name = "EM TRÂNSITO")]
        EMTRANSITO,
        CANCELADO,
        ENTREGUE
    }
}

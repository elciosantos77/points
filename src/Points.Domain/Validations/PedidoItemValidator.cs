using Points.Domain.Pedido;
using FluentValidation;

namespace Points.Domain.Validations
{
    public class PedidoItemValidator : AbstractValidator<PedidoItem>
    {
        public PedidoItemValidator()
        {
            RuleFor(c => c.Quantidade)
                .NotNull().WithMessage("A 'Quantidade' não pode ser nula")
                .NotEmpty().WithMessage("A 'Quantidade' não pode ser 0");
        }
    }
}

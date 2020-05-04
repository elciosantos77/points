using FluentValidation;
using System;
using System.Linq;

namespace Points.Domain.Validations
{
    public class PedidoValidator : BaseValidator<Pedido.Pedido>
    {
        public override bool Validar(Pedido.Pedido pedido)
        {
            ValidarEmail();
            ValidarItensPedido();
            
            ValidationResult = Validate(pedido);
            return ValidationResult.IsValid;
        }
        private void ValidarEmail()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("O 'Email' deve ser informado")
                .Must(email => { return !string.IsNullOrEmpty(email) ? EmailValidation.Validate(email) : true; }).WithMessage("O 'Email' informado é inválido");
        }

        private void ValidarItensPedido()
        {
            RuleFor(p => p.Itens)
                .Must(itens => itens?.Count > 0).WithMessage("É necessário possuir, ao menos, um item no pedido")
                .Must(itens =>
                {
                    var quantidadeValida = !itens.Where(x => x.Quantidade <= 0).Any();
                    return quantidadeValida;
                }).WithMessage($"A quantidade informada para o produto está inválida");
        }
    }
}

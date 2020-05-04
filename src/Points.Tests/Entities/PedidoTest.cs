using Points.Domain.Pedido;
using Points.Domain.Validations;
using System;
using System.Collections.Generic;
using Xunit;

namespace Points.Domain.Tests.Entities
{
    public class PedidoTest
    {
        Pedido.Pedido pedidoValido;
        Endereco.Endereco enderecoValido;
        PedidoValidator pedidoValidator;
        PedidoItem itemValido;
        string emailValido;
        public PedidoTest()
        {
            pedidoValidator = new PedidoValidator();
            emailValido = "elcio-santos3@hotmail.com";
            enderecoValido = new Endereco.Endereco(emailValido, "14801-320", "Goiás", 2525, "AP 503", "Centro", "Araraquara", "São Paulo");
            itemValido = new PedidoItem(Guid.Parse("a41e9498-5a5c-4267-b3d4-8d54dc8a8266"), 1);
            pedidoValido = new Pedido.Pedido("elcio-santos3@hotmail.com", Enums.StatusEntrega.AGUARDANDOENVIO, DateTime.Now, new List<PedidoItem>(), enderecoValido);
        }

        [Theory]
        [InlineData("a41e9498-5a5c-4267-b3d4-8d54dc8a8266", 0, "A quantidade informada para o produto está inválida")]
        [InlineData("a41e9498-5a5c-4267-b3d4-8d54dc8a8266", -1, "A quantidade informada para o produto está inválida")]
        public void QuantidadeIvalida(string produtoId, int quantidade, string mensagemEsperada)
        {
            var pedido = pedidoValido;

            var itens = new List<PedidoItem>();
            itens.Add(new PedidoItem(Guid.Parse(produtoId), quantidade));

            pedido.Itens = itens;

            pedidoValidator.Validar(pedido);
            AssertMensagemEsperada(mensagemEsperada, pedidoValidator);
        }

        [Theory]
        [InlineData("É necessário possuir, ao menos, um item no pedido")]
        public void ItemInvalido(string mensagemEsperada)
        {
            var pedido = pedidoValido;
            var itens = new List<PedidoItem>();
            pedido.Itens = itens;

            pedidoValidator.Validar(pedido);
            AssertMensagemEsperada(mensagemEsperada, pedidoValidator);
        }

        [Fact]
        public void EmailValido()
        {
            var pedido = pedidoValido;

            var itens = new List<PedidoItem>();
            itens.Add(itemValido);

            pedido.Itens = itens;
            var resultado = pedidoValidator.Validar(pedido);

            Assert.True(resultado);
        }

        [Theory]
        [InlineData(null, "O 'Email' deve ser informado")]
        [InlineData("", "O 'Email' deve ser informado")]
        [InlineData("sdfads", "O 'Email' informado é inválido")]
        [InlineData("@zsdfsafd", "O 'Email' informado é inválido")]
        public void EmailInvalido(string email, string mensagemEsperada)
        {
            var pedido = pedidoValido;
            pedido.Email = email;

            pedidoValidator.Validar(pedido);

            AssertMensagemEsperada(mensagemEsperada, pedidoValidator);
        }
        private void AssertMensagemEsperada(string mensagemEsperada, PedidoValidator pedidoValidation)
        {
            Assert.Equal(pedidoValidation.ValidationResult.IsValid, string.IsNullOrEmpty(mensagemEsperada));

            if (string.IsNullOrEmpty(mensagemEsperada))
                Assert.Empty(pedidoValidation.ValidationResult.Errors);
            else
                Assert.Contains(pedidoValidation.ValidationResult.Errors, e => e.ErrorMessage == mensagemEsperada);
        }
    }
}

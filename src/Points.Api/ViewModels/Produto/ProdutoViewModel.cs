using System;
using System.ComponentModel.DataAnnotations;

namespace Points.Api.ViewModels.Produto
{
    public class ProdutoViewModel
    {
        public Guid? Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Pontuação")]
        public int Pontuacao { get; set; }
    }
}

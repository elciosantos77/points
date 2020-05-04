using System;
using System.ComponentModel.DataAnnotations;

namespace Points.Api.ViewModels.Extrato
{
    public class ExtratoViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Pontos")]
        public int Pontos { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Estabelecimento")]
        public string Estabelecimento { get; set; }
    }
}

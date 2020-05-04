using System.ComponentModel.DataAnnotations;

namespace Points.Api.ViewModels
{
    public class EnderecoViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(9, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Display(Name = "Rua")]
        public string Rua { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Numero { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        public string Complemento { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo 0} é obrigatório")]
        public string Bairro { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo 0} é obrigatório")]
        public string Cidade { get; set; }

        [StringLength(50, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "O campo 0} é obrigatório")]
        public string Estado { get; set; }
    }
}

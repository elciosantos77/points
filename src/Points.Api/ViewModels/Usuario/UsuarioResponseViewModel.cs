using System;

namespace Points.Api.ViewModels
{
    public class UsuarioResponseViewModel
    {
        public UsuarioResponseViewModel(Guid id, string nome, string email)
        {
            Id = id;
            Nome = nome;
            Email = email;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}

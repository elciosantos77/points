using Points.Domain.Interfaces.Services;
using Points.Infra.CrossCutting.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Points.Domain.Usuario.Services
{
    public class UsuarioService : IUsuarioService
    {
      
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioService(UserManager<ApplicationUser> userManager,
                              IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<String> Autenticar(ApplicationUser usuario)
        {
            string secret = _configuration["KeyTak5"];
            var payload = new Dictionary<string, object>
                {
                    {"userId", usuario.Id},
                    {"email", usuario.Email}
                };

            var tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("3aZY}Ns3DuuL6rgSlAd?V^/eSV\\SJkxN_1@tzhK'Ots,&D"));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime baseTime = DateTime.UtcNow.AddHours(12);
            var securityToken = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = credential,
                Expires = baseTime,
            });
            return tokenHandler.WriteToken(securityToken);
        }

        public async Task<bool> UsuarioExistente(string email)
        {
            var usuario = await _userManager.FindByEmailAsync(email);
            return usuario != null ? true : false;
        }
    }
}

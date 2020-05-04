using AspNetCore.Identity.MongoDbCore.Models;

namespace Points.Infra.CrossCutting.Identity
{
    public class ApplicationUser : MongoIdentityUser
    {
        public string Nome { get; set; }
    }
}

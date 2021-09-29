using Microsoft.AspNetCore.Identity;
using NetDevPack.Security.Jwt.Interfaces;
using Universal.EBI.Auth.API.Data;
using Universal.EBI.Auth.API.Extensions;

namespace Universal.EBI.Auth.API.Services
{
    public class AuthenticationService
    {
        public readonly SignInManager<IdentityUser> SignInManager;
        public readonly UserManager<IdentityUser> UserManager;
        //private readonly AppSettings _appSettings;
        private readonly AppTokenSettings _appTokenSettings;
        private readonly AuthDbContext _context;
        private readonly IJsonWebKeySetService _jwksService;
        //private readonly IAspNetUser _aspNetUser;
    }
}

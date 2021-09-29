using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Security.Jwt.Model;
using NetDevPack.Security.Jwt.Store.EntityFrameworkCore;
using Universal.EBI.Auth.API.Models;

namespace Universal.EBI.Auth.API.Data
{
    public class AuthDbContext : IdentityDbContext, ISecurityKeyContext
    {       
        public DbSet<SecurityKeyWithPrivate> SecurityKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteInstitucionalBandup.Models;

namespace SiteInstitucionalBandup.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Curriculo> Curriculos { get; set; }

    public DbSet<Evento> Eventos { get; set; }

    public DbSet<Genero> Generos { get; set; }
    
    public DbSet<Home> Homes { get; set; }

    public DbSet<Loja> Lojas { get; set; }

    public DbSet<Marca> Marcas { get; set; }

    public DbSet<Setor> Setores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Polular usuário

        var roles = new List<IdentityRole>
        {
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Usuário", NormalizedName = "USUÁRIO" }
        };
        builder.Entity<IdentityRole>().HasData(roles); 

        var users = new List<IdentityUser>
        {
            new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@cosmos.com",
                NormalizedEmail = "ADMIN@COSMOS.COM",
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                LockoutEnabled = false,
                EmailConfirmed = true
            },
            new IdentityUser
            {
                Id = Guid.NewGuid().ToString(),
                Email = "user@hotmail.com",
                NormalizedEmail = "USER@HOTMAIL.COM",
                UserName = "User",
                NormalizedUserName = "USER",
                LockoutEnabled = true,
                EmailConfirmed = true
            }
        };

        var passwordHasher = new PasswordHasher<IdentityUser>();
        foreach (var user in users)
        {   
            user.PasswordHash = passwordHasher.HashPassword(user, "@adminadmin");
        }
        builder.Entity<IdentityUser>().HasData(users);

        var userRoles = new List<IdentityUserRole<string>>
        {
            new IdentityUserRole<string> { UserId = users[0].Id, RoleId = roles[0].Id },
            new IdentityUserRole<string> { UserId = users[0].Id, RoleId = roles[1].Id },
            new IdentityUserRole<string> { UserId = users[1].Id, RoleId = roles[1].Id }
        };
        builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        
        #endregion
    }
}

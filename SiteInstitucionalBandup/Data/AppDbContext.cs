using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteInstitucionalBandup.Models;

namespace SiteInstitucionalBandup.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Genero> Generos { get; set; }
    public DbSet<Curriculos> Curriculos { get; set; }
    public DbSet<GenerosCurriculos> GenerosCurriculos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Configuração do Muitos para muitos do GenerosCurriculos

        builder.Entity<GenerosCurriculos>().HasKey(gc => new { gc.GeneroId, gc.CurriculoId });

        builder.Entity<GenerosCurriculos>()
            .HasOne(gc => gc.Genero)
            .WithMany(g => g.GenerosCurriculos)
            .HasForeignKey(gc => gc.GeneroId);

        builder.Entity<GenerosCurriculos>()
            .HasOne(gc => gc.Curriculo)
            .WithMany(c => c.GenerosCurriculos)
            .HasForeignKey(gc => gc.CurriculoId);
        
        #endregion

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

        var appUsers = new List<AppUser>
        {
            new AppUser { AppUserId = users[0].Id, Name = "Elisson Guerra", Birthday = DateTime.ParseExact("29/10/1989", "dd/MM/yyyy", null), Photo = "" },
            new AppUser { AppUserId = users[1].Id, Name = "Visitante", Birthday = DateTime.ParseExact("05/10/2008", "dd/MM/yyyy", null), Photo = "" }
        };
        builder.Entity<AppUser>().HasData(appUsers);
        
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

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteInstitucionalBandup.Models;

namespace SiteInstitucionalBandup.Data;

public class AppDbContext : IdentityDbcontext

{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<Genero> Generos { get; set; }
    public DbSet<Curriculos> Curriculos { get; set; }
    public DbSet<GenerosCurriculos> GenerosCurriculos { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        #region Configuração do Muitos para muitos do GenerosCurriculos
        
        builder.Entity<GenerosCurriculos>().HasKey(
            gc => new { gc.GeneroId, gc.GeneroId }
        );
        builder.Entity<GenerosCurriculos>()
            .HasOne(gc => gc.Genero)
            .WithMany(g => g.Curriculos)
            .HasForeignKey(gc => gc.GeneroId);

        builder.Entity<GenerosCurriculos>()
            .HasOne(gc => gc.Genero)
            .WithMany(c => c.Curriculos)
            .HasForeignKey(gc => gc.GeneroId);
        #endregion

         #region Polular usuário
            List<IdentityRole> roles = new(){
                new IdentityRole(){
                    Id = Guid.NewGuid().ToString(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole(){
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuário",
                    NormalizedName = "USUÁRIO"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles); 

            List<IdentityUser> users = new(){
                new IdentityUser(){
                    Id = Guid.NewGuid().ToString(),
                    Email = "admin@cosmos.com",
                    NormalizedEmail = "ADMIN@COSMOS.COM",
                    UserName = "Admin",
                    NormalizedUserName = "ADMIN",
                    LockoutEnabled = false,
                    EmailConfirmed = true
                },
                new IdentityUser(){
                    Id = Guid.NewGuid().ToString(),
                    Email = "user@hotmail.com",
                    NormalizedEmail = "USER@HOTMAIL.COM",
                    UserName = "User",
                    NormalizedUserName = "USER",
                    LockoutEnabled = true,
                    EmailConfirmed = true
                }
            };
            foreach (var user in users)
            {   
                PasswordHasher<IdentityUser> pass = new();
                user.PasswordHash = pass.HashPassword(user, "@adminadmin");
            }
            builder.Entity<IdentityUser>().HasData(users);

            List<AppUser> appUsers = new(){
                new AppUser{
                    AppUserId = users[0].Id,
                    Name = "Elisson Guerra",
                    Birthday = DateTime.Parse("29/10/1989"),
                    Photo = ""
                },
                new AppUser{
                    AppUserId = users[1].Id,
                    Name = "Visitante",
                    Birthday = DateTime.Parse("05/10/2008"),
                    Photo = ""
                },
            };
            builder.Entity<AppUser>().HasData(appUsers);
            
            List<IdentityUserRole<string>> userRoles = new(){
                new IdentityUserRole<string>(){
                    UserId = users[0].Id,
                    RoleId = roles[0].Id
                },
                new IdentityUserRole<string>(){
                    UserId = users[0].Id,
                    RoleId = roles[1].Id
                },
                new IdentityUserRole<string>(){
                    UserId = users[1].Id,
                    RoleId = roles[1].Id
                },
            };
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        #endregion
    }
}
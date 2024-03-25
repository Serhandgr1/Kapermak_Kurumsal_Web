using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccessLayer
{
    public class DataContext : IdentityDbContext<UserEntites>
    {
        public DataContext(DbContextOptions options ):base(options)
        {
            
        }
        DbSet<ContactAdminEntities> ContactAdmins { get; set; }
        DbSet<UserEntites> Users { get; set; }
        DbSet<CategoryEntities> Categorys { get; set; }
        DbSet<AboutEntites> Abouts { get; set; }
        DbSet<ProductEntities> Products { get; set; }
        DbSet<ReferanceEntities> Referances { get; set;}
        DbSet<ContactEntities> Contacts { get; set; }
        DbSet<CommentEntities> Comments { get; set; }
        DbSet<PreferenceEntiites> Preferences { get; set; }
        DbSet<ProjectEntities> Projects { get; set; }
        DbSet<ServicesEntities> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.ApplyConfiguration(new RoleConfiguration()); // oluşturduğumuz config dosyalarını okuyoruz
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //tüm config dosyalarının hepsini okumak için
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }

    }

}


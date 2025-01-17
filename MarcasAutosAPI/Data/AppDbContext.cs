using MarcasAutosAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutosAPI.Data
{
    public class AppDbContext : DbContext
    {

        private readonly bool _applySeedData;

        public DbSet<MarcaAuto> MarcasAutos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, bool applySeedData) : base(options)
        {
            _applySeedData = applySeedData;
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _applySeedData = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            if (_applySeedData)
            {

                // Seed data para la tabla MarcasAutos
                modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto { Id = 1, Nombre = "Toyota", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 2, Nombre = "Ford", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 3, Nombre = "Chevrolet", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 4, Nombre = "Honda", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 5, Nombre = "Nissan", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 6, Nombre = "BMW", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 7, Nombre = "Mercedes-Benz", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 8, Nombre = "Volkswagen", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 9, Nombre = "Hyundai", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 10, Nombre = "Kia", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 11, Nombre = "Audi", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 12, Nombre = "Mazda", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 13, Nombre = "Subaru", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 14, Nombre = "Tesla", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 15, Nombre = "Volvo", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 16, Nombre = "Peugeot", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 17, Nombre = "Renault", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 18, Nombre = "Fiat", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 19, Nombre = "Jeep", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 20, Nombre = "Lexus", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] } );

            }
        }
    }
}

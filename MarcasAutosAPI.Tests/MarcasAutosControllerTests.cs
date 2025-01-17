using MarcasAutosAPI.Controllers;
using MarcasAutosAPI.Data;
using MarcasAutosAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MarcasAutosAPI.Tests
{
    public class MarcasAutosControllerTests
    {
        
        #region Pruebas usando base de datos en memoria

        /// <summary>
        /// Método que prueba el acceso a  la base se datos creada en memoria y que se retornan los 20 registros de la migración
        /// más 2 insertados
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ReturnsAllMarcasAutos()
        {
            // Arrange
            var context = SetupDatabaseContextInMemory("TestDatabase1");
            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 21, Nombre = "Tesla", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 22, Nombre = "Amazon", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] }
            );
            context.SaveChanges();

            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.Equal(22, result.Value.Count());
        }

        /// <summary>
        /// Método que verifica el comportamiento del controlador cuando no hay datos
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ReturnsEmptyList_WhenNoData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("EmptyDatabase")
                .Options;

            using var context = new AppDbContext(options);
            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<MarcaAuto>>>(result);
            Assert.Empty(okResult.Value);
        }

        /// <summary>
        /// Método que verifica que la be devuelva solo los registros que se le han insertado temporalmente
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ReturnsPartialList()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("PartialDatabase")
                .Options;

            using var context = new AppDbContext(options);
            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 2, Nombre = "Ford", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] }
            );
            context.SaveChanges();

            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<MarcaAuto>>>(result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
            Assert.Equal(2, marcas.Count());
        }


        /// <summary>
        /// Método para verificar si es posible devolver duplicados
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_HandlesDuplicateData()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("DuplicateDatabase")
                .Options;

            using var context = new AppDbContext(options);
            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 2, Nombre = "Toyota", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] } // Duplicado
            );
            context.SaveChanges();

            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<ActionResult<IEnumerable<MarcaAuto>>>(result);
            var marcas = Assert.IsAssignableFrom<IEnumerable<MarcaAuto>>(okResult.Value);
            Assert.Equal(2, marcas.Count());
        }

        /// <summary>
        /// Método para verificar el tiempo de respuesta de la bd
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ResponseTimeIsAcceptable()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase("ResponseTimeDatabase")
                .Options;

            using var context = new AppDbContext(options);
            context.MarcasAutos.AddRange(
                new MarcaAuto { Id = 1, Nombre = "Toyota", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
                new MarcaAuto { Id = 2, Nombre = "Ford", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] }
            );
            context.SaveChanges();

            var controller = new MarcasAutosController(context);

            // Act
            var stopwatch = Stopwatch.StartNew();
            await controller.Get();
            stopwatch.Stop();

            // Assert
            Assert.True(stopwatch.ElapsedMilliseconds < 500, "Response time exceeded 500ms");
        }

        #endregion

        #region Pruebas usando docker


        /// <summary>
        /// Método que prueba el acceso a  la base se datos creada en memoria y que se retornan los 20 registros de la migración
        /// más 2 insertados
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Get_ReturnsAllMarcasAutos_UsingDocker()
        {
            // Arrange
            using AppDbContext context = SetupDatabaseContext();

            context.MarcasAutos.AddRange(
            new MarcaAuto { Id = 21, Nombre = "Tesla", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] },
            new MarcaAuto { Id = 22, Nombre = "Amazon", DateCreated = DateTime.UtcNow, CreatedBy = 1, IsDeleted = false, RowVersion = new byte[0] });

            context.SaveChanges();

            var controller = new MarcasAutosController(context);

            // Act
            var result = await controller.Get();

            // Assert
            Assert.NotNull(result.Value); // Verificar que el resultado no es nulo
            Assert.Equal(22, result.Value.Count()); // Verificar que devuelve las 20 marcas del seed
            Assert.Contains(result.Value, m => m.Nombre == "Toyota"); // Verificar que incluye "Toyota"
            Assert.Contains(result.Value, m => m.Nombre == "Ford"); // Verificar que incluye "Ford"

        }
              

        #endregion

        #region Metodos privados

        private AppDbContext SetupDatabaseContextInMemory(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new AppDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }

        private static AppDbContext SetupDatabaseContext()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseNpgsql("Host=localhost;Database=marcasautosdb;Username=user;Password=password;Pooling=true;MinPoolSize=10;MaxPoolSize=100;Include Error Detail=true;")
                .Options;
            var context = new AppDbContext(options);

            context.Database.EnsureDeleted(); // Borra la base de datos si ya existe
            context.Database.EnsureCreated(); // Crea la base de datos y aplica las migraciones
            return context;
        }

        #endregion
    }
}

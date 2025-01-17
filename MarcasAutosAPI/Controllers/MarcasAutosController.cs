using MarcasAutosAPI.Data;
using MarcasAutosAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarcasAutosAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcasAutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MarcasAutosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Controlador que retorna la lista de marcas de autos completa
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarcaAuto>>> Get()
        {
            return await _context.MarcasAutos.ToListAsync();
        }
    }
}

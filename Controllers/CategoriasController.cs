using Microsoft.AspNetCore.Mvc;
using Web3_Examen2.Models;

namespace Web3_Examen2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private static List<Categoria> _categorias = new()
        {
            new Categoria { Id = 1, Nombre = "Electrónica", Descripcion = "Productos electrónicos" },
            new Categoria { Id = 2, Nombre = "Ropa", Descripcion = "Prendas de vestir" }
        };

        [HttpGet(Name = "GetCategorias")]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            return Ok(_categorias);
        }

        [HttpGet("{id}", Name = "GetCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null)
                return NotFound();

            return Ok(categoria);
        }

        [HttpPost(Name = "CreateCategoria")]
        public ActionResult<Categoria> Post(Categoria categoria)
        {
            categoria.Id = _categorias.Any() ? _categorias.Max(c => c.Id) + 1 : 1;
            _categorias.Add(categoria);
            return Created($"/api/categorias/{categoria.Id}", categoria);
        }

        [HttpPut("{id}", Name = "UpdateCategoria")]
        public ActionResult<Categoria> Put(int id, Categoria categoriaActualizada)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null)
                return NotFound();

            categoria.Nombre = categoriaActualizada.Nombre;
            categoria.Descripcion = categoriaActualizada.Descripcion;
            return Ok(categoria);
        }

        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public IActionResult Delete(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria is null)
                return NotFound();

            _categorias.Remove(categoria);
            return NoContent();
        }
    }
}

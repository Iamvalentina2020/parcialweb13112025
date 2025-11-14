using Microsoft.AspNetCore.Mvc;
using Web3_Examen2.Models;

namespace Web3_Examen2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private static List<Producto> _productos = new()
        {
            new Producto { Id = 1, Nombre = "Laptop", Descripcion = "Laptop HP", Precio = 15000, Stock = 10, IdCategoria = 1 },
            new Producto { Id = 2, Nombre = "Camisa", Descripcion = "Camisa de algodón", Precio = 500, Stock = 50, IdCategoria = 2 }
        };

        [HttpGet(Name = "GetProductos")]
        public ActionResult<IEnumerable<Producto>> Get()
        {
            return Ok(_productos);
        }

        [HttpGet("{id}", Name = "GetProducto")]
        public ActionResult<Producto> Get(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto is null)
                return NotFound();

            return Ok(producto);
        }

        [HttpPost(Name = "CreateProducto")]
        public ActionResult<Producto> Post(Producto producto)
        {
            producto.Id = _productos.Any() ? _productos.Max(p => p.Id) + 1 : 1;
            _productos.Add(producto);
            return Created($"/api/productos/{producto.Id}", producto);
        }

        [HttpPut("{id}", Name = "UpdateProducto")]
        public ActionResult<Producto> Put(int id, Producto productoActualizado)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto is null)
                return NotFound();

            producto.Nombre = productoActualizado.Nombre;
            producto.Descripcion = productoActualizado.Descripcion;
            producto.Precio = productoActualizado.Precio;
            producto.Stock = productoActualizado.Stock;
            producto.IdCategoria = productoActualizado.IdCategoria;
            return Ok(producto);
        }

        [HttpDelete("{id}", Name = "DeleteProducto")]
        public IActionResult Delete(int id)
        {
            var producto = _productos.FirstOrDefault(p => p.Id == id);
            if (producto is null)
                return NotFound();

            _productos.Remove(producto);
            return NoContent();
        }
    }
}

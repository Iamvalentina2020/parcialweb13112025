using Microsoft.AspNetCore.Mvc;
using Web3_Examen2.Models;

namespace Web3_Examen2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProveedoresController : ControllerBase
    {
        private static List<Proveedor> _proveedores = new()
        {
            new Proveedor { Id = 1, RazonSocial = "Proveedor SA", Contacto = "contacto@proveedor.com" },
            new Proveedor { Id = 2, RazonSocial = "Distribuidora XYZ", Contacto = "ventas@xyz.com" }
        };

        [HttpGet(Name = "GetProveedores")]
        public ActionResult<IEnumerable<Proveedor>> Get()
        {
            return Ok(_proveedores);
        }

        [HttpGet("{id}", Name = "GetProveedor")]
        public ActionResult<Proveedor> Get(int id)
        {
            var proveedor = _proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor is null)
                return NotFound();

            return Ok(proveedor);
        }

        [HttpPost(Name = "CreateProveedor")]
        public ActionResult<Proveedor> Post(Proveedor proveedor)
        {
            proveedor.Id = _proveedores.Any() ? _proveedores.Max(p => p.Id) + 1 : 1;
            _proveedores.Add(proveedor);
            return Created($"/api/proveedores/{proveedor.Id}", proveedor);
        }

        [HttpPut("{id}", Name = "UpdateProveedor")]
        public ActionResult<Proveedor> Put(int id, Proveedor proveedorActualizado)
        {
            var proveedor = _proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor is null)
                return NotFound();

            proveedor.RazonSocial = proveedorActualizado.RazonSocial;
            proveedor.Contacto = proveedorActualizado.Contacto;
            return Ok(proveedor);
        }

        [HttpDelete("{id}", Name = "DeleteProveedor")]
        public IActionResult Delete(int id)
        {
            var proveedor = _proveedores.FirstOrDefault(p => p.Id == id);
            if (proveedor is null)
                return NotFound();

            _proveedores.Remove(proveedor);
            return NoContent();
        }
    }
}

namespace ProductoController;
using Microsoft.AspNetCore.Mvc;
using PresupuestosRepository;
using Presupuestos;
using PresupuestoDetalle;

[ApiController]
[Route("[controller]")]

public class PresupuestosController : ControllerBase
{

private PresupuestosRepository _presupuestosRepository;

public PresupuestosController()
{
    _presupuestosRepository = new PresupuestosRepository();
}
[HttpGet("presupuestos")]
public IActionResult GetAll()
    {
        var presupuestos = _presupuestosRepository.GetAllPresupuestos();
        return Ok(presupuestos);
    }

    [HttpGet("Presupuesto/{id}")]
    public IActionResult GetById(int id)
    {
        var presupuesto = _presupuestosRepository.GetbyIdPresupuesto(id);
        if (presupuesto == null)
            return NotFound($"No se encontró un presupuesto con ID {id}");

        return Ok(presupuesto);
    }

    [HttpPost("Presupuesto")]
    public IActionResult Create([FromBody] Presupuestos nuevoPresupuesto)
    {
        if (nuevoPresupuesto == null)
            return BadRequest("El producto no puede ser nulo.");

        _presupuestosRepository.InsertPresupuesto(nuevoPresupuesto);
        return Created($"Presupuestos/presupuesto/{nuevoPresupuesto.IdPresupuesto}", nuevoPresupuesto);
    }

    [HttpPost("Presupuesto/{id}/ProductoDetalle")]
    public IActionResult Create(int id, [FromBody] PresupuestoDetalle presupuestoDetalle)
    {
        if (presupuestoDetalle == null)
            return BadRequest("El presupuesto detalle no puede ser nulo");

        _presupuestosRepository.InsertPresupuestoDetalle(id,presupuestoDetalle);
        return Ok("id");
    }

    [HttpDelete("Presupuesto/{id}")]
    public IActionResult Delete(int id)
    {
        var existente = _presupuestosRepository.GetbyIdPresupuesto(id);
        if (existente == null)
            return NotFound($"No existe producto con ID {id} para eliminar.");

        _presupuestosRepository.DeletePresupuesto(id);
        return Ok($"Producto con ID {id} eliminado correctamente.");
    }
}
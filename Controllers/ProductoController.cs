namespace ProductoController;
using Microsoft.AspNetCore.Mvc;
using ProductosRepository;
using Productos;


[ApiController]
[Route("[controller]")]

public class ProductoController : ControllerBase
{

private ProductosRepository _productosRepository;

public ProductoController()
{
    _productosRepository = new ProductosRepository();
}
[HttpGet("products")]
public IActionResult GetAll()
    {
        var productos = _productosRepository.GetAllProductos();
        return Ok(productos);
    }
    [HttpGet("producto/{id}")]
    public IActionResult GetById(int id)
    {
        var producto = _productosRepository.GetbyIdProducto(id);
        if (producto == null)
            return NotFound($"No se encontró un producto con ID {id}");

        return Ok(producto);
    }

    [HttpPost("producto")]
    public IActionResult Create([FromBody] Productos nuevoProducto)
    {
        if (nuevoProducto == null)
            return BadRequest("El producto no puede ser nulo.");

        _productosRepository.InsertProducto(nuevoProducto);
        return Created($"Producto/product/{nuevoProducto.idProducto}", nuevoProducto);
    }

    [HttpPut("producto/{id}")]
    public IActionResult Update(int id, [FromBody] Productos productoActualizado)
    {
        var existente = _productosRepository.GetbyIdProducto(id);
        if (existente == null)
            return NotFound($"No existe producto con ID {id} para actualizar.");

        _productosRepository.UpdateProducto(id, productoActualizado);
        return Ok("Producto actualizado correctamente.");
    }

    [HttpDelete("producto/{id}")]
    public IActionResult Delete(int id)
    {
        var existente = _productosRepository.GetbyIdProducto(id);
        if (existente == null)
            return NotFound($"No existe producto con ID {id} para eliminar.");

        _productosRepository.DeleteProducto(id);
        return Ok($"Producto con ID {id} eliminado correctamente.");
    }
}
namespace ProductoController;
using Microsoft.AspNetCore.Mvc;
using ProductosRepository;


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
}
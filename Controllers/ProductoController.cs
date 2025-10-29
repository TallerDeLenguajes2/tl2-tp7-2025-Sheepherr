using Microsoft.AspNetCore.Mvc;

namespace ProductoController;


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
        var productos = _productosRepository.GetAll();
        return Ok(productos);
    }
}
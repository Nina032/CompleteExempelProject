using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExempelProject.Data;
using ExempelProject.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExempelProject.Controllers
{
  [Route("api/[Controller]")]
  [ApiController]
  [Produces("application/json")]
  public class ProductsController : ControllerBase
  {
    private readonly IExempelRepository _repository;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IExempelRepository repository, ILogger<ProductsController> logger)
    {
      _repository = repository;
      _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public ActionResult<IEnumerable<Product>> Get()
    {
      try
      {
        return Ok(_repository.GetAllProducts()); 
      }
      catch (Exception ex)
      {
        _logger.LogError($"Failed to get products: {ex}");
        return BadRequest("failed to get products");
      }
    }
  }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserApi_Identity.Authorization;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Services;

namespace UserApi_Identity.Controllers;
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
        _service = service;
    }

    [HttpPost]
    public IResult Post(ProductDto dto)
    {
        _service.Create(dto);
        return Results.Ok();
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public IResult Get()
    {
        var products = _service.FindAll();
        return Results.Ok(products);
    }
}
using AutoMapper;
using UserApi_Identity.Data;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Models;

namespace UserApi_Identity.Services;

public class ProductService
{
    private readonly UserDbContext _context;
    private readonly IMapper _mapper;


    public ProductService(UserDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public IEnumerable<ProductDto> FindAll()
    {
        var products = _context.Products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name
        });
        return products;
    }

    public void Create(ProductDto dto)
    {
        Product product = _mapper.Map<Product>(dto);
        _context.Add(product);
        _context.SaveChanges();
    }
}
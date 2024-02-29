using AutoMapper;
using UserApi_Identity.Data.Dtos;
using UserApi_Identity.Models;

namespace UserApi_Identity.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductDto, Product>();
    }
}
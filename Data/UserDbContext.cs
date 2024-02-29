using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserApi_Identity.Models;

namespace UserApi_Identity.Data;

public class UserDbContext : IdentityDbContext<User>
{
    public DbSet<Product> Products { get; set; }
    public UserDbContext(DbContextOptions<UserDbContext> opts) : base(opts){}
}
using Microsoft.AspNetCore.Identity;

namespace UserApi_Identity.Models;

public class User : IdentityUser
{
    //ctor tab tab -- comando para criar construtor
    // não preciso criar campo de id ou username, por exemplo. Pois o IdentityUser ja contem esses campos
    public DateTime BirthDate { get; set; }
    public string Profile { get; set; }
    public User() : base(){}
}
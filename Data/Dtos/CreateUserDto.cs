﻿using System.ComponentModel.DataAnnotations;

namespace UserApi_Identity.Data.Dtos;

public class CreateUserDto
{
    [Required] 
    public string UserName { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [Required]
    [Compare("Password")]
    public string RePassword { get; set; }
}
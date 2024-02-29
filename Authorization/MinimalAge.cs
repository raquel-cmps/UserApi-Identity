using Microsoft.AspNetCore.Authorization;

namespace UserApi_Identity.Authorization;

public class MinimalAge : IAuthorizationRequirement
{
    public int Age { get; set; }

    public MinimalAge(int age)
    {
        Age = age;
    }
}
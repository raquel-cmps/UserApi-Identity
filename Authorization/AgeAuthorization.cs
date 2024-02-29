using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UserApi_Identity.Authorization;

public class AgeAuthorization : AuthorizationHandler<MinimalAge>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimalAge requirement)
    {
        //logica para validar se o usuario tem mais de 18 anos
        var bithDateClaim = context.User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);

        if (bithDateClaim is null)
            return Task.CompletedTask;

        var birthDate = Convert.ToDateTime(bithDateClaim.Value);

        var ageUser = DateTime.Today.Year - birthDate.Year;
        
        if (birthDate > DateTime.Today.AddYears(-ageUser))
            ageUser--;
        
        if(ageUser >= requirement.Age)
            context.Succeed(requirement);

        return Task.CompletedTask;
    }
}
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

[Route("[controller]/[action]")]
public class ManageUser : BaseManager<UserContext>
{
    public ManageUser(DbContextUser context) : base(context.UserContexts_, context)
    {
    }
 
}
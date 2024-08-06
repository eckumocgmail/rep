using System.Collections.Generic;

public interface APIUsers  
{
    UserContext FindByToken(string token);
    UserContext FindByEmail(string email);


    IEnumerable<UserContext> FindByGroup(string group);   
    IEnumerable<UserContext> FindByRole(string role);   


}

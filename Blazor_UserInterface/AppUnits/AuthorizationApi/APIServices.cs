using System.Collections.Generic;




/// <summary>
/// Сервис управления службами веб-API
/// </summary>
public interface APIServices : APIActiveCollection<ServiceContext >
{

    public IDictionary<string, string> Signin( UserContext user );
    public IDictionary<string, string> GetApis();


    
}

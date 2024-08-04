
using System;
using System.Collections.Generic;
using System.Linq;


public class UserBusinessResourcesService
{
    private AuthorizationDbContext _context;


    public UserBusinessResourcesService(AuthorizationDbContext context)
    {
        _context = context;
    }

    /*
    public List<ViewNode> GetUserBusinessResourceNavigation(string BusinessResourceName)
    {
         
        List<ViewNode> links = new List<ViewNode>();
        BusinessResource role = GetBusinessResourceByCode(BusinessResourceName);
        
        Type type = ReflectionService.TypeForShortName(BusinessResourceName);
        List<string> TypeNames =_context.GetEntityTypeNames();
        if (type != null && TypeNames.Contains(BusinessResourceName))
        {
            
            foreach ( var nav in _context.GetNavigationPropertiesForType(type))
            {
                if( nav.Name != "User")
                {
                    links.Add(new Link()
                    {
                        Label = TypeUtils.LabelFor(type, nav.Name),
                        Href = $"/{role.Code}Face/{Naming.GetMultiCountName(nav.Name)}/Index"
                    });
                }
                
            }
        }        
        return links;
    }

    public BusinessResource GetBusinessResourceByCode(string roleCode)
    {
        return (from p in _context.Roles where p.Code == roleCode select p).SingleOrDefault();
    }

    public List<string> GetUserBusinessResourceCodes(UserContext  user)
    {
        List<string> codes = new List<string>();
        BusinessResource prole = user.Role;
        while (prole != null)
        {
            codes.Add(prole.Code);
            if (prole.ParentID == null)
            {
                break;
            }
            else
            {
                prole = _context.Roles.Find((int)prole.ParentID);
            }
        }
        return codes;
    }
    

    /// <summary>
    /// Поиск роли по коду
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public BusinessResource FindBusinessResourceByCode(string code)
    {
        return (from r in _context.Roles where r.Code == code select r).SingleOrDefault();
      
    }


    /// <summary>
    /// Создание новой роли в приложении
    /// </summary>
    /// <param name="name">наименование</param>
    /// <param name="description">описание</param>
    /// <param name="code">код</param>        
    public BusinessResource CreateBusinessResource(string name, string description, string code)
    {
        BusinessResource role = new BusinessResource()
        {
            Name = name,
            Description = description,
            Code = code
        };
        _context.Roles.Add(role);
        _context.SaveChanges();
        return role;
    }*/
}

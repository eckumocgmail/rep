using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;


public abstract class BaseGrouping<TItem, TParent, TGroup>: BaseEntity
    where TGroup: BaseEntity 
    where TParent: BaseEntity
{
    public abstract MethodResult<TGroup> RemoveFromGroup(TItem item, TParent parent);
    public abstract MethodResult<TGroup> AddToGroup(TItem item, TParent parent);    
    public abstract MethodResult<TGroup> SendToGroup(TItem item, UserGroupMessage Message,  TParent parent);
 
    public abstract IEnumerable<TParent> GetSelectedOptions(TItem item);
    public abstract IEnumerable<TParent> GetAvailableOptions(TItem item);

    public abstract string[] GetGroups();
    public abstract string[] GetGroupsForItem(TItem item);

    public abstract MethodResult<UserGroups> AddToGroup(UserContext item, string name);

    public abstract bool RemoveFromGroup(TItem item, string name);
}
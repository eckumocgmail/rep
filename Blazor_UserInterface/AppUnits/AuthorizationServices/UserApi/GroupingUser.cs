using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
 
public class GroupingUser : BaseGrouping<UserContext, UserGroup, UserGroups>
{
    private readonly DbContextUser _model;

    public GroupingUser(DbContextUser model)
    {
        this._model = model;
    }

   

    public override MethodResult<UserGroups> AddToGroup(UserContext item, UserGroup parent)
    {
        try
        {
            UserGroups res;
            this._model.Add(res=new UserGroups(){
                Group = parent,
                User = item
            });
            this._model.SaveChanges();
            return MethodResult<UserGroups>.OnResult(res);
        }
        catch(DbUpdateException ex)
        {
            return MethodResult<UserGroups>.OnError(ex);
        }
    }

  
    public override MethodResult<UserGroups> AddToGroup(UserContext item, string name)
    {
        try
        {
            var group = this._model.UserGroups_.FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());
            if(group is null)
            {
                _model.UserGroups_.Add(new()
                {
                    Name = name,
                    Description = name,
                    Code = name

                });
                _model.SaveChanges();
            };
            group = this._model.UserGroups_.FirstOrDefault(g => g.Name.ToUpper() == name.ToUpper());
            UserGroups res;
            this._model.Add(res=new UserGroups(){
                Group = group,
                User = item
            });
            this._model.SaveChanges();
            return MethodResult<UserGroups>.OnResult(res);
        }
        catch(DbUpdateException ex)
        {
            return MethodResult<UserGroups>.OnError(ex);
        }
    }

 
    public override IEnumerable<UserGroup> GetAvailableOptions(UserContext item)    
        => _model.UserGroups_.Where( g => (_model.UserGroups_UserGroup.Select(ug => ug.GroupID)).Contains(g.Id) == false);


    public override string[] GetGroups()
        => _model.UserGroups_.Select(g => g.Name).ToArray();
    public IEnumerable<string> GetRoles() => _model.UserRoles_.Select(g => g.Code);
    public IEnumerable<string> GetRoles(int userId)
    {
        var user = _model.UserContexts_.Include(u => u.BusinessFunctions).First(u => u.Id == userId);
        return _model.UserRoles_.Where(r => _model.UsersRoles_.Where(bf => bf.UserId == user.Id).Select(bf => bf.RoleId).Contains(r.Id)).Select(r => r.Code);
    }
    public int AddRole(int userId,string role)
    {
        var u = _model.UserContexts_.First(r => r.Id == userId);
        var r = _model.UserRoles_.First(r => r.Code == role);
        _model.UsersRoles_.Add(new()
        {
            UserId = userId,
            RoleId = r.Id
        });
        return _model.SaveChanges();
    }
    public int RemoveRole(int userId, string role)
    {
        var u = _model.UserContexts_.First(r => r.Id == userId);
        var r = _model.UserRoles_.First(r => r.Code == role);
        _model.UsersRoles_.Remove(_model.UsersRoles_.First(ur =>
        
            ur.UserId == userId &&
            ur.RoleId == r.Id
        ));
        _model.SaveChanges();
        return _model.SaveChanges();
    }


    public override string[] GetGroupsForItem(UserContext item)
        => _model.UserGroups_.Where( g => (_model.UserGroups_UserGroup.Select(ug => ug.GroupID)).Contains(g.Id)).Select(g => g.Name).ToArray();


     
    public override IEnumerable<UserGroup> GetSelectedOptions(UserContext item)
        => _model.UserGroups_.Where( g => (_model.UserGroups_UserGroup.Select(ug => ug.GroupID)).Contains(g.Id) == true);


  
    public override MethodResult<UserGroups> RemoveFromGroup(UserContext item, UserGroup parent)
    {
        try
        {
            var ug = _model.UserGroups_UserGroup.FirstOrDefault( ug => ug.UserID==item.Id && ug.GroupID == parent.Id);
            if(ug == null){
                return MethodResult<UserGroups>.OnError(new ArgumentException());
            }
            _model.UserGroups_UserGroup.Remove(ug);
            _model.SaveChanges();
            return MethodResult<UserGroups>.OnResult(ug);

        }catch(Exception ex){
            return MethodResult<UserGroups>.OnError(ex);
        }
    }

   
    public override bool RemoveFromGroup(UserContext item, string name)
    {
    
        var group = _model.UserGroups_.FirstOrDefault( g => g.Name==name);
        var ug = _model.UserGroups_UserGroup.FirstOrDefault( ug => ug.UserID==item.Id && ug.GroupID == group.Id);
        if(ug == null){
            return false;
        }
        _model.UserGroups_UserGroup.Remove(ug);
        _model.SaveChanges();
        return true;
 
    }

    
    public override MethodResult<UserGroups> SendToGroup(UserContext item, UserGroupMessage Message, UserGroup parent)
    {        
        try
        {
            _model.Add(new UserGroupMessage(){
                FromUser = item,
                Group = parent
            });
            _model.SaveChanges();
            return MethodResult<UserGroups>.OnResult(null);

        }catch(Exception ex){
            return MethodResult<UserGroups>.OnError(ex);
        }
    }
}
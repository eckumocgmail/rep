using Microsoft.AspNetCore.Components.Authorization;

using System.Collections.Generic;





public abstract class BaseSignin<TActiveObject, TAuthenticationMember, TObjectInfo>
    where TObjectInfo: BaseEntity 
    where TActiveObject: ActiveObject 
{
    protected readonly ITokenProvider _tokenProvider;

    protected BaseSignin(ITokenProvider tokenProvider)        
    {
        this._tokenProvider = tokenProvider;
    }
    /// <summary>
    /// Asynchronously gets an <see cref="AuthenticationState"/> that describes the current user.
    /// </summary>
    /// <returns>A task that, when resolved, gives an <see cref="AuthenticationState"/> instance that describes the current user.</returns>
    public abstract bool Signout( string key );
    public abstract MethodResult<TActiveObject> Signin( TAuthenticationMember item );
    public abstract bool Validate(string key);

    public abstract T GetFromSession<T>(string key);


}

using System.Collections.Generic;





public abstract class BaseSignup<TActiveObject, TAuthenticationMember, TObjectInfo> 
    where TActiveObject: ActiveObject 
{

    public abstract MethodResult<TActiveObject> Signup(TAuthenticationMember item, TObjectInfo info);
    public abstract bool HasWith(TAuthenticationMember item);
    public abstract bool Compare(TAuthenticationMember stored, TAuthenticationMember input);
    public abstract TActiveObject GetBy(TAuthenticationMember item);
}

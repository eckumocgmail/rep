using System.Linq;
using Microsoft.EntityFrameworkCore;

public class SignupService : BaseSignup<ServiceContext, ServiceSertificate, ServiceInfo>
{
    private readonly DbContextService _model;

    public SignupService(DbContextService model)
    {
        this._model = model;    
    }

    public override bool Compare(ServiceSertificate stored, ServiceSertificate input)
        => Equals(stored.PrivateKey,input.PrivateKey) && Equals(stored.PublicKey,input.PublicKey);

    public override ServiceContext GetBy(ServiceSertificate item)

        => _model.ServiceContexts.Include(sc => sc.ServiceSertificate).First(sc => sc.ServiceSertificate.PublicKey == item.PublicKey);

    public override bool HasWith(ServiceSertificate item)
    
        => _model.ServiceSertificates.Any(i => Equals(i.PublicKey, item.PrivateKey));        
    
 
    public override MethodResult<ServiceContext> Signup(ServiceSertificate item, ServiceInfo info)
    {
        try
        {
            var context = new ServiceContext();
            _model.Add(context.ServiceInfo = info);
            _model.Add(context.ServiceSertificate = item);
            _model.Add(context.ServiceSettings = new ServiceSettings());
            _model.Add(context);
            _model.SaveChanges();
            return MethodResult<ServiceContext>.OnResult(context);
        }
        catch(DbUpdateException ex)
        {
            return MethodResult<ServiceContext>.OnError(ex);
        }
    }
}
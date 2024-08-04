[Icon("build")]
[Label("Микрослужба")]
public class ServiceContext : ActiveObject
{

     public int ServiceInfoId { get; set; }
     public ServiceInfo ServiceInfo { get; set; }
     public int ServiceSettingsId { get; set; }
     public ServiceSettings ServiceSettings { get; set; }
     public int ServiceSertificateId { get; set; }
     public ServiceSertificate ServiceSertificate { get; set; }
     public System.Collections.Generic.ICollection<ServiceLogin> ServiceLogins { get; set; }
     public System.Collections.Generic.ICollection<ServiceMessage> ServiceMessages { get; set; }
     public System.Collections.Generic.ICollection<ServiceGroups> ServiceGroups { get; set; }

    
}

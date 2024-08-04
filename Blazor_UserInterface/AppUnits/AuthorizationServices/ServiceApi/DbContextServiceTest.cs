namespace Console_BlazorApp.AppUnits.AuthorizationServices.ServiceApi
{
    [Label("DbContextService")]
    [Description("Необходимо проверить CRUD операции")]
    public class DbContextServiceTest: TestingElement
    {
        public DbContextServiceTest(IServiceProvider provider) : base(provider)
        {
        }

        public override void OnTest()
        {
            AssertService<DbContextService>
            (
                db => 
                {
                    this.Info(db.ServiceGroups.ToList().ToJsonOnScreen());
                    this.Info(db.ServiceGroupMessages.ToList().ToJsonOnScreen());
                    this.Info(db.ServiceSertificates.ToList().ToJsonOnScreen());
                    this.Info(db.ServiceContexts.ToList().ToJsonOnScreen());
                    this.Info(db.ServiceInfos.ToList().ToJsonOnScreen());
                    this.Info(db.ServiceMessages.ToList().ToJsonOnScreen());
                    return true;
                },
                "Выбор данных из DbContextService работает корректно",
                "Выбор данных из DbContextService работает не корректно"
            );

            AssertService<DbContextService>
            (
                db =>
                {
                    ServiceGroup sg = null;
                    this.Info(db.ServiceGroups.Add(sg = new ServiceGroup() {
                        Name = "WebApi"                       
                    }));
                    db.SaveChanges();
                    ServiceGroupMessage sgm = null;
                    this.Info(db.ServiceGroupMessages.Add(sgm = new ServiceGroupMessage() { 
                        ServiceGroupId = sg.Id
                    }));
                    db.SaveChanges();
                    ServiceSertificate sc = null;
                    this.Info(db.ServiceSertificates.Add(sc = new ServiceSertificate() { }));
                    db.SaveChanges();
                    ServiceInfo si = null;
                    this.Info(db.ServiceInfos.Add(si = new ServiceInfo() { }));
                    db.SaveChanges();
                    ServiceSettings ss = null;
                    this.Info(db.ServiceSettings.Add(ss = new ServiceSettings() { }));
                    db.SaveChanges();
                    ServiceContext sctx = null;
                    this.Info(db.ServiceContexts.Add(sctx = new ServiceContext() {
                        ServiceSertificateId = sc.Id,
                        ServiceInfoId = si.Id,
                        ServiceSettingsId = ss.Id
                    }));
                    db.SaveChanges();
                    ServiceMessage sm = null;
                    this.Info(db.ServiceMessages.Add(sm = new ServiceMessage() {
                        ServiceContextId = sctx.Id
                    }));
                    db.SaveChanges();

                    db.Remove(sg);
                    db.Remove(sgm);
                    db.Remove(sc);
                    db.Remove(si);
                    db.Remove(ss);
                    db.Remove(sctx);
                    db.Remove(sm);
                    db.SaveChanges();
                    return true;
                },
                "Добавление данных в DbContextService работает корректно",
                "Добавление данных в DbContextService работает не корректно"
            );
        }
    }
}

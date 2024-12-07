using Microsoft.EntityFrameworkCore;

using Newtonsoft.Json.Linq;

using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Выполняет инициаллизация данных приложения, 
/// проверяет наличия данных в каждой таблице.
/// </summary>
public class BusinessInitiallizer

{
    static BusinessDataModel db = new BusinessDataModel();
    /// <summary>
    /// Конструктор получает контекст данных
    /// </summary>
    /// <param name="context"></param>
    public BusinessInitiallizer(BusinessDataModel ctx)
    {
        db = ctx;
        //Info(context.GetEntityTypeNames());
    }

    /// <summary>
    /// Права для ИТ-отдела
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BusinessResource> GetItDepartmentRoles()
    {
        return new BusinessResource[] {
            new BusinessResource(){ 
                Name="DBA", 
                Description="" 
            },
            new BusinessResource(){ 
                Name="DBA", Description="" },
            new BusinessResource(){ Name="Dev",     Description="" },
            new BusinessResource(){ Name="SA",      Description="" },
            new BusinessResource(){ Name="Si",      Description="" },
            new BusinessResource(){ Name="User",    Description="" },
            new BusinessResource(){ Name="Boss",    Description="" },
        };
    }

    private void InitMessageAttributes()
    {
        IEnumerable<Type> attributes = GetBaseInputAttributes();
        if (db.MessageAttributes.Count() != attributes.Count())
        {
            foreach (var attribute in attributes)
            {
                foreach (var m in db.MessageAttributes.ToList())
                {
                    db.MessageAttributes.Remove(m);
                }
                db.MessageAttributes.Add(ConvertToMessageAttribute(attribute));
            }
        }
        db.SaveChanges();
    }

    private static IEnumerable<Type> GetBaseInputAttributes()
    {
        return ServiceFactory.Get().GetTypesExtended<BaseInputAttribute>();
    }
    class Utils
    {
        internal static string LabelFor(Type attribute)
        {
            return attribute.GetType().GetLabel();
        }

        internal static string DescriptionFor(Type attribute)
        {
            return attribute.GetType().GetDescription();
        }

        internal static string IconFor(Type attribute)
        {
            return attribute.GetType().GetIcon();
        }
    }
    private static MessageAttribute ConvertToMessageAttribute(Type attribute)
    {
        string atrName = (attribute.GetType().Name.StartsWith("Input") ? attribute.GetType().Name.Substring("Input".Length) : attribute.GetType().Name).Replace("Attribute", "");
        //var icons = new MaterialIconsService();

        BaseInputAttribute attr = (BaseInputAttribute)attribute.GetConstructors().First().Invoke(new object[attribute.GetConstructors().First().GetParameters().Count()]);
        //ReflectionService.CreateWithDefaultConstructor<BaseInputAttribute>(attribute);
        var res = new MessageAttribute()
        {

            //SQLType = attr.GetSqlServerDataType(),
            //CSharpType = attr.GetCSTypeName(),
            SqlServerDataType = attr.GetSqlServerDataType(),
            //MySQLDataType = attr.GetMySQLDataType(),
            PostgreDataType = attr.GetPostgreDataType(),
            OracleDataType = attr.GetOracleDataType(),
            Description = Utils.LabelFor(attribute) + ":\n" + Utils.DescriptionFor(attribute),
            Icon = Utils.IconFor(attribute),
            Name = Utils.LabelFor(attribute),
            InputType = (attribute.Name.StartsWith("Input") ? attribute.Name.Substring("Input".Length) : attribute.Name).Replace("Attribute", "")
        };
        if (res.Name == null)
        {
            int x = 0;
            string label = Utils.LabelFor(attribute);
        }
        return res;
    }
    public void Install()
    {
        //Assembly.GetExecutingAssembly().Location

        InitMessageAttributes();
        db.BusinessResources.ToList().ForEach((p) => { db.BusinessResources.Remove(p); });             

    }
    
    private int InitBusinessResources(BusinessDataModel db)
    {
        if (db.BusinessResources.Count() == 0)
        {
            var user = new BusinessResource()
            {
                Name = "Пользователь",
                Description = "Пользователи имеют возможность воспроизводить свои функциональные обязанности согласно информационной модели.",
                Code = "User"
            };
            var dba = new BusinessResource()
            {
                Name = "Database Administrator (DBA)",
                Description = "Разрабатывает базы данных, процедуры и функции... ",
                Code = "DBA",
                Parent = user
            };
            var analitic = new BusinessResource()
            {
                Name = "System Analist(SA)",
                Description = "Аналитик создает отчёты",
                Code = "SA",
                Parent = user
            };
            var integrator = new BusinessResource()
            {
                Name = "System Integrator(SI)",
                Description = "Аналитик создает отчёты",
                Code = "SI",
                Parent = user
            };


            var dev = new BusinessResource()
            {
                Name = "Разработчик",
                Description = "Разработчик исследует бизнес процессы предприятия (IDEF0), устанавливает связи между функциональными единицами и информационными ресурсами (IDEFO, IDEF3 и DFD). " +
                                "Проектирует инфо-логическую и дата-логическую связанность функций информационной системы с внутренними и внешними бизнес процессами предприятия. ",
                Code = "Dev",
                Parent = user
            };
            var boss = new BusinessResource()
            {
                Name = "Директор",
                Description = "",
                Code = "Boss",
                Parent = user
            };

            db.Add(user);
            db.Add(dba);
            db.Add(integrator);
            db.Add(analitic);
            db.Add(boss);

            db.Add(dev);
            db.SaveChanges();
            dba.Parent = user;
            analitic.Parent = user;
            dev.Parent = user;
            db.SaveChanges();










            //регистрация пользователя для этапа разработки и тестирования
            /*var devRole = db.BusinessResources.Where(r => r.Code == "Dev").SingleOrDefault();
            if (db.Users.Count() == 0)
            {
                UserPerson person = PersonNamesProvider.GetRandomPerson();
                var registration = new RegistrationService(db, new AuthorizationOptions(), new EmailService(new Authorization.Services.Email.EmailOptions()));
                registration.Signup(new UserAccount("eckumoc@gmail.com", "sgdf1423"), person, devRole);
            }*/
        }
        return 1;

    }

    public Dictionary<string,int> InitData(BusinessDataModel db)
    {
        db.Database.EnsureCreated();
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
        Dictionary<string, int> res = new();
        res["Granularities"] = InitGranularities(db);
        res["BusinessIndicators"] = InitBusinessIndicators(db);
        res["BusinessFunction"] = InitBusinessFunctions(db);
        res["BusinessDatasources"] = InitBusinessDatasources(db);
        res["BusinessReports"] = InitBusinessReports(db);
        res["BusinessResources"] = InitBusinessResources(db);
        res["BusinessOLAP"] = InitBusinessOLAP(db);
        res["BusinessDatasets"] = InitBusinessDatasets(db);       
        res["BusinessData"] = InitBusinessData(db);       
        return res;
    }

    private int InitGranularities(BusinessDataModel db)
    {
        if (db.Granularities.Count() != 0)
            return 0;
        foreach(var code in "day,week,quarter,month,year".Split(","))
        {
            db.Granularities.Add(new()
            {
                Code = code,
                Name =
                    code == "day" ? "ежедневный" :
                    code == "week" ? "недельный" :
                    code == "month" ? "месячный" :
                    code == "quarter" ? "квартальный" :
                    code == "year" ? "годовой" : 
                    "",
                Description = "Периодичность отчёта"
            });  
        }
        
        return db.SaveChanges();
    }

    private int InitBusinessDatasets(BusinessDataModel db)
    {
        if (db.BusinessDatasets.Count() > 0)
        {
            return 0;
        }
        string con = null;
        BusinessDatasource p = null;
        db.BusinessDatasources.Add(p=new()
        {
            Name = db.GetType().GetLabel(),
            Description = db.GetType().GetDescription(),
            ConnectionString = db.Database.GetConnectionString()    
        });
        db.SaveChanges();
        db.BusinessDatasets.Add(new()
        {
            Name = "Рейтинг подключения",
            Description = "Оценка эффективности информационных ресурсов",
            Expression = "exec add_resource_statistics",
            BusinessDatasourceID = p.Id
            
        });
        return db.SaveChanges();
    }

    private int InitBusinessIndicators(BusinessDataModel db)
    {
        if(db.BusinessIndicators.Count()>0)
        {
            return 0;
        }
        db.BusinessIndicators.Add(new BusinessIndicator() { 
            Name = "Производительность",
            Description = "Производительность",
            IsNegative = false
        });
        db.BusinessIndicators.Add(new BusinessIndicator()
        {
            Name = "Работоспособность",
            Description = "Работоспособность",
            IsNegative = false
        });
        db.BusinessIndicators.Add(new BusinessIndicator()
        {
            Name = "Своевременность",
            Description = "Своевременность",
            IsNegative = false
        });
        return db.SaveChanges();
    }

    private int InitBusinessFunctions(BusinessDataModel db)
    {
        if (db.BusinessFunctions.Count() > 0) return 0;
        db.BusinessFunctions.Add(new()
        {
            Name = "Продажа запчастей",
            Description = "Онлайн продажа на сайте, продажа в магазинах" 
        });
        return db.SaveChanges();
    }

    private int InitBusinessDatasources(BusinessDataModel db)
    {
        var manager = new OdbcDriverManager();
        this.Info(manager.GetOdbcDatasources().ToJsonOnScreen());
        foreach(var datasource in manager.GetOdbcDatasources())
        {
            db.BusinessDatasources.Add(new()
            {
                Name = datasource.Name,
                Description = manager.GetOdbcDatasources().ToJsonOnScreen(),
                ConnectionString = $"driver={datasource.DriverName};dsn={datasource.Name};uid=;pwd=;"
            });
        }        
        return db.SaveChanges();
    }

    private int InitBusinessReports(BusinessDataModel db)
    {
        /*db.BusinessReports.Add(new()
        {
            
        });*/
        return 0;
    }

    private int InitBusinessOLAP(BusinessDataModel db)
    {
        return 0;
    }

    private int InitBusinessData(BusinessDataModel db)
    {
        var service = new BusinessAnaliticsService(db);
        service.Info(service.GetIndicators().ToJsonOnScreen());
        service.AddIndicator("Эффективность", "Тест");
        service.Info(service.GetIndicators().ToJsonOnScreen());
        var datasetId = service.GetDatasetByName("Рейтинг подключения").Id;

        foreach (var resource in service.GetResources())
        {
            foreach (var indicator in service.GetIndicators())
            {
                var indicatorId = indicator.Id;
                var resourceId = resource.Id;

                service.AddData(resourceId, indicatorId, datasetId, "2024-01-01", 1, 4f);
            }

            this.Info(service.GetDatasetValues(resource.Id, datasetId, "2024-01-01", 1).ToJsonOnScreen());
        }
        return db.SaveChanges();
    }
}
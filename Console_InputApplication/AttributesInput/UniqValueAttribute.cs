using Microsoft.EntityFrameworkCore;
using System.Reflection;

/// <summary>
/// Проверка уникальности значения атрибута сущности
/// </summary>
public class UniqValueAttribute : BaseValidationAttribute, MyValidation
{
    protected string _error;
    private string _dbContextTypeName;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="ErrorMessage">сообщение после отрицательной проверки</param>    
    public UniqValueAttribute(string dbContextTypeName = null, string ErrorMessage =null){
        _error = ErrorMessage;
        _dbContextTypeName = dbContextTypeName;
    }

 
    private object GetDbSet(DbContext subject, string dbset)
    {
        Type type = subject.GetType();        
        foreach (MethodInfo info in type.GetMethods())
        {
            if (info.Name.StartsWith("get_"+ dbset) == true && info.ReturnType.Name.StartsWith("DbSet"))
            {
                return info.Invoke(subject, new object[0]);
            }
        }
        throw new Exception("Коллекция сущностей "+dbset +" не найдена");
    }

    public override string Validate(object model, string property, object value)
    {
        var typeName = model.GetType().GetTypeName();
        var _dbContextType = model.GetDbContextWithEntity();

        if (_dbContextType is null)
            throw new NullReferenceException($"{GetType().GetTypeName()} не удалось определить контекст данных");
        if(_dbContextType is not null)
        {
            _dbContextTypeName = _dbContextType.GetTypeName();
        }
        if (_dbContextTypeName is null)
            throw new NullReferenceException($"{GetType().GetTypeName()} содержит ссылку на null  в свойстве _dbContextTypeName");
        _dbContextTypeName = _dbContextType.GetTypeName();

        using (var db = _dbContextTypeName.New<DbContext>())
        {
            var dbset = db.GetDbSet(model.GetType().GetTypeName());
            int id = (int)new ReflectionService().GetValue(model, "Id");
            bool isUniq = true;
            foreach (var record in ((IEnumerable<dynamic>)dbset))
            {
                if (record.Id != id)
                {
                    object recordPropertyValue = new ReflectionService().GetValue(record, property);
                    if (value == null)
                    {
                        if (recordPropertyValue == null)
                        {
                            isUniq = false;
                            break;
                        }
                    }
                    else
                    {
                        if (recordPropertyValue == null)
                        {
                            continue;
                        }
                        else
                        {
                            if (recordPropertyValue.ToString() == value.ToString())
                            {
                                isUniq = false;
                                break;
                            }
                        }
                    }
                }
            }

            //bool result = (from i in ((IEnumerable<dynamic>)dbsetObj)
            //where new ReflectionService().GetValue(model, property) == value && i.Id != id
            //select i).Count() == 0;
            return isUniq ? null : GetMessage(model, property, value);
        }        
    }

    public string GetMessage(object model, string property, object value)
    {
        if (string.IsNullOrEmpty(_error))
        {
            return "Свойство "+property+" должно иметь уникальное значение";
        }
        else
        {
            return _error;
        }
    }


    /*
    protected  override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {        
        using (ApplicationDbContext db = new ApplicationDbContext())
        {
            string dbsetPropertyName = null;
            if(_dbset == null)
            {
                string entityName = validationContext.ObjectType.Name;
                dbsetPropertyName = Counting.GetMultiCountName(validationContext.ObjectType.Name);
            }
            else
            {
                dbsetPropertyName = _dbset;
            }
            ReflectionService reflection = new ReflectionService();
            object dbsetObj = GetDbSet(db, dbsetPropertyName);
            string propertyName = validationContext.MemberName;            
            bool result = (from i in ((IEnumerable<dynamic>)dbsetObj)
                           where reflection.GetValue((object)i, propertyName) == value
                           select i).Count() == 0;
            return result? null: new ValidationResult(_error);
        }
            
    }*/



}
 

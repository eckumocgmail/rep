using Microsoft.EntityFrameworkCore;
using System.Reflection;

/// <summary>
/// Проверка уникальности значения атрибута сущности
/// </summary>
public class UniqValuesAttribute : BaseValidationAttribute, MyValidation
{
    protected string _error;
    private IEnumerable<string> properties;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="ErrorMessage">сообщение после отрицательной проверки</param>    
    public UniqValuesAttribute( string exp="" ){
        this.properties = exp.Split(",");
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
        try
        {
            var typeName = model.GetType().GetTypeName();
            var _dbContextType = model.GetDbContextWithEntity();

            if (_dbContextType is null)
            {
                throw new NullReferenceException($"{GetType().GetTypeName()} не удалось определить контекст данных");
            }

            string text1 = "";
            foreach (var pproperty in properties)
            {
                var val = model.GetValue(pproperty);
                text1 += $"{val},";
            }

            using (var db = _dbContextType.New<DbContext>())
            {
                var rs = new ReflectionService();
                var dbset = db.GetDbSet(model.GetType().GetTypeName());
                foreach (dynamic next in dbset)
                {
                    string text2 = "";
                    foreach(var pproperty in properties)
                    {
                        var val = rs.GetValue(next, pproperty);
                        text2 += $"{val},";
                    }
                    
                    if (val is null || value is null)
                    {
                        if (value == val)
                        {
                            return GetMessage(model, property, value);
                        }
                    }
                    else
                    {
                        if (value.ToString().ToLower() == val.ToString().ToLower())
                        {
                            return GetMessage(model, property, value);
                        }
                    }
                }
                return null;
            }
        }    
        catch (Exception ex)
        {
            return $"Метод проверки упал с исключением: {ex.Message}";
        }
    }

    public override string GetMessage(object model, string property, object value)
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
 

using Console_UserInterface.Pages.Auth;

using Microsoft.EntityFrameworkCore;

namespace Console_UserInterface.ControlAttributes
{
    public class SelectDataAttribute: ControlAttribute
    {
        string entity; 
        string field;
        public SelectDataAttribute( string entity, string field )
        {
            this.entity = entity;
            this.field = field;
        }
        public ViewItem CreateControl(InputFormField field)
        {
            var exp = $"{entity},{field}";
            Dictionary<int, string> result = new();
            if (String.IsNullOrWhiteSpace(exp) || exp.Contains(",") == false)
                throw new ArgumentException("exp", "Строка должна содержать имя сущности и свойство для отобраажени я через запятую");
            try
            {
                var entity = exp.Split(",")[0];
                var property = exp.Split(",")[1];
                foreach (Type dbtype in GetType().Assembly.GetTypes<DbContext>())
                {
                    DbContext dbc = (DbContext)dbtype.New();
                    var types = dbc.GetEntitiesTypes().Select(p => p.GenericTypeArguments.Count() > 0 ? p.GenericTypeArguments[0].GetTypeName() : p.GetTypeName()).ToList();
                    if (types.Contains(entity))
                    {
                        var pdbset = dbc.GetType().GetProperties().First(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(entity.ToType()));
                        var pdbsetref = pdbset.GetValue(dbc);
                        foreach (object item in (dynamic)pdbsetref)
                        {
                            var id = item.GetValue("Id");
                            if (id is null)
                            {
                                id = item.GetValue("Id");
                            }
                            var label = item.GetValue(property);
                            result[int.Parse(id.ToString())] = label.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error("Не удалось получить справочник " + exp + " " + ex.Message);
            }
          
            return new SelectControlModel()
            {
                Options = result.Values.ToList()
            };
        }
        public ViewItem CreateControl(FormField field) => throw new Exception();
        public virtual void Layout(object form) { }

        public override bool IsValidValue(object value) => true;
    }
}

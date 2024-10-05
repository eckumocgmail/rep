
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Console_UserInterface.ControlAttributes
{
    public class SelectDataAttribute: ControlAttribute
    {
        string entity; 
        string field;
        public SelectDataAttribute( string exp ): base(ControlTypes.Select)
        {
            this.entity = exp.Split(".")[0];
            this.field = exp.Split(".")[1];    
            Dictionary<int, string> result = new();      
            
            bool founded = false;
            List<Type> all = new();
            all.AddRange(GetType().Assembly.GetTypes<DbContext>());
            all.AddRange(Assembly.GetExecutingAssembly().GetTypes<DbContext>());
            all.AddRange(Assembly.GetCallingAssembly().GetTypes<DbContext>());
            ServiceFactory.Get().GetAssemblies().ForEach(assembly => { all.AddRange(assembly.GetTypes<DbContext>()); });
            foreach (Type dbtype in all)
            {
                this.Info(dbtype);
                DbContext dbc = (DbContext)dbtype.New();
                var types = dbc.GetEntitiesTypes().Select(p => p.GenericTypeArguments.Count() > 0 ? p.GenericTypeArguments[0].GetTypeName() : p.GetTypeName()).ToList();
                if (types.Contains(entity))
                {
                    founded = true;
                    var pdbset = dbc.GetType().GetProperties().First(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(entity.ToType()));
                    var pdbsetref = pdbset.GetValue(dbc);
                    foreach (object item in (dynamic)pdbsetref)
                    {
                        var id = item.GetValue("Id");
                        if (id is null)
                        {
                            id = item.GetValue("Id");
                        }
                        var label = item.GetValue(field);
                        result[int.Parse(id.ToString())] = label.ToString();
                    }
                }
            }
            if (!founded)
                throw new Exception("Не найдена DbSet для: "+ entity);


            Options = result.Values.ToList();
            if (Options.Count() == 0)
                throw new ArgumentException();
           
        }
        public List<string> Options { get; set; }

        public override ViewItem CreateControl(InputFormField field)  
        {

            /*Dictionary<int, string> result = new();           
            try
            {
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
                            var label = item.GetValue(this.field);
                            result[int.Parse(id.ToString())] = label.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.Error("Не удалось получить справочник " + " " + ex.Message);
            }*/

                
            Dictionary<int, string> result = new();

            bool founded = false;
            List<Type> all = new();
            all.AddRange(GetType().Assembly.GetTypes<DbContext>());
            all.AddRange(Assembly.GetExecutingAssembly().GetTypes<DbContext>());
            all.AddRange(Assembly.GetCallingAssembly().GetTypes<DbContext>());
            ServiceFactory.Get().GetAssemblies().ForEach(assembly => { all.AddRange(assembly.GetTypes<DbContext>()); });
            foreach (Type dbtype in all)
            {
                this.Info(dbtype);
                DbContext dbc = (DbContext)dbtype.New();
                var types = dbc.GetEntitiesTypes().Select(p => p.GenericTypeArguments.Count() > 0 ? p.GenericTypeArguments[0].GetTypeName() : p.GetTypeName()).ToList();
                if (types.Contains(entity))
                {
                    founded = true;
                    var pdbset = dbc.GetType().GetProperties().First(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(entity.ToType()));
                    var pdbsetref = pdbset.GetValue(dbc);
                    foreach (object item in (dynamic)pdbsetref)
                    {
                        var id = item.GetValue("Id");
                        if (id is null)
                        {
                            id = item.GetValue("Id");
                        }
                        var label = item.GetValue(this.field);
                        result[int.Parse(id.ToString())] = label.ToString();
                    }
                }
            }

            return new SelectControlModel()
            {
                Options = result.Values.ToList()
            };

        }
        public virtual void Layout(object form) { }

        public override bool IsValidValue(object value) => true;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            var result = base.IsValid(value);
            return result;
        }

        public override string Validate(object model, string property, object value)
        {
            return base.Validate(model, property, value);
        }

        public override string OnValidate(object model, string property, object value)
        {
            return base.OnValidate(model, property, value);
        }

        public override string Validate(object value)
        {
            return base.Validate(value);
        }
    }
}

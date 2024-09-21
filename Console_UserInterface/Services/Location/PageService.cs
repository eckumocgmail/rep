using Newtonsoft.Json;

using static Console_UserInterface.Services.Location.LocationDbContext;

namespace Console_UserInterface.Services.Location
{
    public class PageService : NamedObject
    {
        public AppPage CreateNavPage(string title, Dictionary<string, string> model)
        {
            AppPage result = new();
            result.Name = title;
            result.Description = "Навигация";
            result.ModelType = model.GetType().GetTypeName();
            result.ModelInstance = model;
            result.PageModel = model.ToJson();
            foreach (var kv in model)
            {
                var navLink = CreateNavLink(kv.Key, kv.Value);
                var listItem = CreateListItem(navLink);
                result.PageComponents.Add(listItem);
            }
            return result;
        }

        public PageComponent CreateListItem(params PageComponent[] children)
        {
            var component = new PageComponent();
            component.Control = new LayoutRow(children);
            return component;
        }

        public PageComponent CreateNavLink(string key, string value)
        {
            throw new NotImplementedException();
        }

        public List<string> GetInputProperties(Type ptype)
        {
            return ptype.GetInputProperties();
        }
        public string GetInputType(Type ptype)
        {
            switch (ptype.GetTypeName())
            {
                default: throw new NotImplementedException($"Тип {ptype.GetTypeName()} не поддерживается");
            }
        }
        public string GetInputType(Dictionary<string, string> attributes)
        {
            var attribute = attributes.Keys.FirstOrDefault(key => key.ToType().IsExtends(typeof(BaseInputAttribute)));
            if (attribute is null)
            {
                throw new ArgumentException("attributes", $"Аргумент attributes={attributes.ToJson()} не содержит ни одного ключа с атрибутами ввода");
            }
            BaseInputAttribute input = attribute.ToType().Create<BaseInputAttribute>(new object[] { attributes[attribute] });
            return input._InputType;
        }

        public PageComponent CreatePageComponent(AppPage page, Type ptype, object instance, string property)
        {
            PageComponent result = new();

            result.Order = ptype.GetAttribute<InputOrderAttribute>("0").ToInt();
            result.Label = ptype.GetPropertyLabel(property);
            result.Icon = ptype.GetPropertyIcon(property);
            result.Description = ptype.GetPropertyDescription(property);

            result.Name = property;
            result.Attributes = ptype.GetPropertyAttributes(property);

            Type propertyType = ptype.GetProperty(property).PropertyType;
            if (propertyType.IsCollectionType())
            {
                result.IsCollection = true;
                Type ItemType = propertyType.GetGenericArguments()[0];
                result.IsPrimitive = ItemType.IsPrimitive;
                result.Type = ItemType.GetTypeName();
            }
            else
            {
                result.IsPrimitive = propertyType.IsPrimitive();
                if (result.IsPrimitive)
                {
                    ControlAttribute control = GetControlType(result.Attributes);
                    if (control is not null)
                    {
                        result.Control = control;
                    }
                    else
                    {
                        result.Type = GetInputType(result.Attributes);
                        if (result.Type is null)
                        {
                            result.Type = GetInputType(propertyType);
                        }
                    }
                }
                else
                {
                    result.Type = InputTypes.Custom;
                }
            }
            result.Getter = () =>
            {
                var value = instance.GetValue(property);
                this.Info($"get {property} => {value}");
                return value;
            };
            result.Setter = (val) =>
            {
                instance.SetValue(property, val);
                this.Info($"set {property} {val}");
                page.PageModel = instance.ToJson();
                page.ModelInstance.Validate();
            };

            return result;
        }

        public ControlAttribute GetControlType(Dictionary<string, string> attributes)
        {
            var attribute = attributes.Keys.FirstOrDefault(key => key.ToType().IsExtends(typeof(ControlAttribute)));
            if (attribute is null)
            {
                return null;
            }
            ControlAttribute control = attribute.ToType().Create<ControlAttribute>(new object[] { attributes[attribute] });
            return control;
        }

        public AppPage CreatePage<TEntity>()
        {
            return CreatePage(typeof(TEntity), typeof(TEntity).New());
        }
        public AppPage CreatePage(Type ptype, object instance)
        {
            if (ptype is null)
            {
                throw new ArgumentNullException("ptype", $"Необходимо передать в аргумент {nameof(ptype)} функции {nameof(CreatePage)} действительное значение тип {nameof(Type)} ");
            }
            if (instance is null)
            {
                throw new ArgumentNullException("instance", $"Необходимо передать в аргумент {nameof(instance)} функции {nameof(CreatePage)} действительное значение тип {nameof(Object)} ");
            }

            AppPage result = new();
            result.Name = ptype.GetLabel();
            result.Description = ptype.GetDescription();
            result.ModelType = ptype.GetTypeName();
            result.ModelInstance = instance;
            result.PageModel = instance.ToJson();

            var properties = GetInputProperties(ptype);
            this.Info($"CreatePage for type {ptype.GetTypeName()} from {instance.ToJson()}");
            foreach (var prop in properties)
            {
                try
                {
                    var component = CreatePageComponent(result, ptype, instance, prop);
                    result.PageComponents.Add(component);
                    this.Info($"success: Created PageComponent '{prop}' type '{ptype.GetTypeName()}' from '{instance.ToJson()}' completed: {component.ToJson()}");
                }
                catch (Exception ex)
                {
                    this.Error($"error: Created PageComponent '{prop}' type '{ptype.GetTypeName()}' from '{instance.ToJson()}' exception: {ex.Message}");
                    continue;
                }
            }
            return result;
        }


    }
}

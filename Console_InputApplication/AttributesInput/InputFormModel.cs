using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
/// <summary>
/// Модель данных отображаемых в представлении
/// формы ввода
/// </summary>
public class InputFormModel
{



    [Label("Контейнер")]
    [InputSelect("h-group,v-group")]
    public string Container { get; set; }

    /// <summary>
    /// установливает размеры formcontrol
    /// </summary>
    [Label("Размер")]
    [InputSelect("small,normal,big")]
    public string Size { get; set; }

    [Label("Ссылка на объект")]
    [NotMapped]
    [NotInput]
    public object Item { get; set; }

    [Label("Поля ввода")]
    [InputStructureCollection(nameof(InputFormField))]
    [JsonIgnore]
    [NotMapped]
    public List<InputFormField> FormFields { get; set; } = new List<InputFormField>();


    [Label("Действие по завершению")]
    [JsonIgnore()]
    [NotInput("")]
    [NotMapped]
    public Action<object> OnComplete { get; set; }

    [Label("Разрешено изменение")]
    [NotInput]
    public bool Edited { get; set; }

    [NotMapped]
    [JsonIgnore]
    [NotInput]
    public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

    [Label("Состояние")]
    [NotInput]
    [UpdateWhenChanged(false)]
    [InputSelect("valid,invalid")]
    public bool IsValid { get; set; }

    [NotInput]
    [Label("Сообщение об ошибке")]
    [InputRusWord]

    public string Error { get; set; }

    public InputFormModel() : base()
    {
        IsValid = false;
        Title = "Заголовок";
        Description = "Описание";
        Error = "";
        Container = "group";
        Size = "normal";
    }
    public string Title { get; set; }
    public string Description { get; set; }
    public InputFormModel(object item) : this()
    {
        if (item == null)
            throw new Exception("Аргумент item на задан");
        IsValid = false;
        Item = item;
        Title = item.GetType().GetLabel();
        Description = item.GetType().GetDescription();
        Update(item.GetType().GetOwnPropertyNames().ToArray());
    }

    public InputFormModel(object item, params string[] propertyNames) : this()
    {
        if (item == null)
            throw new Exception("Аргумент item на задан");
        IsValid = false;
        Title = item.GetType().GetLabel();
        Description = item.GetType().GetDescription();
        Item = item;
        Update(propertyNames);

    }





    public void EnsureThatItemIsValide()
    {
        if (Item is MyValidatableObject)
        {
            ((MyValidatableObject)Item).EnsureIsValide();
        }
        else
        {
            throw new Exception($"Объект не наследует тип {nameof(MyValidatableObject)}");
        }
    }




    /// <summary>
    /// Обновление изолированной области в модели
    /// </summary>
    /// <param name="model"></param>
    public object Update(string[] propertyNames)
    {
        var item = Item;
        var form = this;
        if (item == null)
        {
            throw new Exception("Установите свойство Item для модели " + GetType().Name);
        }
        form.Item = item;
        form.Title = form.Title == null ? Attrs.LabelFor(item) : form.Title;
        form.Description = Attrs.DescriptionFor(item);
        if (Typing.ReferenceIsDictionary(item) == false)
        {
            form.FormFields = CreateFormFields(item.GetType(), propertyNames, item);

        }
        else
        {
            FormFields = CreateFormFields(Item.GetType(), propertyNames, Item);
        }

        FormFields.Sort((f1, f2) =>
        {
            return f1.Order - f2.Order;
        });

        return this;
    }

    public int OnSort(InputFormField left, InputFormField right)
    {
        if (left.Type.ToLower() == "multilinetext")
        {
            return 1;
        }
        else if (right.Type.ToLower() == "multilinetext")
        {
            return -1;
        }
        else
        {
            return 1;
        }
    }

    /// <summary>
    /// Уникальные поля-1, примитивные-2, Многострочные и файловые-4, остальные-3
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    private int SortOrder(FormField field)
    {

        if (field.Type == "MultilineText" || field.Type == "File" || field.Type == "Image" || field.Type == "Foto") return 3;
        return 2;
    }

    public global::Table ForPrimitiveCollection(dynamic items, Type type, string caption)
    {
        global::Table tm = new global::Table();
        tm.type = type;
        tm.IsPrimitive = true;
        tm.Columns = new List<string> { caption };
        tm.Cells = new List<List<object>>();
        foreach (var item in items)
        {
            tm.Cells.Add(new List<object> { item });
        }
        tm.Editable = true;
        return tm;
    }
    public global::Table ForCollection(dynamic items, Type type)
    {
        global::Table tm = new global::Table();
        tm.type = type;
        tm.Columns = (from p in ReflectionService.GetOwnPropertyNames(type) select p).ToList();
        foreach (var item in items)
        {
            try
            {
                tm.Cells.Add(ReflectionService.Values(item, tm.Columns));
            }
            catch (Exception ex)
            {
                tm.Cells.Add(new List<object>() { $"Ошибка при получении значения: {ex.Message}" });
            }

        }
        return tm;
    }

    public global::Table ForCollectionProperty(object target, string property)
    {
        string typeName =
            Typing.ParseCollectionType(target.GetType().GetProperty(property).PropertyType);
        if (Typing.IsPrimitive(typeName))
        {
            return ForPrimitiveCollection(
                        target.GetType().GetProperty(property).GetValue(target),
                        ReflectionService.TypeForName(typeName),
                        Attrs.DescriptionFor(target.GetType(), property));
        }
        else
        {
            return ForCollection(
                        target.GetType().GetProperty(property).GetValue(target),
                        ReflectionService.TypeForName(typeName));
        }

    }


    /// <summary>
    /// Создание модели табличного представления из свойств обьекта
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public global::Table ForDictionary(object model)
    {

        global::Table tm = new global::Table();

        tm.Columns = ReflectionService.GetOwnMethodNames(model.GetType());
        List<object[]> cells = new List<object[]>();

        //cells
        List<object> row = new();
        int i = 0;
        foreach (string column in tm.Columns)
        {
            row.Add(GetValue(model, column));
        }
        cells.Add(row.ToArray());

        //tm.Cells = cells.ToArray();
        tm.Cells = new List<List<object>>();// new Newtonsoft.Json.Linq.JArray();
        return tm;
        //return ForDictionary(Formating.ToDictionaryLabels(model), Attrs.LabelFor(model.GetType()));
    }

    public Table ForDictionary(IDictionary<string, object> properties, string title)
    {
        var table = new Table();
        table.Title = title;
        table.Searchable = true;
        table.Columns = new List<string> { "Ключ", "Значение" };
        table.Cells = new List<List<object>>();
        foreach (var p in properties)
        {
            table.Cells.Add(new List<object> { p.Key, p.Value });
        }
        return table;
    }

    public InputFormField CreateFormField( Type type, string PropertyName)
    {
        InputFormField field = null;
        this.Info(PropertyName);
        var property = type.GetProperty(PropertyName);
        if (property == null)
        {
            throw new Exception("Ошибка при создании поля " + PropertyName
            );
        }
        var propertyType = Typing.ParsePropertyType(property.PropertyType);
        if(propertyType is null)
        {
            propertyType = property.PropertyType.GetTypeName();
        }
        var Attributes = Attrs.ForProperty(type, property.Name);
        if(Attributes.ContainsKey(nameof(NotInputAttribute)))
        {
            throw new Exception();
        }

        field = new InputFormField();
        field.Target = Item;
        field.Property = property;
        field.Attributes = Attributes;

        field.Order = Attributes.ContainsKey(nameof(InputOrderAttribute)) ?
            Attributes[nameof(InputOrderAttribute)].ToInt() : 0;

        field.Name = property.Name;
        field.Label = Attrs.LabelFor(type, property.Name);
        field.Description = Attrs.DescriptionFor(type, property.Name);
        field.Icon = Attrs.IconFor(type, property.Name);
        field.Help = Attrs.HelpFor(type, property.Name);
        field.IsPrimitive = ReflectionService.IsPrimitive(propertyType);
           
        if (Attributes.ContainsKey(nameof(NotInputAttribute)))
        {
            return field;
        }
        if (ReflectionService.IsPrimitive(propertyType) == false)
        {
            if (Typing.IsCollectionType(property.PropertyType))
            {
                if (property.PropertyType == null)
                    throw new Exception("property.PropertyType == null");
                field.IsCollection = true;
                var itemType = property.PropertyType.GetGenericArguments()[0].GetTypeName();
                field.IsPrimitive = Typing.IsPrimitive(itemType);                                                                           //var table = ForCollectionProperty(Item, property.Name);
                field.Type = "custom";
                field.Value = Item.GetValue(PropertyName);
                if (field.Value is null && property.PropertyType.GetConstructors().Any(c => c.GetParameters().Count() == 0))
                    field.Value = property.PropertyType.New();
                field.ValueType = property.PropertyType.GetGenericArguments()[0].GetTypeName();// Typing.ParseCollectionType(property.PropertyType);
            }
            else
            {

                var p = property.GetValue(Item);
                if (p != null)
                {
                    var table = ForDictionary(p);
                    field.Type = "custom";
                    field.Control = table;
                    field.Value = property.PropertyType.New();
                }
                else
                {
                    field.Value = propertyType.New();
                    field.Type = "custom";
                    field.Value = property.PropertyType.New();
                }


            }
        }
        else
        {
            bool isInput = Attrs.IsInput(type, property.Name);


            field.Type = Attrs.GetControlType(type, property.Name);
            if (field.Type != null)
            {
                if (field.Type.ToLower().Contains("collect"))
                {
                    field.ValueType = Typing.ParseCollectionType(property.PropertyType);
                }
                string input =
                    Attributes.ContainsKey(field.Type) ? Attributes[field.Type] :
                    Attributes.ContainsKey(field.Type.Replace("Attribute", "")) ? Attributes[field.Type.Replace("Attribute", "")] :
                    Attributes.ContainsKey(field.Type + "Attribute") ? Attributes[field.Type + "Attribute"] :
                    throw new Exception("Не найден атрибут соответвующий типу элемента управления " + field.Type);
                Type fieldType = ReflectionService.TypeForShortName(field.Type);
                ControlAttribute attribute = ReflectionService.Create<ControlAttribute>(fieldType, new object[] { input });

                //field.Control = attribute.CreateControl(field);
                field.Type = field.Type.Replace("Attribute", "");
                field.TextValue = input;
                switch (field.Type)
                {
                    case nameof(InputDictionaryAttribute):
                        {
                            string entity = input.Split(",")[0];
                            string display = input.Split(",")[1];

                            break;
                        }
                }
            }
           
            else
            {
                field.Type = GetInputType(type, property.Name);
                field.Type = field.Type.ToLower();
            }
        }
            

        
        field.EnsureIsValide();
        return field;
    }

    /// <summary>
    /// Создание полей формы для отображения и ввода информации
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public List<InputFormField> CreateFormFields(Type type, string[] propertyNames, object target = null)
    {
        List<InputFormField> fields = new List<InputFormField>();
        foreach (var propertyName in propertyNames)
        {

            try
            {
                var field = CreateFormField(type, propertyName);
                if (target != null)
                {
                    var val = GetValue(target, propertyName);
                    if (val is not null)
                    {
                        field.Value = val;
                    }

                }
                fields.Add(field);
            }catch(Exception ex)
            {
                this.Error(ex);
            }
        }
        return fields;
    }

    private ViewItem CreateControl(InputFormField field)
    {
        return null;
    }



    public string GetInputType(Type type, string property)
    {
        string result = Attrs.GetInputType(type, property);
        if (result != null) return result;
        var propertyInfo = type.GetProperty(property);
        string propertyType = Typing.ParsePropertyType(propertyInfo.PropertyType);

        if (Typing.IsDateTime(propertyInfo))
        {
            return "date";
        }
        else if (Typing.IsBoolean(propertyInfo))
        {
            return "checkbox";
        }
        else if (Typing.IsNumber(propertyInfo))
        {
            return "number";
        }
        else
        {
            return "text";
        }




    }

    private bool IsEnumerable(Type type)
    {
        return type.Name.StartsWith("List") || type.Name.StartsWith("HashSet");
    }


    /// <summary>
    /// Извлекает значение свойства из обьекта по наименованию
    /// </summary>
    /// <param name="model"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    private object GetValue(object model, string property)
    {
        PropertyInfo propertyInfo = model.GetType().GetProperty(property);
        FieldInfo fieldInfo = model.GetType().GetField(property);
        return
            fieldInfo != null ? fieldInfo.GetValue(model) :
            propertyInfo != null ? propertyInfo.GetValue(model) :
            null;
    }

    private List<string> Validate(InputFormField field)
    {

        List<string> errors = new List<string>();
        foreach (var validationInfo in field.CustomValidators)
        {
            try
            {
                MyValidation validation = ReflectionService.Create<MyValidation>(validationInfo.Key, validationInfo.Value.ToArray());
                string error = validation.Validate(Item, field.Name, field.Value);
                if (string.IsNullOrEmpty(error) == false)
                {
                    errors.Add(error);
                }
            }
            catch (Exception ex)
            {
                errors.Add($"Элемент ввода: {field.Name}" + "Ошибка при выподлнении валидации. Забавно! " + ex.Message);
            }

        }
        return errors;

    }
    public Dictionary<string, List<string>> Validate()
    {

        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        if (Item is BaseEntity)
        {
            result = ((BaseEntity)Item).Validate();
        }
        else
        {
            if (Typing.ReferenceIsDictionary(Item))
            {
                foreach (var field in FormFields)
                {
                    List<string> errors = Validate((InputFormField)field);
                    if (errors != null && errors.Count() > 0)
                    {
                        result[((InputFormField)field).Name] = errors;
                    }
                }
            }
            else
            {
                return Item.Validate();
            }
        }
        return result;
    }


    public Dictionary<string, object> GetValues()
    {
        Dictionary<string, object> values = new Dictionary<string, object>();
        this.FormFields.ForEach((field) =>
        {
            values[((InputFormField)field).Name] = ((InputFormField)field).Value;
        });
        return values;
    }

    public InputFormField FindFieldByName(string propertyName)
    {
        return (InputFormField)(from p in FormFields where ReflectionService.GetValueFor(p, "Name").ToString() == propertyName select p).SingleOrDefault();
    }




}

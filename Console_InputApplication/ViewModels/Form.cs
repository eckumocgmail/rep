
using NetCoreConstructorAngular.Data.DataAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
/*

[InputIcon("apps")]
public class Form
{
    public string Title { get; set; }
    public string Description { get; set; } = "";



    //ссылка
    public string Area { get; set; } = "";
    public string Controller { get; set; }
    public string Action { get; set; }
    public string Method { get; set; } = "POST";


    [UpdateWhenChanged(false)]
    public bool IsValid { get => Get<bool>("IsValid"); set => Set<bool>("IsValid", value); }

    //[JsonIgnore()]
    //public new object Item { get => Get<object>("Item"); set => Set<object>("Item", value); }
    public string Error { get; set; }
         
    [InputSelect("h-group,v-group")] 
    public string Container { get; set; }


    /// <summary>
    /// установливает размеры formcontrol
    /// </summary>
    [InputSelect("small,normal,big")]
    public string Size { get; set; }

    [JsonIgnore()]
    [NotInput("")]
    public Action<object> OnComplete { get => Get<Action<object>>("OnComplete"); set => Set<Action<object>>("OnComplete", value); }

    


    public bool NullValue { get; set; } = false;

    [JsonIgnore]
    public List<object> FormFields { get; set; } = new List<object>();

  
    [JsonIgnore]
    public List<Button> Buttons { get; set; } = new List<Button>();

    [NotMapped()]
    public string[] PropertyNames { get; set; }



    public FlexContainer StructuredControls { get; set; }
         



    [JsonIgnore()]
    public Func<string, object, object> OnFormFieldValueChanged { get; set; }
    public Form():base(){
        var form = this;
        OnFormFieldValueChanged = (name, val) =>
        {
            return this;
        };
        InitDesignModeControls();
     
        IsValid = false;

        Title = "Заголовок";
        Description = "Описание";
        Error = "";
        Container = "group";
        Edited = true;
        Size = "normal";
        Init();
        Item = new UserPerson();
        OnEvent += (message) => { 
            /*if(message is PropertyChangedMessage)
            {
                if(((PropertyChangedMessage)message).Property=="Item" && ((PropertyChangedMessage)message).Source == this)
                {
                    PropertyNames = ReflectionService.GetPropertyNames(Item.GetType()).ToArray();
                }
            }* /
        };
        this.ContextMenu = new ContextMenu() { 
            Items=new List<ContextMenu>() {
                new ContextMenu() {
                    Label="Test"
                }
            }
        };
        OnComplete = (message) =>
        {

            ConfirmDialog("Привет бро","Как тебе новый функционал?s");


        };


        ContextMenu.Items.Clear();
        ContextMenu.Items.Add(new ContextMenu() { 
            Label = "Проверка функции OnComplete" 

        });
        StructuresAreEditable = true;
        Changed = false;
    }



    public Form(object item) : this()
    {
        if (item == null)
            throw new Exception("Аргумент item на задан");
        IsValid = false;
        Item = item;
        Title = Chapter = Attrs.LabelFor(item.GetType());
        Description = Chapter = Attrs.DescriptionFor(item.GetType());
        if (item == null)
        {
            NullValue = true;
        }
        StructuresAreEditable = false;

        this.Changed = false;
        Update(this.PropertyNames = ReflectionService.GetPropertyNames(item.GetType()).ToArray());        
    }


    public Form(object item, params string[] propertyNames) : this()
    {
        IsValid = false;
        StructuresAreEditable = false;
        Item = item;
        Chapter = Attrs.LabelFor(item.GetType());
        if (item == null)
        {
            NullValue = true;
        };
        Edited = false;
        Update(this.PropertyNames = propertyNames);
        this.Changed = false;
    }



    



    internal Form EnableStructures()
    {
        
        StructuresAreEditable = true;
        Changed = false;
        return this;
    }




    public override ViewItem IsNotForUpdate()
    {
        base.IsNotForUpdate();
        FormFields.ForEach(f => {
            ((FormField)f).IsNotForUpdate();
        });
        return this;
    }


    [Label("Управление сложными типами")]
    public bool StructuresAreEditable { get => Get<bool>("StructuresAreEditable"); set => Set<bool>("StructuresAreEditable", value); }

    internal void EnsureThatItemIsValide()
    {
        if(Item is MyValidatableObject)
        {
            ((MyValidatableObject)Item).EnsureIsValide();
        }
        else
        {
            //TODO: ensure for ther
        }
    }

    private void InitDesignModeControls()
    {
        Top.Visible = DesignMode;

        var ctrl = this;
        /*var EditStructuresButton = new Button() {
            Label = "Вкл. сложные типы",
            OnClick = (button) => {
                ctrl.StructuresAreEditable = ctrl.StructuresAreEditable ? false : true;
                ctrl.Changed = true;
            }
        };
        Bind("EditStructures", EditStructuresButton, "ToggleButtonActive");
        TopToolbox.Append(EditStructuresButton);* /
    }


    public Form Update()
    {
     
        Update(ReflectionService.GetOwnPropertyNames(Item.GetType()).ToArray());
        return this;
    }


    public Form Update(Type selfType)
    {
        Update(ReflectionService.GetOwnPropertyNames(selfType).ToArray());
        return this;
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
            return this;
        }

        form.PropertyNames = propertyNames;
        form.EnableChangeSupport = true;
        form.Item = item;
        form.Title = form.Title == null ? Attrs.LabelFor(item) : form.Title;
        form.Description = Attrs.DescriptionFor(item);
        if (form.PropertyNames == null)
        {
            form.PropertyNames = ReflectionService.GetOwnPropertyNames(item.GetType()).ToArray();
        }
        if (Typing.ReferenceIsDictionary(item) == false)
        {
            form.FormFields = CreateFormFields(item.GetType(), form.PropertyNames, item);

        }

        form.FormFields.ForEach((f) =>
        {
            var fieldName = ((FormField)f).Name;
            ((FormField)f).OnFormFieldValueChanged += (val) => {
                form.OnFormFieldValueChanged(fieldName, val);
            };
            ((FormField)f).Edited = form.Edited ? false : true;
            ((FormField)f).Editable = form.Editable ? false : true;
            ((FormField)f).Selectable = form.Selectable ? false : true;
            ((FormField)f).Focusable = form.Focusable ? false : true;
            ((FormField)f).Checkable = form.Checkable ? false : true;
            ((FormField)f).Draggable = form.Draggable ? false : true;
        });
         
        form.FormFields.Sort((i1,i2)=>{
            return SortOrder((FormField)i1) - SortOrder((FormField)i2);
        });
        return this;
    }

    /// <summary>
    /// Уникальные поля-1, примитивные-2, Многострочные и файловые-4, остальные-3
    /// </summary>
    /// <param name="field"></param>
    /// <returns></returns>
    private int SortOrder(FormField field)
    {
        if( field.IsUniq() ) return 1;
        if(field.Type== "MultilineText" || field.Type == "File" || field.Type == "Image" || field.Type == "Foto") return 3;
        return 2;
    }


    public List<object> CreateFormFields(Type type)
    {
        return CreateFormFields(type, ReflectionService.GetOwnPropertyNames(type).ToArray());
    }


    public Form UpdateFormFields(string[] propertyNames)
    {
        FormFields = CreateFormFields(Item.GetType(), propertyNames, Item);
        return this;
    }

    /// <summary>
    /// Создание полей формы для отображения и ввода информации
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public List<object> CreateFormFields(Type type, string[] propertyNames, object target = null)
    {
        
        List<object> fields = new List<object>();
        StructuredControls = new FlexContainer();
        StructuredControls.Horizontal = false;
        BottomToolbox.Append(StructuredControls);
        foreach (var propertyName in propertyNames)
        {
            try { 
                var property = type.GetProperty(propertyName);        
                if( property == null){
                    throw new Exception("Ошибка при создании поля "+propertyName
                    );
                }
                var propertyType = Typing.ParsePropertyType(property.PropertyType);
                var Attributes = Attrs.ForProperty(type, property.Name);

                global::FormField field = new global::FormField() { };
                field.Attributes = Attributes;
                field.Name = property.Name;
                field.Label = Attrs.LabelFor(type, property.Name);
                field.Description = Attrs.DescriptionFor(type, property.Name);
                field.Icon = Attrs.IconFor(type, property.Name);
                field.Visible = property.Name == "Id" ? false : Attrs.IsVisible(type, property.Name);
                field.Help = Attrs.HelpFor(type, property.Name);
                if (Attributes.ContainsKey(nameof(NotInputAttribute)))
                {
                    continue;
                }
                if (ReflectionService.IsPrimitive(propertyType) == false)
                {                   
                    if (StructuresAreEditable)
                    {
                        if (Typing.IsCollectionType(property.PropertyType))
                        {
                            var service = new TableService();
                            var table = service.ForCollectionProperty(Item, property.Name);
                            StructuredControls.Append(table);
                         
                        }
                        else
                        {
                            var service = new TableService();
                            var p = property.GetValue(Item);
                            if( p != null ){
                                var table = service.ForDictionary(p);
                                StructuredControls.Append(table);
                            }
                                

                        }
                    }
                  
                    continue;
                    
                }
                else
                {
                    bool isInput = Attrs.IsInput(type, property.Name);
                    if (isInput == false)
                    {
                        continue;
                    }

                    field.Type = Attrs.GetControlType(type, property.Name);
                    if (field.Type != null)
                    {

                        string input = Attributes[field.Type];
                        Type fieldType = ReflectionService.TypeForShortName(field.Type);
                        ControlAttribute attribute = ReflectionService.Create<ControlAttribute>(fieldType, new object[] { input });
                        field.Control = attribute.CreateControl(field);
                        field.Type = field.Type.Replace("Attribute", "");
                    }
                    else
                    {
                        field.Type = GetInputType(type, property.Name);
                    }
                }
                if (target != null)
                {
                    field.Value = GetValue(target, property.Name);
                    field._Value.Subscribe((PropertyChangedMessage message) => {
                        property.SetValue(target, message.After);
                        if (target is ViewItem)
                        {
                            ((ViewItem)target).Changed = true;
                        }
                    });
                }



                fields.Add(field);

                
            }catch(Exception ex)
            {
                Writing.ToConsole(ex.Message);
                //                throw new Exception("Ошибка при создании поля ввода для свойства "+propertyName, ex);
            }


        }
        return fields;
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

    public override bool WasChanged()
    {
        return base.WasChanged();
    }

    private List<string> Validate(FormField field)
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
                catch(Exception ex)
                {
                    errors.Add($"Элемент ввода: {field.Name}" + "Ошибка при выподлнении валидации. Забавно! "+ex.Message);
                }

            }
            return errors;
        
    }
    public override Dictionary<string, List<string>> Validate()
    {
  
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();
        if( Item is BaseEntity)
        {
            result = ((BaseEntity)Item).Validate();
        }
        else
        {
            if(Typing.ReferenceIsDictionary(Item))
            {
                foreach(var field in FormFields)
                {
                    List<string> errors = Validate((FormField)field);
                    if(errors!=null && errors.Count() > 0)
                    {
                        result[((FormField)field).Name] = errors;
                    }
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }
        return result;
    }


    public string GetActionLink()
    {
        if( string.IsNullOrEmpty(Area))
        {
            return $"/{Controller}/{Action}";
        }
        else
        {
            return $"/{Area}/{Controller}/{Action}";
        }
    }

    public Dictionary<string, object> GetValues()
    {
        Dictionary<string, object> values = new Dictionary<string, object>();
        this.FormFields.ForEach((field)=> {
            values[((FormField)field).Name] = ((FormField)field).Value;
        });
        return values;
    }

    public FormField FindFieldByName(string propertyName)
    {
        return (FormField)(from p in FormFields where ReflectionService.GetValueFor(p, "Name").ToString() == propertyName select p).SingleOrDefault();
    }

 



    /*public void Update() {
        Clear();
        var form = this;
        FormFields.ForEach(f => { form.Append((ViewItem)f); });
    }
    * /
    public override object Clear()
    {         
        base.Clear();
        new List<ViewNode>(this.Children.ToArray()).ForEach(p =>
        {
            if(p is FormField)
            {
                this.Children.Remove(p);                
            }
        });
        return null;
    }

    public override ViewOptions SetEdited(bool edited)
    {
        base.SetEdited(edited);
        FormFields.ForEach(f => {
            ((FormField)f).SetEdited(edited);
        });
        return this;
    }
}
*/

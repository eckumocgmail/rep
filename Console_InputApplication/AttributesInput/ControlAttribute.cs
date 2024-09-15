using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

public class ControlAttribute : BaseInputAttribute
{
    public ControlAttribute() : base(InputTypes.Custom)
    {
    }

    public virtual ViewItem CreateControl(InputFormField field) => throw new Exception();
    public virtual ViewItem CreateControl(FormField field) => throw new Exception();
    public virtual void Layout(object form) { }

    public override bool IsValidValue(object value) => true;
}


public class InputFormField
{
    [JsonIgnore]
    [NotInput()]
    public MemberInfo Property { get; set; }

    [JsonIgnore()]
    [NotInput()]
    [Label("Ссылка на объект")]
    public object Target { get; set; }

    [NotInput]
    [NotMapped]
    [JsonIgnore]
    public virtual InputFormModel InputFormModel { get; set; }

    [InputHidden]
    [InputNumber]
    public int InputFormModelId { get; set; }

    


    [NotMapped()]
    [JsonIgnore]
    public ViewItem Control { get; set; }

    [InputPositiveInt()]
    [NotNullNotEmpty()]
    [Label("Приоритет вывода")]
    public int Order { get; set; }

    [Label("Видно на экране")]
    public bool Visible { get; set; } = true;



    [Label("Наименование")]
    [NotNullNotEmpty]
    [InputEngWord]
    public string Name { get; set; } = "Undefined";

    [Label("Иконка")]
    [InputIcon]
    [NotNullNotEmpty]
    public string Icon { get; set; } = "home";
    public string TextValue { get; set; }



    [Label("Описание")]
    public string Description { get; set; } = "Нет подробного описания";


    public string Type { get; set; } = "Text";
    public string ValueType { get; set; }

    [Label("Надпись")]
    public string Label { get; set; } = "Неизвестно";

    [Label("Разрешено изменение")]
    public bool Edited { get; set; }


    [NotMapped]
    [JsonIgnore]
    public object Value { get; set; }


    public bool IsPrimitive { get; set; }
    public bool IsCollection { get; set; } = false;

    [Label("Размер")]
    [InputSelect("small,normal,big")]
    public string Size { get; set; } = "normal";


    [Label("Состояние")]
    [InputSelect("valid,invalid,undefined")]
    public string State { get; set; } = "undefined";

    [NotMapped]
    [Label("Подсказка")]
    public string Help { get; set; } = "Нет справочной информации";

    [Label("Ошибки")]
    [NotMapped]
    [NotInput]
    public List<string> Errors { get; set; } = new List<string>();

    [Label("Атрибуты")]
    [NotMapped]
    [NotInput]
    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();


    [Label("Атрибуты")]
    [NotMapped]
    [JsonIgnore]
    [NotInput]
    public Dictionary<string, List<object>> CustomValidators { get; set; } = new Dictionary<string, List<object>>();


    public InputFormField() : base()
    {
  
    }

    public InputFormField(Type model, string name): base()
    {
        this.Icon = model.GetPropertyIcon(name);
        this.Name = model.GetPropertyIcon(name);
        this.Type = model.GetProperty(name).PropertyType.GetTypeName();
        this.State = "undefined";
        this.Label = model.GetPropertyLabel(name);
        this.Description = model.GetPropertyDescription(name);
        this.Help = model.GetPropertyHelp(name);
    }





    public string GetHtmlInputValue()
    {
        InputFormField Model = this;
        switch (Type.ToLower())
        {
            case "date":
                string year = "XXXX";
                string month = "XX";
                string day = "XX";
                if (Model.Value != null)
                {
                    System.DateTime? dateNullable = null;
                    if (Model.Value is System.DateTime)
                    {
                        dateNullable = (System.DateTime)Model.Value;
                    }
                    else
                    {
                        try
                        {
                            var str = Model.Value.ToString();
                            dateNullable = str.ToDate();
                        }catch(Exception)
                        {                            
                        }
                    }
                    if (dateNullable == null)
                    {
                        //throw new Exception("Значение DateTime не получено в итоге");
                        return "";
                    }
                    System.DateTime date = (System.DateTime)dateNullable;
                    year = date.Year.ToString();
                    month = (date.Month >= 10) ? date.Month.ToString() : ("0" + date.Month.ToString());
                    day = (date.Day >= 10) ? date.Day.ToString() : ("0" + date.Day.ToString());
                }
                return $"{year}-{month}-{day}";
            default: return Value != null ? Value.ToString() : "";

        }

    }

    public string GetInputId() => Name + "Input-" + GetHashCode();
    public InputFormField SetEdited(bool val)
    {
        this.Edited = val;
        return this;
    }
    public string GetInputName() => Name + "Input";
}

public class FormModel : FormModel<object>
{
    public FormModel(object target)
    {
        this.Item = target;
    }
}
public class FormModel<T> where T : class
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Message { get; set; }

    public List<FormFieldModel> FormFields { get; set; } = new();

    public T Item { get; set; }
    public Dictionary<string, List<string>> Errors { get; set; }
    public bool IsValid { get; set; } = false;
}

public enum FormFieldType
{
    Select, Checkbox, Text, Number, Data, DateTime, File, Image, Textarea
}

public class FormFieldModel : InputFormField
{
    public List<MyValidation> Validations { get; set; } = new();
    public string Name { get; set; }
    public string Value { get; set; }
    public string State { get; set; }
    public string Label { get; set; }
    public string Help { get; set; }

    public string Type { get; set; }
    public Action<string> OnInput { get; set; } = (str) => { };

    public string GetInputId() => $"id_{GetHashCode()}";
}

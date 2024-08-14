using Console_InputApplication.ViewEvents.Property;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class FormField : ViewItem
{
    public string Name { get => Get<string>("Name"); set => Set<string>("Name", value); }
    public string Type { get => Get<string>("Type"); set => Set<string>("Type", value); }
    public string Icon { get => Get<string>("Icon"); set => Set<string>("Icon", value); }


    public string GetHtmlInputValue()
    {
        return Value != null ? Value.ToString() : "";
    }
    [JsonIgnore()]
    public Property<object> _Value { get; set; }
    public object Value
    {
        get
        {
            return _Value.Value;
        }
        set
        {
            OnFormFieldValueChanged(_Value.Value = value);
        }
    }

    [JsonIgnore()]
    public Action<object> OnFormFieldValueChanged { get; set; }

    // { get => Get<object>("Value"); set => Set<object>("Value", value); }

    public string Label { get; set; }// { get => Get<string>("Label"); set => Set<string>("Label", value); }
    public string Description { get; set; }// { get => Get<string>("Description"); set => Set<string>("Description", value); }
    public override bool Focused { get; set; }

    public bool ShowControls { get; set; } = true;


    [InputSelect("small,normal,big")]
    public string Size { get; set; } = "normal";

    [InputSelect("valid,invalid,undefined")]
    public string State { get; set; }// { get => Get<string>("State"); set => Set<string>("State", value); }


    [NotMapped()]
    public ViewItem Control { get; set; }


    public bool IsUniq()
    {
        return Attributes.ContainsKey(nameof(UniqValueAttribute));
    }


    [NotMapped]
    public string Help { get; set; }// { get => Get<string>("Help"); set => Set<string>("Help", value); }

    [NotMapped]
    public List<string> Errors { get; set; } = new List<string>();
    [NotMapped]
    public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();


    public Dictionary<string, List<object>> CustomValidators = new Dictionary<string, List<object>>();


    public FormField() : base()
    {
        OnFormFieldValueChanged = (val) => {
        };
        this._Value = new Property<object>(null, "Value");
        this.Icon = "home";
        this.ClassList.Add("input-group mb-3");
        this.Name = "Undefined";
        this.Type = "Text";
        this.State = "undefined";
        this.Label = "Неизвестно";
        this.Description = "Нет подробного описания";
        this.Help = "Нет справочной информации";
        this.Focusable = false;
        this.Changed = false;
    }




    public override bool WasChanged()
    {
        return base.WasChanged();
    }



    public string GetInputId()
    {
        return Name + "Input-" + GetContentPath();
    }

    public string GetInputName()
    {
        return Name + "Input";
    }

    public Dictionary<string, List<string>> Validate()
    {
        throw new System.NotImplementedException();
    }
}
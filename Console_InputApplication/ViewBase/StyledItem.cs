using Newtonsoft.Json;

using System;
using System.Collections.Generic;

[Label("Компонент представления со свойствами стилизации")]
public class StyledItem : ViewBuilder
{

    [Label("Курсор")]
    [NotNullNotEmpty("Не задано свойство стилизации курсора")]
    [InputSelect("wait,w-resize,col-resize,grabbing,sw-resize,nw-resize,ne-resize,se-resize,e-resize,s-resize,n-resize,inherit")]
    public string Cursor { get; set; }

    [Label("Относительная ширина (%)")]
    [JsonIgnore()]
    [NotNullNotEmpty("Не задана относительная ширина")]
    [InputPercent("Относительная ширина задаётся как процент ( т.е. значение внутри интервала [0,100])")]
    public int Width { get => Get<int>("Width"); set => Set<int>("Width", value); }

    [Label("Относительная высота (%)")]
    [JsonIgnore()]
    [NotNullNotEmpty("Не задана относительная ширина")]
    [InputPercent("Относительная высота задаётся как процент ( т.е. значение внутри интервала [0,100])")]
    public int Height { get => Get<int>("Height"); set => Set<int>("Height", value); }
 

    [Label("Толщина контура границы")]
    [JsonIgnore()]
    [InputNumber("Нужно использовать численный тип данных")]
    [InputSelect("Значение должно быть положительным")]
    [Units("px")]
    public float BorderWidth { get => Get<float>("BorderWidth"); set => Set<float>("BorderWidth", value); }

    [Label("Цвет контура границы")]
    [JsonIgnore()]
    [InputColor(null)]    
    public string BorderColor { get => Get<string>("BorderColor"); set => Set<string>("BorderColor", value); }


    [JsonIgnore()]
    [InputNumber(null)]
    [Units("px")]
    [Label("Размер шрифта")]
    [InputSelect("Значение должно быть положительным")]
    public float FontSize { get => Get<float>("FontSize"); set => Set<float>("FontSize", value); }

    [Label("Цвет текста")]
    [JsonIgnore()]
    [InputColor(null)]
    public string Color { get => Get<string>("Color"); set => Set<string>("Color", value); }

    [Label("Цвет фона")]
    [JsonIgnore()]
    [InputColor("Введите значечние цвета")]
    public string BackgroundColor { get => Get<string>("BackgroundColor"); set => Set<string>("BackgroundColor", value); }

    [InputSelect("left,right,justify")]
    public string TextAlign { get => Get<string>("TextAlign"); set => Set<string>("TextAlign", value); }

    [Units("px")]
    public float TextIndent { get => Get<float>("TextIndent"); set => Set<float>("TextIndent", value); }

    [Units("px")]
    public float Margin { get => Get<float>("Margin"); set => Set<float>("Margin", value); }

    [Units("px")]
    public float Padding { get => Get<float>("Padding"); set => Set<float>("Padding", value); }


    public StyledItem() : base()
    {
        FontSize = 16.0f;
        Color = "#000000";
        BackgroundColor = "#ffffff";
        BorderWidth = 1.0f;
        BorderColor = "#ffffff";         
        Width = 100;
        Height = 100;
        Cursor = "inherit";
        Changed = false;
    }

    public StyledItem GetStyle()
    {
        return (StyledItem)this;
    }

    public string GetCssText()
    {
        var seriallizer = new CssSerializer();
        string[] options = ReflectionService.GetOwnPropertyNames(typeof(StyledItem)).ToArray();
        Dictionary<string, object> valuesMap = new Dictionary<string, object>();
        foreach (var option in options)
            valuesMap[option] = this.GetValue(option);
        string cssText = seriallizer.Seriallize(valuesMap, CommonUtils.ForAllPropertiesInType(typeof(StyledItem)));
        return cssText;
    }
  
    public StyledItem MaxSize()
    {
        this.Width = 100;
        this.Height = 100;
        return this;
    }

    public void SetBaseTextStyle()
    {
        this.FontSize = 12;
        this.Color = "#000000";
    }
}
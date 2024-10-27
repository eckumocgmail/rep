using Org.BouncyCastle.Crmf;

using System.ComponentModel.DataAnnotations.Schema;

namespace Console_UserInterface.Services.View
{
    public class FormModel : BaseEntity
    {
        public string Title { get; set; } = "Новая форма";
        public List<FieldModel> Fields { get; set; } = new();
    }
    public class FieldModel : BaseEntity
    {
        public int FormModelId { get; set; }
        public string FieldTitle { get; set; }

        public FieldType FieldType { get; set; }

    }
    public enum FieldType
    {
        Input, Control, Collection, Custom
    }
    public class ControlModel : FieldModel
    {
        public ControlType ControlType { get; set; }
        public ControlModel()
        {
            FieldType = FieldType.Control;
        }
    }
    public enum ControlType
    {
        Select, MultiSelect, Checkbox, Callendar, Switch
    }
    public class SelectControl : ControlModel
    {
        public SelectControl()
        {
            ControlType = ControlType.Select;
        }
    }
    public class MultiSelectControl : ControlModel
    {
        public MultiSelectControl()
        {
            ControlType = ControlType.MultiSelect;
        }
    }
    public class CheckboxControl : ControlModel
    {
        public CheckboxControl()
        {
            ControlType = ControlType.Checkbox;
        }
    }
    public class CallendarControl : ControlModel
    {
        public CallendarControl()
        {
            ControlType = ControlType.Callendar;
        }
    }
    public class SwitchControl : ControlModel
    {
        public SwitchControl()
        {
            ControlType = ControlType.Switch;
        }
    }
    public class InputModel : FieldModel
    {
        public InputType InputType { get; set; }
        public InputModel()
        {
            FieldType = FieldType.Input;
        }
    }
    public enum InputType
    {
        Text, Number, Date, Time, Email, Url, Color, File
    }
    public class TextInput : InputModel
    {
        public TextInput()
        {
            InputType = InputType.Text;
        }
    }
    public class FileInput : InputModel
    {
        public FileInput()
        {
            InputType = InputType.File;
        }
    }
    public class EmailInput : InputModel
    {
        public EmailInput()
        {
            InputType = InputType.Email;
        }
    }
    public class UrlInput : InputModel
    {
        public UrlInput()
        {
            InputType = InputType.Url;
        }
    }
    public class ColorInput : InputModel
    {
        public ColorInput()
        {
            InputType = InputType.Color;
        }
    }
    public class NumberInput : InputModel
    {
        public NumberInput()
        {
            InputType = InputType.Number;
        }
    }
    public class DateInput : InputModel
    {
        public DateInput()
        {
            InputType = InputType.Date;
        }
    }
    public class TimeInput : InputModel
    {
        public TimeInput()
        {
            InputType = InputType.Time;
        }
    }
    public class CollectionModel : FieldModel
    {
        public CollectionModel()
        {
            FieldType = FieldType.Collection;
        }
    }
    public class CustomModel : FieldModel
    {
        public CustomModel()
        {
            FieldType = FieldType.Custom;
        }
    }
}

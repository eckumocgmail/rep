using Microsoft.AspNetCore.Components;

namespace Console_UserInterface.Shared.Forms
{

    public class InputTextBase: InputBase
    {
        public static string ConvertToString(Type arr)
        {
            string s = "";
            return s;
        }
        [Label("Вид данных")]
        [Parameter()]
        [NotNullNotEmpty()]
        [InputEnum(typeof(InputTypes))]
        public InputTypes InputType { get; set; }

        /// <summary>
        /// Типы данных
        /// </summary>
        public enum InputTypes
        {
            Undefined,
            Text,
            Number,
            Date,
            Time,
            Color
        }
    }
    public class InputComponentBase: InputBase
    {
        
        public enum InputTextTypes
        {
            Undefined,
            Email,
            URL,
            Eng,
            Rus
        }
    }
    public class CollectionComponentBase: InputBase
    {

        
        public enum CollectionTypes
        {
            Undefined,
            List,
            Dictionary,
            Tree
        }
        
    }
    public class ControlComponentBase: InputBase
    {        
        public enum ControlTypes
        {
            Undefined,
            CheckBox,
            SelectBox,
            ComboBox,
            TextArea
        }
        
    }
    public class InputBase: ComponentBase
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Dictionary<string, List<string>> ValidationErrors { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            this.ValidationErrors = this.Validate();
        }

        [Parameter]
        public ComponentTypes ComponentType { get; set; } = ComponentTypes.Undefined;

        public enum ComponentTypes
        {
            Undefined,
            Input,      //элемент input
            Control,    //один из control
            Collection, //коллекция
            Text,       //текстовый
            Custom      //настроиваемый
        }        
    }
}

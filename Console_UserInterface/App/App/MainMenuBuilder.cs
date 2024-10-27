using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static MainMenuBuilder;
using static System.Net.Mime.MediaTypeNames;

public class MainMenuBuilder
{


   
 
    public interface IMenuItemModel 
    {
        public string GetSize();    // font-size
        public string GetText();    // label text
        public string GetIcon();    // icon-code
        public string GetHotKeys(); // hotkey code
        public bool Expanded { get; set; } 
        
        public Action OnClick();    // action on click
        public Action OnOver();     // action on mouseover
        public Action OnLeave();    // action on mouseleave
    }

    public class MenuItemModel : TypeNode<Action>, IMenuItemModel
    {
        private IMenuItemModel parentNode;
        private List<IMenuItemModel> childNodes = new List<IMenuItemModel>();
        private readonly IMenuItemModel item;

        private string fontSize;
        private string labelText;
        private string iconCode;
        private string hotKeys;
        private Action onClicked;
        private Action onOver;
        private Action onLeave;
        private IMenuItemModel parent;
       
        private List<IMenuItemModel> items;
        private IMenuItemModel model;

        public MenuItemModel(MenuItemModel parent, string text, params IMenuItemModel[] items): base(text, null, parent)
        {
            this.parent = parent;
            this.labelText = text;
            this.items = new List<IMenuItemModel>(items);
        }

        public MenuItemModel(IMenuItemModel parent, IMenuItemModel model) : base("", null, null)
        {
            this.parent = parent;
            this.model = model;
        }

        public MenuItemModel(string text, params IMenuItemModel[] items): base(text, () => { }, null)
        {
            this.labelText = text;
            this.items = new List<IMenuItemModel>(items);
        }

        public MenuItemModel(string Text, Action OnClick) : base(Text, null, null)
        {
            this.onClicked = onClicked;
            this.labelText = Text;
        }

        public List<IMenuItemModel> GetChildNodes() => childNodes;
        public string GetSize() => fontSize;    // font-size
        public string GetText() => labelText;    // label text
        public string GetIcon() => iconCode;    // icon-code
        public string GetHotKeys() => hotKeys; // hotkey code
        public Action OnClick() => onClicked;    // action on click
        public Action OnOver() => onOver;  // action on mouseover
        public Action OnLeave() => onLeave;    // action on mouseleave

        
        public IMenuItemModel GetItem() => item;
        

        public bool Expanded { get; set; }
    }

    public interface IMenuBuilder
    {
        public MenuItemModel CreateMenu(string Text, params IMenuItemModel[] Items);
        public MenuItemModel CreateMenuSelect(MenuItemModel Parent, string Text, params IMenuItemModel[] Items);
    }

    public class MenuBuilder : IMenuBuilder
    {
        public List<MenuItemModel> Menus = new List<MenuItemModel>();
        public MenuItemModel CreateMenu(string Text, params IMenuItemModel[] Items)
        {
            var p = new MenuItemModel(Text, Items);
            
            Menus.Add(p);
            return p;
        }
        public MenuItemModel CreateButtonMenu(string text)
        {
            return new MenuItemModel(text);
        }
        public MenuItemModel CreateMenuSelect(MenuItemModel Parent, string Text, params IMenuItemModel[] Items)
            => new MenuItemModel(Parent, Text, Items);

        
    }
}
public class MainMenuUnit: TestingElement
{
    public override void OnTest()
    {
        var builder = new MenuBuilder();
        builder.CreateMenu("Файл", new MenuItemModel[] {
            builder.CreateButtonMenu("Close"),
            builder.CreateButtonMenu("New"),
            builder.CreateButtonMenu("Open")
        });
        builder.CreateMenu("Вид", new MenuItemModel[] {
            builder.CreateMenuSelect(null, "Размер", new MenuItemModel[]{
                builder.CreateButtonMenu("Максимальный"),
                builder.CreateButtonMenu("Стандарт"),
                builder.CreateButtonMenu("Свернутый")
            })

        }); ;
        this.Info(builder.Menus.ToJsonOnScreen());
    }
}
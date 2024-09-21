using Console_UserInterface.Shared;

using Microsoft.EntityFrameworkCore;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Console_UserInterface.Services.Location
{
    /// <summary>
    /// Хранилище динамических форм ввода с навигацией
    /// </summary>
    public class LocationDbContext : DbContext
    {
        /// <summary>
        /// Маршруты привязывают адреса со страницами
        /// </summary>
        public DbSet<AppRoute> AppRoutes { get; set; }

        /// <summary>
        /// Страница определяют набор визуальных компонентов
        /// </summary>
        public DbSet<AppPage> AppPages { get; set; }

        /// <summary>
        /// Компоненты соединяют данные с параметрами визуализации
        /// </summary>
        public DbSet<PageComponent> PageComponents { get; set; }

        public LocationDbContext() : base() { }
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options) { }

        /// <summary>
        /// Настройка соединения
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer($@"Data Source=DESKTOP-IHJM9RD;Initial Catalog={typeof(LocationDbContext).GetTypeName()};Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }

        /// <summary>
        /// Свойства маршрутизации
        /// </summary>
        public class AppRoute : BaseEntity
        {
            [Label("Маршрут")]
            [NotNullNotEmpty]
            [InputUrl]
            [UniqValue]
            public string Uri { get; set; }

            [Label("Страница")]
            [InputDictionary($"{nameof(AppPage)},{nameof(LocationDbContext.AppPage.Name)}")]
            public int AppPageId { get; set; }
            public AppPage AppPage { get; set; }
        }

        /// <summary>
        /// Содержимое страницы
        /// </summary>
        public class AppPage : NamedObject
        {
            [Label("Модель данных JSON")]
            [InputText]
            [NotNullNotEmpty()]
            public string PageModel { get; set; }

            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            public List<PageComponent> PageComponents { get; set; } = new List<PageComponent>();

            [NotMapped()]
            [JsonIgnore()]
            [NotInput]
            public Dictionary<string, List<string>> ValidationErrors { get; set; } = new();

            [Label("Тип модель данных")]
            [InputText]
            [NotNullNotEmpty()]
            public string ModelType { get; set; }

            [Label("Ссылка на модель")]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            [NotMapped]
            [NotInput]
            public object ModelInstance { get; set; }
        }

        /// <summary>
        /// Компонент страницы
        /// </summary>
        public class PageComponent : BaseEntity
        {
            public AppPage AsPage()
            {
                AppPage res = new();
                res.PageComponents.Add(this);
                return res;
            }
            public int AppPageId { get; set; }
            [Newtonsoft.Json.JsonIgnore]
            public AppPage AppPage { get; set; }

            [Label("Иконка")]
            [InputIcon]
            [NotNullNotEmpty]
            public string Icon { get; set; } = "home";

            [Label("Надпись")]
            public string Label { get; set; } = "Неизвестно";

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

            [Label("Описание")]
            [InputMultilineText]
            public string Description { get; set; } = "Нет подробного описания";

            [InputSelect()]
            public string Type { get; set; } = "Text";


            [Label("Разрешено изменение")]
            public bool Edited { get; set; } = true;

            public bool IsPrimitive { get; set; }
            public bool IsCollection { get; set; } = false;

            [NotMapped]

            public CollectionSettings CollectionSetup { get; set; }
            public class CollectionSettings
            {
                public string ItemType { get; set; }

            }

            //public string TextValue { get; set; }
            //public string ValueType { get; set; }

            [NotMapped]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            public object Control { get; set; }

            [Label("Размер")]
            [InputSelect("small,normal,big")]
            public string Size { get; set; } = "small";


            [Label("Состояние")]
            [InputSelect("valid,invalid,undefined")]
            public string State { get; set; } = "undefined";

            [Label("Подсказка")]
            public string Help { get; set; } = "Нет справочной информации";

            [Label("Ошибки")]
            [NotMapped]
            [NotInput]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            public Dictionary<string, List<string>> Errors { get; set; } = new();

            [Label("Атрибуты")]
            [NotMapped]
            [NotInput]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            public Dictionary<string, string> Attributes { get; set; } = new Dictionary<string, string>();

            [NotMapped]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            [NotInput]

            public Func<object> Getter { get; set; }

            [NotMapped]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            [NotInput]
            public Action<object> Setter { get; set; }

            [NotMapped]
            [Newtonsoft.Json.JsonIgnore]
            [JsonIgnore]
            [NotInput]
            public object Value
            {
                get;
                set;
            }
        }
    }
}

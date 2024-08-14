using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationDb.Entities
{
    [Label("Бинарные данные")]
    public class BinaryResource: BaseEntity
    {
   
        [NotNullNotEmpty("Необходимо указать наименование ресурса")]
        [Label("Наименование")]        
        public string Name { get; set; }

        [NotNull]
        [Label("Тип файла")]
        [InputSelect(1, Options = new string[] { 
            "text/html"
        })]
        [NotNullNotEmpty("Необходимо ввести задать тип ресурса (MimeType)")]
        public string Mime { get; set; }
 

        [Label("Файл")]
        [InputFile("")]        
        public byte[] Data { get; set; }

        [Label("Дата загрузки")]
        [InputDateTime()]
        [NotInput("")]
        [NotNullNotEmpty("Необходимо указать время создания ресурса")]
        public DateTime Created { get; set; } = DateTime.Now;

    }
}

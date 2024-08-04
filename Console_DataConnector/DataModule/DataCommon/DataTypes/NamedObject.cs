

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataCommon.DataTypes
{

    [Label("Именованый ресурс")]
    public class NamedObject : IObjectWithId
    {


        [Label("Наименование")]
        [NotNullNotEmpty(ErrorMessage = "Введите наименование")]
        [UniqValue]
        public virtual string Name { get; set; } = "";
        [Label("Описание")]
        [NotNullNotEmpty]
        [DataType(DataType.MultilineText)]
        public virtual string Description { get; set; } = "";
    }
}

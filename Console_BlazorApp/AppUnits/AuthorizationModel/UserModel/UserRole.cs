using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;

namespace Console_AuthModel.AuthorizationModel.UserModel
{
    public class UserRole : BaseEntity 
    {

          



            [Label("Корневой каталог")]
            [InputDictionary("GetType().Name,Name")]
            public int? ParentID { get; set; }


            [InputHidden(true)]
            public virtual UserRole Parent { get; set; }



            [NotNullNotEmpty()]
            [UniqValue()]
            public string Code { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
    }
}

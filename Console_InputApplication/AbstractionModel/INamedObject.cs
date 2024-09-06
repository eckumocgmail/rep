using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public interface INamedObject
{
    [DisplayName("Наименование")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public string Name { get; set; }// = "наименование";

    [DisplayName("Описание")]
    [Required(ErrorMessage = "Необходимо указать наменование")]
    public string Description { get; set; }// = "Описание";
}

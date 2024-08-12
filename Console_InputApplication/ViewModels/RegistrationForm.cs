using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class RegistrationForm// : Form
{
/*
    public Action<Form> OnComplete { get; set; } = (form)={};

    public RegistrationForm( object p ): base( ReflectionService.Create<MyValidatableObject>((p is Type? (Type)p: p.GetType()).Name, new object[0]))
    {
        Type type = p is Type? (Type)p: p.GetType();
        var form = this;
        lock (this)
        {
             
            var createButton = new Button()
            {
                Label = "Создать",
                OnClick = (button) =>
                {
                    form.OnComplete(form );
                    
                }
            };
            
            createButton.Bind("Enabled", form, "IsValid");
            createButton.Enabled = false;
            form.Buttons.Add(createButton);
            form.EnableChangeSupport = true;
            createButton.EnableChangeSupport = true;
            form.Update(form.PropertyNames = ReflectionService.GetPropertyNames(type).ToArray());
            form.FormFields.Reverse();
            form.Title = Attrs.LabelFor(type);            
            form.Description = Attrs.DescriptionFor(type);
            Changed = false;

        }
        Edited = true;
        Changed = false;

    }
    */
}
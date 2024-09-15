using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;

/// <summary>
/// Расширение форммы ввода
/// </summary>
public static class InputFormExtensions
{
    /// <summary>
    /// Создание формы ввода
    /// </summary>
    /// <param name="controller">тип</param>
    /// <param name="action">метод</param>
    /// <returns>модел формы</returns>
    public static InputFormModel GetInputForm(this Type controller, string action)
    {
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        var validationResults = MethodInfo.GetCurrentMethod().Validate(new object[] {
            controller, action
        });
        if(validationResults.Where(kv => kv.Value.Count()>0).Count() > 0)
        {
            throw new Exception($"Аргументы вызова невалидны: {validationResults.ToJson()}");
        }
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        var methodInfo = controller.GetMethod(action);
        var actionModel = new MyActionModel();
        actionModel.Name = action;
        actionModel.Type = methodInfo.ReturnType.GetTypeName();
        actionModel.Path = $"/api/{controller.GetTypeName()}/{action}";
        actionModel.Method = "POST";
        actionModel.Attributes = controller.GetMethodAttributes(action);
        actionModel.EnsureIsValide();

        var formModel = new InputFormModel();
        formModel.Title = actionModel.Name;
        formModel.Description = controller.GetMethodDescription(action);
        formModel.FormFields = new();
        foreach (var param in controller.GetActionParameters(action))
        {            
            var paramModel = new MyParameterDeclarationModel();
            paramModel.Name = param.Name;
            paramModel.Type = param.ParameterType.GetTypeName(); ;
            paramModel.IsOptional = param.IsOptional;
            paramModel.Position = param.Position;            
            paramModel.Attributes = controller.GetArgumentAttributes(action, param.Name);
            actionModel.Parameters[param.Name] = paramModel;

            var field = formModel.CreateFormField(paramModel);
            formModel.FormFields.Add(field);
        }
        formModel.EnsureIsValide();
        return formModel;

    }
}

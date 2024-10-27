
using System.Reflection;

/// <summary>
/// Расширение форммы ввода
/// </summary>
public static class InputFormExtensions
{

    public static string[] Push( this string[] args, string val)
    {
        var list = args.ToList();
        list.Add(val);
        return list.ToArray();
    }
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
        var methodInfo = controller.GetMethods().FirstOrDefault(m => m.Name == action);
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
        formModel.Item = new Dictionary<string, object>();
        formModel.ItemType = formModel.Item.GetTypeName();
        foreach (var param in controller.GetActionParameters(action))
        {
            try
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
            catch(Exception ex)
            {
                if (param.IsOptional == false)
                    throw;
                formModel.Error($"{ex}");
            }
        }
        formModel.Item = new Dictionary<string, object>();
        formModel.Json = "{}";
        formModel.Container = "v-group";
        formModel.IsValid = false;
        formModel.EnsureIsValide();
        return formModel;

    }
}

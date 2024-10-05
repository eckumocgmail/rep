
using Console_InputApplication.InputApplicationModule.Files;

public class HelpService
{

    /// <summary>
    /// Считываем файлы из папки articles
    /// </summary>    
    public Dictionary<string,string> GetArticles()
    {
        var path = Path.Combine
        (
            Directory.GetCurrentDirectory(),
            "Help",
            "Articles"
        ).ToString();
        this.Info(path);
        var articles = System.IO.Directory.GetFiles
        (
            path
        );
        return new Dictionary<string, string>(
            articles.Select(path => new KeyValuePair<string, string>(path, System.IO.File.ReadAllText(path))));        
    }


    public TypeNode<String> GetContents()
    {
        var resource = new DirectoryResource(Path.Combine
            (
                Directory.GetCurrentDirectory(),
                "Help",
                "Contents"
            ));
        var ctrl = new FileController<TypeNode<String>>
        (
            Path.Combine
            (
                Directory.GetCurrentDirectory(),
                "Help",
                "Contents"
            )
        );
        //resource.GetDirectories
        var root = new TypeNode<String>("Содержание", "Содержание", null);
        GetArticles().Keys.ToList().ForEach(key => new TypeNode<String>(key, key, root));
        ctrl.Model = root;
        ctrl.Set();
        var res = ctrl.Get();
        return res;
    }
}

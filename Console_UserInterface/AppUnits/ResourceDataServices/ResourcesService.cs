using ApplicationCommon.CommonResources;
 

 

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
 

public interface IResource
{
    public void Export(string catalogName, string filePath);
    public void Import(string name, string path);

}


/// <summary>
/// Служба управления ресурсами
/// </summary>
public class ResourcesService: IResource
{
    private readonly ResourcesDataModel _context;


    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="context"></param>
    public ResourcesService(ResourcesDataModel context)
    {
        _context = context;
    }

    public void Export(string catalogName, string filePath)
    {
        var catalogInfo = _context.FileCatalogs.Where(catalog => catalogName != null && catalog.Name.ToLower() == catalogName.ToLower()).SingleOrDefault();
        if (catalogInfo != null)
        {
            //catalogInfo.Join("Files");
            catalogInfo.Files.ForEach(file =>
            {
                Console.WriteLine($"{filePath}\\{file.Data}");
            });
        }        
    }






    /// <summary>
    /// Выполняет сохранение каталога в базу данных
    /// </summary>
    /// <param name="fileCatalog"></param>
    public void Import( string name, string path )
    {
        try
        {

            var fileCatalog = new TypeCatalog(path, name);
            Dictionary<string, FileCatalog> catalogs = new Dictionary<string, FileCatalog>();
            fileCatalog.DoBroadcastToBrothers<TypeCatalog>((TypeCatalog p) => {
                //_context.
                FileCatalog catalog =
                    catalogs[p.GetPath("\\")] = 
                        new FileCatalog(p.Name); 
                if(p.Parent != null)
                {
                    catalog.Parent = catalogs[p.Parent.GetPath("\\")];
                }
                foreach(TypeFile file in p.Item.Values)
                {
                    FileResource resource = new FileResource( );
                    catalog.Files.Add(resource);
                    _context.FilResources.Add(resource);
                }
                _context.FileCatalogs.Add(catalogs[p.GetPath("\\")]);
            });
            _context.SaveChanges();
        }
        catch(Exception ex)
        {
            Info(ex);
        }
    }


    /// <summary>
    /// Вывод сообщения в консоль
    /// </summary>
    /// <param name="message"></param>
    protected void Info(object item)
    {
        string message = $"[{GetType().Name}][{DateTime.Now}] => {item}";
        Debug.WriteLine(message);
        Console.WriteLine(message);
    }
} 
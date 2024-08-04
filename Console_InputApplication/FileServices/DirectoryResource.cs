using Console_InputApplication.InputApplicationModule.Files;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

/// <summary>
/// Уравленеие ресурсами каталога
/// </summary>
public class DirectoryResource : FileController
{
    public DirectoryResource(string pathAbs) : base(pathAbs) { }

   

    /// Наименование директории
    public string GetName() => 
        Path.Substring(Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar) + 1);

    public FileController<T> Bind<T>(string filename) where T : class
    {
        string filePath = Path + System.IO.Path.DirectorySeparatorChar + filename;
        if (filePath.IsFile() == false)
            CreateFile(filePath);
        var ctrl = new FileController<T>(filePath);
        ctrl.Get();
        ctrl.Set();
        return ctrl;
    }

    public FileController CreateFile(string filename)
    {
        string dirpath = Path + System.IO.Path.DirectorySeparatorChar+ filename;
        System.IO.File.WriteAllText(dirpath, "");
        return new FileController(dirpath);
    }
    public FileController CreateTextFile(string filename, string text)
    {
        string dirpath = Path + System.IO.Path.DirectorySeparatorChar + filename;
        System.IO.File.WriteAllText(dirpath, "");
        var ctrl = new FileController(dirpath);
        ctrl.WriteText(text);
        return ctrl;
    }
    public DirectoryResource GetOrCreateDirectory(string dirname)
    {
        string dirpath = Path + System.IO.Path.DirectorySeparatorChar + dirname;
        if (System.IO.Directory.Exists(dirpath) == false)
        {
            System.IO.Directory.CreateDirectory(dirpath);
        }
        return new DirectoryResource(dirpath);
    }
    public override bool Copy(string directory)
    {
        var ctrl = new DirectoryResource(directory);

        foreach (var file in GetFiles())
        {
            ctrl.CreateTextFile(file.NameShort, file.ReadText());
        }
        foreach (var dir in GetDirectories())
        {
            var subctrl = ctrl.GetOrCreateDirectory(dir.NameShort);
            subctrl.Copy(directory + System.IO.Path.DirectorySeparatorChar + dir.NameShort);
        }
        return true;
    }

    public override string ToString()
    {
        return NameShort;
    }
} 
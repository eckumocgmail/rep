using System;
using System.Linq;
using System.Threading.Tasks;
using Console_InputApplication.InputApplicationModule.Files;

[Label("J")]
public class FileController: ChangeSupport 
{
    public string Path { get; }
    public bool IsDirectory { get; }
    public bool IsFile { get; }
    public string NameShort { get => this.Path.Substring(this.Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar));  }


    public FileController( string pathAbs )
    {
        //this.Info($"Create({pathAbs})");
        this.Path = pathAbs;
        this.IsDirectory = System.IO.Directory.Exists(this.Path);
        this.IsFile = System.IO.File.Exists(this.Path);
        if(this.IsDirectory==false && this.IsFile == false)
        {
            throw new Exception("Путь "+pathAbs+" не достоверный потому что не является ни директорией и файлом");
        }
    }


    public virtual bool Copy(string directory)
    {
        var ctrl = new DirectoryResource(directory);
        ctrl.CreateFile(NameShort).WriteText(ReadText());
        return true;
    }

    public string ReadText()
    {
        //this.Info($"ReadText({this.Path})");
        return System.IO.File.ReadAllText(this.Path);
    }
    public void WriteText(string context)
    {
        //this.Info($"WriteText({this.Path})");
        System.IO.File.WriteAllText(this.Path, context);
    }
    public async Task<string> ReadTextAsync()
    {
        return await System.IO.File.ReadAllTextAsync(this.Path);
    }
    public async Task WriteTextAsync(string context)
    {
        await System.IO.File.WriteAllTextAsync(this.Path, context);
    }

    public virtual DirectoryResource[] GetDirectories()
    {
        if (IsFile)
        {
            throw new Exception("Не возможно получить список директорий для файла " + this.Path);
        }
        return System.IO.Directory.GetDirectories(this.Path).Select(path => new DirectoryResource(path)).ToArray();
    }

    public virtual FileController[] GetFiles()
    {
        if (IsFile)
        {
            throw new Exception("Не возможно получить список директорий для файла " + this.Path);
        }
        return System.IO.Directory.GetFiles(this.Path).Select(path => new FileController(path)).ToArray();
    }

    public virtual FileController GetParent()
    {
        return new FileController(this.Path.Substring(0, this.Path.LastIndexOf(System.IO.Path.DirectorySeparatorChar)));
    }
}

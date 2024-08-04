using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

public static class SystemResources
{
    public static async Task<IEnumerable<string>> GetConsoleProjects() =>
        (await SearchFilesProgram.SearchFile("D:\\System-Config\\ProjectsConsole", "*.csproj", 4));
    public static async Task<IEnumerable<string>> GetWebProjects() =>
        (await SearchFilesProgram.SearchFile("D:\\System-Config\\ProjectsConsole", "*.csproj", 4));

}

﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using static System.IO.Directory;


[Label("Программа посика файлов")]
[Description("Доп. предоставляет пути к выполняемым файлам программ из каталога ProgramFiles")]
public class SearchFilesProgram
{
    private const string SearchProjectsPath = @"D:\Projects;D:\System-Config\ProjectsConsole";

    /// <summary>
    /// Каталоги перечисленные в переменной PATH
    /// </summary>    
    public static IEnumerable<string> GetPathDirectories() =>
        Environment.GetEnvironmentVariable("PATH").Split(";");

    /// <summary>
    /// Файлы с расширением *.exe
    /// </summary>    
    public static IEnumerable<string> GetProgramFiles() => 
        SearchFile("*.exe");


    /// <summary>
    /// Поиск файлов
    /// </summary> 
    public static async Task<IEnumerable<string>> SearchFile(string dir, string pattern, int level )
    {
        
        



        Console.WriteLine($"SearchFile({dir},{pattern},{level})");

        List<string> dirs = new List<string>() { dir };
        var result = new ConcurrentBag<string>();
        if (level > 0)
        {
            Task.WaitAll(System.IO.Directory.GetDirectories(dir).Select(dir => Task.Run(async () =>
            {
                //result.AddAll();
                try
                {
                    var items = await SearchFile(dir, pattern, level - 1);
                    items.ToList().ForEach(result.Add);
                }
                catch (Exception ex)
                {
                    InputConsole.GetApp().Error("Не выполнен поиск",ex);
                }
            })).ToArray());
        }
        System.IO.Directory.GetFiles(dir,pattern).ToList().ForEach(result.Add);
        await Task.CompletedTask;
        return result;
    }

    /// <summary>
    /// Поиск файлов
    /// </summary> 
    public static IEnumerable<string> SearchFile( string pattern )
    {
        var results = new List<string>();
        foreach(var dir in GetPathDirectories())
        {
            if (Exists(dir) == false)
                continue;
               
            try
            {
                results.AddRange(GetFiles(dir, pattern, SearchOption.AllDirectories));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return results;
    }


    /// <summary>
    /// Сериализация в JSON
    /// </summary>
    public static string Serialize(object target) => 
        System.Text.Json.JsonSerializer.Serialize(target);


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchList<T> : HashSet<T>
    {
        public IEnumerable<T> Search(Predicate<T> itemContainsInfoFn) => this.Where(item => itemContainsInfoFn(item));
    }

 
    public static async Task<IEnumerable<string>> DoTestAsync()
        => (await Task.WhenAll<IEnumerable<string>>(
            SearchProjectsPath.Split(':').Select(dir => SearchFile(dir, "*.csproj", 2))
        )).Aggregate((list1, list2) => list1.Concat(list2));    
}
 

using NSwag.CodeGeneration.CSharp;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Console_CommonData
{
    public class RestIntegration
    {
        public async Task<string> CreateClient(string url = "https://localhost:5001/swagger/v1/swagger.json")
        {
            var document = await NSwag.OpenApiDocument.FromUrlAsync(url);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = "{controller}Client",
            };
            var generator = new CSharpClientGenerator(document, settings);
            string text = generator.GenerateFile();
            Console.WriteLine(text);
            return text;
        }

        public async Task<string> CreateClientFile([Required]string file, string url = "https://localhost:5001/swagger/v1/swagger.json")
        {
            var cscode = await CreateClient(url);
            System.IO.File.WriteAllText(file, cscode);
            return cscode;
        }
    }
}

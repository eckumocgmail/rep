using Microsoft.AspNetCore.Mvc;
using NSwag.CodeGeneration.CSharp;

namespace Console_Blazor.Controllers
{

    [Route("/api/services")]
    public class ServiceController: Controller
    {

        [HttpGet("gen")]
        public async Task<string> Gen([FromQuery] string url = "https://sps.euroauto.ru/api/detaliusbot/swagger/v1/swagger.json")
        {
            var document = await NSwag.OpenApiDocument.FromUrlAsync(url);
            var settings = new CSharpClientGeneratorSettings
            {
                ClassName = "{controller}Client",
                UseRequestAndResponseSerializationSettings = true                
            };
            var generator = new CSharpClientGenerator(document, settings);         
            return generator.GenerateFile();
        }

    }

}

using Console_UserInterface.Services;

using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

namespace Console_UserInterface.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class FilesController: Controller
    {
        public async Task ImageFromFile([FromServices] ISessionService sessions, string filename = "1.png", string session_id = "Image0")
        {
            var fileData = sessions.GetValue<byte[]>(session_id);

            string image_dir = "C:\\Users\\123\\Pictures";
            string path = @$"{image_dir}\{filename}";
            var data = System.IO.File.ReadAllBytes(path);
            for (int i = 0; i < data.Length; i++)
            {
                if(fileData[i] == data[i])
                {
                    continue;
                }
                else
                {
                    throw new Exception($"Данные отличаются c {i} из {data.Length}");
                }
            }                                                                   
            await Response.Body.WriteAsync(data, 0, data.Length);   
        }




    }
}

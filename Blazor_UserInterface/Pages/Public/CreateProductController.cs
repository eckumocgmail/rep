using Microsoft.AspNetCore.Mvc;

namespace Console_BlazorApp.Pages.Public
{
    [Route("[controller]/[action]")]
    public class CreateProductController: Controller
    {
       
        [HttpGet()]
        public async Task Image([FromServices] SigninUser signin, int productId = 0)
        {
            var data = System.IO.File.ReadAllBytes(@$"C:\Users\123\Pictures\1.png");
            //"CreateProduct"
            //var datas = signin.GetFromSession<List<byte[]>>("CreateProduct");
            
            Response.ContentType = "image/json";
            //var data = datas[i];
            await Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}

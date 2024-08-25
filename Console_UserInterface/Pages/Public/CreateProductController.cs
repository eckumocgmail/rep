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

        [HttpGet()]
        public async Task ImageFromSession([FromServices] SigninUser signin, string insessionid)
        {
            if(signin.IsSignin())
            {
                var data = signin.GetFromSession<byte[]>(insessionid);
                Response.ContentType = "image/json";
                await Response.Body.WriteAsync(data, 0, data.Length);
            }
            else
            {
                Response.StatusCode = 400;
                await Response.WriteAsync(new ProblemDetails() { 
                    Title = "Объект идентификатор объекта неверный"
                }.ToJson());
            }
            
            //var data = System.IO.File.ReadAllBytes(@$"C:\Users\123\Pictures\1.png");
            //"CreateProduct"
            //var datas = signin.GetFromSession<List<byte[]>>("CreateProduct");
            //var data = datas[i];            
        }
    }
}

using System.Collections.Concurrent;

namespace Console_UserInterface.Controllers
{

    /// <summary>
    /// Управление методами API
    /// </summary>
    public class ApiController
    {
        private List<Tuple<Predicate<HttpContext>, Func<HttpContext, object>>> deligates = new();


        public Action<HttpContext> GetControllerActionByRoute(string url)
        {
            return (http) =>
            {

            };
        }

        public void Add(Predicate<HttpContext> handler, Func<HttpContext, object> execute)
        {

        }

        public void Apply(HttpContext http)
        {
            deligates.ForEach(tu =>
            {
                if (tu.Item1(http))
                {
                    var result = tu.Item2(http);
                    var responseText = Newtonsoft.Json.JsonConvert.SerializeObject(result);
                    http.Response.StatusCode = 200;
                    http.Response.WriteAsync(responseText).Wait();
                }
            });
        }
    }
}
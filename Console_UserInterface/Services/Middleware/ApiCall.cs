using Microsoft.AspNetCore.Http.Extensions;
using Console_DataConnector.DataModule.DataADO.ADOWebApiService;

public class ApiCall
{
    public static void UseApi(WebApplication app)
    {
        app.MapWhen(http => http.Request.GetDisplayUrl().IndexOf("/api") != -1, app => {
            app.Run(async http => {
                var uri = http.Request.GetDisplayUrl().Substring(http.Request.GetDisplayUrl().IndexOf("/api") + 3);
                if (uri.IndexOf("?") != -1)
                {
                    uri = uri.Substring(0, uri.IndexOf("?"));
                }

                var api = http.RequestServices.Get<SqlServerWebApi>();
                var args = new Dictionary<string, string>(http.Request.Query.Select(q => new KeyValuePair<string, string>(q.Key, q.Value.First())));
                var headers = new Dictionary<string, string>(http.Request.Headers.Select(q => new KeyValuePair<string, string>(q.Key, q.Value.First())));
                var response = await api.Request(uri, args, headers);
                //http.Response.StatusCode = response.Item1;
                http.Response.ContentType = "application/json; utf-8";
                await http.Response.WriteAsync(response.Item2.ToJson());
            });
        });
        //app.Use(async (http, next) => { await http.Response.WriteAsync(new { }.ToJsonOnScreen()); await next.Invoke(); });
        app.Use(async (http, next) =>
        {
            if (http.Request.GetDisplayUrl().IndexOf("/api") != -1)
            {

                /*var signin = http.RequestServices.GetService<SigninUser>();
                if (signin.IsSignin())
                {
                    UserContext context = signin.Verify();
                    context.UserAgent = http.Request.Headers.UserAgent;
                    using (var db = new DbContextUser())
                    {
                        db.Update(context);
                        db.SaveChanges();
                    }

                }*/
            }
            else
            {
                await next.Invoke();
            }

        });
    }
}

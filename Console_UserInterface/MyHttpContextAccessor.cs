using Microsoft.AspNetCore.Http.Features;

using System.Security.Claims;

namespace Console_UserInterface
{
    public class MyHttpContextAccessor: IHttpContextAccessor
    {
        public class MyHttpRequest: HttpRequest
        {
            public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default)
            {
                throw new NotImplementedException();
            }

            public override HttpContext HttpContext => throw new NotImplementedException();

            public override string Method { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string Scheme { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override bool IsHttps { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override HostString Host { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override PathString PathBase { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override PathString Path { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override QueryString QueryString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override IQueryCollection Query { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string Protocol { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override IHeaderDictionary Headers => throw new NotImplementedException();

            public override IRequestCookieCollection Cookies { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override long? ContentLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string? ContentType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override Stream Body { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public override bool HasFormContentType => throw new NotImplementedException();

            public override IFormCollection Form { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
        public class MyHttpContext: HttpContext
        {
            public override void Abort()
            {
                throw new NotImplementedException();
            }

            public override IFeatureCollection Features => throw new NotImplementedException();

            public override HttpRequest Request => throw new NotImplementedException();

            public override HttpResponse Response => throw new NotImplementedException();

            public override ConnectionInfo Connection => throw new NotImplementedException();

            public override WebSocketManager WebSockets => throw new NotImplementedException();

            public override ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override IDictionary<object, object?> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override IServiceProvider RequestServices { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override CancellationToken RequestAborted { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override string TraceIdentifier { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
            public override ISession Session { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        }
        public HttpContext? HttpContext { get => new MyHttpContext(); set => throw new NotImplementedException(); }
    }
}

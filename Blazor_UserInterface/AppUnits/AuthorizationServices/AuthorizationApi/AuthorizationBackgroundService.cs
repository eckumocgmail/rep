using Microsoft.Extensions.Hosting;

using System.Threading;
using System.Threading.Tasks;

namespace Console_AuthModel.AuthorizationServices.AuthorizationApi
{
    public class AuthorizationBackgroundService: BackgroundService
    {
        private readonly AuthorizationUsers _users;

        public AuthorizationBackgroundService(AuthorizationUsers users)
        {
            _users = users;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while( stoppingToken.IsCancellationRequested == false)
            {
                _users.DoCheck();
                await Task.Delay(1000);
            }
        }
    }
}

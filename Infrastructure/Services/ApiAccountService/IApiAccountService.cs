using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService;

namespace IntegrationApiSynchroniser.Infrastructure.Services.ApiAccountService
{
    public interface IApiAccountService : IApiClientServices<UserLoginDto>
    {
        UserLoginDto Authenticate(UserLoginDto model);
    }
}

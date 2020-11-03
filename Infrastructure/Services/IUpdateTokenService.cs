using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    public interface IUpdateTokenService
    {
        Task UpdateToken(CancellationToken cancellationToken);
    }
}

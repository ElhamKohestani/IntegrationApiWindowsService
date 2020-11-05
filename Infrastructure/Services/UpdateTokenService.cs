using IntegrationApiSynchroniser.Infrastructure.Enums;
using IntegrationApiSynchroniser.Infrastructure.Helpers;
using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiAccountService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    /// <summary>
    /// Update the token only if current time is between times set in appsettings and
    /// the task will run every [from setting] minutes in between the start and end time intervals
    /// it will only access the ARD api if the last update of token is not in the start and end interval
    /// means it will only access it once and update the token
    /// 
    /// 
    /// </summary>
    public class UpdateTokenService : IUpdateTokenService
    {
        private IConfiguration _conf;
        private IApiAccountService _ardAccountService;
        private IServiceScopeFactory _serviceScopeFactory;

        public UpdateTokenService(IServiceScopeFactory serviceScopeFactory, IApiAccountService apiAccountService, IConfiguration conf)
        {
            _conf = conf;
            
            _ardAccountService = apiAccountService;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<int> UpdateToken(CancellationToken cancellationToken)
        {
            

            while (!cancellationToken.IsCancellationRequested)
            {

                TimeSpan start_interval = new TimeSpan(_conf.GetValue<int>("ATU_INTERVAL_START_HOUR"), _conf.GetValue<int>("ATU_INTERVAL_START_MIN"), 0);
                TimeSpan end_interval = new TimeSpan(_conf.GetValue<int>("ATU_INTERVAL_STOP_HOUR"), _conf.GetValue<int>("ATU_INTERVAL_STOP_MIN"), 0);
                int REPEAT_INTERVAL = _conf.GetValue<int>("ATU_SERIVCE_REPEAT_INTERVAL");

                if (_conf.GetValue<bool>("ATU_SERVICE"))
                {
                    try
                    {
                        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
                        {
                            WorkerContext _context = scope.ServiceProvider.GetRequiredService<WorkerContext>();
                            if (DateTime.Now.DayOfWeek == DayOfWeek.Thursday && DateTime.Now.TimeOfDay <= end_interval && DateTime.Now.TimeOfDay >= start_interval)
                            {
                                StakeholderApis stakeholderApis = await _context.StakeholderApis.
                                    Where(a => a.StakeholderId == (int)StakeholderIDs.ARD_ID)
                                    .SingleOrDefaultAsync();

                                if (stakeholderApis != null && ((stakeholderApis.LastUpdateTime.Value.TimeOfDay > end_interval || stakeholderApis.LastUpdateTime.Value.TimeOfDay < start_interval) || stakeholderApis.LastUpdateTime == null))
                                {
                                    string UserName = stakeholderApis.UserName;
                                    string Password = EncryptionHelper.Decrypt(stakeholderApis.Password);

                                    // Uncomment
                                    UserLoginDto credentials = _ardAccountService.Authenticate(new UserLoginDto()
                                    {
                                        Username = UserName,
                                        Password = Password

                                    });


                                    //Delete
                                    //UserLoginDto credentials = new UserLoginDto()
                                    //{

                                    //    Username = "x",
                                    //    Password = "ddd",
                                    //    Token = "Test"
                                    //};


                                    if (!string.IsNullOrEmpty(credentials.Token))
                                    {
                                        stakeholderApis.Token = credentials.Token;
                                        stakeholderApis.LastUpdateTime = DateTime.Now;


                                        await _context.SaveChangesAsync();
                                    }
                                }
                            }
                        }
                    }
          
                    catch(Exception ex)
                    {

                    }
                }
                await Task.Delay(REPEAT_INTERVAL, cancellationToken);
            }

            return 0;
        }
    }
}

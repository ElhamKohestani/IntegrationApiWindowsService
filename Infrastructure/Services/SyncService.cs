using IntegrationApiSynchroniser.Infrastructure.Models;
using IntegrationApiSynchroniser.Infrastructure.Services.ApiClientService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    public class SyncService : ISyncService
    {
        private IServiceScopeFactory _serviceScopeFactory;
        private IConfiguration _conf;
        private IApiClientServices _apiClientServices;

        public SyncService(IConfiguration conf, IServiceScopeFactory serviceScopeFactory, IApiClientServices clientServices)
        {

            _serviceScopeFactory = serviceScopeFactory;
            _conf = conf;
            _apiClientServices = clientServices;
        }
        public async Task Sync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                int REPEAT_INTERVAL = _conf.GetValue<int>("ARS_SERIVCE_REPEAT_INTERVAL");
                string BASE_URL = _conf.GetValue<string>("API_BASE_URL");
                string AUTH_TOKEN = _conf.GetValue<string>("AUTH_TOKEN");
                HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
                int syncIdInCurrentLoop = 0;

                if (_conf.GetValue<bool>("ARS_SERVICE"))
                {

                    var scope = _serviceScopeFactory.CreateScope();

                    try
                    {
                        WorkerContext context = scope.ServiceProvider.GetRequiredService<WorkerContext>();


                        List<TblApiSync> failedRecords = await context.TblApiSync.Where(a => a.SyncStatus == false && a.TryCount <= 4).ToListAsync();
                        if (failedRecords.Any())
                        {
                            failedRecords.ForEach(async s =>
                            {
                                syncIdInCurrentLoop = s.ApiSynId;
                                TblApiSync toUpdateRecord = await context.TblApiSync.Where(r => r.ApiSynId == s.ApiSynId).SingleOrDefaultAsync();

                                try
                                {
                                    httpResponseMessage = await _apiClientServices.GetAsync(new StringBuilder(BASE_URL).Append(s.EndPoint).ToString(),
                                   AUTH_TOKEN, s.Parameter);

                                    if (httpResponseMessage.StatusCode == HttpStatusCode.OK || httpResponseMessage.StatusCode == HttpStatusCode.Created)
                                    {
                                        toUpdateRecord.SyncStatus = true;
                                    }
                                    else
                                    {
                                        toUpdateRecord.TryCount = s.TryCount + 1;
                                    }

                                    await context.SaveChangesAsync();
                                }
                                catch(Exception ex)
                                {
                                    toUpdateRecord.SyncStatus = false;
                                    toUpdateRecord.TryCount = toUpdateRecord.TryCount + 1;
                                    await context.SaveChangesAsync();
                                }
                            });

                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                // The sync operation will repeat after recieving value of settings.
                await Task.Delay(REPEAT_INTERVAL, stoppingToken);
            }
        }
    }
}

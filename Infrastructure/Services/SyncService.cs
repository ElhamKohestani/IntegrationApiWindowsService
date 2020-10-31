using IntegrationApiSynchroniser.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationApiSynchroniser.Infrastructure.Services
{
    public class SyncService : ISyncService
    {
        public WorkerContext _context { get; }
        private IConfiguration _conf;

        public SyncService(IConfiguration conf, WorkerContext context)
        {
            _context = context;
            _conf = conf;
        }
        public async Task Sync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //TimeSpan start_interval = new TimeSpan(_conf.GetValue<int>("ARS_INTERVAL_START_HOUR"), _conf.GetValue<int>("ARS_INTERVAL_START_MIN"), 0);
                //TimeSpan end_interval = new TimeSpan(_conf.GetValue<int>("ARS_INTERVAL_STOP_HOUR"), _conf.GetValue<int>("ARS_INTERVAL_STOP_MIN"), 0);
                //TimeSpan current_time = DateTime.Now.TimeOfDay;

                if (_conf.GetValue<bool>("ARS_SERVICE"))
                {



                    List<TblApiSync> failedRecords = await _context.TblApiSync.Where(a => a.SyncStatus == false && a.TryCount <=4).ToListAsync();
                    if (failedRecords.Any())
                    {

                    }


                }
                // The service will repeat every 10 minutes
                await Task.Delay(600000, stoppingToken);
            }
        }
    }
}

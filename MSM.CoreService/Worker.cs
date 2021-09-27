using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MSM.IService;
using MSM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using MSM.IRepository;
using MSM.Repository;
using MSM.Model;
using Microsoft.EntityFrameworkCore;

namespace MSM.CoreService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;


        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Timer timer = new Timer(async obj => {
                IServiceCollection services = new ServiceCollection();

                services.AddDbContext<MsmDbContext>(action =>
                {
                    action.UseSqlServer("Data Source=.;Initial Catalog=MettingSystemManageDb;Persist Security Info=True;User ID=sa;password=lp_lucky");
                });

                services.AddSingleton<IGoodsService, GoodsService>();
                services.AddSingleton<IGoodsRepository, GoodsRepository>();
                services.AddSingleton<IUnitOfWork, UnitOfWork>();

                IServiceProvider serviceProvider = services.BuildServiceProvider();
                var service = serviceProvider.GetService<IGoodsService>();

                var list = await service.GetAllAsync();

                using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "corlog.log")))
                {
                    foreach (var item in list)
                    {
                        await writer.WriteLineAsync(item.GoodsName);
                    }
                }
            },null,100 * 1000, -1);
        }
    }
}

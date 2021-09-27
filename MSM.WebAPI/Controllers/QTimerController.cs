using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Quartz.Spi;
using Microsoft.Extensions.DependencyInjection;
using MSM.IService;
using MSM.Service;
using MSM.IRepository;
using MSM.Repository;

namespace MSM.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class QTimerController : ControllerBase
    {
        private IWebHostEnvironment webHostEnvironment;
        public QTimerController(IWebHostEnvironment _webHostEnvironment)
        {
            this.webHostEnvironment = _webHostEnvironment;
        }


        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Start()
        {
            //调度工厂
            StdSchedulerFactory factory = new StdSchedulerFactory();
            
            //获取一个调度器
            var sche = await factory.GetScheduler();

            JobDataMap pairs = new JobDataMap();
            pairs.Add("webhost", webHostEnvironment);

            //创建作业
            IJobDetail job = JobBuilder.Create<MyJob>()
                            .WithIdentity("myjob")
                            .UsingJobData(pairs)
                            .Build();

            //创建触发器
            ITrigger trigger = TriggerBuilder.Create()
                            .WithIdentity("myTrigger", "group1")
                            .StartNow()
                            .UsingJobData("treekey", "abc")
                            .WithSimpleSchedule(x => x
                                .WithInterval(TimeSpan.FromMilliseconds(100))
                                .RepeatForever())
                            .Build();



            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<MyJob>();

            //配置文件读取
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true);
            var configuration = builder.Build();
            

            //单例方式注入
            serviceCollection.AddSingleton<IConfiguration>(configuration);

            serviceCollection.AddSingleton<IGoodsService, GoodsService>();
            serviceCollection.AddSingleton<IGoodsRepository, GoodsRepository>();
            serviceCollection.AddSingleton<IUnitOfWork, UnitOfWork>();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            sche.JobFactory = new MyJobFactory(serviceProvider);

            await sche.ScheduleJob(job, trigger);

            await sche.Start();

            return Ok();
        }


        /// <summary>
        /// 关机、停止
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Stop()
        {
            var sche = await StdSchedulerFactory.GetDefaultScheduler();
            await sche.Shutdown();
            return Ok();
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Pause()
        {
            var sche = await StdSchedulerFactory.GetDefaultScheduler();

            await sche.PauseJob(new JobKey("myjob"));

            return Ok();
        }

        /// <summary>
        /// 继续
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Resume()
        {
            var sche = await StdSchedulerFactory.GetDefaultScheduler();

            await sche.ResumeJob(new JobKey("myjob"));

            return Ok();
        }
    }

    /// <summary>
    /// readonly const 区别
    /// </summary>
    public class MyJobFactory : IJobFactory
    {        
        private readonly IServiceProvider _serviceProvider;

        public MyJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            return _serviceProvider.GetService(typeof(MyJob)) as IJob;
        }

        public void ReturnJob(IJob job)
        {
            var disposable = job as IDisposable;
            disposable?.Dispose();
        }
    }

    /// <summary>
    /// 作业（任务）
    /// </summary>
    public class MyJob : IJob
    {
        public IConfiguration configuration;

        public MyJob(IConfiguration _configuration)
        {
            this.configuration = _configuration;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (StreamWriter writer = new StreamWriter(Path.Combine(Directory.GetCurrentDirectory(), "quartz.log"), true))
            {
                var data = context.MergedJobDataMap;
                var webhost = data["webhost"] as IWebHostEnvironment;

                await writer.WriteLineAsync($"string:{DateTime.Now}");
            }
        }
    }
}

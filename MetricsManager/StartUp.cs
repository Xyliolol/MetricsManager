using AutoMapper;
using FluentMigrator.Runner;
using MetricsManager.DAL.Interface;
using MetricsManager.DAL.Repository;
using MetricsManager.Jobs;
using MetricsManager.Jobs.Job;
using MetricsManager.ManagerJobs;
using MetricsManager.ManagerJobs.Jobs;
using Microsoft.OpenApi.Models;
using Polly;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace MetricsManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "MetricsManager",
                    Description = "Менеджер сбора метрик, который будет спрашивать у сервисов-агентов данные о собранных ими метриках.",
                    Contact = new OpenApiContact
                    {
                        Name = "Пахниц Артем",
                        Email = "jeeytis@yandex.ru"
                    },
                    Version = "v2"
                });
            });

            var mapperConfiguration = new MapperConfiguration(mapper => mapper.AddProfile(new MapperProfile()));
            var mapper = mapperConfiguration.CreateMapper();

            services.AddSingleton(mapper);
            services.AddSingleton<IConnectionManager, ConnectionManager>();

            services.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
                // Добавляем поддержку SQLite 
                .AddSQLite()
                // Устанавливаем строку подключения
                .WithGlobalConnectionString(ConnectionManager.ConnectionString)
                // Подсказываем, где искать классы с миграциями
                .ScanIn(typeof(Startup).Assembly).For.Migrations()
            ).AddLogging(lb => lb
                .AddFluentMigratorConsole());


            services.AddSingleton<ICpuMetricsRepository, CpuMetricsRepository>();
            services.AddSingleton<IDotNetMetricsRepository, DotNetMetricsRepository>();
            services.AddSingleton<IHddMetricsRepository, HddMetricsRepository>();
            services.AddSingleton<INetworkMetricsRepository, NetworkMetricsRepository>();
            services.AddSingleton<IRamMetricsRepository, RamMetricsRepository>();


            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

            services.AddHostedService<QuartzHostedService>();

            services.AddSingleton<CpuMetricJob>();
            services.AddSingleton<DotNetMetricJob>();
            services.AddSingleton<HddMetricJob>();
            services.AddSingleton<NetworkMetricJob>();
            services.AddSingleton<RamMetricJob>();


            services.AddSingleton(new JobSchedule(
               jobType: typeof(CpuMetricJob),
               cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(DotNetMetricJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(HddMetricJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(NetworkMetricJob),
                cronExpression: "0/5 * * * * ?"));
            services.AddSingleton(new JobSchedule(
                jobType: typeof(RamMetricJob),
                cronExpression: "0/5 * * * * ?"));


            services.AddHttpClient<IMetricsAgentClient, MetricsAgentClient>()
                .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(1000)));
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMigrationRunner migrationRunner)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v2/swagger.json", "MetricsManager v2"));
            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            migrationRunner.MigrateUp();
        }
    }
}


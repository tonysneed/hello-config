using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace hello_config
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MySettings>(
                Configuration.GetSection(nameof(MySettings)));
            services.AddSingleton<IMySettings>(sp =>
                sp.GetRequiredService<IOptions<MySettings>>().Value);
            var settings = services.AddSingleton<IMyOtherSettings>(sp =>
                new MyOtherSettings
                {
                    Setting1 = sp.GetRequiredService<IMySettings>().Setting1,
                    Setting2 = sp.GetRequiredService<IMySettings>().Setting2
                });

            var env = Environment.EnvironmentName;
            var setting1 = Configuration.GetValue<string>("MySettings:Setting1");
            var setting2 = Configuration.GetValue<string>("MySettings:Setting2");
            Console.WriteLine($"Env: {env}, Setting1: {setting1}, Setting2: {setting2}");

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

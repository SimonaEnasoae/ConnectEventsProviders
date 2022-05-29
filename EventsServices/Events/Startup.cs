using Events.Grpc;
using Events.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Events
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
            services.AddCors();

            services.AddDbContext<EventDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionString"],
                   sqlServerOptionsAction: sqlOptions =>
                   {
                       sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                       sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                   }));

            services.AddControllers();
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = 20 * 1024 * 1024; // 2 MB
                options.MaxSendMessageSize = 50 * 1024 * 1024; // 5 MB
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Events", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events v1"));
            }

            app.UseCors(builder => builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<EventService>();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGet("/_proto/", async ctx =>
                {
                    ctx.Response.ContentType = "text/plain";
                    using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "event.proto"), FileMode.Open, FileAccess.Read);
                    using var sr = new StreamReader(fs);
                    while (!sr.EndOfStream)
                    {
                        var line = await sr.ReadLineAsync();
                        if (line != "/* >>" || line != "<< */")
                        {
                            await ctx.Response.WriteAsync(line);
                        }
                    }
                });
            });
        }
    }
}

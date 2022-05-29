using Authentication.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Authentication.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System;
using Microsoft.EntityFrameworkCore;
using Authentication.Services;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Authentication
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
            // Add framework services.
            services.AddDbContext<UserDbContext>(options =>
            options.UseSqlServer(Configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    }));

            services.AddTransient<IAuthService, AuthService>();

            //services.Configure<AppSettings>(Configuration);

            services.AddControllers();
            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = 20 * 1024 * 1024; // 2 MB
                options.MaxSendMessageSize = 50 * 1024 * 1024; // 5 MB
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Authentication", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Authentication v1"));
            }
            app.UseCors(builder => builder
                     .AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());

            //app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<Grpc.AuthService>();
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
                endpoints.MapGet("/_proto/", async ctx =>
                {
                    ctx.Response.ContentType = "text/plain";
                    using var fs = new FileStream(Path.Combine(env.ContentRootPath, "Proto", "auth.proto"), FileMode.Open, FileAccess.Read);
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

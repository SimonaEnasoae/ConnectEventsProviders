using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using WebBff.Services;
using GrpcRequest;
using GrpcEvent;
using GrpcProvider;

namespace WebBff
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebHttpAggregator", Version = "v1" });
            });

            //services.AddTransient<GrpcExceptionInterceptor>();

            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IProviderService, ProviderService>();

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2Support", true);

            services.AddGrpcClient<Request.RequestClient>((services, options) =>
            {
                //var basketApi = services.GetRequiredService<IOptions<UrlConfig>>().Value.GrpcBasket;
                //options.Address = new Uri("http://192.168.0.87:49156");
                options.Address = new Uri("http://localhost:5006");
            });

            services.AddGrpcClient<Event.EventClient>((services, options) =>
            {
                //var basketApi = services.GetRequiredService<IOptions<UrlConfig>>().Value.GrpcBasket;
                //options.Address = new Uri("http://192.168.0.87:49154");
                options.Address = new Uri("http://localhost:5002");
            });

            services.AddGrpcClient<Provider.ProviderClient>((services, options) =>
            {
                //var basketApi = services.GetRequiredService<IOptions<UrlConfig>>().Value.GrpcBasket;
                //options.Address = new Uri("http://192.168.0.87:49155");
                options.Address = new Uri("http://localhost:5004");
            });
            //.AddInterceptor<GrpcExceptionInterceptor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebHttpAggregator v1"));
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
                endpoints.MapControllers();
            });
        }
    }
}

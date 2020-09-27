using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sewasoft.Lagangatho.Chat.Api.Hubs;
using Sewasoft.Lagangatho.Chat.Api.Models;
using Sewasoft.Lagangatho.Chat.Api.Services;

namespace Sewasoft.Lagangatho.Chat.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

            services.AddScoped<IChatRoomService, ChatRoomService>();
            services.AddScoped<IMessageService, MessageService>();

            services.AddSignalR();

            services.AddCors(options =>
            {
            //options.AddPolicy(name: MyAllowSpecificOrigins,
            //                  builder =>
            //                  {
            //                      builder.WithOrigins("http://localhost:3000",
            //                                          "http://www.contoso.com")
            //                                           .AllowAnyMethod()
            //                                           .AllowAnyHeader()
            //                                           .AllowAnyOrigin()
            //                                           .AllowCredentials();

            http://localhost:3000/
                //                  });

                options.AddPolicy("signalr",
                   builder =>
                    builder.WithOrigins("http://localhost:3000",
                 "http://www.contoso.com")
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
                   .SetIsOriginAllowed(hostName => true));
            });
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

            app.UseCors("signalr");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
    }
}

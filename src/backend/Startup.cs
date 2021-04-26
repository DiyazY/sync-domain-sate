using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace backend
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddCors(options => 
                options.AddPolicy("cors", builder =>
                {
                    builder
                        .SetIsOriginAllowed(_ => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }));
        }

        class Entity
        {
            public int Id { get; set; }
            public DateTime Time { get; set; }
            public override string ToString() => $"{{id:{Id}, time:{Time}}}";
        }

        private List<Entity> _entities = new List<Entity>
        {
            new Entity {Id = 1, Time = DateTime.Now},
            new Entity {Id = 2, Time = DateTime.Now},
            new Entity {Id = 3, Time = DateTime.Now},
            new Entity {Id = 4, Time = DateTime.Now}
        };

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("cors");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<EntityHub>("/entityHub");
                
                endpoints.MapGet("/get-entity",
                    async context =>
                    {
                        var id = int.Parse(context.Request.Query["id"]);
                        var entity = _entities.FirstOrDefault(p => p.Id == id);
                        await context.Response.WriteAsync(entity.ToString());
                    });

                endpoints.MapGet("/trigger-update-entity", async context =>
                {
                    var id = int.Parse(context.Request.Query["id"]);
                    var entity = _entities.FirstOrDefault(p => p.Id == id);
                    entity.Time = DateTime.Now;
                    var hub = app.ApplicationServices.GetService<IHubContext<EntityHub>>();
                    hub?.Clients.All.SendAsync("updated-entity", id);
                    await context.Response.WriteAsync(entity.ToString());
                });
            });
        }
    }
}

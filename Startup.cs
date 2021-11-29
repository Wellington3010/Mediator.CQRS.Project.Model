using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using mediator_cqrs_project.Interfaces;
using mediator_cqrs_project.Models;
using mediator_cqrs_project.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using AutoMapper;
using mediator_cqrs_project.Commands;
using mediator_cqrs_project.Notifications;
using mediator_cqrs_project.Queries;
using mediator_cqrs_project.Persistence;
using Microsoft.EntityFrameworkCore;

namespace mediator_cqrs_project
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
            var mapperConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<RegisterAccountCommand, Account>();
                config.CreateMap<UpdateAccountCommand, Account>();
                config.CreateMap<DeleteAccountCommand, Account>();
                config.CreateMap<FindAccountByDocumentNumberQuery, Account>();
                config.CreateMap<FindAccountByTypeQuery, Account>();
                config.CreateMap<Account, CommandAccountRegisterNotification>();
                config.CreateMap<Account, CommandAccountUpdateNotification>();
                config.CreateMap<Account, CommandAccountDeleteNotification>();
                config.CreateMap<Account, QueryAccountNotification>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<AccountContext>(connctionString => connctionString.UseSqlServer(Configuration.GetConnectionString("LocalConnection")));
            services.AddScoped<IRepository<Account>, AccountRepository>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "mediator_cqrs_project", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "mediator_cqrs_project v1"));
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

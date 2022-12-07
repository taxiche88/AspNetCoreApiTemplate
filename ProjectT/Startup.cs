using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectT.Infrastructure.Mappings;
using ProjectT.Infrastructure.Validators;
using ProjectT.Repository.Impletement;
using ProjectT.Repository.Interface;
using ProjectT.Service.Impletement;
using ProjectT.Service.Infrastructure.Mappings;
using ProjectT.Service.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjectT
{
    /// <summary>
    /// �Ұ��I
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// �Ұ��I
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// �պA�]�w
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ���U�A��
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region -- Service --
            services.AddScoped<ICardService, CardService>();
            #endregion

            #region -- Repository --
            services.AddScoped<ICardRepository>(sp =>
            {
                var connectionString = this.Configuration.GetValue<string>("ConnectionString");
                return new CardRepository(connectionString);
            });
            services.AddScoped<ICardTypeRepository>(sp =>
            {
                var connectionString = this.Configuration.GetValue<string>("ConnectionString");
                return new CardTypeRepository(connectionString);
            });
            #endregion

            // Others
            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Startup>();

            services.AddSwaggerGen(c =>
            {
                // API �A��²��
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Asp.Net Core 3.1 API",
                    Description = "�o�OAsp.Net Core 3.1 API���d��, �Ѧ�\"�����s�V�O���d�� API\"",
                });

                // Ū�� XML �ɮײ��� API ����
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// �պA�]�w
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
        }
    }
}

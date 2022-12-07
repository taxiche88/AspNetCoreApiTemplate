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
    /// 啟動點
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 啟動點
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 組態設定
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 註冊服務
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
                // API 服務簡介
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Asp.Net Core 3.1 API",
                    Description = "這是Asp.Net Core 3.1 API的範例, 參考\"菜雞新訓記的範例 API\"",
                });

                // 讀取 XML 檔案產生 API 說明
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// 組態設定
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

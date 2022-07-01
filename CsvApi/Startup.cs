using CSVBusiness;
using CSVBusiness.Interface;
using CSVDataAccess;
using CSVDataAccess.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvApi
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
            services.AddSwaggerGen();
            services.AddScoped<ISaveFileBusiness, SaveFileBusiness>();
            services.AddScoped<IValidateCredentialsBusiness, ValidateCredentialsBusiness>();
            services.AddScoped<ISavedRegistersDataAccess, SavedRegistersDataAccess>();
            services.AddScoped<ISavedFileNameDataAccess, SavedFileNameDataAccess>();
            services.AddScoped<IListFilesBusiness, ListFilesBusiness>();
            services.AddScoped<IListRegistersBusiness, ListRegistersBusiness>();
            services.AddScoped<IListFilesDataAccess, ListFilesDataAccess>();
            services.AddScoped<IListRegistersDataAccess, ListRegistersDataAccess>();
            services.AddCors(options =>
            {
                options.AddPolicy("foo",
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                });
            });



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors("foo"); 
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CSV Api");
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            //app.UseCors("AllowOrigin");
        }
    }
}

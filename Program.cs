
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore.Internal;
using Registration.Models;
using Registration.Services;
using Serilog;
using System.IO;

namespace Registration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRegisterService, RegisterService>();
            builder.Services.AddSqlServer<RegistrationContext>(builder.Configuration.GetConnectionString("Connect"));
            #region logginginconsole
            //builder.Services.AddHttpLogging(httpLogging =>
            //{
            //    httpLogging.LoggingFields = HttpLoggingFields.All;
            //});
            #endregion


            var _logger = new LoggerConfiguration().WriteTo.File("C:\\Users\\user\\OneDrive\\Рабочий стол\\loggs-.log", rollingInterval: RollingInterval.Day).CreateLogger();
            builder.Logging.AddSerilog(_logger);
            
            var app = builder.Build();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(x=>x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
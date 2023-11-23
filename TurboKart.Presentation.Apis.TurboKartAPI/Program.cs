global using TurboKart.Application.Interfaces;
global using TurboKart.Application.UseCases;
global using TurboKart.Domain.Entities;

using Microsoft.EntityFrameworkCore;
using TurboKart.Infrastructure.Persistence.EfContexts;
using TurboKart.Infrastructure.Persistence.Interfaces;
using TurboKart.Infrastructure.Persistence.Repositories;

namespace TurboKart.Presentation.Apis.TurboKartAPI
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


            builder.Services.AddDbContext<DbContext, TurboKartContext>();
            builder.Services.AddTransient<IBookingRepository, BookingRepository>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IBookingUseCase, BookingUseCase>();
            builder.Services.AddTransient<ICustomerUseCase, CustomerUseCase>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
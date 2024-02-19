using Microsoft.EntityFrameworkCore;
using TP4.Models.DataManager;
using TP4.Models.EntityFramework;
using TP4.Models.Repository;

namespace TP4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<FilmRatingsDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("FilmRatingsDBContext")));
            builder.Services.AddControllers();
            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ElectionSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // إضافة الخدمات
            builder.Services.AddControllers();

            // تسجيل DbContext باستخدام Connection String
            builder.Services.AddDbContext<ElectionDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // 📌 إضافة CORS Policy
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // إضافة Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // 🔥 تفعيل Swagger في كل البيئات
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            // 📌 تفعيل CORS
            app.UseCors("AllowAll");

            app.UseAuthorization();

            // Map Controllers
            app.MapControllers();

            app.Run();
        }
    }
}

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

            
builder.Services.AddSerilog( providers =>
                                 {
                                     providers.MinimumLevel.Verbose();

                                     providers.Enrich.WithDemystifiedStackTraces()
                                              .Enrich.WithAssemblyInformationalVersion()
                                              .Enrich.WithClientIp()
                                              .Enrich.WithCorrelationId()
                                              .Enrich.WithSpan()
                                              .Enrich.WithMemoryUsage()
                                              .Enrich.WithMachineName()
                                              .Enrich.WithMemoryUsage()
                                              .Enrich.WithProcessId()
                                              .Enrich.WithProcessName()
                                              .Enrich.FromLogContext()
                                              .Enrich.WithEnvironmentName()
                                              .Enrich.WithProcessId()
                                              .Enrich.WithThreadId()
                                              .Enrich.WithThreadName();


                                     providers.WriteTo.Async( configure => configure.Console(), blockWhenFull: true )
                                              .WriteTo.Async( configure => configure.Debug(), blockWhenFull: true );


                                     providers.WriteTo
                                              .Async( configure => configure.File( "Logs/log-.log", fileSizeLimitBytes: 1024 * 1024 * 50, shared: true, rollingInterval: RollingInterval.Day ),
                                                      blockWhenFull: true );

                                 } );

            
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

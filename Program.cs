using BankSystem.Data;
using BankSystem.Models;
using BankSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace BankSystem;

public class Program
{
    public static int Main(string[] args)
    {
        string assemblyName = typeof(Program).Assembly.GetName().FullName ?? "BankSystem";
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
                .CreateBootstrapLogger();

            Log.Information("Starting {aseemblyName} Host", assemblyName);
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, provider, loggerConfig) =>
            {
                loggerConfig.ReadFrom.Configuration(context.Configuration);
            });

            // Add services to the container.
            var mvcBuilder = builder.Services.AddRazorPages();

#if DEBUG
            mvcBuilder.AddRazorRuntimeCompilation();
#endif

            builder.Services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
                options.UseLoggerFactory(loggerFactory);

#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();

                options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
#endif
            });

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddSingleton<IEmailSender, DummyEmailSender>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();

            return 0;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "{aseemblyName} Host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }
}
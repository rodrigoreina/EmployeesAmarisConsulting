using Business.Interfaces;
using Business.Services;
using DAL.Interfaces;
using DAL.Repository;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            string? employeeHttpClientName = builder.Configuration["EmployeeHttpClientName"];
            ArgumentException.ThrowIfNullOrEmpty(employeeHttpClientName);
            builder.Services.AddHttpClient(
                employeeHttpClientName,
                client =>
                {
                    client.BaseAddress = new Uri("http://dummy.restapiexample.com/api/");
                });

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

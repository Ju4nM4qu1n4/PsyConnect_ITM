var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

namespace Hello_repo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Update");

        }

    }

}

using ShoperBackend.Services;
using System.Net.Http.Headers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddHttpClient<IProductService, ProductService>(client =>
        {
            var token = builder.Configuration["Shoper:ApiToken"];
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        });

        var app = builder.Build();

        app.MapControllers();

        app.Run();
    }
}
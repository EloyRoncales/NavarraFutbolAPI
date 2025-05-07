using Microsoft.EntityFrameworkCore;
using NavarraFutbolAPI.Data;
using NavarraFutbolAPI.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Servicios
// -------------------------
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("NavarraFutbolDB")
           .EnableSensitiveDataLogging()
           .LogTo(Console.WriteLine));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

// -------------------------
// Middleware
// -------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// -------------------------
// Seed de base de datos
// -------------------------
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedDatabase(dbContext);
}

app.Run();

// -------------------------
// Métodos auxiliares
// -------------------------

void SeedDatabase(AppDbContext context)
{
    var jsonPath = Path.Combine(AppContext.BaseDirectory, "Data", "BBDD.json");

    if (!File.Exists(jsonPath))
    {
        Console.WriteLine($"❌ Archivo JSON no encontrado: {jsonPath}");
        return;
    }

    var json = File.ReadAllText(jsonPath);
    try
    {
        var root = JsonSerializer.Deserialize<Root>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        if (root != null && root.Categorias.Any())
        {
            if (!context.Categorias.Any())
            {
                context.Categorias.AddRange(root.Categorias);
                context.SaveChanges();
                Console.WriteLine($"✅ {root.Categorias.Count} categorías cargadas en memoria.");
            }
            else
            {
                Console.WriteLine("ℹ️ La base de datos ya contiene categorías.");
            }
        }
        else
        {
            Console.WriteLine("⚠️ El archivo JSON está vacío o mal formado.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error al deserializar el archivo JSON: {ex.Message}");
    }
}

// Clase raíz del JSON
public class Root
{
    public List<Categoria> Categorias { get; set; } = new();
}

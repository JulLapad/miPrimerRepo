using System.Text.Json;

var configPath = FindAppSettingsPath();

if (configPath is null)
{
    Console.WriteLine("No se encontró appsettings.json.");
    return;
}

using var document = JsonDocument.Parse(File.ReadAllText(configPath));
var connectionString = document.RootElement
    .GetProperty("ConnectionStrings")
    .GetProperty("DefaultConnection")
    .GetString();

Console.WriteLine("Simulación de cadena de conexión:");
Console.WriteLine(connectionString ?? "No disponible");

static string? FindAppSettingsPath()
{
    var currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

    while (currentDirectory is not null)
    {
        var candidate = Path.Combine(currentDirectory.FullName, "appsettings.json");
        if (File.Exists(candidate))
        {
            return candidate;
        }

        currentDirectory = currentDirectory.Parent;
    }

    return null;
}

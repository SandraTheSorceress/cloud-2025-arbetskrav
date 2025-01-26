using MySql.Data.MySqlClient;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(80); // HTTP
    });
}

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", async () =>
{
    // Connection string from configuration
    //string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    var connectionString = "server=db;port=3306;database=todo_database;user=root;password=superSecretPassword";


    // Query to get all rows from the 'todo' table
    string query = "SELECT * FROM todo";

    using var connection = new MySqlConnection(connectionString);
    await connection.OpenAsync();

    using var command = new MySqlCommand(query, connection);
    using var reader = await command.ExecuteReaderAsync();

    var todos = new List<object>();

    while (await reader.ReadAsync())
    {
        todos.Add(new
        {
            Id = reader.GetInt32("id"),
            Description = reader.GetString("description")
        });
    }

    return todos;
})
.WithName("GetTodos")
.WithOpenApi();

app.Run();

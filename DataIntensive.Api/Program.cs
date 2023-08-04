using DataIntensive.Api.Domain.Products;
using DataIntensive.Api.Domain.Users;
using DataIntensive.Api.Infrastructure.Data.EF;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddDbContext<DataIntensiveEntityFrameworkContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("ApplicationDB"),
        x => x.MigrationsAssembly(typeof(DataIntensiveEntityFrameworkContext).Assembly.FullName))
);

Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(
                    options:
                        new ElasticsearchSinkOptions(new Uri(builder.Configuration.GetValue<string>("ElasticSearch:Uri")!))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                            IndexFormat = "data-intensive-api-{0:yyyy.MM.dd}"
                        })
                .WriteTo.SQLite(
                    sqliteDbPath: builder.Configuration.GetConnectionString("LogsDB"),
                    tableName: "Logs")
                .WriteTo.Console()
                .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var serviceProvider = serviceScope.ServiceProvider;

    var context = serviceProvider.GetRequiredService<DataIntensiveEntityFrameworkContext>();
    await SeedData(context);
}

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

static async Task SeedData(DataIntensiveEntityFrameworkContext context)
{
    if (context.Set<Product>().Any()) return;

    var product1 = new Product
    {
        Id = new Guid("2e4bdf30-df7a-4b6b-a732-3fd8b9d1b119"),
        Description = "Product 1",
        Price = 10m
    };

    var product2 = new Product
    {
        Id = new Guid("53995cfc-243d-457e-8e94-d05bc3db7ae6"),
        Description = "Product 2",
        Price = 10.99m
    };

    var product3 = new Product
    {
        Id = new Guid("ae55208a-a4f9-4cb0-8778-3ca875bc7844"),
        Description = "Product 3",
        Price = 57.58m
    };

    var product4 = new Product
    {
        Id = new Guid("b8d8d46c-a3b7-4c9b-8394-9fb2fafc4128"),
        Description = "Product 4",
        Price = 25m
    };

    var product5 = new Product
    {
        Id = new Guid("00759bf8-3cc8-4233-914d-05b0a663411b"),
        Description = "Product 5",
        Price = 75m
    };

    context.AddRange(product1, product2, product3, product4, product5);

    var user1 = new User
    {
        Id = new Guid("5382194e-f7fe-44d6-a2f7-dd13f22495a9"),
        Username = "User 1"
    };

    var user2 = new User
    {
        Id = new Guid("22b117ce-4d2d-4beb-a5fd-bccb6215856d"),
        Username = "User 2"
    };

    var user3 = new User
    {
        Id = new Guid("2d2d53c7-3d86-4c13-8f63-e6c813bb11ce"),
        Username = "User 3"
    };

    var user4 = new User
    {
        Id = new Guid("aee3969d-5557-4784-90c5-f7cc6dfeb1a0"),
        Username = "User 4"
    };

    var user5 = new User
    {
        Id = new Guid("f2817b66-fe04-4a13-8a85-970435c4c66a"),
        Username = "User 5"
    };

    context.AddRange(user1, user2, user3, user4, user5);

    await context.SaveChangesAsync();
}
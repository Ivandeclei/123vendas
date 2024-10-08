using _123Vendas.Application;
using _123Vendas.DbAdapter;
using _123Vendas.DbAdapter.DbAdapterConfiguration;
using _123Vendas.Domain.Adapters;
using _123Vendas.Domain.Services;
using _123VendasApplication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PublishQueueAdapter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<Context>();

var connectionString = builder.Configuration.GetSection("DbAdapterConfiguration:ConnectionString").Value;
builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContext<Context>(options =>
              options.UseSqlServer(connectionString, c => c.MigrationsAssembly(typeof(Context).Assembly.FullName)));

// Configura��es do RabbitMQ
builder.Services.AddSingleton<ConfigurationRabbitMq>();
builder.Services.AddScoped<IPublishQueue, PublishMessageQueueAdapter>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "policyCors",
        policy =>
        {
            policy.WithOrigins("*")
            .AllowAnyHeader()
            .AllowAnyMethod();

        });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(ISaleReadAdapter), typeof(SaleReadAdapter));
builder.Services.AddScoped(typeof(ISaleWriteAdapter), typeof(SaleWriteAdapter));


builder.Services.AddTransient<ISaleService, SaleService>();

builder.Services.AddAutoMapper(typeof(WebApiMapperProfile),
                typeof(IMapper));

builder.Services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

var app = builder.Build();

// Migrate database
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<Context>();
    // Criar o banco de dados se ele n�o existir e executa migration
    dbContext.Database.EnsureCreated();

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

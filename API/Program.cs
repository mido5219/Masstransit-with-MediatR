using Application.Contracts;
using Application.Products.Commands.CreateProduct;
using Application.Products.Queries.GetProducts;
using Infrastructure.Persistence;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.EnableSensitiveDataLogging(true);
    options.UseNpgsql(builder.Configuration.GetConnectionString("default"), x =>
    {
        x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
    });

});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
builder.Services.Configure<ConsumerEndpointsSettings>(builder.Configuration.GetSection("ConsumerEndpoints"));
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(new Uri(builder.Configuration.GetConnectionString("cloudmq")), h =>
        {
            h.UseSsl(s =>
            {
                s.Protocol = SslProtocols.Tls12;
            });
        });
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

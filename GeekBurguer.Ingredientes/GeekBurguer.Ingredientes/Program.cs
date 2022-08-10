using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Repository;
using GeekBurguer.Ingredientes.Repository.Context;
using GeekBurguer.Ingredientes.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<ProductsDbContext>
                (o => o.UseInMemoryDatabase("geekburger-ingredients"));

builder.Services.AddSingleton<IngredientsService>();
builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IIngredientsService, IngredientsService>();
builder.Services.AddSingleton<ILabelImageConsumer, LabelImageConsumerService>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

var labelImageConsumer = app.Services.GetService<ILabelImageConsumer>();
Task.Run(() => labelImageConsumer.Run());


app.Run();



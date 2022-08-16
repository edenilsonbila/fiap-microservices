using GeekBurguer.Ingredientes.Interfaces;
using GeekBurguer.Ingredientes.Repository;
using GeekBurguer.Ingredientes.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IProductRepository, ProductRepository>();
builder.Services.AddSingleton<IIngredientsService, IngredientsService>();
builder.Services.AddSingleton<ILabelImageConsumer, LabelImageConsumerService>();
builder.Services.AddSingleton<IngredientsService>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();

app.MapControllers();

using (var serviceScope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
{
    var labelImageConsumer = serviceScope.ServiceProvider.GetService<ILabelImageConsumer>();
    var ingredientsService = serviceScope.ServiceProvider.GetService<IIngredientsService>();

    Task.Run(() => labelImageConsumer.Run());
    Task.Run(() => ingredientsService.GetProducts());
}

//var labelImageConsumer = app.Services.GetService<ILabelImageConsumer>();
//var ingredientsService = app.Services.GetService<IIngredientsService>();


app.Run();



using BuberBreakfast.Mapping;
using BuberBreakfast.Services.Breakfasts;

var builder = WebApplication.CreateBuilder(args);
{
    // Add services to the container.

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddMappings();
    builder.Services.AddSingleton<IBreakfastService, BreakfastService>();

}
var app = builder.Build();
{
    app.UseExceptionHandler("/error");
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
}
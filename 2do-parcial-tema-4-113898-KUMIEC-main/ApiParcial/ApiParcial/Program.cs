

using ApiParcial.Business;
using ApiParcial.Data;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<GetAvion>());

builder.Services.AddDbContext<ContextBD>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDataBase"));
});

builder.Services.AddMediatR(typeof(GetAvion.Manejador).Assembly);
builder.Services.AddMediatR(typeof(PutAvion.Manejador).Assembly);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
    });
});
var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseCors(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
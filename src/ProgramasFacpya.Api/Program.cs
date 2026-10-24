using ProgramasFacpya.Api.Common;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddFluentValidationConfig();
builder.Services.AddOpenApi();

//registrar dbcontext como singleton para que se pueda inyectar en los controladores
builder.Services.AddSingleton<ProgramasFacpya.Api.Data.DbContext>();
//registrar servicios
builder.Services.AddScoped<ProgramasFacpya.Api.Features.Alumnos.AlumnoService>(); 
builder.Services.AddScoped<ProgramasFacpya.Api.Features.Maestros.MaestroService>(); 
builder.Services.AddScoped<ProgramasFacpya.Api.Features.Carreras.CarreraService>(); 


//CORS - para permitir que el front pueda consumir la API
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseHttpsRedirection();
}

//app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();

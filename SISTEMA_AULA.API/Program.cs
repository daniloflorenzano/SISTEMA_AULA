using Microsoft.EntityFrameworkCore;
using SISTEMA_AULA.MODEL.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'Identity_ContextConnection' not found.");

// Add services to the container.

builder.Services.AddDbContext<DbsistemasContext>(opt => opt.UseSqlServer(connectionString));

// https://stackoverflow.com/questions/72060349/form-field-is-required-even-if-not-defined-so
// https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.mvcoptions.suppressimplicitrequiredattributefornonnullablereferencetypes?view=aspnetcore-6.0#microsoft-aspnetcore-mvc-mvcoptions-suppressimplicitrequiredattributefornonnullablereferencetypes
builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();

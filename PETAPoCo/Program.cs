global using PetaPoco;
using PETAPoCo.ApplicationSR.Data;
using PETAPoCo.ApplicationSR.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddScoped<IDatabase, DatabaseFactory>();
builder.Services.AddScoped<IDatabase>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var cs = config.GetConnectionString("DefaultConnection");

    return new Database(cs, "Microsoft.Data.SqlClient");
});

builder.Services.AddScoped<DepartmentRepo>();
builder.Services.AddScoped<EmployeeRepo>();

builder.Services.AddControllers();

var app = builder.Build();

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

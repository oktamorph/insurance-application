
using Insurance.API.Services;
using Microsoft.EntityFrameworkCore;
using Storage.API.DBContext;
using Storage.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorageContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("InsuranceDBConnection"));
});

builder.Services.AddScoped<IStorageService, StorageService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();

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

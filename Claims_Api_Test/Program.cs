using Claims_Api.Repositories;
using Claims_Api_Test.Interfaces;
using Claims_Api_Test.Models;
using Claims_Api_Test.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IRepositoryBase<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepositoryBase<ClaimType>, ClaimTypeRepository>();
builder.Services.AddScoped<IRepositoryBase<Claim>, ClaimRepository>();
//builder.Services.AddSingleton<List<Company>>();
//builder.Services.AddSingleton<List<ClaimType>>();
//builder.Services.AddSingleton<List<Claim>>();

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

using Claims_Api.Interfaces;
using Claims_Api.Models;
using Claims_Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IRepositoryBase<Company>, CompanyRepository>();
builder.Services.AddScoped<IRepositoryBase<ClaimType>, ClaimTypeRepository>();
builder.Services.AddScoped<IRepositoryBase<Claim>, ClaimRepository>();
builder.Services.AddScoped<ClaimRepository>();
builder.Services.AddScoped<CompanyRepository>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

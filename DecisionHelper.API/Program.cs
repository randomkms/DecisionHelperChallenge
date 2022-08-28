using DecisionHelper.API.Extensions;

const string corsPolicyName = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCorsConfig(corsPolicyName);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCustomServices();

var app = builder.Build();

app.UseCors(corsPolicyName);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();//TODO mb remove

app.MapControllers();

app.Run();
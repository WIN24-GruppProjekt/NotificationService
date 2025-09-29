using SmSService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add local configuration file
builder.Configuration.AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddHttpClient<ISmsService, SmsService>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllers();

app.Run();

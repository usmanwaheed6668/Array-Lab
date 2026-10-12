using ArrayLabApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add controllers
builder.Services.AddControllers();

// Register our services as Singletons so state persists between requests
builder.Services.AddSingleton<ArrayOperations>(sp => new ArrayOperations(15));
builder.Services.AddSingleton<StudentManager>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Enable Swagger always (useful for labs/demos)
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

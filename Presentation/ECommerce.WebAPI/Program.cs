using ECommerce.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(); // normalde presentation katman�yla yatay ili�kide olan persistence ve infrastructure buradaki gibi pers. ve infr.
                                           // i�indeki serviceler i�in dependency injection uygulanmas� durumunda referans al�nabilir katmanlard�r.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

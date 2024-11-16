using ECommerce.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices(); // normalde presentation katmanýyla yatay iliþkide olan persistence ve infrastructure buradaki gibi pers. ve infr.
                                           // içindeki serviceler için dependency injection uygulanmasý durumunda referans alýnabilir katmanlardýr.

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

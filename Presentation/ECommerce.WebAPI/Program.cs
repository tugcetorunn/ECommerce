using ECommerce.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();

// client tan gelecek istekler i�in same-origin policy yi hafifletmek amac�yla cors politikas� ekliyoruz client hostumuzu kabul edilir yapaca��z.
// addDefaultPolicy -> uygulama bazl� default bir policy se�eriz. uygulaman�n her noktas�nda ge�erli olacak bir policy.
// addPolicy -> farkl� sitelere g�re farkl� policy ler belirleriz.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    // policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // allowAnyHeader : gelen b�t�n header lara, allowAnyMethod : gelen t�m metodlara, allowAnyOrigin : gelen t�m adreslere izin ver.
    // yukar�daki �� izni verdi�imizde art�k apimize her istek atan apimizi t�ketebilir g�venilir de�il test ama�l� kullan�yoruz.
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cors service i ekledikten sonra cors middleware ini �a��rmadan bir anlam ifade etmez.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();

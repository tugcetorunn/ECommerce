using ECommerce.Persistence.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceServices();

// client tan gelecek istekler için same-origin policy yi hafifletmek amacýyla cors politikasý ekliyoruz client hostumuzu kabul edilir yapacaðýz.
// addDefaultPolicy -> uygulama bazlý default bir policy seçeriz. uygulamanýn her noktasýnda geçerli olacak bir policy.
// addPolicy -> farklý sitelere göre farklý policy ler belirleriz.
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => 
    // policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin() // allowAnyHeader : gelen bütün header lara, allowAnyMethod : gelen tüm metodlara, allowAnyOrigin : gelen tüm adreslere izin ver.
    // yukarýdaki üç izni verdiðimizde artýk apimize her istek atan apimizi tüketebilir güvenilir deðil test amaçlý kullanýyoruz.
    policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()
));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// cors service i ekledikten sonra cors middleware ini çaðýrmadan bir anlam ifade etmez.
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization(); 

app.MapControllers();

app.Run();

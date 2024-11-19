using ECommerce.Application.Repositories;
using ECommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductWriteRepository productWriteRepository;
        private readonly IProductReadRepository productReadRepository;

        public ProductsController(IProductWriteRepository _productWriteRepository, IProductReadRepository _productReadRepository)
        {
            productWriteRepository = _productWriteRepository;
            productReadRepository = _productReadRepository;
        } // constructor larda çok fazla injection olursa buna constructor injection hell deniyor. bunu düzeltmek için tek bir proxy üzerinden yapabiliriz. daha ileride göreceğiz.
        // repoları test için metod oluşturalım.
        [HttpGet]
        public async Task Get()
        // ilk başta scoped tan dolayı dispose olduğunu düşünmüştük singleton ayarladık düzeldi fakat asıl sıkıntı bu metodu void olarak çalıştırmamız.
        // aşağıdaki metodlar async metod. addrangeasync fonksiyonunun beklenmemesinden kaynaklı saveasync metoduna gelmeden iproductwritereppo nesnesi
        // imha (dispose) ediliyor. bekleme süreci olmadığından repodaki context nesnesi de dispose edilerek patlama yaşanıyor. bu metodu task yaparsak
        // context dispose edilmeden ilgili repo beklenecek ve diğer ikinci metod da aynı nesne ile çalışmış olacak. 
        {
            await productWriteRepository.AddRangeAsync(new()
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    CreatedDate = DateTime.UtcNow,
                    Price = 200,
                    Stock = 10
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    CreatedDate = DateTime.UtcNow,
                    Price = 400,
                    Stock = 5
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    CreatedDate = DateTime.UtcNow,
                    Price = 600,
                    Stock = 20
                }
            });
            var count = await productWriteRepository.SaveChangesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}

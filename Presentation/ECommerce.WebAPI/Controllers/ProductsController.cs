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
        }

        [HttpGet]
        public async Task Get()
        {
            //await productWriteRepository.AddRangeAsync(new()
            //{
            //    new()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Product 1",
            //        CreatedDate = DateTime.UtcNow,
            //        Price = 200,
            //        Stock = 10
            //    },
            //    new()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Product 2",
            //        CreatedDate = DateTime.UtcNow,
            //        Price = 400,
            //        Stock = 5
            //    },
            //    new()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Product 3",
            //        CreatedDate = DateTime.UtcNow,
            //        Price = 600,
            //        Stock = 20
            //    }
            //});
            //var count = await productWriteRepository.SaveChangesAsync();

            var product1 = await productReadRepository.GetByIdAsync("d1f088c5-b2c8-4898-9af3-0bddedc021c0"); // önce tracking true iken çalıştıralım.
            product1.Name = "Mouse";
            await productWriteRepository.SaveChangesAsync();
            // read işleminde tracking i inaktif etedik. ve gönderdiğimiz data takip edilmeye devam etti ve ismi mouse olarak değiştirip kaydettik takip
            // ettiği için bunu da uyguladı.
            // bir de tracking i inaktif edelim.

            var product2 = await productReadRepository.GetByIdAsync("e3215396-78d0-446c-a7ca-867a33293167", false); 
            product2.Name = "Klavye";
            await productWriteRepository.SaveChangesAsync();
            // tracking mekanizmasını kapattığımız için datayı getirdi ama takip etmedi. aşağıdaki değer tanımlamalarının hiçbir önemi olmadı bu yüzden.

            // ayrıca read ile çektiğimiz bir veriyi nasıl bir de write yapabildik? repolar da context de scoped olduğu için bir requeste özel bir tane
            // instance oluşturuldu. ve tüm taleplerde o instance ı gönderiyor. hem write hem read operasyonlarında da aynı context instance ı kullanılıyor.
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            Product product = await productReadRepository.GetByIdAsync(id, false);
            return Ok(product);
        }
    }
}

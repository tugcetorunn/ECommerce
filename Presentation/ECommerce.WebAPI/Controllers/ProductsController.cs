using ECommerce.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // .net core projesi ile ioc container ı otomatik geliyor (interfaceleri tutmak için). UI tarafından bu controller a request geldiğinde örneğin 
        // burada product listesini getir isteği geliyor. biz de bunu ilgili service ten almak için önce applicationdan bunun interface ini talep ediyoruz.
        // ioc containerda da ıproductservice türünden bir talep gelirse persistence tan productservice nesnesini getir bilgisini tutuyor ıservicecollection
        // parametresiyle oluşturduğumuz extension sayesinde. ve program.cs de bu extension çalıştırıldıktan sonra.
        private readonly IProductService productService;
        public ProductsController(IProductService _productService) // bu interface referansına karşılık persistence taki concrete service ten nesne gelecek.
        {
            productService = _productService;
        }

        [HttpGet]
        public IActionResult GetProductList() 
        {
            return Ok(productService.GetProducts());
        }
    }
}

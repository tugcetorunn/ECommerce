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
        private readonly IOrderWriteRepository orderWriteRepository;
        private readonly IOrderReadRepository orderReadRepository;
        private readonly ICustomerWriteRepository customerWriteRepository;
        private readonly ICustomerReadRepository customerReadRepository;

        public ProductsController(IProductWriteRepository _productWriteRepository, IProductReadRepository _productReadRepository, IOrderWriteRepository _orderWriteRepository, ICustomerWriteRepository _customerWriteRepository, ICustomerReadRepository _customerReadRepository, IOrderReadRepository _orderReadRepository)
        {
            productWriteRepository = _productWriteRepository;
            productReadRepository = _productReadRepository;
            orderWriteRepository = _orderWriteRepository;
            customerWriteRepository = _customerWriteRepository;
            customerReadRepository = _customerReadRepository;
            orderReadRepository = _orderReadRepository;
        }

        [HttpGet]
        public async Task Get()
        {
            // var customerId = Guid.NewGuid();
            // await customerWriteRepository.AddAsync(new() { Name = "tugce", Id = customerId });
            // customer ile order arasında foreign key olduğu için customer da bağlamamız gerekiyor.
            // await orderWriteRepository.AddAsync(new(){ Description = "descripto", Address = "bursa", CustomerId = customerId });
            // await orderWriteRepository.AddAsync(new(){ Description = "descripto 2", Address = "manisa", CustomerId = customerId });
            // await orderWriteRepository.SaveChangesAsync(); // scoped ile register edildiği için tek bir requestte aynı context nesnesi üzerinde
            //                                               // savechanges yapmış oluyoruz. hangi entity de ne işlem yapıyor olursak yapalım bir tane
            //                                               // savechanges operasyonu aynı context nesnesi üzrinde hepsine uygulanır. 

            // insert için interceptor ı denedikten sonra update için deneyelim.
            var order = await orderReadRepository.GetByIdAsync("edd4c3ea-b102-49c4-b875-8aa4a0729dda");
            order.Description = "water"; // id ile getirdiğimiz order nesnesini ef core zaten track edeceği için buradaki değişikliği görecek. bu yüzden
                                         // update operasyonu yapmasak da olur
            await orderWriteRepository.SaveChangesAsync();



        }
    }
}

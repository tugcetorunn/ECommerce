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
        public IQueryable<Order> Get()
        {
            var orders = orderReadRepository.GetAll();
            return orders;
        }
    }
}

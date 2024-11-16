using ECommerce.Application.Abstractions;
using ECommerce.Domain.Entities;

namespace ECommerce.Persistence.Concretes
{
    public class ProductService : IProductService
    {
        public List<Product> GetProducts()
            => new() // normalde veritabanından alıyoruz fakat şuan onion architecture simülasyonu yaptığımız için yandaki target type new özelliği ile
                     // referansı belli olan nesne oluştururken kullanabildiğimiz yapıyla örnek obje oluşturuyoruz. yani normalde metod içinde
                     // return list a = new list() yaparken tipi belli olan bu nesne oluşturmalarda direk new() ile oluşturabiliyoruz. (c# 9 dan sonra)
            {
                new() // burada da normalde new product() ile oluştururken tip zaten metodda belli olduğu için içeride product oluşturulacağı belli
                      // o yüzden sadece new() ile yapabiliyoruz.
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 1",
                    Stock = 10,
                    Price = 1000,
                    CreatedDate = DateTime.Now
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 2",
                    Stock = 10,
                    Price = 1500,
                    CreatedDate = DateTime.Now
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 3",
                    Stock = 10,
                    Price = 1800,
                    CreatedDate = DateTime.Now
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 4",
                    Stock = 10,
                    Price = 2000,
                    CreatedDate = DateTime.Now
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product 5",
                    Stock = 10,
                    Price = 3000,
                    CreatedDate = DateTime.Now
                }
            };
    }
}

using ECommerce.Domain.Entities.Common;

namespace ECommerce.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string Description { get; set; }
        public string Address { get; set; } // adres customer bazlı değil, order bazlı olsun.
        public ICollection<Product> Products { get; set; }
        public Guid CustomerId { get; set; } // yazmazsak ef core kendi de oluşturabiliyor.
        public Customer Customer { get; set; }
    }
}


namespace ECommerce.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        // public DateTime DeletedDate { get; set; } verileri direk silmeden delete işlemi yapılırsa o tarihi gönderecek veriyi kaldırmayacak ola property
        // deletedDate boş olan silinmemiş, tarih olan o tarihte silinmiştir.
    }
}

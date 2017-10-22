using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Models
{
    public class Sale
    {
        public int Id { get; set; }

        public decimal Discount { get; set; }

        public int Car_Id { get; set; }

        [ForeignKey("Car_Id")]
        public virtual Car Car { get; set; }

        public int Customer_Id { get; set; }

        [ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }
    }
}

using System;

namespace Warehouse.Entities
{
    public class Good:BaseEntity
    {
       
        public string Name { get; set; }
        public int TypeId { get; set; }
        public int SupplierId { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public DateTime DeliveryDate { get; set; }

        public override string ToString()
        {
            return Id + "\t" + Name + "\t" + TypeId + "\t" + SupplierId + "\t" + Quantity + "\t" + Cost + "\t" + DeliveryDate;
        }
    }
}

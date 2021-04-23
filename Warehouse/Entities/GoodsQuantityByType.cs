using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entities
{
    public class GoodsQuantityByType:BaseEntity
    {
        public int TypeId { get; set; }
        public int AverageQuantity { get; set; }

        public override string ToString()
        {
            return "TypeId: " + TypeId + "\t" + "AverageQuantity: " + AverageQuantity;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entities
{
    public class Supplier:BaseEntity
    {  
        public string Name { get; set; }

        public override string ToString()
        {
            return Id + "\t" + Name;
        }
    }
}

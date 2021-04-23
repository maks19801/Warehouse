using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse.Entities
{
    public class TypesOfGood:BaseEntity
    {
        public string Type { get; set; }
        public override string ToString()
        {
            return Id + "\t" + Type;
        }
    }
}

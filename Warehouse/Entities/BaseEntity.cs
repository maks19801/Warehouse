namespace Warehouse.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public override abstract string  ToString();
    }
}

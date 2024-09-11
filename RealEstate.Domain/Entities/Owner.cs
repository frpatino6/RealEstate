namespace RealEstate.Domain.Entities
{
    public class Owner
    {
        public Guid IdOwner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Photo { get; set; }
        public DateTime Birthday { get; set; }

        public List<Property> Properties { get; set; } = new List<Property>();
    }
}
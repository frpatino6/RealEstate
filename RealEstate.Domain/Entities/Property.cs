namespace RealEstate.Domain.Entities
{
    public class Property
    {
        public Guid IdProperty { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public string CodeInternal { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }
        public Guid IdOwner { get; set; }
        public Owner Owner { get; set; }

        public List<PropertyImage> Images { get; set; } = new List<PropertyImage>();

        public List<PropertyTrace> Traces { get; set; } = new List<PropertyTrace>();
    }
}
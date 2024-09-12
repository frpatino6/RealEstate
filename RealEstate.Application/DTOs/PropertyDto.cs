namespace RealEstate.Application.DTOs
{
    public record PropertyDto(Guid IdProperty, string Name, string Address, decimal Price, int Year);

}

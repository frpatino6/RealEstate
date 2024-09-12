namespace RealEstate.Application.DTOs
{
    public record OwnerDto(
        Guid IdOwner,
        string Name,
        string Address,
        string Photo,
        DateTime Birthday
    );

}

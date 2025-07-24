namespace PlaceOfInterest.Domain.Interface;

public interface INameId
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}

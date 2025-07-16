namespace PlaceOfInterest.Domain.Interface;

public interface IEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }
}

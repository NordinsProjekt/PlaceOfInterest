namespace Contracts.Models.Dto;

public sealed record StartLocationApiDto(Guid Id, string Name, string Description, string Location, bool Verified);

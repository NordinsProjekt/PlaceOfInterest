namespace Contracts.Models.Dto;

public sealed record EndLocationApiDto(Guid Id, string Name, string Description, string Location, bool Verified);

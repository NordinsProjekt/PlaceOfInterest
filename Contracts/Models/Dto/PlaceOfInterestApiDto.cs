using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Dto;

public sealed record PlaceOfInterestApiDto(
    Guid Id,
    string Name,
    string Description,
    StartLocationApiDto StartLocation,
    EndLocationApiDto EndLocation,
    string ImageUrl,
    byte TerrainScore,
    byte Score,
    bool Verified);

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaceOfInterest.Domain;

public class PlaceOfInterest
{
    public Guid Id { get; set; }
    public string PublicUniqueToken { get; set; } = "";
    public DateTime Created { get; set; }
    public StartLocation StartLocation { get; set; }
    public EndLocation EndLocation { get; set; }
    public byte[] ImageBytes { get; set; } = [];
    public string ImageType { get; set; } = "";
    public string ImageUrl { get; set; } = "";
    public byte TerrainScore { get; set; }
    public byte Score { get; set; } //1-5 stars
    public bool Verified { get; set; }
}
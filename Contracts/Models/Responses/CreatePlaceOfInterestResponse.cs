using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Models.Responses;

public sealed record CreatePlaceOfInterestResponse(bool Success, string Token);

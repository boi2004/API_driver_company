using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class Location
{
    public int Id { get; set; }

    public int? VehicleId { get; set; }

    public double Latitude { get; set; }

    public double Longitude { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}

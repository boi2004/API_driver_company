using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class Livestream
{
    public int Id { get; set; }

    public int? VehicleId { get; set; }

    public int? DriverId { get; set; }

    public string? AudioUrl { get; set; }

    public string? VideoUrl { get; set; }

    public string? DownloadUrl { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public virtual User? Driver { get; set; }

    public virtual Vehicle? Vehicle { get; set; }

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}

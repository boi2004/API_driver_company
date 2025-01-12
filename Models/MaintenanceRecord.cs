using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class MaintenanceRecord
{
    public int Id { get; set; }

    public int? VehicleId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly MaintenanceDate { get; set; }

    public decimal? Cost { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}

using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class Vehicle
{
    public int Id { get; set; }

    public string VehicleName { get; set; } = null!;

    public string LicensePlate { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? CurrentDriverId { get; set; }

    public bool? IsActive { get; set; }

    public DateOnly? LastMaintenanceDate { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? DeletedAt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User? CurrentDriver { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Livestream> Livestreams { get; set; } = new List<Livestream>();

    public virtual ICollection<Location> Locations { get; set; } = new List<Location>();

    public virtual ICollection<MaintenanceRecord> MaintenanceRecords { get; set; } = new List<MaintenanceRecord>();
}

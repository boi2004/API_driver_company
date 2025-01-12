using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class Document
{
    public int Id { get; set; }

    public int? VehicleId { get; set; }

    public string DocType { get; set; } = null!;

    public string? DocNumber { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Vehicle? Vehicle { get; set; }
}

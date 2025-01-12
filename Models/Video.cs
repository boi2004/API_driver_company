using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models;

public partial class Video
{
    public int Id { get; set; }

    public int? LivestreamId { get; set; }

    public string VideoUrl { get; set; } = null!;

    public string? Description { get; set; }

    public decimal? FileSize { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Livestream? Livestream { get; set; }
}

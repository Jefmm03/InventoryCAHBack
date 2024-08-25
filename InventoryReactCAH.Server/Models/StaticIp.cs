using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class StaticIp
{
    public int Id { get; set; }

    public string? Device { get; set; }

    public string? Area { get; set; }

    public string? NetworkPoint { get; set; }

    public string? Switch { get; set; }

    public string Ipaddress { get; set; } = null!;

    public string? Line { get; set; }

    public string? Location { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

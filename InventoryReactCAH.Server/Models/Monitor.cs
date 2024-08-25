using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class Monitor
{
    public int Id { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string? ActivoCr { get; set; }

    public string? Model { get; set; }

    public string? User { get; set; }

    public int? Size { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

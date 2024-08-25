using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class CellPhone
{
    public int Id { get; set; }

    public long Imei { get; set; }

    public string? Model { get; set; }

    public long? Number { get; set; }

    public string? User { get; set; }

    public int? Pin { get; set; }

    public long? Puk { get; set; }

    public string? IcloudPass { get; set; }

    public string? IcloudUser { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

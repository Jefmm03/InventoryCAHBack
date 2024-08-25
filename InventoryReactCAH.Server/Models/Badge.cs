using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class Badge
{
    public int Id { get; set; }

    public int Number { get; set; }

    public string? Department { get; set; }

    public int Status { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

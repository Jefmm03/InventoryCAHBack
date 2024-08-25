using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class Telephone
{
    public int Id { get; set; }

    public string Serie { get; set; } = null!;

    public string? Activo { get; set; }

    public int? Ext { get; set; }

    public string? Employee { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class TelCode
{
    public int Id { get; set; }

    public int Code { get; set; }

    public int Cor { get; set; }

    public int CallType { get; set; }

    public string? Asignation { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

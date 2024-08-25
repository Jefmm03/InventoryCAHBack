using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool Active { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

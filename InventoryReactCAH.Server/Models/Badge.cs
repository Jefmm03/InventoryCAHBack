using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryReactCAH.Server.Models;

public partial class Badge
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 

    public int Id { get; set; }

    public int Number { get; set; }

    public string? Department { get; set; }

    public int Status { get; set; }

    public string? Comment { get; set; }

    public int? ParentId { get; set; }

    public string? ModifiedBy { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

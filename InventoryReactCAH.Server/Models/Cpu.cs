using System;
using System.Collections.Generic;

namespace InventoryReactCAH.Server.Models;

public partial class Cpu
{
    public int Id { get; set; }

    public string SerialNumber { get; set; } = null!;

    public string? AssetNcr { get; set; }

    public string? AssetN { get; set; }

    public string? Model { get; set; }

    public string? User { get; set; }

    public string? Department { get; set; }

    public string? WireMac { get; set; }

    public string? WirelessMac { get; set; }

    public decimal? Memory { get; set; }

    public string? Processor { get; set; }

    public decimal? ProcessorSpeed { get; set; }

    public decimal? HardDisk { get; set; }

    public string? OperatingSystem { get; set; }

    public string? ComputerName { get; set; }

    public string? Domain { get; set; }

    public string? OpticalDrive { get; set; }

    public string? PadLock { get; set; }

    public string? Comment { get; set; }

    public int Status { get; set; }

    public int? ParentId { get; set; }

    public string ModifiedBy { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}

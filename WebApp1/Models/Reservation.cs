using System;
using System.Collections.Generic;

namespace WebApp1.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string? ReserverName { get; set; }

    public int RoomId { get; set; }

    public virtual Room Room { get; set; } = null!;
}

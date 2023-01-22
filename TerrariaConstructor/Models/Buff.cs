using System;

namespace TerrariaConstructor.Models;

public class Buff
{
    public int Id { get; set; }
    public string Name { get; set; }
    public TimeSpan DurationTime { get; set; }
    public string Description { get; set; }
}
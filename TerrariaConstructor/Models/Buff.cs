using System;

namespace TerrariaConstructor.Models;

public class Buff
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int ImageId { get; set; }
    public byte[] Image { get; set; }
    public bool IsNegative { get; set; }
    public TimeSpan DurationTime { get; set; }
    public Item[] Sources { get; set; }
    public string ToolTip { get; set; }
    public string EffectDescription { get; set; }
    public string WikiUrl { get; set; }
}
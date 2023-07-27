using System;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TerrariaConstructor.Models;

public class Buff : ReactiveObject
{
    [Reactive]
    public int Id { get; set; }
    [Reactive]
    public string Name { get; set; }
    [Reactive]
    public int ImageId { get; set; }
    [Reactive]
    public byte[] Image { get; set; }
    [Reactive]
    public bool IsNegative { get; set; }
    [Reactive]
    public TimeSpan? BaseDurationTime { get; set; }
    [Reactive]
    public DateTime? CurrentDurationTime { get; set; }
    [Reactive]
    public string[] Sources { get; set; }
    [Reactive]
    public Item[] ItemSources { get; set; }
    [Reactive]
    public string ToolTip { get; set; }
    [Reactive]
    public string EffectDescription { get; set; }
    [Reactive]
    public string WikiUrl { get; set; }
}
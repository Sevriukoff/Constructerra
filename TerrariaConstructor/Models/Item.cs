using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace TerrariaConstructor.Models;

public class Item : ReactiveObject
{
    private bool _isResearched;
    private int _stack;

    #region Properties
    [Reactive]
    public int Id { get; set; }
    [Reactive]
    public string Name { get; set; }
    public string InternalName { get; set; }
    [Reactive]
    public byte[] Image { get; set; }
    [Reactive]
    public string Description { get; set; }
    [Reactive]
    public string Tooltip { get; set; }
    [Reactive]
    public ItemRarity Rarity { get; set; }
    [Reactive]
    public string[] Categories { get; set; }

    #region Damage

    [Reactive]
    public int Damage { get; set; }
    [Reactive]
    public DamageType DamageType { get; set; }
    [Reactive]
    public int CriticalChance { get; set; }
    [Reactive]
    public double Knockback { get; set; }
    public KnockbackType KnockbackType { get; set; }

    #endregion
   
    [Reactive]
    public int UseTime { get; set; }

    #region Power

    [Reactive]
    public int PickaxePower { get; set; }
    [Reactive]
    public int AxePower { get; set; }
    [Reactive]
    public int HammerPower { get; set; }

    #endregion
    
    [Reactive]
    public int DefensePoint { get; set; }
    [Reactive]
    public int QuantityForResearch { get; set; }

    #region Cost

    [Reactive]
    public long CostByBuy { get; set; }
    [Reactive]
    public long CostBySell { get; set; }

    #endregion

    #region Prefix

    [Reactive]
    public byte Prefix { get; set; }
    [Reactive]
    public Modifier Modifier { get; set; }

    #endregion

    #region Stack

    public int Stack
    {
        get => _stack;
        set
        {
            this.RaiseAndSetIfChanged(ref _stack, value);
            
            _isResearched = _stack >= QuantityForResearch;
            this.RaisePropertyChanged(nameof(IsResearched));
        }
    }

    [Reactive]
    public int MaxStack { get; set; }

    #endregion

    [Reactive]
    public bool IsPlaceable { get; set; }
    [Reactive]
    public bool IsFavorite { get; set; }
    public string WikiUrl { get; set; }
    
    #endregion

    public bool IsResearched
    {
        get => _isResearched;
        set
        {
            _isResearched = true;
            Stack = QuantityForResearch;
            this.RaisePropertyChanged(nameof(Stack));
            this.RaisePropertyChanged();
        }
    }
}

public enum ItemRarity
{
    None = -2,
    Gray,
    White,
    Blue,
    Green,
    Orange,
    LightRed,
    Pink,
    LightPurple,
    Lime,
    Yellow,
    Cyan,
    Red,
    Purple,
    FieryRed,
    Amber
}

public enum DamageType
{
    None,
    Melee,
    Ranged,
    Magic,
    Summon,
    Sentry,
    Explosion
}

public enum KnockbackType
{
    None,
    NoKnockback,
    ExtremelyWeak,
    VeryWeak,
    Weak,
    Average,
    Strong,
    VeryStrong,
    Insane
}
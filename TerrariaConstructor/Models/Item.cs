namespace TerrariaConstructor.Models;

public class Item
{
    #region Properties
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string InternalName { get; set; }
    public byte[] Image { get; set; }
    public string Description { get; set; }
    public string Tooltip { get; set; }
    public ItemRarity Rarity { get; set; }
    public string[] Categories { get; set; }

    #region Damage

    public int Damage { get; set; }
    public DamageType DamageType { get; set; }
    public int CriticalChance { get; set; }
    public double Knockback { get; set; }
    public KnockbackType KnockbackType { get; set; }

    #endregion
   
    public int UseTime { get; set; }

    #region Power

    public int PickaxePower { get; set; }
    public int AxePower { get; set; }
    public int HammerPower { get; set; }

    #endregion
    
    public int DefensePoint { get; set; }
    public int QuantityForResearch { get; set; }

    #region Cost

    public long CostByBuy { get; set; }
    public long CostBySell { get; set; }

    #endregion

    #region Prefix

    public byte Prefix { get; set; }
    public Modifier Modifier { get; set; }

    #endregion

    #region Stack

    public int Stack { get; set; }
    public int MaxStack { get; set; }

    #endregion

    public bool IsPlaceable { get; set; }
    public bool IsFavorite { get; set; }
    public string WikiUrl { get; set; }
    
    #endregion
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
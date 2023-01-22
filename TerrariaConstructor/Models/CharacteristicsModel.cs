using System;
using System.Drawing;

namespace TerrariaConstructor.Models;

public class CharacteristicsModel
{
     
    #region Player statistics
    
    public string Name { get; set; }
    public byte Difficulty { get; set; }
    public TimeSpan PlayTime { get; set; }

    public int Hair { get; set; }
    public byte HairDye { get; set; }
    public byte SkinVariant { get; set; }
    
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public int Mana { get; set; }
    public int MaxMana { get; set; }
    public int TaxMoney { get; set; }

    public Color HairColor { get; set; }
    public Color SkinColor { get; set; }
    public Color EyeColor { get; set; }
    public Color ShirtColor { get; set; }
    public Color UnderShirtColor { get; set; }
    public Color PantsColor { get; set; }
    public Color ShoeColor { get; set; }

    #endregion

    #region Other

    public byte[] HideBytes { get; set; } = new byte[3];
    public bool ExtraAccessory { get; set; }
    public bool UnlockedBiomeTorches { get; set; }
    public bool UsingBiomeTorches { get; set; }
    public bool AteArtisanBread { get; set; }
    public bool UsedAegisCrystal { get; set; }
    public bool UsedAegisFruit { get; set; }
    public bool UsedArcaneCrystal { get; set; }
    public bool UsedGalaxyPearl { get; set; }
    public bool UsedGummyWorm { get; set; }
    public bool UsedAmbrosia { get; set; }
    public bool DownedDd2EventAnyDifficulty { get; set; }
    public int NumberOfDeathsPve { get; set; }
    public int NumberOfDeathsPvp { get; set; }
    public bool IsHotBarLocked { get; set; }
    public bool[] HideInfo { get; set; } = new bool[13];
    public int AnglerQuestsFinished { get; set; }
    public int[] DPadRadialBindings { get; set; } = new int[4];
    public int[] BuilderAccStatus { get; set; } = new int[12];
    public int BartenderQuestLog { get; set; }
    public int GolferScoreAccumulated { get; set; }
    public long LastTimePlayerWasSaved { get; set; }
    public int RespawnTimer { get; set; }
    public bool IsDead { get; set; }
    
    #endregion

}
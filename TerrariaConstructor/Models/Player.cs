using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TerrariaConstructor.Models;

public class Player
{
    private const string EncryptKey = "h3y_gUyZ";
    
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

    public Item[] Armor { get; set; } = new Item[20];
    public Item[] Dye { get; set; } = new Item[10];
    public Item[] Inventory { get; set; } = new Item[50];
    public Item[] MiscEquip { get; set; } = new Item[5];
    public Item[] MiscDye { get; set; } = new Item[5];
    public Item[] Bank1 { get; set; } = new Item[40];
    public Item[] Bank2 { get; set; } = new Item[40];
    public Item[] Bank3 { get; set; } = new Item[40];
    public Item[] Bank4 { get; set; } = new Item[40];
    public Item[] Purse { get; set; } = new Item[4];
    public Item[] Ammo { get; set; } = new Item[4];
    public Item[] Buffs { get; set; } = new Item[44]; //TODO: Create custom class for buffs

    public int[] SpawnX { get; set; } = new int[200];
    public int[] SpawnY { get; set; } = new int[200];
    public int[] WorldId { get; set; } = new int[200];
    public string[] WorldNames { get; set; } = new string[200];
    
    #endregion

    #region Other

    public byte[] HideBytes { get; private set; } = new byte[3];
    public bool ExtraAccessory { get; private set; }
    public bool UnlockedBiomeTorches { get; private set; }
    public bool UsingBiomeTorches { get; private set; }
    public bool AteArtisanBread { get; private set; }
    public bool UsedAegisCrystal { get; private set; }
    public bool UsedAegisFruit { get; private set; }
    public bool UsedArcaneCrystal { get; private set; }
    public bool UsedGalaxyPearl { get; private set; }
    public bool UsedGummyWorm { get; private set; }
    public bool UsedAmbrosia { get; private set; }
    public bool DownedDd2EventAnyDifficulty { get; private set; }
    public int NumberOfDeathsPve { get; private set; }
    public int NumberOfDeathsPvp { get; private set; }
    public bool IsHotBarLocked { get; private set; }
    public bool[] HideInfo { get; private set; } = new bool[13];
    public int AnglerQuestsFinished { get; private set; }
    public int[] DPadRadialBindings { get; private set; } = new int[4];
    public int[] BuilderAccStatus { get; private set; } = new int[12];
    public int BartenderQuestLog { get; private set; }
    public int GolferScoreAccumulated { get; private set; }
    public long LastTimePlayerWasSaved { get; private set; }
    public int RespawnTimer { get; private set; }
    public bool IsDead { get; private set; }
    
    
    #endregion

    public Player()
    {
        
    }

    private BinaryReader DecryptPlayer(string path)
    {
        byte[] key = Encoding.Unicode.GetBytes(EncryptKey);

        try
        {
            var managed = new RijndaelManaged{Padding = PaddingMode.None};
            var decryptor = managed.CreateDecryptor(key, key);

            using (MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(path)))
            {
                using (CryptoStream input = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    byte[] buffer = new byte[memoryStream.Length];
                    input.Read(buffer, 0, buffer.Length);
                    var newMemoryStream = new MemoryStream(buffer);
                    return new BinaryReader(newMemoryStream);
                }
            }
                   
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void EncryptPlayer(string path)
    {
        
    }

    public void LoadPlayer(string plrPath)
    {
        var reader = DecryptPlayer(plrPath);
        
        int terrariaVersion = reader.ReadInt32();
        
        long num1 = (long) reader.ReadUInt64();
        if ((num1 & 72057594037927935L) != 27981915666277746L)
            throw new FormatException("Expected Re-Logic file format.");
        
        var revision = reader
            .ReadUInt32(); // Useless metadata stuff in player files, for now? Only used in maps
        var isFavorite =
            ((long) reader.ReadUInt64() & 1L) ==
            1L; // Does not actually matter? favorites.json seems to handle favorites
        
        Name = reader.ReadString();
        Difficulty = reader.ReadByte();
        PlayTime = new TimeSpan(reader.ReadInt64());
        Hair = reader.ReadInt32();
        HairDye = reader.ReadByte();
        HideBytes = reader.ReadBytes(3);
        SkinVariant = reader.ReadByte();
        Health = reader.ReadInt32();
        MaxHealth = reader.ReadInt32();
        Mana = reader.ReadInt32();
        MaxMana = reader.ReadInt32();
        ExtraAccessory = reader.ReadBoolean();
        UnlockedBiomeTorches = reader.ReadBoolean();
        UsingBiomeTorches = reader.ReadBoolean();
        AteArtisanBread = reader.ReadBoolean();
        UsedAegisCrystal = reader.ReadBoolean();
        UsedAegisFruit = reader.ReadBoolean();
        UsedArcaneCrystal = reader.ReadBoolean();
        UsedGalaxyPearl = reader.ReadBoolean();
        UsedGummyWorm = reader.ReadBoolean();
        UsedAmbrosia = reader.ReadBoolean();
        DownedDd2EventAnyDifficulty = reader.ReadBoolean();
        TaxMoney = reader.ReadInt32();
        NumberOfDeathsPve = reader.ReadInt32();
        NumberOfDeathsPvp = reader.ReadInt32();
        HairColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        SkinColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        EyeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        ShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        UnderShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        PantsColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        ShoeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        for (int i = 0; i < Armor.Length; i++)
        {
            Armor[0] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < Dye.Length; i++)
        {
            Dye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Inventory.Length; i++)
        {
            Inventory[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < Purse.Length; i++)
        {
            Purse[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < Ammo.Length; i++)
        {
            Ammo[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < MiscEquip.Length; i++)
        {
            MiscEquip[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
            };

            MiscDye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < Bank1.Length; i++)
        {
            Bank1[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Bank2.Length; i++)
        {
            Bank2[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Bank3.Length; i++)
        {
            Bank3[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Bank4.Length; i++)
        {
            Bank4[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        var voidVaultInfo = reader.ReadByte();

        for (int i = 0; i < Buffs.Length; i++)
        {
            var id = reader.ReadInt32();
            var time = reader.ReadInt32();
        }

        for (int i = 0; i < SpawnX.Length; i++)
        {
            var spawnX = reader.ReadInt32();

            if (spawnX != -1)
            {
                SpawnX[i] = spawnX;
                SpawnY[i] = reader.ReadInt32();
                WorldId[i] = reader.ReadInt32();
                WorldNames[i] = reader.ReadString();
            }
            else
            {
                break;
            }
        }

        IsHotBarLocked = reader.ReadBoolean();

        for (int i = 0; i < HideInfo.Length; i++)
            HideInfo[i] = reader.ReadBoolean();
        
        AnglerQuestsFinished = reader.ReadInt32();

        for (int i = 0; i < DPadRadialBindings.Length; i++)
            DPadRadialBindings[i] = reader.ReadInt32();

        for (int i = 0; i < BuilderAccStatus.Length; i++)
            BuilderAccStatus[i] = reader.ReadInt32();

        BartenderQuestLog = reader.ReadInt32();

        IsDead = reader.ReadBoolean();

        if (IsDead)
            RespawnTimer = reader.ReadInt32();

        LastTimePlayerWasSaved = 0L;

        LastTimePlayerWasSaved = terrariaVersion < 202 ? DateTime.UtcNow.ToBinary() : reader.ReadInt64();

        GolferScoreAccumulated = reader.ReadInt32();
    }
    
    public void SavePlayer()
    {
        
    }
    
}
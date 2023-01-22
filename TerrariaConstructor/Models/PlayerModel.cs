using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TerrariaConstructor.Models;

public class PlayerModel
{
    private const string EncryptKey = "h3y_gUyZ";

    #region SubModels

    public CharacteristicsModel Characteristic { get; set; }
    public EquipsModel Equips { get; set; }
    public ToolsModel Tools { get; set; }
    public InventoriesModel Inventories { get; set; }
    public BuffsModel Buffs { get; set; }

    #endregion
    
    #region Player statistics
    public int[] SpawnX { get; set; } = new int[200];
    public int[] SpawnY { get; set; } = new int[200];
    public int[] WorldId { get; set; } = new int[200];
    public string[] WorldNames { get; set; } = new string[200];
    
    #endregion
    
    public PlayerModel()
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
        
        Characteristic.Name = reader.ReadString();
        Characteristic.Difficulty = reader.ReadByte();
        Characteristic.PlayTime = new TimeSpan(reader.ReadInt64());
        Characteristic.Hair = reader.ReadInt32();
        Characteristic.HairDye = reader.ReadByte();
        Characteristic.HideBytes = reader.ReadBytes(3);
        Characteristic.SkinVariant = reader.ReadByte();
        Characteristic.Health = reader.ReadInt32();
        Characteristic.MaxHealth = reader.ReadInt32();
        Characteristic.Mana = reader.ReadInt32();
        Characteristic.MaxMana = reader.ReadInt32();
        Characteristic.ExtraAccessory = reader.ReadBoolean();
        Characteristic.UnlockedBiomeTorches = reader.ReadBoolean();
        Characteristic.UsingBiomeTorches = reader.ReadBoolean();
        Characteristic.AteArtisanBread = reader.ReadBoolean();
        Characteristic.UsedAegisCrystal = reader.ReadBoolean();
        Characteristic.UsedAegisFruit = reader.ReadBoolean();
        Characteristic.UsedArcaneCrystal = reader.ReadBoolean();
        Characteristic.UsedGalaxyPearl = reader.ReadBoolean();
        Characteristic.UsedGummyWorm = reader.ReadBoolean();
        Characteristic.UsedAmbrosia = reader.ReadBoolean();
        Characteristic.DownedDd2EventAnyDifficulty = reader.ReadBoolean();
        Characteristic.TaxMoney = reader.ReadInt32();
        Characteristic.NumberOfDeathsPve = reader.ReadInt32();
        Characteristic.NumberOfDeathsPvp = reader.ReadInt32();
        Characteristic.HairColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.SkinColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.EyeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.ShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.UnderShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.PantsColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        Characteristic.ShoeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        for (int i = 0; i < Equips.Armor.Length; i++)
        {
            Equips.Armor[0] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < Equips.Dye.Length; i++)
        {
            Equips.Dye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Inventories.Inventory.Length; i++)
        {
            Inventories.Inventory[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < Equips.Purse.Length; i++)
        {
            Equips.Purse[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < Equips.Ammo.Length; i++)
        {
            Equips.Ammo[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < Tools.MiscEquip.Length; i++)
        {
            Tools.MiscEquip[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
            };

            Tools.MiscDye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < Inventories.Bank1.Length; i++)
        {
            Inventories.Bank1[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Inventories.Bank2.Length; i++)
        {
            Inventories.Bank2[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Inventories.Bank3.Length; i++)
        {
            Inventories.Bank3[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < Inventories.Bank4.Length; i++)
        {
            Inventories.Bank4[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        var voidVaultInfo = reader.ReadByte();

        for (int i = 0; i < Buffs.Buffs.Length; i++)
        {
            Buffs.Buffs[i] = new Buff
            {
                Id = reader.ReadInt32(),
                DurationTime = new TimeSpan(reader.ReadInt32())
            };
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

        Characteristic.IsHotBarLocked = reader.ReadBoolean();

        for (int i = 0; i < Characteristic.HideInfo.Length; i++)
            Characteristic.HideInfo[i] = reader.ReadBoolean();
        
        Characteristic.AnglerQuestsFinished = reader.ReadInt32();

        for (int i = 0; i < Characteristic.DPadRadialBindings.Length; i++)
            Characteristic.DPadRadialBindings[i] = reader.ReadInt32();

        for (int i = 0; i < Characteristic.BuilderAccStatus.Length; i++)
            Characteristic.BuilderAccStatus[i] = reader.ReadInt32();

        Characteristic.BartenderQuestLog = reader.ReadInt32();

        Characteristic.IsDead = reader.ReadBoolean();

        if (Characteristic.IsDead)
            Characteristic.RespawnTimer = reader.ReadInt32();

        Characteristic.LastTimePlayerWasSaved = 0L;

        Characteristic.LastTimePlayerWasSaved = terrariaVersion < 202 ? DateTime.UtcNow.ToBinary() : reader.ReadInt64();

        Characteristic.GolferScoreAccumulated = reader.ReadInt32();
    }
    
    public void SavePlayer()
    {
        
    }
    
}
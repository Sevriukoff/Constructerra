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

    private readonly CharacteristicsModel _characteristic;
    private readonly EquipsModel _equips;
    private readonly ToolsModel _tools;
    private readonly InventoriesModel _inventories;
    private readonly BuffsModel _buffs;

    #endregion
    
    #region Player statistics
    public int[] SpawnX { get; set; } = new int[200];
    public int[] SpawnY { get; set; } = new int[200];
    public int[] WorldId { get; set; } = new int[200];
    public string[] WorldNames { get; set; } = new string[200];
    
    #endregion
    
    public PlayerModel(CharacteristicsModel characteristic,
        EquipsModel equips,
        ToolsModel tools,
        InventoriesModel inventories,
        BuffsModel buffs)
    {
        _characteristic = characteristic;
        _equips = equips;
        _tools = tools;
        _inventories = inventories;
        _buffs = buffs;
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
        
        _characteristic.Name = reader.ReadString();
        _characteristic.Difficulty = reader.ReadByte();
        _characteristic.PlayTime = new TimeSpan(reader.ReadInt64());
        _characteristic.Hair = reader.ReadInt32();
        _characteristic.HairDye = reader.ReadByte();
        _characteristic.HideBytes = reader.ReadBytes(3);
        _characteristic.SkinVariant = reader.ReadByte();
        _characteristic.Health = reader.ReadInt32();
        _characteristic.MaxHealth = reader.ReadInt32();
        _characteristic.Mana = reader.ReadInt32();
        _characteristic.MaxMana = reader.ReadInt32();
        _characteristic.ExtraAccessory = reader.ReadBoolean();
        _characteristic.UnlockedBiomeTorches = reader.ReadBoolean();
        _characteristic.UsingBiomeTorches = reader.ReadBoolean();
        _characteristic.AteArtisanBread = reader.ReadBoolean();
        _characteristic.UsedAegisCrystal = reader.ReadBoolean();
        _characteristic.UsedAegisFruit = reader.ReadBoolean();
        _characteristic.UsedArcaneCrystal = reader.ReadBoolean();
        _characteristic.UsedGalaxyPearl = reader.ReadBoolean();
        _characteristic.UsedGummyWorm = reader.ReadBoolean();
        _characteristic.UsedAmbrosia = reader.ReadBoolean();
        _characteristic.DownedDd2EventAnyDifficulty = reader.ReadBoolean();
        _characteristic.TaxMoney = reader.ReadInt32();
        _characteristic.NumberOfDeathsPve = reader.ReadInt32();
        _characteristic.NumberOfDeathsPvp = reader.ReadInt32();
        _characteristic.HairColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.SkinColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.EyeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.ShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.UnderShirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.PantsColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.ShoeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        for (int i = 0; i < _equips.Armor.Length; i++)
        {
            _equips.Armor[0] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < _equips.Dye.Length; i++)
        {
            _equips.Dye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < _inventories.Inventory.Length; i++)
        {
            _inventories.Inventory[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < _equips.Purse.Length; i++)
        {
            _equips.Purse[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < _equips.Ammo.Length; i++)
        {
            _equips.Ammo[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
                IsFavorite = reader.ReadBoolean()
            };
        }

        for (int i = 0; i < _tools.MiscEquip.Length; i++)
        {
            _tools.MiscEquip[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte(),
            };

            _tools.MiscDye[i] = new Item
            {
                Id = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        for (int i = 0; i < _inventories.Bank1.Length; i++)
        {
            _inventories.Bank1[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < _inventories.Bank2.Length; i++)
        {
            _inventories.Bank2[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < _inventories.Bank3.Length; i++)
        {
            _inventories.Bank3[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }
        
        for (int i = 0; i < _inventories.Bank4.Length; i++)
        {
            _inventories.Bank4[i] = new Item
            {
                Id = reader.ReadInt32(),
                Stack = reader.ReadInt32(),
                Prefix = reader.ReadByte()
            };
        }

        var voidVaultInfo = reader.ReadByte();

        for (int i = 0; i < _buffs.Buffs.Length; i++)
        {
            _buffs.Buffs[i] = new Buff
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

        _characteristic.IsHotBarLocked = reader.ReadBoolean();

        for (int i = 0; i < _characteristic.HideInfo.Length; i++)
            _characteristic.HideInfo[i] = reader.ReadBoolean();
        
        _characteristic.AnglerQuestsFinished = reader.ReadInt32();

        for (int i = 0; i < _characteristic.DPadRadialBindings.Length; i++)
            _characteristic.DPadRadialBindings[i] = reader.ReadInt32();

        for (int i = 0; i < _characteristic.BuilderAccStatus.Length; i++)
            _characteristic.BuilderAccStatus[i] = reader.ReadInt32();

        _characteristic.BartenderQuestLog = reader.ReadInt32();

        _characteristic.IsDead = reader.ReadBoolean();

        if (_characteristic.IsDead)
            _characteristic.RespawnTimer = reader.ReadInt32();

        _characteristic.LastTimePlayerWasSaved = 0L;

        _characteristic.LastTimePlayerWasSaved = terrariaVersion < 202 ? DateTime.UtcNow.ToBinary() : reader.ReadInt64();

        _characteristic.GolferScoreAccumulated = reader.ReadInt32();
    }
    
    public void SavePlayer()
    {
        
    }
    
}
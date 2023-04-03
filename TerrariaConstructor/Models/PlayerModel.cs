using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Autofac;
using TerrariaConstructor.Common.Structures;
using TerrariaConstructor.Infrastructure;

namespace TerrariaConstructor.Models;

public class PlayerModel
{
    private const string EncryptKey = "h3y_gUyZ";
    public event Action PlayerUpdated;

    #region SubModels

    private readonly CharacteristicsModel _characteristic;
    private readonly EquipsModel _equips;
    private readonly ToolsModel _tools;
    private readonly InventoriesModel _inventories;
    private readonly BuffsModel _buffs;
    private readonly ResearchModel _research;

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
        BuffsModel buffs,
        ResearchModel research)
    {
        _characteristic = characteristic;
        _equips = equips;
        _tools = tools;
        _inventories = inventories;
        _buffs = buffs;
        _research = research;
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

    private BinaryWriter EncryptPlayer(string path)
    {
        byte[] key = Encoding.Unicode.GetBytes(EncryptKey);
        var managed = new RijndaelManaged();
        var encryptor = managed.CreateEncryptor(key, key);
        Stream fileStream = new FileStream(path, FileMode.Create);
        CryptoStream output = new CryptoStream(fileStream, encryptor, CryptoStreamMode.Write);
        return new BinaryWriter(output);
    }
    
    private void EncryptDatToPlr(string inputFilePath, string outputFilePath)
    {
        byte[] key = Encoding.Unicode.GetBytes(EncryptKey);
        var managed = new RijndaelManaged();
        var encryptor = managed.CreateEncryptor(key, key);

        using (var inputFileStream = new FileStream(inputFilePath, FileMode.Open))
        using (var outputFileStream = new FileStream(outputFilePath, FileMode.Create))
        using (var cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
        {
            inputFileStream.CopyTo(cryptoStream);
        }
    }

    public void LoadPlayer(string plrPath)
    {
        using var scope = App.Container.BeginLifetimeScope();
        var unitOfWork = scope.Resolve<UnitOfWork>();

        var reader = DecryptPlayer(plrPath);

        int terrariaVersion = reader.ReadInt32();
        
        long num1 = (long) reader.ReadUInt64();
        if ((num1 & 72057594037927935L) != 27981915666277746L)
            throw new FormatException("Expected Re-Logic file format.");
        
        _characteristic.Revision = reader
            .ReadUInt32(); // Useless metadata stuff in player files, for now? Only used in maps
        _characteristic.IsFavorite =
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
        _characteristic.UndershirtColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.PantsColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());
        _characteristic.ShoeColor = Color.FromArgb(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

        for (int i = 0; i < _equips.Loadouts[0].Armor.Length; i++)
        {
            int id = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _equips.Loadouts[0].Armor[i] = GetItem(id, prefix);
        }

        for (int i = 0; i < _equips.Loadouts[0].Dye.Length; i++)
        {
            int id = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _equips.Loadouts[0].Dye[i] = GetItem(id, prefix);
        }

        for (int i = 0; i < _inventories.Inventory.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();
            bool isFavorite = reader.ReadBoolean();

            _inventories.Inventory[i] = GetItem(id, prefix, stack, isFavorite);
        }

        for (int i = 0; i < _equips.Purse.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();
            bool isFavorite = reader.ReadBoolean();

            _equips.Purse[i] = GetItem(id, prefix, stack, isFavorite);
        }

        for (int i = 0; i < _equips.Ammo.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();
            bool isFavorite = reader.ReadBoolean();

            _equips.Ammo[i] = GetItem(id, prefix, stack, isFavorite);
        }

        for (int i = 0; i < _tools.MiscEquip.Length; i++)
        {
            int id = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _tools.MiscEquip[i] = GetItem(id, prefix);

            id = reader.ReadInt32();
            prefix = reader.ReadByte();

            _tools.MiscDye[i] = GetItem(id, prefix);
        }

        for (int i = 0; i < _inventories.Bank1.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _inventories.Bank1[i] = GetItem(id, prefix, stack);
        }
        
        for (int i = 0; i < _inventories.Bank2.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _inventories.Bank2[i] = GetItem(id, prefix, stack);
        }
        
        for (int i = 0; i < _inventories.Bank3.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();

            _inventories.Bank3[i] = GetItem(id, prefix, stack);
        }

        for (int i = 0; i < _inventories.Bank4.Length; i++)
        {
            int id = reader.ReadInt32();
            int stack = reader.ReadInt32();
            byte prefix = reader.ReadByte();
            bool isFavorite = reader.ReadBoolean();

            _inventories.Bank4[i] = GetItem(id, prefix, stack, isFavorite);
        }

        _characteristic.VoidVaultInfo = reader.ReadByte();

        int buffIterator = 44;
        for (int i = 0; i < buffIterator; i++)
        {
            _buffs.Buffs[i] = new Buff
            {
                Id = reader.ReadInt32(),
                DurationTime = TimeSpan.FromSeconds(reader.ReadInt32())
            };

            if (_buffs.Buffs[i].Id != 0)
            {
                var buff = unitOfWork.BuffsRepository.GetById(_buffs.Buffs[i].Id);
                _buffs.Buffs[i].Image = buff.Image;
                //_buffs.Buffs[i].DurationTime = buff.DurationTime;
            }
            
            if (_buffs.Buffs[i].Id == 0)
            {
                --i;
                --buffIterator;
            }

        }

        for (int i = 0; i < SpawnX.Length; i++)
        {
            var spawnX = reader.ReadInt32();

            if (spawnX != -1)
            {
                SpawnX[i] = spawnX;
                SpawnY[i] = reader.ReadInt32();
                WorldId[i] = reader.ReadInt32();
                try
                {
                    WorldNames[i] = reader.ReadString();
                }
                catch (FormatException e) { }
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

        int numberOfIteration = reader.ReadInt32(); //CreativeTracker.Load();

        for (int i = 0; i < numberOfIteration; i++)
        {
            string key = reader.ReadString();
            int value = reader.ReadInt32();
            
            _research.Items.Add(new Item
            {
                Name = key,
                Stack = value
            });
        }

        BitsByte bitsByte = reader.ReadByte();

        int num3 = !bitsByte[3] ? 3 : 4;
        for (int i = 0; i < num3; i++)
        {
            if (bitsByte[i])
            {
                reader.ReadInt32();
                reader.ReadInt32();
                reader.ReadByte();
            }
        }

        for (int i = 0; i < 4; i++)
        {
            _characteristic.PowersById = new Dictionary<ushort, string>();
            
            ushort val = 1;
            
            if (!(reader.ReadBoolean() && (val = reader.ReadUInt16()) <= 14))
                break;
            
            switch (i)
            {
                case 2:
                    _characteristic.PowersById.Add(val, Convert.ToString(reader.ReadSingle(), CultureInfo.InvariantCulture));
                    break;
                case < 2:
                {
                    _characteristic.PowersById.Add(val, Convert.ToString(reader.ReadBoolean()));
                    break;
                }
            }
        }

        bitsByte = reader.ReadByte();

        _characteristic.UnlockedSuperCart = bitsByte[0];
        _characteristic.EnabledSuperCart = bitsByte[1];

        _equips.CurrentLoadoutIndex = reader.ReadInt32();
        _equips.Loadouts[_equips.CurrentLoadoutIndex] = _equips.Loadouts[0];

        for (int i = 0; i < _equips.Loadouts.Length; i++)
        {
            if (i == _equips.CurrentLoadoutIndex)
            {
                reader.ReadBytes((4+4+1) * 20 + (4+4+1) * 10 + 1 * 10);
                continue;
            }
            
            _equips.Loadouts[i] = new EquipsModel.Loadout();
            for (int j = 0; j < _equips.Loadouts[i].Armor.Length; j++)
            {
                var item = unitOfWork.ItemsRepository.GetById(reader.ReadInt32());

                _equips.Loadouts[i].Armor[j] = item;

                _equips.Loadouts[i].Armor[j].Stack = reader.ReadInt32();
                _equips.Loadouts[i].Armor[j].Prefix = reader.ReadByte();
            }
            
            for (int j = 0; j < _equips.Loadouts[i].Dye.Length; j++)
            {
                _equips.Loadouts[i].Dye[j] = new Item
                {
                    Id = reader.ReadInt32(),
                    Stack = reader.ReadInt32(),
                    Prefix = reader.ReadByte()
                };
            }
            
            for (int j = 0; j < _equips.Loadouts[i].IsHide.Length; j++)
            {
                _equips.Loadouts[i].IsHide[j] = reader.ReadBoolean();
            }
        }
        
        OnPlayerUpdated();
        
        Item GetItem(int id, byte prefix, int stack = -1, bool isFavorite = false)
        {
            var item = unitOfWork.ItemsRepository.GetById(id);
            
            item.Modifier = unitOfWork.ModifierRepository.GetById(prefix);
            item.Stack = stack;
            item.IsFavorite = isFavorite;

            return item;
        }
    }
    
    public void SavePlayer(string plrPath)
    {
        var writer = EncryptPlayer(plrPath);
        
        writer.Write(279);
        writer.Write(244154697780061554L); //27981915666277746L
        writer.Write(_characteristic.Revision);
        writer.Write((ulong)(!_characteristic.IsFavorite ? 0 : 1) & 1 | 0);
        
        writer.Write(_characteristic.Name);
        writer.Write(_characteristic.Difficulty);
        writer.Write(_characteristic.PlayTime.Ticks);
        writer.Write(_characteristic.Hair);
        writer.Write(_characteristic.HairDye);
        writer.Write(_characteristic.HideBytes);
        writer.Write(_characteristic.SkinVariant);
        writer.Write(_characteristic.Health);
        writer.Write(_characteristic.MaxHealth);
        writer.Write(_characteristic.Mana);
        writer.Write(_characteristic.MaxMana);
        writer.Write(_characteristic.ExtraAccessory);
        writer.Write(_characteristic.UnlockedBiomeTorches);
        writer.Write(_characteristic.UsingBiomeTorches);
        writer.Write(_characteristic.AteArtisanBread);
        writer.Write(_characteristic.UsedAegisCrystal);
        writer.Write(_characteristic.UsedAegisFruit);
        writer.Write(_characteristic.UsedArcaneCrystal);
        writer.Write(_characteristic.UsedGalaxyPearl);
        writer.Write(_characteristic.UsedGummyWorm);
        writer.Write(_characteristic.UsedAmbrosia);
        writer.Write(_characteristic.DownedDd2EventAnyDifficulty);
        writer.Write(_characteristic.TaxMoney);
        writer.Write(_characteristic.NumberOfDeathsPve);
        writer.Write(_characteristic.NumberOfDeathsPvp);
        writer.Write(_characteristic.HairColor.R);
        writer.Write(_characteristic.HairColor.G);
        writer.Write(_characteristic.HairColor.B);
        writer.Write(_characteristic.SkinColor.R);
        writer.Write(_characteristic.SkinColor.G);
        writer.Write(_characteristic.SkinColor.B);
        writer.Write(_characteristic.EyeColor.R);
        writer.Write(_characteristic.EyeColor.G);
        writer.Write(_characteristic.EyeColor.B);
        writer.Write(_characteristic.ShirtColor.R);
        writer.Write(_characteristic.ShirtColor.G);
        writer.Write(_characteristic.ShirtColor.B);
        writer.Write(_characteristic.UndershirtColor.R);
        writer.Write(_characteristic.UndershirtColor.G);
        writer.Write(_characteristic.UndershirtColor.B);
        writer.Write(_characteristic.PantsColor.R);
        writer.Write(_characteristic.PantsColor.G);
        writer.Write(_characteristic.PantsColor.B);
        writer.Write(_characteristic.ShoeColor.R);
        writer.Write(_characteristic.ShoeColor.G);
        writer.Write(_characteristic.ShoeColor.B);
        
        for (int i = 0; i < _equips.Loadouts[_equips.CurrentLoadoutIndex].Armor.Length; i++)
        {
            writer.Write(_equips.Loadouts[_equips.CurrentLoadoutIndex].Armor[i].Id);
            writer.Write(_equips.Loadouts[_equips.CurrentLoadoutIndex].Armor[i].Prefix);
        }
        
        for (int i = 0; i < _equips.Loadouts[_equips.CurrentLoadoutIndex].Dye.Length; i++)
        {
            writer.Write(_equips.Loadouts[_equips.CurrentLoadoutIndex].Dye[i].Id);
            writer.Write(_equips.Loadouts[_equips.CurrentLoadoutIndex].Dye[i].Prefix);
        }
        
        for (int i = 0; i < _inventories.Inventory.Length; i++)
        {
            writer.Write(_inventories.Inventory[i].Id);
            writer.Write(_inventories.Inventory[i].Stack);
            writer.Write(_inventories.Inventory[i].Prefix);
            writer.Write(_inventories.Inventory[i].IsFavorite);
        }
        
        for (int i = 0; i < _equips.Purse.Length; i++)
        {
            writer.Write(_equips.Purse[i].Id);
            writer.Write(_equips.Purse[i].Stack);
            writer.Write(_equips.Purse[i].Prefix);
            writer.Write(_equips.Purse[i].IsFavorite);
        }
        
        for (int i = 0; i < _equips.Ammo.Length; i++)
        {
            writer.Write(_equips.Ammo[i].Id);
            writer.Write(_equips.Ammo[i].Stack);
            writer.Write(_equips.Ammo[i].Prefix);
            writer.Write(_equips.Ammo[i].IsFavorite);
        }
        
        for (int i = 0; i < _tools.MiscEquip.Length; i++)
        {
            writer.Write(_tools.MiscEquip[i].Id);
            writer.Write(_tools.MiscEquip[i].Prefix);
            
            writer.Write(_tools.MiscDye[i].Id);
            writer.Write(_tools.MiscDye[i].Prefix);
        }
        
        for (int i = 0; i < _inventories.Bank1.Length; i++)
        {
            writer.Write(_inventories.Bank1[i].Id);
            writer.Write(_inventories.Bank1[i].Stack);
            writer.Write(_inventories.Bank1[i].Prefix);
        }
        
        for (int i = 0; i < _inventories.Bank2.Length; i++)
        {
            writer.Write(_inventories.Bank2[i].Id);
            writer.Write(_inventories.Bank2[i].Stack);
            writer.Write(_inventories.Bank2[i].Prefix);
        }
        
        for (int i = 0; i < _inventories.Bank3.Length; i++)
        {
            writer.Write(_inventories.Bank3[i].Id);
            writer.Write(_inventories.Bank3[i].Stack);
            writer.Write(_inventories.Bank3[i].Prefix);
        }
        
        for (int i = 0; i < _inventories.Bank4.Length; i++)
        {
            writer.Write(_inventories.Bank4[i].Id);
            writer.Write(_inventories.Bank4[i].Stack);
            writer.Write(_inventories.Bank4[i].Prefix);
            writer.Write(_inventories.Bank4[i].IsFavorite);
        }
        
        writer.Write(_characteristic.VoidVaultInfo);
        
        for (int i = 0; i < 44; i++)
        {
            if (_buffs.Buffs[i] == null)
            {
                writer.Write(0);
                writer.Write(0);
            }
            else
            {
                writer.Write(_buffs.Buffs[i].Id);
                writer.Write((int)0); //TODO: Fix buff time
            }
        }
        
        for (int i = 0; i < SpawnX.Length; i++)
        {
            if (WorldNames[i] == null)
            {
                writer.Write(-1);
                break;
            }

            writer.Write(SpawnX[i]);
            writer.Write(SpawnY[i]);
            writer.Write(WorldId[i]);
            writer.Write(WorldNames[i]);
        }
        
        writer.Write(_characteristic.IsHotBarLocked);
        
        for (int i = 0; i < _characteristic.HideInfo.Length; i++)
            writer.Write(_characteristic.HideInfo[i]);
        
        writer.Write(_characteristic.AnglerQuestsFinished);
        
        for (int i = 0; i < _characteristic.DPadRadialBindings.Length; i++)
            writer.Write(_characteristic.DPadRadialBindings[i]);
        
        for (int i = 0; i < _characteristic.BuilderAccStatus.Length; i++)
            writer.Write(_characteristic.BuilderAccStatus[i]);
        
        writer.Write(_characteristic.BartenderQuestLog);
        
        writer.Write(_characteristic.IsDead);

        if (_characteristic.IsDead)
            writer.Write(_characteristic.RespawnTimer);

        writer.Write(DateTime.UtcNow.ToBinary());
        
        writer.Write(_characteristic.GolferScoreAccumulated);
        
        writer.Write(_research.Items.Count);

        foreach (var researchItem in _research.Items)
        {
            writer.Write(researchItem.Name);
            writer.Write(researchItem.Stack);
        }
        
        // SaveTemporaryItemSlotContents
        BitsByte bitsByte = 0;
        bitsByte[0] = false;
        bitsByte[1] = false;
        bitsByte[2] = false;
        bitsByte[3] = false;
        writer.Write(bitsByte);

        foreach (var powerById in _characteristic.PowersById)
        {
            writer.Write(true);
            writer.Write(powerById.Key);

            try
            {
                writer.Write(bool.Parse(powerById.Value));
            }
            catch (Exception e)
            {
                writer.Write(0.5f);
            }
        }
        
        writer.Write(false);
        
        writer.Write(new BitsByte
        {
            [0] = _characteristic.UnlockedSuperCart,
            [1] = _characteristic.EnabledSuperCart
        });
        
        writer.Write(_equips.CurrentLoadoutIndex);
        
        for (int i = 0; i < _equips.Loadouts.Length; i++)
        {
            if (i == _equips.CurrentLoadoutIndex)
            {
                writer.Write(Enumerable.Repeat((byte)0, 280).ToArray());
                continue;
            }
            
            for (int j = 0; j < _equips.Loadouts[i].Armor.Length; j++)
            {
                writer.Write(_equips.Loadouts[i].Armor[j].Id);
                writer.Write(_equips.Loadouts[i].Armor[j].Stack);
                writer.Write(_equips.Loadouts[i].Armor[j].Prefix);
            }
            
            for (int j = 0; j < _equips.Loadouts[i].Dye.Length; j++)
            {
                writer.Write(_equips.Loadouts[i].Dye[j].Id);
                writer.Write(_equips.Loadouts[i].Dye[j].Stack);
                writer.Write(_equips.Loadouts[i].Dye[j].Prefix);
            }
            
            for (int j = 0; j < _equips.Loadouts[i].IsHide.Length; j++)
            {
                writer.Write(_equips.Loadouts[i].IsHide[j]);
            }
        }
        
        writer.Flush();
        writer.Close();
        writer.BaseStream.Close();
        writer.BaseStream.Dispose();
    }

    public void CreateNewPlayer()
    {
        _characteristic.Name = "ConstrucTerraPlayer";
        _characteristic.Revision = 100;
        _characteristic.IsFavorite = false;
        _characteristic.Difficulty = 1;
        _characteristic.PlayTime = TimeSpan.FromSeconds(0);
        _characteristic.Hair = 1;
        _characteristic.SkinVariant = 1;
        _characteristic.Health = 100;
        _characteristic.MaxHealth = 100;
        _characteristic.Mana = 20;
        _characteristic.MaxMana = 20;
        _characteristic.TaxMoney = 0;
        _characteristic.HairColor = Color.White;
        _characteristic.SkinColor = Color.White;
        _characteristic.EyeColor = Color.White;
        _characteristic.ShirtColor = Color.White;
        _characteristic.UndershirtColor = Color.White;
        _characteristic.PantsColor = Color.White;
        _characteristic.ShoeColor = Color.White;

        _characteristic.ExtraAccessory = false;
        _characteristic.UnlockedBiomeTorches = false;
        _characteristic.UsingBiomeTorches = false;
        _characteristic.AteArtisanBread = false;
        _characteristic.UsedAegisCrystal = false;
        _characteristic.UsedAegisFruit = false;
        _characteristic.UsedArcaneCrystal = false;
        _characteristic.UsedGalaxyPearl = false;
        _characteristic.UsedGummyWorm = false;
        _characteristic.UsedAmbrosia = false;
        _characteristic.DownedDd2EventAnyDifficulty = false;
        _characteristic.NumberOfDeathsPve = 0;
        _characteristic.NumberOfDeathsPvp = 0;
        _characteristic.VoidVaultInfo = 0;
        _characteristic.IsHotBarLocked = false;
        _characteristic.AnglerQuestsFinished = 0;
        _characteristic.BartenderQuestLog = 0;
        _characteristic.GolferScoreAccumulated = 0;
        _characteristic.LastTimePlayerWasSaved = 0;
        _characteristic.RespawnTimer = 0;
        _characteristic.IsDead = false;
        _characteristic.UnlockedSuperCart = false;
        _characteristic.EnabledSuperCart = false;

        InitializationArray(_equips.Purse);
        InitializationArray(_equips.Ammo);

        for (int i = 0; i < _equips.Loadouts.Length; i++)
        {
            var loadout = new EquipsModel.Loadout();
            InitializationArray(loadout.Armor);
            InitializationArray(loadout.Dye);

            _equips.Loadouts[i] = loadout;
        }

        _equips.CurrentLoadoutIndex = 0;
        
        InitializationArray(_tools.MiscEquip);
        InitializationArray(_tools.MiscDye);
        
        InitializationArray(_inventories.Inventory);
        InitializationArray(_inventories.Bank1);
        InitializationArray(_inventories.Bank2);
        InitializationArray(_inventories.Bank3);
        InitializationArray(_inventories.Bank4);

        _buffs.Buffs[0] = new Buff
        {
            Id = 0
        };
        
        OnPlayerUpdated();
    }
    
    [Obsolete]
    public void SavePlayerIntoDat(string plrPath)
    {
        using (Stream fileStream = new FileStream(plrPath, FileMode.Create))
        {
            using (BinaryWriter writer = new BinaryWriter(fileStream))
            {
                writer.Write(279);
                writer.Write(244154697780061554L);
                writer.Write(_characteristic.Revision);
                writer.Write((ulong) (!_characteristic.IsFavorite ? 0 : 1) & 1 | 0);

                writer.Write(_characteristic.Name);
                writer.Write(_characteristic.Difficulty);
                writer.Write(_characteristic.PlayTime.Ticks);
                writer.Write(_characteristic.Hair);
                writer.Write(_characteristic.HairDye);
                writer.Write(_characteristic.HideBytes);
                writer.Write(_characteristic.SkinVariant);
                writer.Write(_characteristic.Health);
                writer.Write(_characteristic.MaxHealth);
                writer.Write(_characteristic.Mana);
                writer.Write(_characteristic.MaxMana);
                writer.Write(_characteristic.ExtraAccessory);
                writer.Write(_characteristic.UnlockedBiomeTorches);
                writer.Write(_characteristic.UsingBiomeTorches);
                writer.Write(_characteristic.AteArtisanBread);
                writer.Write(_characteristic.UsedAegisCrystal);
                writer.Write(_characteristic.UsedAegisFruit);
                writer.Write(_characteristic.UsedArcaneCrystal);
                writer.Write(_characteristic.UsedGalaxyPearl);
                writer.Write(_characteristic.UsedGummyWorm);
                writer.Write(_characteristic.UsedAmbrosia);
                writer.Write(_characteristic.DownedDd2EventAnyDifficulty);
                writer.Write(_characteristic.TaxMoney);
                writer.Write(_characteristic.NumberOfDeathsPve);
                writer.Write(_characteristic.NumberOfDeathsPvp);
                writer.Write(_characteristic.HairColor.R);
                writer.Write(_characteristic.HairColor.G);
                writer.Write(_characteristic.HairColor.B);
                writer.Write(_characteristic.SkinColor.R);
                writer.Write(_characteristic.SkinColor.G);
                writer.Write(_characteristic.SkinColor.B);
                writer.Write(_characteristic.EyeColor.R);
                writer.Write(_characteristic.EyeColor.G);
                writer.Write(_characteristic.EyeColor.B);
                writer.Write(_characteristic.ShirtColor.R);
                writer.Write(_characteristic.ShirtColor.G);
                writer.Write(_characteristic.ShirtColor.B);
                writer.Write(_characteristic.UndershirtColor.R);
                writer.Write(_characteristic.UndershirtColor.G);
                writer.Write(_characteristic.UndershirtColor.B);
                writer.Write(_characteristic.PantsColor.R);
                writer.Write(_characteristic.PantsColor.G);
                writer.Write(_characteristic.PantsColor.B);
                writer.Write(_characteristic.ShoeColor.R);
                writer.Write(_characteristic.ShoeColor.G);
                writer.Write(_characteristic.ShoeColor.B);

                /*
                for (int i = 0; i < _equips.Armor.Length; i++)
                {
                    writer.Write(_equips.Armor[i].Id);
                    writer.Write(_equips.Armor[i].Prefix);
                }

                for (int i = 0; i < _equips.Dye.Length; i++)
                {
                    writer.Write(_equips.Dye[i].Id);
                    writer.Write(_equips.Dye[i].Prefix);
                }
                */
                for (int i = 0; i < _inventories.Inventory.Length; i++)
                {
                    writer.Write(_inventories.Inventory[i].Id);
                    writer.Write(_inventories.Inventory[i].Stack);
                    writer.Write(_inventories.Inventory[i].Prefix);
                    writer.Write(_inventories.Inventory[i].IsFavorite);
                }

                for (int i = 0; i < _equips.Purse.Length; i++)
                {
                    writer.Write(_equips.Purse[i].Id);
                    writer.Write(_equips.Purse[i].Stack);
                    writer.Write(_equips.Purse[i].Prefix);
                    writer.Write(_equips.Purse[i].IsFavorite);
                }

                for (int i = 0; i < _equips.Ammo.Length; i++)
                {
                    writer.Write(_equips.Ammo[i].Id);
                    writer.Write(_equips.Ammo[i].Stack);
                    writer.Write(_equips.Ammo[i].Prefix);
                    writer.Write(_equips.Ammo[i].IsFavorite);
                }

                for (int i = 0; i < _tools.MiscEquip.Length; i++)
                {
                    writer.Write(_tools.MiscEquip[i].Id);
                    writer.Write(_tools.MiscEquip[i].Prefix);

                    writer.Write(_tools.MiscDye[i].Id);
                    writer.Write(_tools.MiscDye[i].Prefix);
                }

                for (int i = 0; i < _inventories.Bank1.Length; i++)
                {
                    writer.Write(_inventories.Bank1[i].Id);
                    writer.Write(_inventories.Bank1[i].Stack);
                    writer.Write(_inventories.Bank1[i].Prefix);
                }

                for (int i = 0; i < _inventories.Bank2.Length; i++)
                {
                    writer.Write(_inventories.Bank2[i].Id);
                    writer.Write(_inventories.Bank2[i].Stack);
                    writer.Write(_inventories.Bank2[i].Prefix);
                }

                for (int i = 0; i < _inventories.Bank3.Length; i++)
                {
                    writer.Write(_inventories.Bank3[i].Id);
                    writer.Write(_inventories.Bank3[i].Stack);
                    writer.Write(_inventories.Bank3[i].Prefix);
                }

                for (int i = 0; i < _inventories.Bank4.Length; i++)
                {
                    writer.Write(_inventories.Bank4[i].Id);
                    writer.Write(_inventories.Bank4[i].Stack);
                    writer.Write(_inventories.Bank4[i].Prefix);
                    writer.Write(_inventories.Bank4[i].IsFavorite);
                }

                writer.Write(_characteristic.VoidVaultInfo);

                for (int i = 0; i < 44; i++)
                {
                    if (_buffs.Buffs[i] == null)
                    {
                        writer.Write(0);
                        writer.Write(0);
                    }
                    else
                    {
                        writer.Write(_buffs.Buffs[i].Id);
                        writer.Write((int) 0); //TODO: Fix buff time
                    }
                }

                for (int i = 0; i < SpawnX.Length; i++)
                {
                    if (WorldNames[i] == null)
                    {
                        writer.Write(-1);
                        break;
                    }

                    writer.Write(SpawnX[i]);
                    writer.Write(SpawnY[i]);
                    writer.Write(WorldId[i]);
                    writer.Write(WorldNames[i]);
                }

                writer.Write(_characteristic.IsHotBarLocked);

                for (int i = 0; i < _characteristic.HideInfo.Length; i++)
                    writer.Write(_characteristic.HideInfo[i]);

                writer.Write(_characteristic.AnglerQuestsFinished);

                for (int i = 0; i < _characteristic.DPadRadialBindings.Length; i++)
                    writer.Write(_characteristic.DPadRadialBindings[i]);

                for (int i = 0; i < _characteristic.BuilderAccStatus.Length; i++)
                    writer.Write(_characteristic.BuilderAccStatus[i]);

                writer.Write(_characteristic.BartenderQuestLog);

                writer.Write(_characteristic.IsDead);

                if (_characteristic.IsDead)
                    writer.Write(_characteristic.RespawnTimer);

                writer.Write(DateTime.UtcNow.ToBinary());

                writer.Write(_characteristic.GolferScoreAccumulated);

                writer.Write(_research.Items.Count);

                foreach (var researchItem in _research.Items)
                {
                    writer.Write(researchItem.Name);
                    writer.Write(researchItem.Stack);
                }

                // SaveTemporaryItemSlotContents
                BitsByte bitsByte = 0;
                bitsByte[0] = false;
                bitsByte[1] = false;
                bitsByte[2] = false;
                bitsByte[3] = false;
                writer.Write(bitsByte);

                foreach (var powerById in _characteristic.PowersById)
                {
                    writer.Write(true);
                    writer.Write(powerById.Key);

                    try
                    {
                        writer.Write(bool.Parse(powerById.Value));
                    }
                    catch (Exception e)
                    {
                        writer.Write(0.5f);
                    }
                }

                writer.Write(false);

                writer.Write(new BitsByte
                {
                    [0] = _characteristic.UnlockedSuperCart,
                    [1] = _characteristic.EnabledSuperCart
                });

                writer.Write(_equips.CurrentLoadoutIndex);

                for (int i = 0; i < _equips.Loadouts.Length; i++)
                {
                    for (int j = 0; j < _equips.Loadouts[i].Armor.Length; j++)
                    {
                        writer.Write(_equips.Loadouts[i].Armor[j].Id);
                        writer.Write(_equips.Loadouts[i].Armor[j].Stack);
                        writer.Write(_equips.Loadouts[i].Armor[j].Prefix);
                    }

                    for (int j = 0; j < _equips.Loadouts[i].Dye.Length; j++)
                    {
                        writer.Write(_equips.Loadouts[i].Dye[j].Id);
                        writer.Write(_equips.Loadouts[i].Dye[j].Stack);
                        writer.Write(_equips.Loadouts[i].Dye[j].Prefix);
                    }

                    for (int j = 0; j < _equips.Loadouts[i].IsHide.Length; j++)
                    {
                        writer.Write(_equips.Loadouts[i].IsHide[j]);
                    }
                }
                
                //writer.Flush();
            }
        }

        EncryptDatToPlr(plrPath, plrPath.Remove(plrPath.Length - 4));
    }

    protected virtual void OnPlayerUpdated()
    {
        PlayerUpdated?.Invoke();
    }

    private void InitializationArray(Item[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            items[i] = new Item
            {
                Id = 0,
                Stack = 0,
                Prefix = 0
            };
        }
    }
    
}
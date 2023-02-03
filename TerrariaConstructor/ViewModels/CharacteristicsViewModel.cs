using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ReactiveUI;
using Splat.ModeDetection;
using TerrariaConstructor.Models;
using Color = System.Drawing.Color;

namespace TerrariaConstructor.ViewModels;

public class CharacteristicsViewModel : ReactiveObject
{
    public CharacteristicsModel Model { get; set; }
    public string PlayerName { get; set; }
    
    private string _name;
    private int _health;
    private int _maxHealth;
    private int _manna;
    private int _maxManna;
    private byte _difficulty;
    private int _anglerQuestsFinished;
    private int _golferScoreAccumulated;
    private TimeSpan _playTime;
    private int _hairId;
    private byte _skinId;
    private Color _hairColor;
    private Color _skinColor;
    private Color _eyeColor;
    private Color _shirtColor;
    private Color _undershirtColor;
    private Color _pantsColor;
    private Color _shoeColor;
    private bool _ateArtisanBread;
    private bool _usedAegisCrystal;
    private bool _usedAegisFruit;
    private bool _usedArcaneCrystal;
    private bool _usedGalaxyPearl;
    private bool _usedGummyWorm;
    private bool _usedAmbrosia;

    #region Properties
    
    public string Name
    {
        get => _name;
        set
        { 
            this.RaiseAndSetIfChanged(ref _name, value);
            Model.Name = value;
        }
    }
    
    public byte Difficulty
    {
        get => _difficulty;
        set
        {
            this.RaiseAndSetIfChanged(ref _difficulty, value);
            Model.Difficulty = value;
        }
    }

    public int Health
    {
        get => _health;
        set
        {
            this.RaiseAndSetIfChanged(ref _health, value);
            Model.Health = value;
        }
    }
    
    public int MaxHealth
    {
        get => _maxHealth;
        set
        {
            this.RaiseAndSetIfChanged(ref _maxHealth, value);
            Model.MaxHealth = value;
        }
    }

    public int Manna
    {
        get => _manna;
        set
        {
            this.RaiseAndSetIfChanged(ref _manna, value);
            Model.Mana = value;
        }
    }
    
    public int MaxManna
    {
        get => _maxManna;
        set
        {
            this.RaiseAndSetIfChanged(ref _maxManna, value);
            Model.MaxMana = value;
        }
    }

    public int AnglerQuestsFinished
    {
        get => _anglerQuestsFinished;
        set
        {
            this.RaiseAndSetIfChanged(ref _anglerQuestsFinished, value);
            Model.AnglerQuestsFinished = value;
        }
    }

    public int GolferScoreAccumulated
    {
        get => _golferScoreAccumulated;
        set
        {
            this.RaiseAndSetIfChanged(ref _golferScoreAccumulated, value);
            Model.GolferScoreAccumulated = value;
        }
    }

    public TimeSpan PlayTime
    {
        get => _playTime;
        set
        {
            this.RaiseAndSetIfChanged(ref _playTime, value);
            Model.PlayTime = value;
        }
    }

    public int HairId
    {
        get => _hairId;
        set
        {
            this.RaiseAndSetIfChanged(ref _hairId, value);
            Model.Hair = value;
        }
    }

    public byte SkinId
    {
        get => _skinId;
        set
        {
            this.RaiseAndSetIfChanged(ref _skinId, value);
            Model.SkinVariant = value;
        }
    }

    public Color HairColor
    {
        get => _hairColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _hairColor, value);
            Model.HairColor = value;
        }
    }

    public Color SkinColor
    {
        get => _skinColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _skinColor, value);
            Model.SkinColor = value;
        }
    }

    public Color EyeColor
    {
        get => _eyeColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _eyeColor, value);
            Model.EyeColor = value;
        }
    }

    public Color ShirtColor
    {
        get => _shirtColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _shirtColor, value);
            Model.ShirtColor = value;
        }
    }

    public Color UndershirtColor
    {
        get => _undershirtColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _undershirtColor, value);
            Model.UnderShirtColor = value;
        }
    }

    public Color PantsColor
    {
        get => _pantsColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _pantsColor, value);
            Model.PantsColor = value;
        }
    }

    public Color ShoeColor
    {
        get => _shoeColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _shoeColor, value);
            Model.ShoeColor = value;
        }
    }

    public bool AteArtisanBread
    {
        get => _ateArtisanBread;
        set
        {
            this.RaiseAndSetIfChanged(ref _ateArtisanBread, value);
            Model.AteArtisanBread = value;
        }
    }

    public bool UsedAegisCrystal
    {
        get => _usedAegisCrystal;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedAegisCrystal, value);
            Model.UsedAegisCrystal = value;
        }
    }

    public bool UsedAegisFruit
    {
        get => _usedAegisFruit;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedAegisFruit, value);
            Model.UsedAegisFruit = value;
        }
    }

    public bool UsedArcaneCrystal
    {
        get => _usedArcaneCrystal;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedArcaneCrystal, value);
            Model.UsedArcaneCrystal = value;
        }
    }

    public bool UsedGalaxyPearl
    {
        get => _usedGalaxyPearl;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedGalaxyPearl, value);
            Model.UsedGalaxyPearl = value;
        }
    }

    public bool UsedGummyWorm
    {
        get => _usedGummyWorm;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedGummyWorm, value);
            Model.UsedGummyWorm = value;
        }
    }

    public bool UsedAmbrosia
    {
        get => _usedAmbrosia;
        set
        {
            this.RaiseAndSetIfChanged(ref _usedAmbrosia, value);
            Model.UsedAmbrosia = value;
        }
    }

    #endregion
    
    public CharacteristicsViewModel(CharacteristicsModel model)
    {
        Model = model;

        /*
        this.WhenAnyValue(x => x.Model.Name)
            .Select(Observable.Return)
            .Switch()
            .Subscribe(x => MessageBox.Show(x));
        
        this.WhenAnyValue(x => x.Model.Name)
            .Throttle(TimeSpan.FromSeconds(1))
            .Subscribe(x => Name = x);
        */
    }

    public void Update()
    {
        Name = Model.Name;
        Health = Model.Health;
        PlayTime = Model.PlayTime;
        HairColor = Model.HairColor;
    }
}
    
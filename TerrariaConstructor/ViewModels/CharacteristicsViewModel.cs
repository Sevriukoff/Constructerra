using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DynamicData.Binding;
using ReactiveUI;
using Splat.ModeDetection;
using TerrariaConstructor.Models;
using Wpf.Ui.Controls.Navigation;
using Color = System.Windows.Media.Color;

namespace TerrariaConstructor.ViewModels;

public class CharacteristicsViewModel : ReactiveObject, INavigationAware
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
    private int _numberOfDeathsPve;
    private int _numberOfDeathsPvp;

    #region Properties

    public List<Appearance> Hairs { get; set; }
    public List<Appearance> Skins { get; set; }

    public Appearance SelectedAppearance
    {
        get => _selectedAppearance;
        set => this.RaiseAndSetIfChanged(ref _selectedAppearance, value);
    }

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
        set => this.RaiseAndSetIfChanged(ref _hairId, value);
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

    public System.Windows.Media.Color HairColor
    {
        get => _hairColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _hairColor, value);
            Model.HairColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color SkinColor
    {
        get => _skinColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _skinColor, value);
            Model.SkinColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color EyeColor
    {
        get => _eyeColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _eyeColor, value);
            Model.EyeColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color ShirtColor
    {
        get => _shirtColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _shirtColor, value);
            Model.ShirtColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color UndershirtColor
    {
        get => _undershirtColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _undershirtColor, value);
            Model.UndershirtColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color PantsColor
    {
        get => _pantsColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _pantsColor, value);
            Model.PantsColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
        }
    }

    public Color ShoeColor
    {
        get => _shoeColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _shoeColor, value);
            Model.ShoeColor = System.Drawing.Color.FromArgb(value.A, value.R, value.G, value.B);
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

    public bool UsingBiomeTorches
    {
        get => _usingBiomeTorches;
        set
        {
            this.RaiseAndSetIfChanged(ref _usingBiomeTorches, value);
            Model.UsingBiomeTorches = value;
        }
    }

    public bool UnlockedBiomeTorches
    {
        get => _unlockedBiomeTorches;
        set
        {
            this.RaiseAndSetIfChanged(ref _unlockedBiomeTorches, value);
            Model.UnlockedBiomeTorches = value;
        }
    }

    public bool DownedDd2EventAnyDifficulty
    {
        get => _downedDd2EventAnyDifficulty;
        set
        {
            this.RaiseAndSetIfChanged(ref _downedDd2EventAnyDifficulty, value);
            Model.DownedDd2EventAnyDifficulty = value;
        }
    }

    public bool ExtraAccessory
    {
        get => _extraAccessory;
        set
        {
            this.RaiseAndSetIfChanged(ref _extraAccessory, value);
            Model.ExtraAccessory = value;
        }
    }

    public bool UnlockedSuperCart
    {
        get => _unlockedSuperCart;
        set
        {
            this.RaiseAndSetIfChanged(ref _unlockedSuperCart, value);
            Model.UnlockedSuperCart = value;
        }
    }

    public bool EnabledSuperCart
    {
        get => _enabledSuperCart;
        set
        {
            this.RaiseAndSetIfChanged(ref _enabledSuperCart, value);
            Model.EnabledSuperCart = value;
        }
    }

    public int NumberOfDeathsPve
    {
        get => _numberOfDeathsPve;
        set => this.RaiseAndSetIfChanged(ref _numberOfDeathsPve, value);
    }
    
    public int NumberOfDeathsPvp
    {
        get => _numberOfDeathsPvp;
        set => this.RaiseAndSetIfChanged(ref _numberOfDeathsPvp, value);
    }

    public List<string> Difficulties => new List<string>
    {
        "Классика", "Средняя сложность", "Сложный режим", "Путешествие"
    };

    #endregion

    #region Commands
    
    private Appearance _selectedAppearance;

    private readonly ReactiveCommand<Appearance, Unit> _selectHairCommand;
    public ReactiveCommand<Appearance, Unit> SelectHairCommand => _selectHairCommand;
    
    private readonly ReactiveCommand<Appearance, Unit> _selectSkinCommand;
    private bool _usingBiomeTorches;
    private bool _unlockedBiomeTorches;
    private bool _downedDd2EventAnyDifficulty;
    private bool _extraAccessory;
    private bool _unlockedSuperCart;
    private bool _enabledSuperCart;
    public ReactiveCommand<Appearance, Unit> SelectSkinCommand => _selectSkinCommand;
    
    #endregion
    public CharacteristicsViewModel(CharacteristicsModel model)
    {
        Model = model;
        Hairs = Model.GetHairs();
        Skins = Model.GetSkins();

        this.WhenAnyValue(x => x.HairId)
            .Where(x => x > 0 && x<= Hairs.Count)
            .Subscribe(id =>
            {
                foreach (var hair in Hairs)
                {
                    hair.IsSelected = hair.Id == HairId;
                }

                Model.Hair = id;
            });
        
        this.WhenAnyValue(x => x.SkinId)
            .Where(x => x > 0 && x<= Skins.Count)
            .Subscribe(id =>
            {
                foreach (var skin in Skins)
                {
                    skin.IsSelected = skin.Id == SkinId;
                }
            });

        _selectHairCommand = ReactiveCommand.Create<Appearance>(selectedHair =>
        {
            HairId = selectedHair.Id;
        });

        _selectSkinCommand = ReactiveCommand.Create<Appearance>(selectedSkin =>
        {
            SkinId = (byte) selectedSkin.Id;
        });

        //ConstructTerra

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
        Difficulty = Model.Difficulty;
        Health = Model.Health;
        MaxHealth = Model.MaxHealth;
        Manna = Model.Mana;
        MaxManna = Model.MaxMana;
        AnglerQuestsFinished = Model.AnglerQuestsFinished;
        GolferScoreAccumulated = Model.GolferScoreAccumulated;
        PlayTime = Model.PlayTime;
        HairId = Model.Hair;
        SkinId = Model.SkinVariant;

        HairColor = Color.FromArgb(Model.HairColor.A,Model.HairColor.R,Model.HairColor.G,Model.HairColor.B);
        SkinColor = Color.FromArgb(Model.SkinColor.A,Model.SkinColor.R,Model.SkinColor.G,Model.SkinColor.B);
        EyeColor = Color.FromArgb(Model.EyeColor.A,Model.EyeColor.R,Model.EyeColor.G,Model.EyeColor.B);
        ShirtColor = Color.FromArgb(Model.ShirtColor.A,Model.ShirtColor.R,Model.ShirtColor.G,Model.ShirtColor.B);
        UndershirtColor = Color.FromArgb(Model.UndershirtColor.A,Model.UndershirtColor.R,Model.UndershirtColor.G,Model.UndershirtColor.B);
        PantsColor = Color.FromArgb(Model.PantsColor.A,Model.PantsColor.R,Model.PantsColor.G,Model.PantsColor.B);
        ShoeColor = Color.FromArgb(Model.ShoeColor.A,Model.ShoeColor.R,Model.ShoeColor.G,Model.ShoeColor.B);

        AteArtisanBread = Model.AteArtisanBread;
        UsedAegisCrystal = Model.UsedAegisCrystal;
        UsedAegisFruit = Model.UsedAegisFruit;
        UsedArcaneCrystal = Model.UsedArcaneCrystal;
        UsedGalaxyPearl = Model.UsedGalaxyPearl;
        UsedGummyWorm = Model.UsedGummyWorm;
        UsedAmbrosia = Model.UsedAmbrosia;
        UsingBiomeTorches = Model.UsingBiomeTorches;
        UnlockedBiomeTorches = Model.UnlockedBiomeTorches;
        DownedDd2EventAnyDifficulty = Model.DownedDd2EventAnyDifficulty;
        ExtraAccessory = Model.ExtraAccessory;
        UnlockedSuperCart = Model.UnlockedSuperCart;
        EnabledSuperCart = Model.EnabledSuperCart;

        NumberOfDeathsPve = Model.NumberOfDeathsPve;
        NumberOfDeathsPvp = Model.NumberOfDeathsPvp;
    }

    public void OnNavigatedTo()
    {
        Update();
    }

    public void OnNavigatedFrom()
    {
        
    }
}
    
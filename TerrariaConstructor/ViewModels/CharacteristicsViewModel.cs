using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using DynamicData.Binding;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat.ModeDetection;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using Wpf.Ui.Controls.Navigation;
using Color = System.Windows.Media.Color;

namespace TerrariaConstructor.ViewModels;

public class CharacteristicsViewModel : ReactiveObject, INavigationAware
{
    private CharacteristicsModel Model { get; set; }

    private TimeSpan _playTime;
    private Color _hairColor;
    private Color _skinColor;
    private Color _eyeColor;
    private Color _shirtColor;
    private Color _undershirtColor;
    private Color _pantsColor;
    private Color _shoeColor;
    private Color _flyoutColor;
    private Appearance _selectedAppearance;

    #region Properties

    public List<Appearance> Hairs { get; set; }
    public List<Appearance> Skins { get; set; }

    public Appearance SelectedAppearance
    {
        get => _selectedAppearance;
        set => this.RaiseAndSetIfChanged(ref _selectedAppearance, value);
    }

    [Reactive]
    public string Name { get; set; }
    [Reactive]
    public byte Difficulty { get; set; }
    [Reactive]
    public int Health { get; set; }
    [Reactive]
    public int MaxHealth { get; set; }
    [Reactive]
    public int Mana { get; set; }
    [Reactive]
    public int MaxMana { get; set; }
    [Reactive]
    public int AnglerQuestsFinished { get; set; }
    [Reactive]
    public int GolferScoreAccumulated { get; set; }

    public TimeSpan PlayTime
    {
        get => _playTime;
        set
        {
            this.RaiseAndSetIfChanged(ref _playTime, value);
            Model.PlayTime = value;
        }
    }

    [Reactive]
    public int HairId { get; set; }
    [Reactive]
    public byte SkinId { get; set; }

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
    [Reactive]
    public bool AteArtisanBread { get; set; }
    [Reactive]
    public bool UsedAegisCrystal { get; set; }
    [Reactive]
    public bool UsedAegisFruit { get; set; }
    [Reactive]
    public bool UsedArcaneCrystal { get; set; }
    [Reactive]
    public bool UsedGalaxyPearl { get; set; }
    [Reactive]
    public bool UsedGummyWorm { get; set; }
    [Reactive]
    public bool UsedAmbrosia { get; set; }
    [Reactive]
    public bool UsingBiomeTorches { get; set; }
    [Reactive]
    public bool UnlockedBiomeTorches { get; set; }
    [Reactive]
    public bool DownedDd2EventAnyDifficulty { get; set; }
    [Reactive]
    public bool ExtraAccessory { get; set; }
    [Reactive]
    public bool UnlockedSuperCart { get; set; }
    [Reactive]
    public bool EnabledSuperCart { get; set; }
    [Reactive]
    public int NumberOfDeathsPve { get; set; }
    [Reactive]
    public int NumberOfDeathsPvp { get; set; }

    public List<string> Difficulties => new List<string>
    {
        "Классика", "Средняя сложность", "Сложный режим", "Путешествие"
    };

    public string CurrentColorSelected { get; set; }

    public Color FlyoutColor
    {
        get => _flyoutColor;
        set
        {
            this.RaiseAndSetIfChanged(ref _flyoutColor, value);

            switch (CurrentColorSelected)
            {
                case "HairColor":
                    HairColor = value;
                    break;
                
                case "SkinColor":
                    SkinColor = value;
                    break;
                
                case "EyeColor":
                    EyeColor = value;
                    break;
                
                case "ShirtColor":
                    ShirtColor = value;
                    break;
                
                case "PantsColor":
                    PantsColor = value;
                    break;
                
                case "ShoeColor":
                    ShoeColor = value;
                    break;

                case "UndershirtColor":
                    UndershirtColor = value;
                    break;
            }
        }
    }

    [Reactive]
    public bool UndershirtColorFlyout { get; set; }

    #endregion

    #region Commands

    private readonly ReactiveCommand<Appearance, Unit> _selectHairCommand;
    public ReactiveCommand<Appearance, Unit> SelectHairCommand => _selectHairCommand;

    private readonly ReactiveCommand<Appearance, Unit> _selectSkinCommand;
    public ReactiveCommand<Appearance, Unit> SelectSkinCommand => _selectSkinCommand;
    
    public ReactiveCommand<string, Unit> ToggleFlyoutCommand { get; }
    
    #endregion
    public CharacteristicsViewModel(CharacteristicsModel model)
    {
        Model = model;
        Hairs = Model.GetHairs();
        Skins = Model.GetSkins();
        
        Update();
     
        var properties = new (Expression<Func<CharacteristicsViewModel, object>> property, Action<object> setter)[] {
            (x => x.Name, x => Model.Name = (string) x),
            (x => x.Difficulty, x => Model.Difficulty = (byte) x),
            (x => x.Health, x => Model.Health = (int) x),
            (x => x.MaxHealth, x => Model.MaxHealth = (int) x),
            (x => x.Mana, x => Model.Mana = (int) x),
            (x => x.MaxMana, x => Model.MaxMana = (int) x),
            (x => x.AnglerQuestsFinished, x => Model.AnglerQuestsFinished = (int) x),
            (x => x.GolferScoreAccumulated, x => Model.GolferScoreAccumulated = (int) x),
            (x => x.AteArtisanBread, x => Model.AteArtisanBread = (bool) x),
            (x => x.UsedAegisCrystal, x => Model.UsedAegisCrystal = (bool) x),
            (x => x.UsedAegisFruit, x => Model.UsedAegisFruit = (bool) x),
            (x => x.UsedArcaneCrystal, x => Model.UsedArcaneCrystal = (bool) x),
            (x => x.UsedGalaxyPearl, x => Model.UsedGalaxyPearl = (bool) x),
            (x => x.UsedGummyWorm, x => Model.UsedGummyWorm = (bool) x),
            (x => x.UsedAmbrosia, x => Model.UsedAmbrosia = (bool) x),
            (x => x.UsingBiomeTorches, x => Model.UsingBiomeTorches = (bool) x),
            (x => x.UnlockedBiomeTorches, x => Model.UnlockedBiomeTorches = (bool) x),
            (x => x.DownedDd2EventAnyDifficulty, x => Model.DownedDd2EventAnyDifficulty = (bool) x),
            (x => x.ExtraAccessory, x => Model.ExtraAccessory = (bool) x),
            (x => x.UnlockedSuperCart, x => Model.UnlockedSuperCart = (bool) x),
            (x => x.EnabledSuperCart, x => Model.EnabledSuperCart = (bool) x),
            (x => x.NumberOfDeathsPve, x => Model.NumberOfDeathsPve = (int) x),
            (x => x.NumberOfDeathsPvp, x => Model.NumberOfDeathsPvp = (int) x)
        };

        foreach (var (property, setter) in properties)
        {
            this.WhenAnyValue(property)
                .Subscribe(x => setter(x));
        }

        T GetValueOrNull<T>(object value) where T : struct
        {
            if (value == null) throw new ArgumentException();
            if (!(value is T)) throw new ArgumentException();
            return (T)value;
        }

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

                Model.SkinVariant = id;
            });

        _selectHairCommand = ReactiveCommand.Create<Appearance>(selectedHair =>
        {
            HairId = selectedHair.Id;
        });

        _selectSkinCommand = ReactiveCommand.Create<Appearance>(selectedSkin =>
        {
            SkinId = (byte) selectedSkin.Id;
        });

        IDisposable flyoutDisposable;

        ToggleFlyoutCommand = ReactiveCommand.Create<string>(parameter =>
        {
            CurrentColorSelected = parameter;
            UndershirtColorFlyout = !UndershirtColorFlyout;
        });

        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());

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
        Mana = Model.Mana;
        MaxMana = Model.MaxMana;
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
    
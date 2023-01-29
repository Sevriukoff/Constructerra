using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class CharacteristicViewModel : ReactiveObject
{
    public CharacteristicsModel Model { get; set; }
    public string PlayerName { get; set; }
    
    private string _name;
    public string Name
    {
        get => _name;
        set
        { 
            this.RaiseAndSetIfChanged(ref _name, value);
            Model.Name = value;
        }
    }

    public CharacteristicViewModel(CharacteristicsModel model)
    {
        Model = model;
        _name = model.Name;

        this.WhenAnyValue(x => x.Model.Name)
            .Select(Observable.Return)
            .Switch()
            .Subscribe(x => MessageBox.Show(x));
        
        this.WhenAnyValue(x => x.Model.Name)
            .Subscribe(x => Name = x);
    }
}
    
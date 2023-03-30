using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class EquipsViewModel : ReactiveObject
{
    public EquipsModel Model { get; set; }
    public ObservableCollection<Item> SelectedLoadout { get; set; }

    #region Commands

    public ReactiveCommand<string, Unit> ChangeLoadout { get; }

    #endregion
    
    public EquipsViewModel(EquipsModel model)
    {
        Model = model;

        SelectedLoadout = new ObservableCollection<Item>(Model.Loadouts[0].Armor);

        ChangeLoadout = ReactiveCommand.Create<string>(Change);
    }

    private void Change(string parameter)
    {
        if (int.TryParse(parameter as string, out int index))
        {
            SelectedLoadout.Clear();
            SelectedLoadout.AddRange(Model.Loadouts[index].Armor);
        }
        
    }

    public void Update()
    {
        
    }
}
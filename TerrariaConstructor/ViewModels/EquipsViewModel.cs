using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class EquipsViewModel : ReactiveObject
{
    public EquipsModel Model { get; set; }
    public ObservableCollection<Item> SelectedLoadout { get; set; }
    public ObservableCollection<Item> Tools { get; set; }
    
    [Reactive]
    public int SelectedIndex { get; set; }

    #region Commands

    public ReactiveCommand<string, Unit> ChangeLoadout { get; }

    #endregion
    
    public EquipsViewModel(EquipsModel model, ToolsModel toolsModel)
    {
        Model = model;

        SelectedIndex = 0;
        SelectedLoadout = new ObservableCollection<Item>(Model.Loadouts[0].Armor.Union(Model.Loadouts[0].Dye));
        Tools = new ObservableCollection<Item>(toolsModel.MiscEquip.Union(toolsModel.MiscDye));

        ChangeLoadout = ReactiveCommand.Create<string>(Change);
    }

    private void Change(string parameter)
    {
        if (int.TryParse(parameter as string, out int index))
        {
            SelectedIndex = index;
            SelectedLoadout.Clear();
            SelectedLoadout.AddRange(Model.Loadouts[index].Armor.Union(Model.Loadouts[index].Dye));
        }
        
    }

    public void Update()
    {
        
    }
}
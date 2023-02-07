using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels;

public class MainInventoryViewModel : BaseInventoryViewModel
{
    public MainInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Inventory);
    }
}
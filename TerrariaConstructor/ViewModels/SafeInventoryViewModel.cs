using System.Collections.ObjectModel;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels;

public class SafeInventoryViewModel : BaseInventoryViewModel
{
    public SafeInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Bank2);
    }
}
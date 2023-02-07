using System.Collections.ObjectModel;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels;

public class ForgeInventoryViewModel : BaseInventoryViewModel
{
    public ForgeInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Bank3);
    }
}
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.ViewModels;

public class PiggyInventoryViewModel : BaseInventoryViewModel, INavigationAware
{
    public PiggyInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Bank1);
    }

    public void OnNavigatedTo()
    {
        
    }

    public void OnNavigatedFrom()
    {
        
    }
}
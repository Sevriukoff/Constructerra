using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels;

public class MainInventoryViewModel : ReactiveObject
{
    private readonly InventoriesModel _model;
    private ObservableCollection<Item> _items;

    public ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }

    public MainInventoryViewModel(InventoriesModel model)
    {
        _model = model;

        Items = new ObservableCollection<Item>(model.Inventory);
    }
}
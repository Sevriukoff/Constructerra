using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels.Base;

public class BaseInventoryViewModel : ReactiveObject
{
    #region Fields

    private readonly InventoriesModel _model;

    #endregion

    #region Properties

    private ObservableCollection<Item> _items;

    public ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }

    #endregion

    #region ctor's

    public BaseInventoryViewModel(InventoriesModel model)
    {
        _model = model;
    }

    #endregion
}
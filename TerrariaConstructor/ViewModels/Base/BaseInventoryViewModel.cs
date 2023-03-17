using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Models;

namespace TerrariaConstructor.ViewModels.Base;

public abstract class BaseInventoryViewModel : ReactiveObject
{
    #region Fields

    protected readonly InventoriesModel _model;

    #endregion

    #region Properties

    protected ObservableCollection<Item> _items;

    public ObservableCollection<Item> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }

    #endregion

    #region ctor's

    protected BaseInventoryViewModel(InventoriesModel model)
    {
        _model = model;
    }

    #endregion

    #region Methods

    protected abstract void Update();

    #endregion
}
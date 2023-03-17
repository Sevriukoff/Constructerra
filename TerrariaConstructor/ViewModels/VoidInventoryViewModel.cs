using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels;

public class VoidInventoryViewModel : BaseInventoryViewModel
{
    public VoidInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Bank4);

        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());
    }

    protected override void Update()
    {
        Items = new ObservableCollection<Item>(_model.Bank4);
    }
}
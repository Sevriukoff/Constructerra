using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;
using Wpf.Ui.Controls.Navigation;

namespace TerrariaConstructor.ViewModels;

public class PiggyInventoryViewModel : BaseInventoryViewModel
{
    public PiggyInventoryViewModel(InventoriesModel model) : base(model)
    {
        Items = new ObservableCollection<Item>(model.Bank1);
        
        MessageBus.Current.Listen<PlayerUpdatedEvent>()
            .Subscribe(x => Update());
    }
    
    protected override void Update()
    {
        Items = new ObservableCollection<Item>(_model.Bank1);
    }
}
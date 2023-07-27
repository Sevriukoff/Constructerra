using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels.Inventories;

public class MainInventoryViewModel : BaseInventoryViewModel
{
    public MainInventoryViewModel(InventoriesModel model) : base(model, model.Inventory)
    {
        
    }
}
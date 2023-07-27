using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels.Inventories;

public class PiggyInventoryViewModel : BaseInventoryViewModel
{
    public PiggyInventoryViewModel(InventoriesModel model) : base(model,  model.Bank1)
    {
        
    }
}
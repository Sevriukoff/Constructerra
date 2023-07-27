using System;
using System.Collections.ObjectModel;
using ReactiveUI;
using TerrariaConstructor.Common.Events;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels.Inventories;

public class ForgeInventoryViewModel : BaseInventoryViewModel
{
    public ForgeInventoryViewModel(InventoriesModel model) : base(model, model.Bank3)
    {
        
    }
}
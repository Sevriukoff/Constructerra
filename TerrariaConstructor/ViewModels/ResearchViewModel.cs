using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Threading;
using ReactiveUI;
using TerrariaConstructor.Models;
using TerrariaConstructor.ViewModels.Base;

namespace TerrariaConstructor.ViewModels;

public class ResearchViewModel : BaseInventoryViewModel
{
    private readonly ResearchModel _researchModel;

    public ResearchViewModel(InventoriesModel inventoriesModel, ResearchModel researchModel)
        : base(inventoriesModel, researchModel.Items)
    {
        _researchModel = researchModel;

        var countItem = Items.Count(x => x.Id > 0);

        if (countItem == Items.Count)
        {
            Items.Add(new Item());
        }

        Items.CollectionChanged += (sender, args) =>
        {
            if (args.Action == NotifyCollectionChangedAction.Replace)
            {
                if (Items.Count(x => x.Id > 0) == Items.Count)
                {
                    Dispatcher.CurrentDispatcher.InvokeAsync(() =>
                    {
                        Items.Add(new Item());
                    }, DispatcherPriority.Background);
                }
            }
        };
    }
}